using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainCommon.Daemon;
using MainCommon;
using System.Collections.Specialized;
using System.Net.Mail;
using MainEntity.Models.Shopping;
using System.Text;

namespace Main.Common
{
    public static class Mailer
    {

        public static void AddMessage(MailMessage message, string to_bcc=null)
        {
            if (!string.IsNullOrEmpty(to_bcc))
                message.Bcc.Add(to_bcc);
            SubscribeDaemon.AddMessage(message);
        }

        #region SkipJack

        public static MailMessage SkipjackFailsBecauseNoResponseInGivenTime(string from, string to, string skipjack_log)
        {
            var body = Resources.MailMessage.SkipjackFailsBecauseNoResponseInGivenTime
                .Replace("{skipjack_log}", skipjack_log);

            var subject = Resources.MailMessage.SkipjackFailsBecauseNoResponseInGivenTime_subject;
            return new MailMessage(from, to, subject, body);
        }

        public static MailMessage ChargeOnSkipjackIsDeclined(string from, string to, string skipjack_log)
        {
            var body = Resources.MailMessage.ChargeOnSkipjackIsDeclined
                .Replace("{skipjack_log}", skipjack_log);

            var subject = Resources.MailMessage.ChargeOnSkipjackIsDeclined_subject;
            return new MailMessage(from, to, subject, body);

        }

        public static MailMessage ChargeOnSkipjackIsSuccess(string from, string to,string amount, string skipjack_log)
        {
            var body = Resources.MailMessage.ChargeOnSkipjackIsSuccess
                .Replace("{skipjack_log}", skipjack_log);

            var subject = Resources.MailMessage.ChargeOnSkipjackIsSuccess_subject.Replace("{amount}", amount);
            return new MailMessage(from, to, subject, body);
        }

        public static MailMessage NoResponseFromTheSkipjack(string from, string to, string user_id, string transaction_id, string amount, string skipjack_log)
        {
            var body = Resources.MailMessage.NoResponseFromTheSkipjack
                .Replace("{user_id}", user_id).Replace("{transaction_id}", transaction_id).Replace("{amount}", amount).Replace("{skipjack_log}", skipjack_log);

            var subject = Resources.MailMessage.NoResponseFromTheSkipjack_subject;
            return new MailMessage(from, to, subject, body);
        }

        #endregion


        public static MailMessage RegularSalesIsMade(string from, string to, string transaction_log)
        {
            var body = Resources.MailMessage.RegularSalesIsMade
                .Replace("{transaction_log}", transaction_log);

            var subject = Resources.MailMessage.RegularSalesIsMade_subject;
            return new MailMessage(from, to, subject, body);

        }

        public static MailMessage PrepaidSaleGoesThroughForNewUser(string from, string to, string full_name, string user_id, string shipping_address, string shopping_products, string total)
        {
            var body = Resources.MailMessage.PrepaidSaleGoesThroughForNewUser
                .Replace("{full_name}", full_name).Replace("{user_id}", user_id).Replace("{email}", to)
                .Replace("{shipping_address}", shipping_address).Replace("{shopping_products}", shopping_products).Replace("{total}", total);

            var subject = Resources.MailMessage.PrepaidSaleGoesThroughForNewUser_subject;
            return new MailMessage(from, to, subject, body);
        }

        public static MailMessage PrepaidSaleGoesThroughForExistAccount(string from, string to, string full_name, string user_id, string shipping_address, string shopping_products, string total)
        {
            var body = Resources.MailMessage.PrepaidSaleGoesThroughForExistAccount
                .Replace("{full_name}", full_name).Replace("{user_id}", user_id).Replace("{email}", to)
                .Replace("{shipping_address}", shipping_address).Replace("{shopping_products}", shopping_products).Replace("{total}", total);

            var subject = Resources.MailMessage.PrepaidSaleGoesThroughForExistAccount_subject;
            return new MailMessage(from, to, subject, body);
        }

        public static MailMessage OnSomeoneJoiningAsUpgradeMonthlyMember(string from, string to, string full_name, string user_id, string price)
        {
            var body = Resources.MailMessage.OnSomeoneJoiningAsUpgradeMonthlyMember
                .Replace("{full_name}", full_name).Replace("{price}", price).Replace("{user_id}", user_id).Replace("{email}", to);

            var subject = Resources.MailMessage.OnSomeoneJoiningAsUpgradeMonthlyMember_subject;
            return new MailMessage(from, to, subject, body);

        }

        public static MailMessage OnSomeoneJoiningAsNewMonthlyMember(string from, string to, string full_name, string user_id, string price)
        {
            var body = Resources.MailMessage.OnSomeoneJoiningAsNewMonthlyMember
                .Replace("{full_name}", full_name).Replace("{price}", price).Replace("{user_id}", user_id).Replace("{email}", to);

            var subject = Resources.MailMessage.OnSomeoneJoiningAsNewMonthlyMember_subject;
            return new MailMessage(from, to, subject, body);
            
        }

        public static MailMessage OnSomeoneUpdatingTheirCreditCard(string from, string to, string full_name, string charged, string date)
        {
            var body = Resources.MailMessage.OnSomeoneUpdatingTheirCreditCard
                .Replace("{full_name}", full_name).Replace("{charged}", charged).Replace("{date}", date);

            var subject = Resources.MailMessage.OnSomeoneUpdatingTheirCreditCard_subject;
            return new MailMessage(from, to, subject, body);
        }

        public static MailMessage OnMonthlyMembershipCancellation(string from, string to, string full_name, string user_name)
        {
            var body = Resources.MailMessage.OnMonthlyMembershipCancellation
                .Replace("{full_name}", full_name).Replace("{user_name}", user_name).Replace("{email}", to);

            var subject = Resources.MailMessage.OnMonthlyMembershipCancellation_subject;
            return new MailMessage(from, to, subject, body);
        }

        public static MailMessage OnSomeoneChangeCreditCard(string from, string to, string full_name)
        {
            var body = Resources.MailMessage.OnSomeoneChangeCreditCard
                .Replace("{full_name}", full_name);

            var subject = Resources.MailMessage.OnSomeoneChangeCreditCard_subject;
            return new MailMessage(from, to, subject, body);
        }

        #region UserRegistration

        public static MailMessage OnRegisterFreeUser(string from, string to, string full_name, string password, string link)
        {
            var body = Resources.MailMessage.OnRegisterFreeUser
                .Replace("{full_name}", full_name).Replace("{email}", to).Replace("{link}", link).Replace("{password}", password);

            var subject = Resources.MailMessage.OnRegisterFreeUser_subject;
            return new MailMessage(from, to, subject, body);
        }

        public static MailMessage OnRegister2FreeDownloadsUser(string from, string to, string password, string link)
        {
            var body = Resources.MailMessage.OnRegister2FreeDownloadsUser
                .Replace("{email}", to).Replace("{link}", link).Replace("{password}", password);

            var subject = Resources.MailMessage.OnRegister2FreeDownloadsUser_subject;
            return new MailMessage(from, to, subject, body);
        }


        public static MailMessage OnRegisterFreeUserWithoutEmailValidate(string from, string to, string full_name, string password)
        {
            var body = Resources.MailMessage.OnRegisterFreeUserWithoutEmailValidate
                .Replace("{full_name}", full_name).Replace("{email}", to).Replace("{password}", password);

            var subject = Resources.MailMessage.OnRegisterFreeUserWithoutEmailValidate_subject;
            return new MailMessage(from, to, subject, body);
        }


        public static MailMessage OnRegisterMonthlyUser(string from, string to, string full_name, string password)
        {
            var body = Resources.MailMessage.OnRegisterMonthlyUser
                .Replace("{full_name}", full_name).Replace("{email}", to).Replace("{password}", password);

            var subject = Resources.MailMessage.OnRegisterMonthlyUser_subject;
            return new MailMessage(from, to, subject, body);
        }

        #endregion

        public static MailMessage OnUserBuyForMany(string from, string to, string full_name, long transaction_id, string shipping_address, ShoppingTransactionInfo info)
        {
            var buying = new StringBuilder();
            foreach (var shopping in info.ShoppingPrice)
            {
                try
                {
                    buying.AppendFormat(@"-----------------------

Product #: {0}
Title: {1}
Type: {2}
Qty: {3}
Price: {4}
Item Total: {5}

Notes: {6}

", string.IsNullOrEmpty(shopping.EntityItem.Code) ? shopping.EntityItem.CodeParent : shopping.EntityItem.Code,
               shopping.Title.Replace("[Tape] ", "").Replace("[Disk] ", "").Replace("[File/Small Poster] ", "").Replace("[File] ", ""),
               ((ProductTypeEnum)shopping.ProductTypeID).ToString(),
               shopping.cnt,
               shopping.price1 > 0 ? "$" + (shopping.price1 / shopping.cnt).ToString("0.##") : (shopping.price2 / shopping.cnt).ToString("0.##") + " unit(s)",
               shopping.price1 > 0 ? "$" + shopping.price1.ToString("0.##") : shopping.price2.ToString("0.##") + " unit(s)",
               shopping.EntityItem.ProductEntity != null ? shopping.EntityItem.ProductEntity.description : string.Empty);
                }
                catch (Exception ex)
                {
                    buying.AppendFormat("-----------------------\r\n{0}\r\n\r\n", ex.Message);
                }
            }

            var body = Resources.MailMessage.OnUserBuyForMany
                .Replace("{full_name}", full_name).Replace("{email}", to).Replace("{shipping_address}", shipping_address).Replace("{order_id}", transaction_id.ToString())
                .Replace("{products_info}", buying.ToString())
                .Replace("{sub_total}", info.Amount.ToString("0.##")).Replace("{shipping}", "0").Replace("{taxes}", "0").Replace("{final_total}", info.Amount.ToString("0.##"));
            var subject = Resources.MailMessage.OnUserBuyForMany_subject;

            return new MailMessage(from, to, subject, body);
        }

    }
}