using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Main.Common.Attributes
{
    public class RequiresAuthorizeAttribute : AuthorizeAttribute
    {
        public RequiresAuthorizeAttribute()
        {
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var parts = FormsAuthentication.LoginUrl.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var route = new { ReturnUrl = filterContext.HttpContext.Request.Url.PathAndQuery };
            var redirectUrl = new UrlHelper(filterContext.RequestContext).Action(parts.Length > 1 ? parts[1] : null, parts[0], route);
            filterContext.Result = new RedirectResult(redirectUrl);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            MembershipUser user = httpContext.GetMembershipUser();

            return user != null;
        }

    }
}