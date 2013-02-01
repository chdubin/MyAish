using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.Common.Attributes
{
    public class AllowRegisterPortalAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!filterContext.HttpContext.GetCurrentPortal().allowRegister)
                throw new HttpException(404, "Page not available");
        }  
    }
}