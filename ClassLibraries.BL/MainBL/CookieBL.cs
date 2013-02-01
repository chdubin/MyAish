using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MainBL
{
    public static class CookieBL
    {
        private const string SHOW_DETAILED_LIST_COOKIE_NAME = "ShowDetailedList";

        public static bool ShowDetailed(HttpCookieCollection cookies, HttpCookieCollection optional_cookies)
        {
            bool rval = false;

            rval = cookies.AllKeys.Contains(SHOW_DETAILED_LIST_COOKIE_NAME) && cookies[SHOW_DETAILED_LIST_COOKIE_NAME].Value == "true";

            if (optional_cookies != null
                && optional_cookies.AllKeys.Contains(SHOW_DETAILED_LIST_COOKIE_NAME)
                && optional_cookies[SHOW_DETAILED_LIST_COOKIE_NAME].Value == "false")
                rval = false;

            return rval;
        }

        public static void SetDetailedList(HttpCookieCollection request_cookies, HttpCookieCollection responce_cookies, bool show_detailed)
        {
            if (show_detailed)
            {
                responce_cookies.Add(new HttpCookie(SHOW_DETAILED_LIST_COOKIE_NAME, "true")
                {
                    Expires = DateTime.Now.AddMonths(6)
                });
            }
            else
            {
                responce_cookies.Set(new HttpCookie(SHOW_DETAILED_LIST_COOKIE_NAME, "false") { Expires = DateTime.Now.AddDays(-1) });
            }
        }
    }
}
