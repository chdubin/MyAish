using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainCommon
{
    public enum ProductTypeEnum : short
    {
        [StringValue("Tape")]
        Tape = 1,
        [StringValue("Disk")]
        Disk = 2,
        [StringValue("File")]
        File = 3,
        [StringValue("Package")]
        Package = 4,

        Units = 5,

        Subscribe = 6,

        Shipping = 7,

        Taxes = 8
    }
}
