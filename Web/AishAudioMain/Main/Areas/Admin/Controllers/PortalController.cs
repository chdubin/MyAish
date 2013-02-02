using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainEntity.Interfaces;
using Main.Common.Attributes;
using Main.Common;
using MainCommon;
using Main.Areas.Admin.Models.Catalog;
using Main.Areas.Admin.Models.Common;
using MainEntity.Models.Catalog;
using System.Web.Routing;
using System.Web.Security;
using Main.Utilities;

namespace Main.Areas.Admin.Controllers
{
    [HandleError]
    [MyRequireHttps]
    public class PortalController : Controller
    {
        private IPortalService _portalService;
        private ICatalogService _classService;
        private IUserService _userService;

        public PortalController(IPortalService portal_service, ICatalogService class_service, IUserService user_service)
        {
            _portalService = portal_service;
            _classService = class_service;
            _userService = user_service;
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult Index()
        {
            var mUser = this.HttpContext.GetMembershipUser();
            var isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(mUser.UserName, UserRoles.SUPERUSER_ROLE);
            var currentPortalID = this.HttpContext.GetCurrentPortal().portalID;

            var model = PortalController.GetPortalsForEdit(_portalService);

            if (!isSuperUser) model = model.Where(p => p.portalID == currentPortalID).ToArray();

            return View(model);
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult Edit(long portal_id)
        {
            if (!ValidatePortalAccess(portal_id))
                return Redirect(FormsAuthentication.LoginUrl);

            var portal = _portalService.GetPortal(portal_id, false, true);
            var model = new Main.Areas.Admin.Models.Portal.EditModel(portal);

            return View(model);
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult Edit(Main.Areas.Admin.Models.Portal.EditModel data)
        {
            if (this.ModelState.IsValid)
            {
                if (!ValidatePortalAccess(data.PortalID))
                    return Redirect(FormsAuthentication.LoginUrl);

                var aliases = GetAliases(data.Aliases);
                if (!AliasesExsist(aliases, data.PortalID))
                {
                    if (!string.IsNullOrEmpty(data.PasswordProtection)) data.ApplicationName = Properties.Settings.Default.PasswordProtectionAppName;

                    _portalService.Update(data.PortalID, data.Name, data.ApplicationName, data.ThemeName, aliases, data.Active,
                        data.AuthorizedOnly, data.AllowAuthorize, data.AllowRegister, data.AllowBuyFiles, data.AllowBuyProducts, data.PasswordProtection,
                        prev_password => CreatePasswordProtectionAccess(data.PortalID, data.PasswordProtection, prev_password, data.ApplicationName));

                    TempData["success"] = true;

                    MvcApplication.RegisterAllRouters();
                }
                else
                    this.ModelState.AddModelError("", "One or more aliases already exists in other portals");
            }
            return View("Edit", data);
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult Create()
        {
            var model = new Main.Areas.Admin.Models.Portal.EditModel();

            return View(model);
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult Create(Main.Areas.Admin.Models.Portal.EditModel data)
        {

            if (this.ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(data.PasswordProtection) || data.PasswordProtection.Length >= 6)
                {

                    var aliases = GetAliases(data.Aliases);
                    var user = this.HttpContext.GetMembershipUser();
                    if (!AliasesExsist(aliases, 0))
                    {
                        if (!string.IsNullOrEmpty(data.PasswordProtection)) data.ApplicationName = Properties.Settings.Default.PasswordProtectionAppName;

                        var role = Roles.Providers.Cast<RoleProvider>().
                            Where(m => m.ApplicationName.Equals(data.ApplicationName, StringComparison.InvariantCultureIgnoreCase)).Single();
                        if (!role.RoleExists(UserRoles.PORTALADMIN_ROLE)) role.CreateRole(UserRoles.PORTALADMIN_ROLE);

                        _portalService.InsertPortal(data.Name, data.ApplicationName, data.ThemeName, aliases, data.Active,
                            Main.GlobalConstant.ROOT_ENTITY_ID, (Guid)user.ProviderUserKey,
                            data.AuthorizedOnly, data.AllowAuthorize, data.AllowRegister, data.AllowBuyFiles, data.AllowBuyProducts, data.PasswordProtection,
                            portal_id => CreatePasswordProtectionAccess(portal_id, data.PasswordProtection, null, data.ApplicationName));

                        TempData["success"] = true;

                        MvcApplication.RegisterAllRouters();

                        return RedirectToAction("Index");
                    }
                    else
                        this.ModelState.AddModelError("", "One or more aliases is already exist in other portals");
                }
                else
                    this.ModelState.AddModelError("", "Password protection should be null or greater than 5 characters");
                
            }

            return View(data);
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult Delete(long portal_id)
        {
            if (portal_id != Main.GlobalConstant.MAIN_PORTAL_ID)
            {
                _portalService.DeletePortal(portal_id);
                MvcApplication.RegisterAllRouters();
            }
            else
                this.ModelState.AddModelError("", "Can not delete main portal");

            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult Administrators(long portal_id)
        {
            if (!ValidatePortalAccess(portal_id))
                return Redirect(FormsAuthentication.LoginUrl);

            var portal = _portalService.GetPortal(portal_id, false, true);
            var model = _userService.GetAspnetUsers(portal.applicationName, UserRoles.PORTALADMIN_ROLE, 0, int.MaxValue);
            return PartialView(model);
        }

        private bool ValidatePortalAccess(long portal_id)
        {
            var mUser = this.HttpContext.GetMembershipUser();
            var isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(mUser.UserName, UserRoles.SUPERUSER_ROLE);

            return isSuperUser || this.HttpContext.GetCurrentPortal().portalID == portal_id;
        }

        private  bool AliasesExsist(string[] from_aliases, long exclude_portal_id)
        {
            var aliases = _portalService.GetAliases();
            int cnt = 0;
            foreach (var alias in from_aliases)
                cnt += aliases.Where(a => a.portalID!=exclude_portal_id && a.alias.Equals(alias, StringComparison.InvariantCultureIgnoreCase)).Count();

            foreach (var alias in from_aliases.Where(a => !a.StartsWith("/"))
                .Select(a => a.Split(new[] { '/' }, 2))
                .Select(a => a.Length > 1 ? a[1].ToLowerInvariant() + "/" : string.Empty))
            {
                if (GlobalConstant.BlOCKED_ALIAS_NAMES.Where(a => alias.StartsWith(a)).Any()) cnt++;
            }

            return cnt > 0;
        }

        private static string[] GetAliases(string from_aliases_string)
        {
            return from_aliases_string.Split(new char[] { '\r', '\n', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Select(a => a.Trim()).ToArray();
        }

        public static MainEntity.Models.Portal.PortalEntity[] GetPortalsForEdit(IPortalService portal_service)
        {
            return portal_service.GetPortals(false, true, true);
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult Products(long portal_id, int p = 1, int ps = 100, int sort = 0, int desc = 0, int category_id = 0,
            string stitle = null, int[] scategory = null, string sspeaker = null, string scode = null, long sportal = 0)
        {
            var userName = this.User.Identity.Name;
            var isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);
            portal_id = isSuperUser ? portal_id : this.HttpContext.GetCurrentPortal().portalID;
            sportal = isSuperUser ? sportal : this.HttpContext.GetCurrentPortal().portalID;
            scategory = scategory == null && category_id != 0 ? new int[] { category_id } : scategory;
            var filterNew = scategory != null && scategory.Contains(-1);
            if (filterNew) scategory = scategory.Except(new int[] { -1 }).ToArray();

            int recordsCount = _classService.GetCatalogItemsCnt(GlobalConstant.ROOT_ENTITY_ID, false, true,
                sportal, scategory, stitle, sspeaker, scode, filterNew);
            var catalogItems = _classService.GetCatalogItems(GlobalConstant.ROOT_ENTITY_ID, false, true, (p - 1) * ps, ps,
                (SortParametersEnum)sort, desc == 1,
                portal_id, sportal, scategory, stitle, sspeaker, scode, filterNew);

            var model = CatalogItem.GetForList(portal_id, catalogItems);

            this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, 0, 100);
            this.ViewData["PortalName"] = _portalService.GetPortal(portal_id, false, false).EntityItem.title;

            List<KeyValuePair<string, string>> qs = MyUtils.ExcludeQueryParam(Request.QueryString, new string[] { "sort", "desc" });

            this.ViewData["SortTitleUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.Title;
            this.ViewData["SortDateUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.Date;
            //this.ViewData["SortActiveUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.Active;
            this.ViewData["SortVisibleUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.Visible;
            this.ViewData["SortFreeUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.Free;
            this.ViewData["SortFreeOfferUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.FreeOffer;

            switch ((SortParametersEnum)sort)
            {
                case SortParametersEnum.Title:
                    this.ViewData["SortTitleHeaderClass"] = desc == 1 ? "headerSortUp" : "headerSortDown";
                    this.ViewData["SortTitleUrl"] += (desc == 0 ? "&desc=1" : string.Empty);
                    break;
                case SortParametersEnum.Date:
                    this.ViewData["SortDateHeaderClass"] = desc == 1 ? "headerSortUp" : "headerSortDown";
                    this.ViewData["SortDateUrl"] += (desc == 0 ? "&desc=1" : string.Empty);
                    break;
                /*case SortParametersEnum.Active:
                    this.ViewData["SortActiveHeaderClass"] = desc == 1 ? "headerSortUp" : "headerSortDown";
                    this.ViewData["SortActiveUrl"] += (desc == 0 ? "&desc=1" : string.Empty);
                    break;*/
                case SortParametersEnum.Visible:
                    this.ViewData["SortVisibleHeaderClass"] = desc == 1 ? "headerSortUp" : "headerSortDown";
                    this.ViewData["SortVisibleUrl"] += (desc == 0 ? "&desc=1" : string.Empty);
                    break;
                case SortParametersEnum.Free:
                    this.ViewData["SortFreeHeaderClass"] = desc == 1 ? "headerSortUp" : "headerSortDown";
                    this.ViewData["SortFreeUrl"] += (desc == 0 ? "&desc=1" : string.Empty);
                    break;
                case SortParametersEnum.FreeOffer:
                    this.ViewData["SortFreeOfferHeaderClass"] = desc == 1 ? "headerSortUp" : "headerSortDown";
                    this.ViewData["SortFreeOfferUrl"] += (desc == 0 ? "&desc=1" : string.Empty);
                    break;
            }

            this.ViewData["SortTitleUrl"] = ((string)this.ViewData["SortTitleUrl"]).TrimEnd(new char[] { '?', '&' });
            this.ViewData["SortDateUrl"] = ((string)this.ViewData["SortDateUrl"]).TrimEnd(new char[] { '?', '&' });
            //this.ViewData["SortActiveUrl"] = ((string)this.ViewData["SortActiveUrl"]).TrimEnd(new char[] { '?', '&' });
            this.ViewData["SortVisibleUrl"] = ((string)this.ViewData["SortVisibleUrl"]).TrimEnd(new char[] { '?', '&' });
            this.ViewData["SortFreeUrl"] = ((string)this.ViewData["SortFreeUrl"]).TrimEnd(new char[] { '?', '&' });
            this.ViewData["SortFreeOfferUrl"] = ((string)this.ViewData["SortFreeOfferUrl"]).TrimEnd(new char[] { '?', '&' });

            List<KeyValuePair<string, string>> qs_2 = MyUtils.ExcludeQueryParam(Request.QueryString, new string[] { "category_id", "p", "ps" });
            this.ViewData["ClearFilterPath"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs_2);
            this.ViewData["Categories"] = _classService.GetAllCategories();
            this.ViewData["CurrentCategoryID"] = category_id;

            return View(model);
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult SavePortalClasses(CatalogItem[] CatalogItems, long portal_id)
        {
            try
            {
                EntityItem[] catalogItems = CatalogItems.Select(i => new EntityItem
                {
                    entityID = i.CatalogItemID,
                    active = i.Active,
                    CurrentPortal = new CatalogItemXrefPortal
                    {
                        portalID = i.Selected ? portal_id : 0,
                        isVisible = i.Visible ?? false,
                        isFree = i.IsFree ?? false,
                        isFreeOffer = i.IsFreeOffer ?? false
                    },
                    CatalogItemExtend = new CatalogItemExtend
                    {
                        notes = i.Notes
                    }
                }).ToArray();

                _classService.UpdatePortalItems(catalogItems, portal_id);
            }
            catch (Exception ex) 
            {
                this.ModelState.AddModelError("", "Unhandled Error is: " + ex.Message);
            }

            this.TempData["ViewMessage"] = ViewMessageEnum.UpdateSuccess;

            RouteValueDictionary rv = new RouteValueDictionary();

            foreach (string key in Request.QueryString.AllKeys)
                rv.Add(key, Request.QueryString[key]);

            return RedirectToAction("Products", rv);
        }

        private void CreatePasswordProtectionAccess(long portal_id, string password, string prev_password, string application_name)
        {
            var provider = Membership.Providers.Cast<MembershipProvider>().
                        Where(m => m.ApplicationName.Equals(application_name, StringComparison.InvariantCultureIgnoreCase)).Single();
            var userName = string.Format(Properties.Settings.Default.PasswordProtectionUserName, portal_id);
            var prevUser = provider.GetUser(userName, false);
            if (!string.IsNullOrEmpty(password))
            {
                if (prevUser != null)
                {
                    prevUser.IsApproved = true;
                    provider.UpdateUser(prevUser);
                    prevUser.ChangePassword(prev_password, password);
                }
                else
                    _userService.CreateUser(provider, userName, password, true, "branch password protection access", null, 0, null, null, null, null, null, null, null, false);
            }
            else if (prevUser != null)
                provider.DeleteUser(prevUser.UserName, true);

        }
    }
}
