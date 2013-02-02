using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Interfaces;
using System.Web.Security;
using System.Web;

namespace MainBL
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, string application_name, HttpCookieCollection response_cookies)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");

            var timeout = TimeSpan.FromMinutes(44640);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2,
              userName,
              DateTime.Now,
              DateTime.Now.Add(timeout),
              false,
              application_name,
              FormsAuthentication.FormsCookiePath);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            cookie.Expires = DateTime.Now.Add(timeout);
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Secure = FormsAuthentication.RequireSSL;
            response_cookies.Add(cookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

    }
}
