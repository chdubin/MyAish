using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainCommon.Skipjack
{
    public class SJTimeoutException : Exception
    {
        public SJTimeoutException(string message)
            : base(message)
        {
        }
    }
}