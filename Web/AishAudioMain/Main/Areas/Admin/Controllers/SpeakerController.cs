﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Common.Attributes;
using Main.Common;
using MainEntity.Interfaces;
using MainCommon;
using Main.Areas.Admin.Models.Common;
using System.IO;
using System.Configuration;
using Main.Areas.Admin.Models.Catalog;
using Main.Utilities;

namespace Main.Areas.Admin.Controllers
{
    [MyRequireHttps]
    public class SpeakerController : Controller
    {
        private ISpeakerService _speakerService;
        private ICatalogService _catalogService;
///////////////////////////////////////////////////////
//this was added on feb 2 from version i had download from git hube
        private IShoppingService _shoppingService;
        private IActivityLogService _activityLogService;
        private IFileService _fileService;
////////////////////////////////////////////////////////

                public SpeakerController(ISpeakerService speaker_service, ICatalogService catalog_service,
            IShoppingService shopping_service, IFileService file_service, IActivityLogService log_activity_service)
        {
            _speakerService = speaker_service;
            _catalogService = catalog_service;
   _shoppingService = shopping_service;
            _fileService = file_service;
            _activityLogService = log_activity_service;
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
          public ActionResult Index(int p = 1, int ps = 100, int sort = 0, int desc = 0, string downloadtype = "normal")
        {
            var portalID = this.HttpContext.GetCurrentPortal().portalID;

            int recordsCount = _speakerService.GetSpeakersCnt1(GlobalConstant.ROOT_ENTITY_ID, null, true, portalID);

            var model = _speakerService.GetSpeakers1(GlobalConstant.ROOT_ENTITY_ID, null, true, (p - 1) * ps, ps, (SortParametersEnum)sort, desc == 1, portalID);

            this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, 0, 100);

            List<KeyValuePair<string, string>> qs = MyUtils.ExcludeQueryParam(Request.QueryString, new string[] { "sort", "desc" });

            this.ViewData["SortTitleHeaderClass"] =
                this.ViewData["SortDateHeaderClass"] =
                this.ViewData["SortVisibleHeaderClass"] = string.Empty;

            this.ViewData["SortTitleUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.Title;
            this.ViewData["SortDateUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.Date;
            this.ViewData["SortActiveUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.Active;
            this.ViewData["SortVisibleUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.Visible;
            this.ViewData["SortFreeUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.Free;

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
                case SortParametersEnum.Active:
                    this.ViewData["SortActiveHeaderClass"] = desc == 1 ? "headerSortUp" : "headerSortDown";
                    this.ViewData["SortActiveUrl"] += (desc == 0 ? "&desc=1" : string.Empty);
                    break;
                case SortParametersEnum.Visible:
                    this.ViewData["SortVisibleHeaderClass"] = desc == 1 ? "headerSortUp" : "headerSortDown";
                    this.ViewData["SortVisibleUrl"] += (desc == 0 ? "&desc=1" : string.Empty);
                    break;
                case SortParametersEnum.Free:
                    this.ViewData["SortFreeHeaderClass"] = desc == 1 ? "headerSortUp" : "headerSortDown";
                    this.ViewData["SortFreeUrl"] += (desc == 0 ? "&desc=1" : string.Empty);
                    break;
            }

            this.ViewData["SortTitleUrl"] = ((string)this.ViewData["SortTitleUrl"]).TrimEnd(new char[] { '?', '&' });
            this.ViewData["SortDateUrl"] = ((string)this.ViewData["SortDateUrl"]).TrimEnd(new char[] { '?', '&' });
            this.ViewData["SortActiveUrl"] = ((string)this.ViewData["SortActiveUrl"]).TrimEnd(new char[] { '?', '&' });
            this.ViewData["SortVisibleUrl"] = ((string)this.ViewData["SortVisibleUrl"]).TrimEnd(new char[] { '?', '&' });
            this.ViewData["SortFreeUrl"] = ((string)this.ViewData["SortFreeUrl"]).TrimEnd(new char[] { '?', '&' });

            return View(model);

        }


        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult Classes(long speaker_id, int p = 1, int ps = 100, int category_id = 0)
        {
            string userName = this.User.Identity.Name;
            bool isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);
            long portalID = (this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE) ? 0 : 
                this.HttpContext.GetCurrentPortal().portalID);

            int recordsCount = _catalogService.GetSpeakerClassesCnt(GlobalConstant.ROOT_ENTITY_ID, speaker_id, false, true, portalID, category_id, isSuperUser);
            var catalogItems = _catalogService.GetSpeakerClasses(GlobalConstant.ROOT_ENTITY_ID, speaker_id, (p - 1) * ps, ps, false, true, portalID, category_id, isSuperUser);
            var model = CatalogItem.GetForList(portalID, catalogItems);

            this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, 0, 100);

            List<KeyValuePair<string, string>> qs_2 = MyUtils.ExcludeQueryParam(Request.QueryString, new string[] { "category_id", "p", "ps" });
            this.ViewData["ClearFilterPath"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs_2);
            this.ViewData["Categories"] = _catalogService.GetAllCategories();
            this.ViewData["CurrentCategoryID"] = category_id;

            return View(model);
        }
        
  

        #region Edit Speaker

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult Edit(long speaker_id)
        {
            var speaker = _speakerService.GetSpeaker(speaker_id);
            var model = new Main.Areas.Admin.Models.Speaker.SpeakerEditModel(speaker);

            // work with file
            model.PhotoPath = _speakerService.GetSpeakerPhoto(speaker_id);

            return View(model);
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult Edit(Main.Areas.Admin.Models.Speaker.SpeakerEditModel speaker)
        {
            if (this.ModelState.IsValid)
            {
                var userName = this.User.Identity.Name;
                var user = this.HttpContext.GetCurrentMembershipProvider().GetUser(userName, true);

                _speakerService.Update((Guid)user.ProviderUserKey, GlobalConstant.ROOT_ENTITY_ID, speaker.SpeakerID, speaker.Name, speaker.Photo, 
                    speaker.Photo != null ? Path.Combine(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["UploadSpeakerImgFolderName"]),
                                Path.GetFileName(speaker.Photo.FileName)) : string.Empty, speaker.ContactInfo, speaker.Description, speaker.Active);
                TempData["success"] = true;
            }


            // work with file
            speaker.PhotoPath = _speakerService.GetSpeakerPhoto(speaker.SpeakerID);

            return View("Edit", speaker);
        }

        #endregion


        #region Create Speaker

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult Create()
        {
            var model = new Main.Areas.Admin.Models.Speaker.SpeakerEditModel();

            return View(model);
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult Create(Main.Areas.Admin.Models.Speaker.SpeakerEditModel speaker)
        {
            if (this.ModelState.IsValid)
            {
                var userName = this.User.Identity.Name;
                var user = this.HttpContext.GetCurrentMembershipProvider().GetUser(userName, true);

                long entityID = _speakerService.InsertSpeaker((Guid)user.ProviderUserKey, GlobalConstant.ROOT_ENTITY_ID, speaker.Name, speaker.Photo,
                    speaker.Photo != null ? Path.Combine(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["UploadSpeakerImgFolderName"]),
                                Path.GetFileName(speaker.Photo.FileName)) : string.Empty, speaker.Description, speaker.ContactInfo, speaker.Active);
                TempData["success"] = true;
                return RedirectToAction("Create");
            }

            return View(speaker);
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public JsonResult CreateSpeaker(string name)
        {
            name = name.Trim();
            if (string.IsNullOrEmpty(name))
                return null;

            long entityID = 0;
            MainEntity.Models.Speaker.EntityItem s = _speakerService.GetSpeaker(name);
            if (s == null)
            {
                string userName = this.User.Identity.Name;
                var user = this.HttpContext.GetCurrentMembershipProvider().GetUser(userName, true);
                entityID = _speakerService.InsertSpeaker((Guid)user.ProviderUserKey, GlobalConstant.ROOT_ENTITY_ID, name, string.Empty, string.Empty);
            }
            else
                entityID = s.entityID;

            return new JsonResult { Data = new { name = name, id = entityID } };
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public void CreateSpeaker1(string name)
        {
            name = name.Trim();
            if (string.IsNullOrEmpty(name))
                return;

            long entityID = 0;
            MainEntity.Models.Speaker.EntityItem s = _speakerService.GetSpeaker(name);
            if (s == null)
            {
                string userName = this.User.Identity.Name;
                var user = this.HttpContext.GetCurrentMembershipProvider().GetUser(userName, true);
                entityID = _speakerService.InsertSpeaker((Guid)user.ProviderUserKey, GlobalConstant.ROOT_ENTITY_ID, name, string.Empty, string.Empty);
            }
            else
                entityID = s.entityID;

        //    return new JsonResult { Data = new { name = name, id = entityID } };
        }
        #endregion


        #region Delete Speaker

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult Delete(long speaker_id)
        {
            _speakerService.DeleteSpeaker(speaker_id);

            return RedirectToAction("Index");
        }

        #endregion
    }
}
