using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Common.Attributes;
using Main.Common;
using MainEntity.Interfaces;
using Main.Utilities;

namespace Main.Areas.Admin.Controllers
{
    [MyRequireHttps]
    public class AudioController : Controller
    {
        private IFileService _fileService;

        public AudioController(IFileService file_service)
        {
            _fileService = file_service;
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult GetStream(string name, long id=0, bool low = false)
        {
            if (!string.IsNullOrEmpty(name))
            {
                this.ViewData["src"] = MvcApplication.S3Amazon.GetPreSignedUrlRequest(name);
            }
            else 
            {
                var userName = this.HttpContext.User.Identity.Name;
                var portalID = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE) ? null : (long?)this.HttpContext.GetCurrentPortal().portalID;

                var file = _fileService.GetFile(id, portalID);
                var filePath = !low ? file.filePath : file.alternateFilePath;

                this.ViewData["src"] = MvcApplication.S3Amazon.GetPreSignedUrlRequest(filePath);
            }

            return View();
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public RedirectResult GetAudio(string name, long id=0, bool low = false)
        {
            string url;

            if (!string.IsNullOrEmpty(name))
            {
                url = MvcApplication.S3Amazon.GetPreSignedUrlRequest(name);
            }
            else
            {
                var userName = this.HttpContext.User.Identity.Name;
                var portalID = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE) ? null : (long?)this.HttpContext.GetCurrentPortal().portalID;

                var file = _fileService.GetFile(id, portalID);
                var filePath = !low ? file.filePath : file.alternateFilePath;

                url = MvcApplication.S3Amazon.GetPreSignedUrlRequest(filePath);
            }

            return Redirect(url);
        }
    }
}