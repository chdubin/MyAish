using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainEntity.Interfaces;
using Main.Common;
using System.Web.UI;
using Main.Common.Attributes;
using Main.Utilities;

namespace Main.Controllers
{
    [MyRequireHttpsAttribute]
    [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
    [AuthorizedOnlyPortal]
    public class AudioController : Controller
    {
        private IFileService _fileService;
        private IShoppingService _shoppingService;
		private IActivityLogService _activityLogService;
		private IClassService _classService;

        public AudioController(IFileService file_service, IShoppingService shopping_service, IActivityLogService activitylog_service, IClassService class_service)
        {
            _fileService = file_service;
            _shoppingService = shopping_service;
			_activityLogService = activitylog_service;
			_classService = class_service;
        }

        [RequiresAuthorize]
        public RedirectResult GetAudio(long id, bool low = false)
        {
            string userName = this.User.Identity.Name;
            var provider = this.HttpContext.GetCurrentMembershipProvider();
            var user = provider.GetUser(userName, false);
            var userID = (Guid)user.ProviderUserKey;

            var shoppingID = _shoppingService.GetShoppingID(userID, id);
            if (shoppingID == 0)
                shoppingID = _activityLogService.GetLogID(userID, id);

            string url = "/Account/MyLibrary";

            if (shoppingID > 0)
            {
                var file = _fileService.GetFile(id, this.HttpContext.GetCurrentPortal().portalID);
                if (file != null)
                {
                    var filePath = !low ? file.filePath : file.alternateFilePath;

                    url = MvcApplication.S3Amazon.GetPreSignedUrlRequest(filePath);

                    try
                    {
                        _activityLogService.LoggingShopping(MainCommon.ActivityLogTypeEnum.DownloadClass, this.HttpContext.Request.UserHostAddress, shoppingID, id, userID);
                        _classService.IncreaseStatDownloadCnt(id);
                    }
                    catch { }
                }
            }
            else
                throw new Exception("Access denied.");

            return Redirect(url);
        }

        [RequiresAuthorize]
        public RedirectResult GetAudioNer(long id, bool low = false)
        {
            string userName = this.User.Identity.Name;
            var provider = this.HttpContext.GetCurrentMembershipProvider();
            var user = provider.GetUser(userName, false);
            var userID = (Guid)user.ProviderUserKey;

            string url = "/";

            var file = _fileService.GetFile(id, this.HttpContext.GetCurrentPortal().portalID);
            if (file != null)
            {
                var filePath = !low ? file.filePath : file.alternateFilePath;

                url = MvcApplication.S3Amazon.GetPreSignedUrlRequest(filePath);

                try
                {
                    _activityLogService.LoggingShopping(MainCommon.ActivityLogTypeEnum.DownloadClass, this.HttpContext.Request.UserHostAddress, 1, id, userID);
                    _classService.IncreaseStatDownloadCnt(id);
                }
                catch { }
            }

            return Redirect(url);
        }

		[RequiresAuthorize]
		public ActionResult GetStream(long id, bool low = false)
		{
			string userName = this.User.Identity.Name;
			var provider = this.HttpContext.GetCurrentMembershipProvider();
			var user = provider.GetUser(userName, false);
			var userID = (Guid)user.ProviderUserKey;

            var shoppingID = _shoppingService.GetShoppingID(userID, id);
            if (shoppingID == 0)
                shoppingID = _activityLogService.GetLogID(userID, id);

			if (shoppingID > 0)
			{

				var file = _fileService.GetFile(id, this.HttpContext.GetCurrentPortal().portalID);
				var filePath = !low ? file.filePath : file.alternateFilePath;
				this.ViewData["src"] = MvcApplication.S3Amazon.GetPreSignedUrlRequest(filePath);

                try
                {
                    _activityLogService.LoggingShopping(MainCommon.ActivityLogTypeEnum.StreamingClass, this.HttpContext.Request.UserHostAddress, shoppingID, id, userID);
                    _classService.IncreaseStatListenCnt(id);
                }
                catch { }
			}
			else
				throw new Exception("Access denied.");

			return View();
		}

		[RequiresAuthorize]
		public ActionResult GetFreeStream(long id)
        {
            string userName = this.User.Identity.Name;
            var provider = this.HttpContext.GetCurrentMembershipProvider();
            var user = provider.GetUser(userName, false);

			var file = _fileService.GetFile(id, this.HttpContext.GetCurrentPortal().portalID);
			if (file.CatalogItemInPortal.isFree && file.CatalogItemInPortal.isVisible)
			{
                var filePath = string.IsNullOrEmpty(file.filePath) ? file.alternateFilePath : file.filePath;
				Guid? userID = null;
				if (user!=null) userID = (Guid)user.ProviderUserKey;
				this.ViewData["src"] = MvcApplication.S3Amazon.GetPreSignedUrlRequest(filePath);

                try
                {
                    _activityLogService.LoggingEntityItem(MainCommon.ActivityLogTypeEnum.StreamingFreeClass, this.HttpContext.Request.UserHostAddress, id, userID);
                    _classService.IncreaseStatListenCnt(id);
                }
                catch { }
			}
			else
				throw new Exception("Only free stream");

            return View("GetStream");
        }

        public ActionResult GetFullFreeStream(long id)
        {
            string userName = this.User.Identity.Name;
            var provider = this.HttpContext.GetCurrentMembershipProvider();
            var user = provider.GetUser(userName, false);

            var file = _fileService.GetFile(id, this.HttpContext.GetCurrentPortal().portalID);
            if (file.CatalogItemInPortal.isFullFree && file.CatalogItemInPortal.isVisible)
            {
                var filePath = string.IsNullOrEmpty(file.filePath) ? file.alternateFilePath : file.filePath;
                Guid? userID = null;
                if (user != null) userID = (Guid)user.ProviderUserKey;
                this.ViewData["src"] = MvcApplication.S3Amazon.GetPreSignedUrlRequest(filePath);

                try
                {
                    _activityLogService.LoggingEntityItem(MainCommon.ActivityLogTypeEnum.StreamingFullFreeClass, this.HttpContext.Request.UserHostAddress, id, userID);
                    _classService.IncreaseStatListenCnt(id);
                }
                catch { }
            }
            else
                throw new Exception("Only full free stream");

            return View("GetStream");
        }

    }
}
 