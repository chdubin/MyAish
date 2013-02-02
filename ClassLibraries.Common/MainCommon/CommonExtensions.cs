using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MainCommon
{
    public static class CommonExtensions
    {
        public static string GetValue(this HttpContext context, MainCommon.CartItemTypeEnum key, string default_value)
        {
            string rval;
            var sessionValue = context.Request.Cookies[key.ToString()];
            if (sessionValue == null)
            {
                //session[key.ToString()] = default_value;
                rval = default_value;
            }
            else rval = sessionValue.Value;

            return rval;
        }

        public static void SetValue(this HttpContext context, MainCommon.CartItemTypeEnum key, string val)
        {
            if (val == String.Empty)
            {
                if (context.Response.Cookies[key.ToString()] != null)
                {
                    HttpCookie cookie = context.Response.Cookies[key.ToString()];
                    cookie.Expires = DateTime.Now.AddYears(-1);
                    //context.Response.Cookies.Add(cookie);
                }
                context.Request.Cookies.Remove(key.ToString());
            }
            else
            {
                if (context.Request.Cookies[key.ToString()] == null)
                {
                    HttpCookie cookie = new HttpCookie(key.ToString(), val);
                    cookie.Expires = DateTime.Now.AddYears(100);
                    context.Request.Cookies.Add(cookie);
                    context.Response.Cookies.Add(cookie);
                }
                else
                {
                    HttpCookie cookie = context.Response.Cookies[key.ToString()];
                    cookie.Expires = DateTime.Now.AddYears(100);
                    cookie.Value = val;
                    cookie = context.Request.Cookies[key.ToString()];
                    cookie.Expires = DateTime.Now.AddYears(100);
                    cookie.Value = val;
                }
            }
        }
    }
}
