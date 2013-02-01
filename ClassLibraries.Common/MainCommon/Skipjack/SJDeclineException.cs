using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainCommon.Skipjack
{
    public class SJDeclineException:Exception
    {
        public SJDeclineException(string message)
            : base(message)
        {
        }
    }
}