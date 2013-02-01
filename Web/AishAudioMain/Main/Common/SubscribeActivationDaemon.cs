using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainEntity.Interfaces;
using System.Threading;
using MainCommon;
using MainEntity.Models.User;
using System.Text;
using MainCommon.Skipjack;

namespace Main.Common
{

    public class SubscribeActivationDaemon : IDisposable
    {
        private IShoppingService _shoppingService;
        private IUserService _userService;
        private Thread _workThread;

        protected bool StopDaemon { get; set; }
        protected EventWaitHandle DaemonStoped { get; private set; }

        public SubscribeActivationDaemon(IUserService user_service, IShoppingService shopping_service)
        {
            _shoppingService = shopping_service;
            _userService = user_service;

            StopDaemon = false;
            DaemonStoped = new EventWaitHandle(true, EventResetMode.ManualReset);
        }

        public void Start()
        {
            lock (this)
            {
                if (!IsStarted)
                {
                    DaemonStoped.WaitOne();
                    StopDaemon = false;
                    DaemonStoped.Reset();
                    _workThread = new Thread(new ThreadStart(Do));
                    _workThread.Start();
                }
            }
        }

        public bool IsStarted
        {
            get
            {
                lock (this) return _workThread != null && _workThread.IsAlive;
            }
        }

        public EventWaitHandle Stop()
        {
            StopDaemon = true;
            return DaemonStoped;
        }

        public void Dispose()
        {
            Stop().WaitOne();
        }

        private void Do()
        {
            try
            {
                while (!StopDaemon)
                {
                    try
                    {
                        MainEntity.Models.User.Membership[] subscribers = _userService.GetActivationSubscribers(0, int.MaxValue).Where(u => !u.suspended)
//#if DEBUG
//                            .Where(u=>u.firstName.ToLowerInvariant().StartsWith("nik"))
//#endif
                            .ToArray();
                        foreach (var subscriber in subscribers)
                        {
                            try
                            {
                                var sjLog = new StringBuilder();
                                long transactionID = 0;
                                var transactionInfo = _shoppingService.GetShoppingTransactionInfo(new[] { subscriber.NextSubscribePlanID },
                                    subscriber.UserId, false, (decimal)Properties.Settings.Default.SubscriberDiscount, Properties.Settings.Default.MaxUnitsOnSubscribe, true);
                                try
                                {
                                    var card = _userService.GetLastMembershipCard(subscriber.UserId);
                                    var chargeType = ShoppingTransactionTypeEnum.monthly.ToString();
                                    var tranID = card.tranID;
                                    var cardID = card.membershipCartID;
                                    var authShoppingTransactionID = card.shoppingTransactionID;
                                    KeyValuePair<long, long?>? addresses = null;

                                    ////////_shoppingService.ShoppingPay(subscriber.UserId, transactionInfo, chargeType, cardID, addresses,
                                    ////////    (transaction_id) =>
                                    ////////    {
                                    ////////        transactionID = transaction_id;
                                    ////////        return SkipjackExecutor.Pay(tranID, transaction_id, authShoppingTransactionID, transactionInfo.Amount,
                                    ////////        Properties.Settings.Default.SJSerialNumber, Properties.Settings.Default.SJDeveloperSerialNumber,
                                    ////////        Properties.Settings.Default.SJChangeStatusUrl, Properties.Settings.Default.SJGetStatusUrl, sjLog);
                                    ////////    });

                                    Mailer.AddMessage(Mailer.ChargeOnSkipjackIsSuccess(Properties.Settings.Default.FromEmailNotification, Properties.Settings.Default.AdminEmailSubscribeNotification, transactionInfo.Amount.ToString(), sjLog.ToString())); 


                                    //Mailer.AddMessage(Mailer.OnSomeoneUpdatingTheirCreditCard(
                                    //        Properties.Settings.Default.FromEmailNotification, subscriber.Email, subscriber.firstName + " " + subscriber.lastName, transactionInfo.Amount.ToString(), DateTime.Now.ToString("D")),
                                    //    Properties.Settings.Default.AdminEmailSubscribeNotification);
                                }
                                catch (SJTimeoutException)
                                {
                                    Mailer.AddMessage(Mailer.NoResponseFromTheSkipjack(
                                        Properties.Settings.Default.FromEmailNotification, Properties.Settings.Default.AdminEmailSubscribeNotification, subscriber.UserId.ToString(), transactionID.ToString(), transactionInfo.Amount.ToString(), sjLog.ToString()));
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
                            }
                            catch
                            {
                                _userService.DelaySubscribeActivation(subscriber.UserId, 24, 3);
                            }
                        }
                    }
                    catch
                    {
                    }
                    for (int i = 0; i < 1800 && !StopDaemon; i++)
                        Thread.Sleep(2000);

                }
            }
            finally
            {
                DaemonStoped.Set();
            }
        }




    }

}