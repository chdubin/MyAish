using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainCommon.Skipjack
{
    public class SJUnhandledException:Exception
    {
        public SJUnhandledException(Exception inner):base(null, inner)
        {
        }
    }
}