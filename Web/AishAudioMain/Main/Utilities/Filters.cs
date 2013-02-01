using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.Utilities
{
    public class RequireHttp : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // If the request has arrived via HTTPS...
            if (filterContext.HttpContext.Request.IsSecureConnection)
            {
                filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.Url.ToString().Replace("https:", "http:")); // Go on, bugger off "s"!
                filterContext.Result.ExecuteResult(filterContext);
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class MyRequireHttpsAttribute : RequireHttpsAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string domain = filterContext.HttpContext.Request.Url.Host;

            if (!filterContext.HttpContext.Request.IsSecureConnection && domain.Equals("aishaudio.com"))
                base.OnAuthorization(filterContext);
        }
    }
}