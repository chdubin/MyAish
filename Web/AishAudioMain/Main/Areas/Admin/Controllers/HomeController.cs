using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Common.Attributes;
using Main.Common;
using MainEntity.Interfaces;
using Main.Areas.Admin.Models.ControllerView;
using Main.Utilities;

namespace Main.Areas.Admin.Controllers
{
    [HandleError]
    [MyRequireHttps]
    public class HomeController : Controller
    {
        private IPortalService _portalService;
        private ICatalogService _catalogService;
        private IFileService _fileService;

        public HomeController(IPortalService portal_service, ICatalogService catalog_service, IFileService file_service)
        {
            _portalService = portal_service;
            _catalogService = catalog_service;
            _fileService = file_service;
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult Index()
        {
            return View();
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult StopSubscribeDaemon()
        {
            MvcApplication.ActivationDaemon.Stop().WaitOne();
            return View("Index");
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult StartSubscribeDaemon()
        {
            MvcApplication.ActivationDaemon.Start();
            return View("Index");
        }

        public ActionResult FixAmazonFileUrl()
        {
            var dbFiles = _catalogService.GetClassEntitiesWithFiles(GlobalConstant.MAIN_PORTAL_ID, new long[] { }, 0, int.MaxValue, 0,null,null,null,null,false);
            var problemFiles = new List<FixAmazonFile>();
            var fixedFiles = new List<KeyValuePair<string, string>>();

            if (MvcApplication.S3Amazon.Hierarchy != null)
            {
                var amazonFiles = MvcApplication.S3Amazon.Hierarchy.Files;
                foreach (var dbFile in dbFiles)
                {
                    var fileMask = dbFile.FileEntity.filePath.Replace('_', '?');
                    var alternateFileMask = dbFile.FileEntity.alternateFilePath.Replace('_', '?');
                    var filePath = dbFile.FileEntity.filePath;
                    var alternateFilePath = dbFile.FileEntity.alternateFilePath;

                    if (fileMask.Length > 0)
                    {
                        var files = amazonFiles.Where(af => CompareWithMask(af.Path + af.Name, fileMask)).ToArray();
                        if (files.Length > 1 || files.Length == 0)
                            problemFiles.Add(new FixAmazonFile(dbFile.entityID, dbFile.title, dbFile.FileEntity.fileID, false, dbFile.FileEntity.filePath, files));
                        else filePath = files[0].Path + files[0].Name;

                    }
                    if (alternateFileMask.Length > 0)
                    {
                        var files = amazonFiles.Where(af => CompareWithMask(System.IO.Path.Combine(af.Path, af.Name), alternateFileMask)).ToArray();
                        if (files.Length > 1 || files.Length == 0)
                            problemFiles.Add(new FixAmazonFile(dbFile.entityID, dbFile.title, dbFile.FileEntity.fileID, true, dbFile.FileEntity.alternateFilePath, files));
                        else alternateFilePath = files[0].Path + files[0].Name;
                    }

                    if (filePath != dbFile.FileEntity.filePath)
                        fixedFiles.Add(new KeyValuePair<string, string>(dbFile.FileEntity.filePath, filePath));
                    if (alternateFilePath != dbFile.FileEntity.alternateFilePath)
                        fixedFiles.Add(new KeyValuePair<string, string>(dbFile.FileEntity.alternateFilePath, alternateFilePath));

                    if (filePath != dbFile.FileEntity.filePath || alternateFilePath != dbFile.FileEntity.alternateFilePath)
                    {
                        var updated = _fileService.UpdateFilePath(dbFile.FileEntity.fileID, filePath, alternateFilePath);
                    }

                }
            }

            this.ViewData["ProblemFiles"] = problemFiles;
            this.ViewData["FixedFiles"] = fixedFiles;

            return View();

        }

        public ActionResult ChangeFilePath(long file_id, string path, bool alternate)
        {
            var updated = _fileService.UpdateFilePath(file_id, alternate ? null : path, alternate ? path : null);
            return new EmptyResult();
        }

        private static bool CompareWithMask(string file, string file_mask)
        {
            var parts = file_mask.ToLowerInvariant().Split('?');
            var startIndex = 0;
            foreach (var part in parts)
            {
                if(file.Length<(startIndex+part.Length))return false;

                if (file.Substring(startIndex, part.Length).ToLowerInvariant() == part)
                {
                    startIndex += part.Length + 1;
                }
                else if (startIndex > 0 && file.Substring(startIndex - 1, part.Length).ToLowerInvariant() == part)
                {
                    startIndex += part.Length;
                }
                else return false;
            }
            return true;
        }

    }
}
