using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainCommon;
using Main.Models.Account;
using MainEntity.Models.Shopping;
using MainEntity.Interfaces;
using System.Web.Security;
using System.Web.UI;
using Main.Common;
using System.Text;
using MainCommon.Skipjack;

namespace Main.Controllers
{
    public partial class AccountController
    {
        private long Purchase(EnterCCInfoModel model, System.Web.Security.MembershipUser user, bool createUser, MainEntity.Models.Shopping.ShoppingTransactionInfo transactionInfo, MainEntity.Models.User.MembershipCart card,
            KeyValuePair<long, long?> addresses)
        {
            var userID = (Guid)user.ProviderUserKey;
            var userName = user.UserName;
            var sjLog = new StringBuilder();
            long transactionID = 0;

            try
            {
                if (model.CreditCardValidated && card != null)
                {
                    ///Purchase from authorized card
                    string chargeType = transactionInfo.SubscribePlanFree ? ShoppingTransactionTypeEnum.monthlyfee_purchase.ToString() : ShoppingTransactionTypeEnum.purchase.ToString();
                    string tranID = card.tranID;
                    string cardID = card.membershipCartID;
                    var authShoppingTransactionID = card.shoppingTransactionID;

                    transactionID = _shoppingService.ShoppingPay((Guid)user.ProviderUserKey, transactionInfo, chargeType, cardID, addresses,
                        (transaction_id) =>
                        {
                            transactionID = transaction_id;
                            return SkipjackExecutor.Pay(tranID, transaction_id, authShoppingTransactionID, transactionInfo.Amount, Properties.Settings.Default.SJSerialNumber, Properties.Settings.Default.SJDeveloperSerialNumber, Properties.Settings.Default.SJChangeStatusUrl, Properties.Settings.Default.SJGetStatusUrl, sjLog);
                        });
                }
                else
                {
                    ///Purchase from new card

                    string chargeType;
                    if (transactionInfo.SubscribePlanFree)
                        chargeType = (transactionInfo.Amount == 0 ? ShoppingTransactionTypeEnum.authorize_monthlyfee : ShoppingTransactionTypeEnum.authorize_monthlyfee_purchase).ToString();
                    else
                        chargeType = (transactionInfo.Amount == 0 ? ShoppingTransactionTypeEnum.authorize : ShoppingTransactionTypeEnum.authorize_purchase).ToString();

                    var cardID = model.CreditCardNumber.Substring(model.CreditCardNumber.Length - 4);
                    var expirationDate = new DateTime(model.ExpirationDateYear, model.ExpirationDateMonth, 1);
                    string creditCardType = model.CreditCard;
                    var oldCardID = card != null ? card.membershipCartID : null;

                    _userService.InsertOrUpdateMembershipCard(userID, expirationDate, creditCardType, cardID, CartStateEnum.Deleted);

                    transactionID = _shoppingService.ShoppingPayAuthorize(transactionInfo, addresses, chargeType, cardID, userID,
                        (transaction_id) =>
                            AuthorizeCreditCard(model, transaction_id, createUser, userID, userName, cardID, oldCardID, sjLog),
                        (transaction_id, tran_id) =>
                        {
                            transactionID = transaction_id;
                            return SkipjackExecutor.Pay(tran_id, transaction_id, transaction_id, transactionInfo.Amount, Properties.Settings.Default.SJSerialNumber, Properties.Settings.Default.SJDeveloperSerialNumber, Properties.Settings.Default.SJChangeStatusUrl, Properties.Settings.Default.SJGetStatusUrl, sjLog);
                        });

                }

                Mailer.AddMessage(Mailer.OnUserBuyForMany(Resources.MailMessage.FromMailAddress, model.Email, model.FirstName1 + " " + model.LastName1, transactionID,
                    string.Format("{0} {1}\r\n{2}\r\n{3} {4}, {5}\r\n{6}\r\n{7}",
                        addresses.Value.HasValue ? model.FirstName2 : model.FirstName1,
                        addresses.Value.HasValue ? model.LastName2 : model.LastName1,
                        addresses.Value.HasValue ? model.PostalAddress2 : model.PostalAddress1,
                        addresses.Value.HasValue ? model.City2 : model.City1,
                        addresses.Value.HasValue ? model.State2 : model.State1,
                        addresses.Value.HasValue ? model.PostalCode2 : model.PostalCode1,
                        addresses.Value.HasValue ? model.Country2 : model.Country1,
                        addresses.Value.HasValue ? model.Phone2 : model.Phone1), transactionInfo), Properties.Settings.Default.AdminEmailSubscribeNotification);

            }
            catch (SJTimeoutException)
            {
                Mailer.AddMessage(Mailer.NoResponseFromTheSkipjack(
                    Properties.Settings.Default.FromEmailNotification, Properties.Settings.Default.AdminEmailSubscribeNotification, user.ProviderUserKey.ToString(), transactionID.ToString(), transactionInfo.Amount.ToString(), sjLog.ToString()));
                throw;
            }
            catch (SJDeclineException)
            {
                Mailer.AddMessage(Mailer.ChargeOnSkipjackIsDeclined(
                    Properties.Settings.Default.FromEmailNotification, Properties.Settings.Default.AdminEmailSubscribeNotification, sjLog.ToString()));
                throw;
            }
            catch (SJUnhandledException)
            {
                Mailer.AddMessage(Mailer.SkipjackFailsBecauseNoResponseInGivenTime(
                    Properties.Settings.Default.FromEmailNotification, Properties.Settings.Default.AdminEmailSubscribeNotification, sjLog.ToString()));
                throw;
            }

            Mailer.AddMessage(Mailer.ChargeOnSkipjackIsSuccess(Properties.Settings.Default.FromEmailNotification, Properties.Settings.Default.AdminEmailSubscribeNotification, transactionInfo.Amount.ToString(), sjLog.ToString())); 

            return transactionID;
        }

        private string AuthorizeCreditCard(EnterCCInfoModel model, long shopping_transaction_id, bool create_user, Guid user_id, string user_name, string card_id, string old_card_id, StringBuilder sj_log)
        {
            string tranID = AuthorizeCreditCard(shopping_transaction_id, model.FirstName1, model.LastName1, model.Email, model.PostalAddress1,
                model.City1, model.State1, model.PostalCode1, model.Country1, model.Phone1, Convert.ToInt64(model.CreditCardNumber),
                new DateTime(model.ExpirationDateYear, model.ExpirationDateMonth, 1), user_id, card_id, old_card_id, sj_log, this.UserService);

            if (create_user) _formsService.SignIn(user_name, this.HttpContext.GetCurrentMembershipProvider().ApplicationName, this.Response.Cookies);

            return tranID;
        }

        public static string AuthorizeCreditCard(long shopping_transaction_id,
            string first_name, string last_name, string email, string postal_address,string city,string state,string postal_code, string country, string phone, long credit_card_number, DateTime expiration,
            Guid user_id, string card_id, string old_card_id, StringBuilder sj_log, IUserService user_service)
        {
            try
            {
                string tranID = null;
                var amount = country == "US" ? (decimal)0.01 : (decimal)1;

                Pair resp = SkipjackExecutor.SJAuthorize(shopping_transaction_id,
                    first_name, last_name, email, postal_address,
                    city, state, postal_code, country, phone, credit_card_number, expiration, amount,
                    Properties.Settings.Default.SJSerialNumber, Properties.Settings.Default.SJDeveloperSerialNumber, Properties.Settings.Default.SJAuthorizeServiceUrl, sj_log);
                tranID = (string)resp.First;

                user_service.UpdateUserAfterCardAuthorization(user_id, card_id, tranID, shopping_transaction_id);
                if (!string.IsNullOrEmpty(old_card_id) && old_card_id != card_id)
                    user_service.UpdateMembershipCardState(user_id, old_card_id, CartStateEnum.Deleted);

                return tranID;
            }
            catch
            {
                user_service.UpdateMembershipCardState(user_id, card_id, CartStateEnum.Deleted);
                throw;
            }

        }


    }
}