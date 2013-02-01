using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Common
{
    public static class UserRoles
    {
        public const string SUPERUSER_ROLE = "SuperUser";
        public const string PORTALADMIN_ROLE = "PortalAdmin";

        public const string SUPERUSER_PORTALADMIN_ROLES = SUPERUSER_ROLE + "," + PORTALADMIN_ROLE;
    }
}