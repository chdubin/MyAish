using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Common;

namespace Main.Common.Attributes
{
    public class PortalAuthorizeAttribute : RequiresAuthorizeAttribute
    {
        public bool AllRoleToAllow { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var rval = base.AuthorizeCore(httpContext);
            if (rval && !string.IsNullOrEmpty(this.Roles))
            {
                var roles = this.Roles.Split(',').Select(r => r.Trim()).ToArray();
                if (roles.Length > 1)
                {
                    var userRoles = httpContext.GetCurrentRoleProvider().GetRolesForUser(httpContext.User.Identity.Name);
                    var toRoles = userRoles.Where(ur => roles.Contains(ur)).Count();
                    if (AllRoleToAllow)
                        rval = toRoles == roles.Length;
                    else
                        rval = toRoles > 0;
                }
                else
                    rval = httpContext.GetCurrentRoleProvider().IsUserInRole(httpContext.User.Identity.Name, roles[0]);
            }

            return rval;
        }
    }
}