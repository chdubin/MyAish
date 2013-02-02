using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;

namespace MainEntity.Interfaces
{
    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, string application_name, HttpCookieCollection response_cookies);
        void SignOut();

    }
}
