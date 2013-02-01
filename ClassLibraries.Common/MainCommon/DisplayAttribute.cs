using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MainCommon
{
    [AttributeUsage(AttributeTargets.All)]
    public class DisplayAttribute : DisplayNameAttribute
    {
        public DisplayAttribute(string display_name)
            : base(display_name)
        {
        }
    }
}
