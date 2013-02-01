using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Collections.Specialized;

namespace MainCommon.Daemon
{

    public static class SubscribeDaemon
    {
        private static Queue<MailMessage> _subscribeQueue;
        private static System.Threading.Thread _subscribeThread;
        private static System.Threading.AutoResetEvent _endSubscribeThread;
        private static volatile bool _exitSubscribeThread;

        public static void StartSubscribeThread()
        {
            if (_subscribeThread != null) StopSubscribeThread();
            _exitSubscribeThread = false;
            _subscribeQueue = new Queue<MailMessage>();
            _endSubscribeThread = new System.Threading.AutoResetEvent(false);
            _subscribeThread = new System.Threading.Thread(new System.Threading.ThreadStart(SubscribeThread));
            _subscribeThread.Start();
        }
        public static void StopSubscribeThread()
        {
            _exitSubscribeThread = true;
            _endSubscribeThread.WaitOne();
            _subscribeThread = null;
        }

        public static void AddMessage(MailMessage message)
        {
            lock (_subscribeQueue)
            {
                _subscribeQueue.Enqueue(message);
            }
        }


        private static void SubscribeThread()
        {
            try
            {
                List<MailMessage> messages = new List<MailMessage>();
                SmtpClient sc = new SmtpClient();
                
                while (!_exitSubscribeThread)
                {
                    messages.Clear();
                    lock (_subscribeQueue)
                    {
                        while (messages.Count < 100 && _subscribeQueue.Count > 0)
                            messages.Add(_subscribeQueue.Dequeue());
                    }
                    foreach (var message in messages)
                    {
                        try
                        {
                            sc.EnableSsl = true;
                            sc.Send(message);
                        }
                        catch (Exception ex)
                        {
                            //MyUtils.WriteExceptionToLog(ex, System.Diagnostics.EventLogEntryType.Error, message.To.ToString(), message.CC.ToString(), 0);
                        }
                    }

                    System.Threading.Thread.Sleep(10000);
                }
            }
            finally
            {
                _endSubscribeThread.Set();
            }
        }
    }
}
