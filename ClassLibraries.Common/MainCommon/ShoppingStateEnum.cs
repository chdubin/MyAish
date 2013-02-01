using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainCommon
{
    public enum ShoppingStateEnum:short
    {
        PayInProccess = 1,
        PaySuccessed = 2,
        PayError = 3,
        Prepaid = 4,
    }
}
