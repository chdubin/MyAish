using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Main.Common.Attributes
{
    public class AuthorizedOnlyPortalAttribute : RequiresAuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isLoginAction = httpContext.Request.Url.AbsolutePath.EndsWith(FormsAuthentication.LoginUrl, StringComparison.InvariantCultureIgnoreCase);

            return isLoginAction || !httpContext.GetCurrentPortal().authorizedOnly || base.AuthorizeCore(httpContext);
        }
    }
}