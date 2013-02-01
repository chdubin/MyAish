using System.Linq;
using System.Web.Mvc;
using Main.Common;
using MainEntity.Interfaces;
using Main.Common.Routing;

namespace Main.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            var portalService = MvcApplication.Container.Resolve<IPortalService>();

            var aliases = portalService.GetAliases();
            var constraints = new { portal_part = new PortalConstraint(aliases) };

            context.MapPortalRoute(
                "Portal/Admin_default",
                portalService,
                "Admin/{portal_part}/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints,
                new string[] { "Main.Areas.Admin.Controllers" }
            );

            context.MapPortalRoute(
                "Admin_default",
                portalService,
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                null,
                new string[] { "Main.Areas.Admin.Controllers" }
            );
        }
    }
}
