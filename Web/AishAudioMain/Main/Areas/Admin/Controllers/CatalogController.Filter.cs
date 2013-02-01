using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Areas.Admin.Models.ControllerView.Catalog;
using Main.Common;

namespace Main.Areas.Admin.Controllers
{
    public partial class CatalogController
    {
        [ChildActionOnly]
        public ActionResult ClassFilter(string stitle, int[] scategory, string sspeaker, string scode, long sportal=0, long portal_id=0, Guid? user_id=null)
        {
            var userName = this.User.Identity.Name;
            var isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);
            this.ViewData["IsSuperUser"] = isSuperUser;
            this.ViewData["user_id"] = user_id;

            var categoryList = this.HttpContext.Cache.GetValue(MainCommon.CacheEnum.CategoryCache.ToString(),
                TimeSpan.FromSeconds(GlobalConstant.CATEGORY_IN_CACHE_SEC), () => _tagService.GetCategories());
            var portalList = this.HttpContext.Cache.GetValue(MainCommon.CacheEnum.PortalCache.ToString(),
                TimeSpan.FromSeconds(GlobalConstant.PORTAL_ALIASES_IN_CACHE_SEC), () => _portalService.GetPortals(true,true,false));

            var model = new ClassFilter(sspeaker, sportal, stitle, scode, scategory, portalList, categoryList, portal_id, user_id);

            return PartialView(model);
        }
    }
}