using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using MainCommon;
using MainEntity.Interfaces;

namespace Main.Common.Skipjack
{
    public class SkipjackDaemon: IDisposable
    {
        private IShoppingService _shoppingService;
        private Thread _workThread;

        protected bool StopDaemon { get; set; }
        protected AutoResetEvent DaemonStoped { get; private set; }

        public SkipjackDaemon(IShoppingService shopping_service)
        {
            _shoppingService = shopping_service;

            StopDaemon = false;
            DaemonStoped = new AutoResetEvent(false);

            _workThread = new Thread(new ThreadStart(Do));
            _workThread.Start();
        }

        private void Do()
        {
            try
            {
                while(!StopDaemon)
                {
                    try
                    {
                        var transactions = _shoppingService.GetShoppingTransactions(ShoppingStateEnum.Prepaid);
                        foreach (var tr in transactions)
                        {
                            var resp = SkipjackExecutor.SJGetStatus(tr.shoppingTransactionID,
								Properties.Settings.Default.SJSerialNumber, Properties.Settings.Default.SJDeveloperSerialNumber, Properties.Settings.Default.SJGetStatusUrl);
                            if (resp[SkipJackGetStatus.StatusCode].Substring(0, 1) == "3")
                                _shoppingService.ShoppingComplete(tr.shoppingTransactionID, resp[SkipJackGetStatus.StatusMessage]);
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

        public void Dispose()
        {
            StopDaemon = true;
            DaemonStoped.WaitOne();
        }
    }
}