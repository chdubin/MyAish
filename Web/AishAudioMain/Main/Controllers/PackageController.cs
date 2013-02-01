using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainEntity.Interfaces;
using Main.Common;
using MainCommon;
using Main.Common.Attributes;
using Main.Utilities;

namespace Main.Controllers
{
    [MyRequireHttpsAttribute]
    [AuthorizedOnlyPortal]
    public class PackageController : BaseController
    {
        private ICatalogService _catalogService;

        public PackageController(ICatalogService catalog_service)
        {
            _catalogService = catalog_service;
        }


        public ActionResult Index()
        {
            long currentPortalID = this.HttpContext.GetCurrentPortal().portalID;
            var model = _catalogService.GetPackages(GlobalConstant.ROOT_ENTITY_ID, currentPortalID);

            return View(model);
        }

    }
}
