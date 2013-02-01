using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainEntity.Interfaces;
using Main.Common.Attributes;
using Main.Common;
using MainEntity.Models.Catalog;
using MainCommon;
using Main.Areas.Admin.Models.ControllerView;
using Main.Areas.Admin.Models.Catalog;
using System.IO;
using System.Configuration;
using Main.Areas.Admin.Models.ControllerView.Catalog;
using Main.Areas.Admin.Models.Common;
using System.Web.Routing;
using MainCommon.Models;
using Main.Utilities;

namespace Main.Areas.Admin.Controllers
{
    [MyRequireHttps]
    public partial class CatalogController : Controller
    {
        private ICatalogService _classService;
        private ISpeakerService _speakerService;
        private IPortalService _portalService;
        private ITagService _tagService;

        public CatalogController(ICatalogService class_service, IPortalService portal_service, ITagService tag_service, ISpeakerService speakerService)
        {
            _classService = class_service;
            _portalService = portal_service;
            _tagService = tag_service;
            _speakerService = speakerService;
        }

        //
        // GET: /Admin/Catalog/

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult Index(int p = 1, int ps = 100, int sort = (int)SortParametersEnum.Date, int desc = 1, int category_id = 0,
            string stitle=null, int[] scategory=null, string sspeaker=null, string scode=null, long sportal = 0)
        {
            string userName = this.User.Identity.Name;
            bool isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);
            this.ViewData["isSuperUser"] = isSuperUser;

            sportal = isSuperUser ? sportal : this.HttpContext.GetCurrentPortal().portalID;
            scategory = scategory == null && category_id != 0 ? new int[] { category_id } : scategory;
			var filterNew = scategory != null && scategory.Contains(-1);
			if (filterNew) scategory = scategory.Except(new int[] { -1 }).ToArray();


            int recordsCount = _classService.GetCatalogItemsCnt(GlobalConstant.ROOT_ENTITY_ID, false, true,
				sportal, scategory, stitle, sspeaker, scode, filterNew);
            var catalogItems = _classService.GetCatalogItems(GlobalConstant.ROOT_ENTITY_ID, false, true, (p - 1) * ps, ps,
                (SortParametersEnum)sort, desc == 1, this.HttpContext.GetCurrentPortal().portalID,
				sportal, scategory, stitle, sspeaker, scode, filterNew);

            var model = CatalogItem.GetForList(sportal, catalogItems);

            this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, 0, 100);

            #region Sorting

            List<KeyValuePair<string, string>> qs = MyUtils.ExcludeQueryParam(Request.QueryString, new string[] { "sort", "desc" });

            this.ViewData["SortTitleUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.Title;
            this.ViewData["SortDateUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.Date;
            this.ViewData["SortActiveUrl"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs) + "sort=" + (int)SortParametersEnum.Active;
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
                case SortParametersEnum.FreeOffer:
                    this.ViewData["SortFreeOfferHeaderClass"] = desc == 1 ? "headerSortUp" : "headerSortDown";
                    this.ViewData["SortFreeOfferUrl"] += (desc == 0 ? "&desc=1" : string.Empty);
                    break;
            }

            this.ViewData["SortTitleUrl"] = ((string)this.ViewData["SortTitleUrl"]).TrimEnd(new char[] { '?', '&' });
            this.ViewData["SortDateUrl"] = ((string)this.ViewData["SortDateUrl"]).TrimEnd(new char[] { '?', '&' });
            this.ViewData["SortActiveUrl"] = ((string)this.ViewData["SortActiveUrl"]).TrimEnd(new char[] { '?', '&' });
            this.ViewData["SortVisibleUrl"] = ((string)this.ViewData["SortVisibleUrl"]).TrimEnd(new char[] { '?', '&' });
            this.ViewData["SortFreeUrl"] = ((string)this.ViewData["SortFreeUrl"]).TrimEnd(new char[] { '?', '&' });
            this.ViewData["SortFreeOfferUrl"] = ((string)this.ViewData["SortFreeOfferUrl"]).TrimEnd(new char[] { '?', '&' });

            #endregion

            #region Filtering

            List<KeyValuePair<string, string>> qs_2 = MyUtils.ExcludeQueryParam(Request.QueryString, new string[] { "category_id", "p", "ps" });
            this.ViewData["ClearFilterPath"] = Request.Path + "?" + MyUtils.GetKeyValuePairString(qs_2);
            this.ViewData["Categories"] = _classService.GetAllCategories();
            this.ViewData["CurrentCategoryID"] = category_id;

            #endregion

            return View(model);
        }

        
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult Details(int catalog_item_id)
        {
            return View();
        }

        #region Class

        //
        // GET: /Admin/Catalog/Create
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult CreateClass()
        {
            long currentPortalID = this.HttpContext.GetCurrentPortal().portalID;
            string userName = this.User.Identity.Name;
			var portals = _portalService.GetPortals(false, true, false).ToArray();
			var model = Main.Areas.Admin.Models.ControllerView.Catalog.CreateClass.GetForCreate(portals, currentPortalID);

            this.ViewData["isSuperUser"] = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);
			this.ViewData["currentPortalID"] = currentPortalID;

            var speakers = _classService.GetSpeakers(GlobalConstant.ROOT_ENTITY_ID);
			var shippingLoactions = _classService.GetAllShippingLocations();
			var levels = _classService.GetAllTags(TagTypeEnum.ClassLevel);
			model.InitializeLists(speakers, shippingLoactions, levels);

            return View(model);
        }

        //
        [HttpGet]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public JsonResult GetCategories(string q)
        {
            var data = this.HttpContext.Cache.GetValue(MainCommon.CacheEnum.CategoryCache.ToString(),
                TimeSpan.FromSeconds(GlobalConstant.CATEGORY_IN_CACHE_SEC),()=> _tagService.GetCategories());
			q = q.ToLower();

            var result = data.Where(t => t.name.ToLower().Contains(q)).Select(x => new { id = x.tagID, name = x.name });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult CreateClass(Main.Areas.Admin.Models.ControllerView.Catalog.CreateClass model)
        {
            var portalID = this.HttpContext.GetCurrentPortal().portalID;
            var userName = this.User.Identity.Name;
            var isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);

            try
            {
                if (this.ModelState.IsValid)
                {
                    var user = this.HttpContext.GetCurrentMembershipProvider().GetUser(userName, true);
                    var products = new List<ProductEntity>();
                    var files = new List<FileEntity>();

                    CatalogItemXrefPortal[] xrefPortals;

                    if (isSuperUser)
                    {
                        xrefPortals = model.InPortals.Where(p => p.Selected).
							Select(p => new CatalogItemXrefPortal()
							{
								portalID = p.PortalID,
								isVisible = p.IsVisible,
								isFree = p.IsFree,
								isFreeOffer = p.IsFreeOffer,
								isFullFree = p.IsFullFree
							}).ToArray();
                    }
                    else
                    {
                        xrefPortals = new CatalogItemXrefPortal[1] 
                        {
                            new CatalogItemXrefPortal()
                            {
                                portalID = portalID,
                                isVisible = model.IsVisible,
                                isFree = model.IsFree,
                                isFreeOffer = model.IsFreeOffer,
								isFullFree = model.IsFullFree
                            }
                        };
                    }

                    TimeSpan duration = new TimeSpan(model.Hour, model.Min, 0);

                    if (model.TapeAvailable)
                        products.Add(new ProductEntity() { productTypeID = (short)ProductTypeEnum.Tape, price1 = model.TapePriceUSD, ShippingLocationID = model.ShippingLocationID });
                    if (model.DiskAvailable)
                        products.Add(new ProductEntity() { productTypeID = (short)ProductTypeEnum.Disk, price1 = model.DiskPriceUSD, ShippingLocationID = model.ShippingLocationID });
                    if (model.DownloadAvailable && !string.IsNullOrEmpty(model.AmazonFilePath))
						products.Add(new ProductEntity()
						{
							productTypeID = (short)ProductTypeEnum.File,
							price1 = model.DownloadPriceUSD,
							price2 = model.DownloadPriceUnit,
							File = new FileEntity() { filePath = model.AmazonFilePath, alternateFilePath = model.AmazonFilePath2, fileTypeID = (int)FileTypeIDEnum.S3File }
						});

                    //throw new Exception("makar exception");

                    ImageInfo image = GetImage(model.Image, model.ImageType, model.ImageUrl);

					_classService.InsertClass(model.Title, model.Description, (Guid)user.ProviderUserKey, 
						isSuperUser ? (bool?)model.Active : null, GlobalConstant.ROOT_ENTITY_ID, model.SpeakerID, model.NewOrder,
						model.LevelTagID,
						xrefPortals.ToArray(), products.ToArray(), image,
						model.Categories, duration, model.Code, model.Notes);

                    this.TempData["ViewMessage"] = ViewMessageEnum.CreateSuccess;
                    return RedirectToAction("CreateClass");
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Unhandled Error is: " + ex.Message);
            }

			var speakers = _classService.GetSpeakers(GlobalConstant.ROOT_ENTITY_ID);
			var shippingLoactions = _classService.GetAllShippingLocations();
			var levels = _classService.GetAllTags(TagTypeEnum.ClassLevel);
			model.InitializeLists(speakers, shippingLoactions, levels);

            this.ViewData["currentPortalID"] = portalID;
            this.ViewData["isSuperUser"] = isSuperUser;

            return View(model);
        }

        private ImageInfo GetImage(HttpPostedFileBase image, int image_type, string image_url)
        {
            ImageInfo rval = null;
            if (image_type == 0 && image != null)
            {
                rval = new ImageInfo(image.InputStream, image.ContentLength, Path.GetExtension(image.FileName), ConfigurationManager.AppSettings["UploadClassImgFolderName"]);
            }
            else if (image_type == 1 && !string.IsNullOrEmpty(image_url))
            {
                Uri imageUrl = null;
                if (Uri.TryCreate(image_url, UriKind.RelativeOrAbsolute, out imageUrl))
                    rval = new ImageInfo(imageUrl);
            }
            return rval;
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult EditClass(int catalog_item_id)
        {
            long portalID = this.HttpContext.GetCurrentPortal().portalID;
            string userName = this.User.Identity.Name;
            bool isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);
            this.ViewData["isSuperUser"] = isSuperUser;

            Main.Areas.Admin.Models.ControllerView.Catalog.CreateClass model = null;

            var catalogItem = _classService.GetCatalogItem(catalog_item_id, true);
            if (isSuperUser || catalogItem.CatalogItemXrefPortals.Where(c => c.portalID == portalID).Count() > 0)
            {
                CatalogItemXPortal[] portals = CatalogItemXPortal.GetPortalsForCatalogItemEdit(_portalService.GetPortals(false, true, false),
                        catalogItem.CatalogItemXrefPortals.ToArray());

                model = new Main.Areas.Admin.Models.ControllerView.Catalog.CreateClass()
                {
                    Active = catalogItem.active,
                    ClassID = catalogItem.entityID,
                    CreateDate = catalogItem.createDate,
                    SpeakerID = catalogItem.ClassEntity.speakerID,
                    SpeakerName = catalogItem.ClassEntity.SpeakerName,
					NewOrder = catalogItem.ClassEntity.newOrder,
                    Title = catalogItem.title,
                    Description = catalogItem.ClassEntity.description,
                    InPortals = portals,
                    Hour = catalogItem.ClassEntity.duration != null ? catalogItem.ClassEntity.duration.Value.Hours : 0,
                    Min = catalogItem.ClassEntity.duration != null ? catalogItem.ClassEntity.duration.Value.Minutes : 0,
                    Code = catalogItem.CatalogItemExtend.code,
                    Notes = catalogItem.CatalogItemExtend.notes,
					LevelTagID = catalogItem.TagXrefEntities.Where(t => t.Tag.tagTypeID == (short)TagTypeEnum.ClassLevel).Select(t=>t.Tag.tagID).FirstOrDefault(),
					ShippingLocationID = catalogItem.ChildEnities.Where(c=>c.typeID == (int)EntityItemTypeEnum.ProductItem && c.ProductEntity.ShippingLocationID!=0).Select(c=>c.ProductEntity.ShippingLocationID).FirstOrDefault()
                };
				var speakers = _classService.GetSpeakers(GlobalConstant.ROOT_ENTITY_ID);
				var shippingLoactions = _classService.GetAllShippingLocations();
				var levels = _classService.GetAllTags(TagTypeEnum.ClassLevel);
				model.InitializeLists(speakers, shippingLoactions, levels);


                if (!isSuperUser)
                {
                    portals = portals.Where(p => p.PortalID == portalID).ToArray();

                    model.IsVisible = portals[0].IsVisible;
                    model.IsFree = portals[0].IsFree;
                    model.IsFreeOffer = portals[0].IsFreeOffer;
					model.IsFullFree = portals[0].IsFreeOffer;
                }

                var products = _classService.GetClassProducts(catalogItem.entityID);
                var typeProduct = products.Where(p => p.ProductEntity.productTypeID == (short)ProductTypeEnum.Tape).FirstOrDefault();
                var discProduct = products.Where(p => p.ProductEntity.productTypeID == (short)ProductTypeEnum.Disk).FirstOrDefault();
                var downloadProduct = products.Where(p => p.ProductEntity.productTypeID == (short)ProductTypeEnum.File).FirstOrDefault();

                model.TapeAvailable = typeProduct != null;
                model.TapePriceUSD = typeProduct != null ? typeProduct.ProductEntity.price1 ?? 0 : 0;

                model.DiskAvailable = discProduct != null;
                model.DiskPriceUSD = discProduct != null ? discProduct.ProductEntity.price1 ?? 0 : 0;

				model.DownloadAvailable = downloadProduct != null;
				if (model.DownloadAvailable)
				{
					model.DownloadPriceUSD = downloadProduct.ProductEntity.price1 ?? 0;
					model.DownloadPriceUnit = downloadProduct.ProductEntity.price2 ?? 0;
					model.AmazonFilePath = downloadProduct.ProductEntity.File != null ? downloadProduct.ProductEntity.File.filePath : null;
					model.AmazonFilePath2 = downloadProduct.ProductEntity.File != null ? downloadProduct.ProductEntity.File.alternateFilePath : null;
                    model.DownloadFileID = downloadProduct.ProductEntity.File != null ? downloadProduct.ProductEntity.File.fileID : 0;
				}

				model.Categories = string.Join("|", catalogItem.TagXrefEntities.Where(t => t.Tag.tagTypeID == (short)TagTypeEnum.Category).Select(t => t.Tag.name).ToArray());

                model.ImageUrl = MyUtils.GetImageUrl(ConfigurationManager.AppSettings["UploadClassImgFolderName"], _classService.GetClassImage(catalogItem.entityID));
            }

            this.ViewData["currentPortalID"] = isSuperUser ? 0 : portalID;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult EditClass(Main.Areas.Admin.Models.ControllerView.Catalog.CreateClass model)
        {
            string userName = this.User.Identity.Name;
            bool isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);
            long portalID = this.HttpContext.GetCurrentPortal().portalID;
            try
            {
                if (this.ModelState.IsValid)
                {
                    var user = this.HttpContext.GetCurrentMembershipProvider().GetUser(userName, true);
                    var products = new List<ProductEntity>();

                    CatalogItemXrefPortal[] xrefPortals;

                    if (isSuperUser)
                    {
						xrefPortals = model.InPortals.Where(p => p.Selected).
							Select(p => new CatalogItemXrefPortal()
							{
								portalID = p.PortalID,
								isFree = p.IsFree,
								isFreeOffer = p.IsFreeOffer,
								isVisible = p.IsVisible,
								isFullFree = p.IsFullFree
							}).ToArray();
                    }
                    else
                    {
                        xrefPortals = new CatalogItemXrefPortal []
                        {
                            new CatalogItemXrefPortal()
                            {
                                portalID = portalID,
                                isVisible = model.IsVisible,
                                isFree = model.IsFree,
                                isFreeOffer = model.IsFreeOffer,
								isFullFree = model.IsFullFree
                            }
                        };
                    }

                    if (model.TapeAvailable)
                        products.Add(new ProductEntity() { productTypeID = (short)ProductTypeEnum.Tape, price1 = model.TapePriceUSD, ShippingLocationID = model.ShippingLocationID });
                    if (model.DiskAvailable)
						products.Add(new ProductEntity() { productTypeID = (short)ProductTypeEnum.Disk, price1 = model.DiskPriceUSD, ShippingLocationID = model.ShippingLocationID });
                    if (model.DownloadAvailable)
                    {
						products.Add(new ProductEntity()
						{
							productTypeID = (short)ProductTypeEnum.File,
							price1 = model.DownloadPriceUSD,
							price2 = model.DownloadPriceUnit,
							File = new FileEntity() { filePath = model.AmazonFilePath, alternateFilePath=model.AmazonFilePath2, fileTypeID = (int)FileTypeIDEnum.S3File }
						});
                    }

                    ImageInfo image = GetImage(model.Image, model.ImageType, model.ImageUrl);

                    _classService.UpdateClass(model.ClassID, model.Title, model.Description, (Guid)user.ProviderUserKey,
						isSuperUser ? (bool?)model.Active : null, GlobalConstant.ROOT_ENTITY_ID, model.SpeakerID, model.NewOrder,
                        model.LevelTagID, xrefPortals.ToArray(), products.ToArray(), image, model.Categories, new TimeSpan(model.Hour, model.Min, 0), model.Code, model.Notes);

                    this.TempData["ViewMessage"] = ViewMessageEnum.UpdateSuccess;
                    return RedirectToAction("EditClass", new { catalog_item_id = model.ClassID });
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Unhandled Error is: " + ex.Message);
            }

			var speakers = _classService.GetSpeakers(GlobalConstant.ROOT_ENTITY_ID);
			var levels = _classService.GetAllTags(TagTypeEnum.ClassLevel);
			var shippingLoactions = _classService.GetAllShippingLocations();
			model.InitializeLists(speakers, shippingLoactions, levels);

            this.ViewData["currentPortalID"] = isSuperUser ? 0 : portalID;

            return View(model);
        }

        //
        // GET: /Admin/Catalog/Delete/5
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult DeleteClass(int catalog_item_id)
        {
            _classService.DeleteClass(catalog_item_id);

            return RedirectToAction("Index");
        }

        #endregion

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult ChangeCategories(long catalog_item_id)
        {
            var catalogItem = _classService.GetCatalogItem(catalog_item_id, true);
            var model = catalogItem.TagXrefEntities.Where(t => t.Tag.tagTypeID == (short)TagTypeEnum.Category).Select(t => t.Tag.name).ToArray();
            ViewData["CatalogItemID"] = catalog_item_id;
            return PartialView(model);
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult ShowCategories(long catalog_item_id)
        {
            var catalogItem = _classService.GetCatalogItem(catalog_item_id, true);
            var model = catalogItem.TagXrefEntities.Where(t => t.Tag.tagTypeID == (short)TagTypeEnum.Category).Select(t => t.Tag.name).ToArray();
            ViewData["CatalogItemID"] = catalog_item_id;
            return PartialView("ShowCategories",model);
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult UpdateCategories(long catalog_item_id, string categories)
        {

            _classService.InsertCategories(categories, catalog_item_id);
            return ShowCategories(catalog_item_id);
        }
        


        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult GetImages(int page_num = 1, int page_size = 15, string title = "", string speaker = "")
        {
            throw new NotImplementedException();
        }


        #region Package

        //
        // GET: /Admin/Catalog/CreatePackage
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult CreatePackage()
        {
            var userName = this.User.Identity.Name;
            var currentPortalID = this.HttpContext.GetCurrentPortal().portalID;
            this.ViewData["currentPortalID"] = currentPortalID;
            this.ViewData["isPackage"] = true;
            this.ViewData["isSuperUser"] = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);
            var portals = _portalService.GetPortals(false, true, false).ToArray();
            var speakers = _classService.GetSpeakers(GlobalConstant.ROOT_ENTITY_ID);
			var model = new EditPackage();
			var shippingLoactions = _classService.GetAllShippingLocations();
            model.InitializeLists(speakers, shippingLoactions, portals, currentPortalID);


            return View(model);
        }

        //
        // POST: /Admin/Catalog/CreatePackage

        [HttpPost]
        [ValidateInput(false)]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult CreatePackage(Main.Areas.Admin.Models.ControllerView.Catalog.EditPackage model)
        {
            var userName = this.User.Identity.Name;
            var currentPortalID = this.HttpContext.GetCurrentPortal().portalID;
            var isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);
            this.ViewData["currentPortalID"] = currentPortalID;
            this.ViewData["isPackage"] = true;
            this.ViewData["isSuperUser"] = isSuperUser;

            if (this.ModelState.IsValid)
            {
                try
                {
                    long[] idsToAdd = new long[0];
                    long[] idsToDelete = new long[0];
                    if (!string.IsNullOrEmpty(model.ProductsToAdd))
                    {
                        idsToAdd = model.ProductsToAdd.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(p => Convert.ToInt64(p.Trim())).ToArray();
                    }

                    if (!string.IsNullOrEmpty(model.ProductsToDelete))
                    {
                        idsToDelete = model.ProductsToDelete.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(p => Convert.ToInt64(p.Trim())).ToArray();
                    }


                    var attachedProductsIds = idsToAdd.Except(idsToDelete).Distinct().ToArray();
                    var user = this.HttpContext.GetCurrentMembershipProvider().GetUser(userName, true);

                    CatalogItemXrefPortal[] xrefPortals;

                    if (isSuperUser)
                        xrefPortals = model.InPortals.Where(p => p.Selected).Select(p => new CatalogItemXrefPortal() { portalID = p.PortalID, isVisible = p.IsVisible }).ToArray();
                    else
                        xrefPortals = new [] {new CatalogItemXrefPortal(){portalID = currentPortalID,isVisible = model.IsVisible}};

                    ImageInfo image = GetImage(model.Image, 0, null);

                    _classService.InsertPackage(model.Title,
                        isSuperUser ? (bool?)model.Active : null, (Guid)user.ProviderUserKey, GlobalConstant.ROOT_ENTITY_ID,
                        model.PriceUSD, model.Description, xrefPortals.ToArray(), model.ShippingLocationID,
                        attachedProductsIds, model.SubscribePlanMonths, long.Parse(ConfigurationManager.AppSettings["MonthlyMembershipSubscribeID"]), model.FreeUnitsOnSubscribe, model.FreeUnitsOnNextSubscribe,
                        image, model.Categories, model.Code, model.unlimitedAccessInLibrary, model.SpeakerID);

                    this.TempData["ViewMessage"] = ViewMessageEnum.CreateSuccess;
                    return RedirectToAction("CreatePackage");

                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("", "Unhandled Error is: " + ex.Message);
                }
            }

            this.ViewData["currentPortalID"] = currentPortalID;

			var shippingLoactions = _classService.GetAllShippingLocations();
            var speakers = _classService.GetSpeakers(GlobalConstant.ROOT_ENTITY_ID);
            model.InitializeLists(speakers, shippingLoactions);

            return View(model);
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult EditPackage(int catalog_item_id)
        {
            var userName = this.User.Identity.Name;
            var currentPortalID = this.HttpContext.GetCurrentPortal().portalID;
            var isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);
            this.ViewData["currentPortalID"] = currentPortalID;
            this.ViewData["isPackage"] = true;
            this.ViewData["isSuperUser"] = isSuperUser;

            Main.Areas.Admin.Models.ControllerView.Catalog.EditPackage model = null;

            var catalogItem = _classService.GetPackage(catalog_item_id);
            var catalogItem2 = _classService.GetCatalogItem(catalog_item_id, true);
            if (isSuperUser || catalogItem.CatalogItemXrefPortals.Where(c => c.portalID == currentPortalID).Count() > 0)
            {
                var portals = CatalogItemXPortal.GetPortalsForCatalogItemEdit(
                        _portalService.GetPortals(false, true, false),
                        catalogItem.CatalogItemXrefPortals.ToArray());

                var products = _classService.GetProductXrefEntities(catalogItem.entityID);
                string productsToAdd = string.Empty;
                foreach (var p in products)
                    productsToAdd += p.entityID.ToString() + ",";

                var subscribePlanDesc = (catalogItem.ChildEnities.Where(e => e.typeID == (short)EntityItemTypeEnum.SubscribePlanItem)
                    .Select(p => string.IsNullOrEmpty(p.SubscribePlanEntity.description) ? (p.SubscribePlanEntity.durationInMonths + "," + p.SubscribePlanEntity.freeUnitsOnSubscribe) : p.SubscribePlanEntity.description).SingleOrDefault() ?? "0").Split(',');
                short subscribePlanMonths;
                int freeUnitsOnSubscribe=0, freeUnitsOnNextSubscribe=0;

                short.TryParse(subscribePlanDesc[0],out subscribePlanMonths);
                if(subscribePlanDesc.Length>1) int.TryParse(subscribePlanDesc[1], out freeUnitsOnSubscribe);
                if(subscribePlanDesc.Length>2) int.TryParse(subscribePlanDesc[2], out freeUnitsOnNextSubscribe);

                model = new Main.Areas.Admin.Models.ControllerView.Catalog.EditPackage()
                {
                    Active = catalogItem.active,
                    unlimitedAccessInLibrary = catalogItem.ProductEntity.unlimitedAccessInLibrary,
                    PackageID = catalogItem.entityID,
                    SpeakerID = catalogItem2.ClassEntity.speakerID,
                    CreateDate = catalogItem.createDate,
                    Title = catalogItem.title,
                    Description = catalogItem.ProductEntity.description,
                    PriceUSD = (decimal)catalogItem.ProductEntity.price1,
                    Code = catalogItem.CatalogItemExtend.code,
                    Products = products,
                    ProductsToAdd = productsToAdd,
                    Categories = string.Join("|", catalogItem.TagXrefEntities.Where(t => t.Tag.tagTypeID == (short)TagTypeEnum.Category).Select(t => t.Tag.name).ToArray()),
                    ClassImagePath = MyUtils.GetImageUrl(ConfigurationManager.AppSettings["UploadClassImgFolderName"], _classService.GetClassImage(catalogItem.entityID)),

                    SubscribePlanMonths = subscribePlanMonths,
                    FreeUnitsOnSubscribe = freeUnitsOnSubscribe,
                    FreeUnitsOnNextSubscribe = freeUnitsOnNextSubscribe,
                    InPortals = portals,
					ShippingLocationID = catalogItem.ProductEntity.ShippingLocationID
                };
                var speakers = _classService.GetSpeakers(GlobalConstant.ROOT_ENTITY_ID);
				var shippingLoactions = _classService.GetAllShippingLocations();
                model.InitializeLists(speakers, shippingLoactions);

                if (!isSuperUser)
                {
                    portals = portals.Where(p => p.PortalID == currentPortalID).ToArray();

                    model.IsVisible = portals[0].IsVisible;
                }
            }

            this.ViewData["currentPortalID"] = currentPortalID;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult EditPackage(Main.Areas.Admin.Models.ControllerView.Catalog.EditPackage model)
        {
            var userName = this.User.Identity.Name;
            var currentPortalID = this.HttpContext.GetCurrentPortal().portalID;
            var isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);
            this.ViewData["currentPortalID"] = currentPortalID;
            this.ViewData["isPackage"] = true;
            this.ViewData["isSuperUser"] = isSuperUser;

            if (this.ModelState.IsValid)
            {
                try
                {
                    long[] idsToAdd = new long[0];
                    long[] idsToDelete = new long[0];
                    if (!string.IsNullOrEmpty(model.ProductsToAdd))
                    {
                        idsToAdd = model.ProductsToAdd.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(p => Convert.ToInt64(p.Trim())).ToArray();
                       
                    }

                    if (!string.IsNullOrEmpty(model.ProductsToDelete))
                    {
                        idsToDelete = model.ProductsToDelete.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(p => Convert.ToInt64(p.Trim())).ToArray();
                    }


                    var classesIds = idsToAdd.Remove(idsToDelete).Distinct().ToArray();
                    var products = _classService.GetProductEntityItems(classesIds, ProductTypeEnum.File);
                    //TimeSpan? dfdf = new TimeSpan(0,0,0);
                    //foreach (var g in products)
                    //{
                    //    if (g.ClassEntity.duration != null)
                    //    {
                    //  dfdf= dfdf + g.ClassEntity.duration;
                    //    }
                    //}
                    var user = this.HttpContext.GetCurrentMembershipProvider().GetUser(userName, true);

                    CatalogItemXrefPortal[] xrefPortals;

                    if (isSuperUser)
                    {
                        xrefPortals = model.InPortals.Where(p => p.Selected).
                            Select(p => new CatalogItemXrefPortal() { portalID = p.PortalID, isVisible = p.IsVisible }).ToArray();
                    }
                    else
                    {
                        xrefPortals = new CatalogItemXrefPortal[1] 
                        {
                            new CatalogItemXrefPortal()
                            {
                                portalID = currentPortalID,
                                isVisible = model.IsVisible
                            }
                        };
                    }
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //var productsX = new List<ProductEntity>();
                    //    productsX.Add(new ProductEntity()
                    //    {
                    //        productTypeID = (short)ProductTypeEnum.File,
                    //        price1 = model.PriceUSD,
                    //        price2 = null,//model.DownloadPriceUnit,
                    //        File = null//new FileEntity() { filePath = model.AmazonFilePath, alternateFilePath = model.AmazonFilePath2, fileTypeID = (int)FileTypeIDEnum.S3File }
                    //    });
                    

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ImageInfo image = GetImage(model.Image, 0, null);

                    _classService.UpdatePackage(model.PackageID, model.Title, model.PriceUSD, model.Description, (Guid)user.ProviderUserKey,
                        isSuperUser ? (bool?)model.Active : null,
                        GlobalConstant.ROOT_ENTITY_ID, xrefPortals.ToArray(), model.ShippingLocationID,
                        classesIds, model.SubscribePlanMonths, long.Parse(ConfigurationManager.AppSettings["MonthlyMembershipSubscribeID"]), model.FreeUnitsOnSubscribe, model.FreeUnitsOnNextSubscribe,
                        image, model.Categories, model.Code, model.unlimitedAccessInLibrary, model.SpeakerID);

                    this.TempData["ViewMessage"] = ViewMessageEnum.UpdateSuccess;
                    return RedirectToAction("EditPackage", new { catalog_item_id = model.PackageID });

                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("", "Unhandled Error is: " + ex.Message);
                }
            }

			var shippingLoactions = _classService.GetAllShippingLocations();
            var speakers = _classService.GetSpeakers(GlobalConstant.ROOT_ENTITY_ID);
            model.InitializeLists(speakers, shippingLoactions);


            return View(model);
        }

        [HttpGet]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult DeletePackage(long catalog_item_id)
        {
            try
            {
                _classService.DeletePackage(catalog_item_id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Unhandled Error is: " + ex.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult AddProductsToPackage(long random, int page_num = 1, int page_size = 15, string title = "", string speaker = "", string products_to_add = "", string products_to_delete = "")
        {
            int startIndex = (page_num - 1) * page_size;
            ClassEntity[] classes = _classService.GetClassItems(startIndex, page_size, ProductTypeEnum.File, title, speaker);
            int totalRowsCount = _classService.GetClassItemsCount(ProductTypeEnum.File, title, speaker);
            this.ViewData["Pager"] = new DefaultPagingData(page_num, totalRowsCount, page_size, "AddProductsToPackage", "addProductsToPackage", Request.QueryString);

            var idsToAdd = products_to_add.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(p => Convert.ToInt64(p.Trim())).ToArray();

            var idsToDelete = products_to_delete.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(p => Convert.ToInt64(p.Trim())).ToArray();

            var selectedProductsIds = idsToAdd.Remove(idsToDelete).Distinct().ToArray();

            return View(new Main.Areas.Admin.Models.ControllerView.Catalog.AddProductsToPackage(classes, title, speaker, selectedProductsIds));
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult ProductsInPackage(string products_to_add, string products_to_delete)
        {
            var idsToAdd = products_to_add.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(p => Convert.ToInt64(p.Trim())).ToArray();

            var idsToDelete = products_to_delete.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(p => Convert.ToInt64(p.Trim())).ToArray();

			var productsIds = idsToAdd.Remove(idsToDelete).Distinct().ToArray();

            var products = _classService.GetProductEntityItems(productsIds, ProductTypeEnum.File);

            return View(new Main.Areas.Admin.Models.ControllerView.Catalog.ProductsInPackage(products));
        }

        
        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public JsonResult CreateSpeaker(string name)
        {
            name = name.Trim();
            if (string.IsNullOrEmpty(name))
                return null;

            long entityID = 0;
           // MainEntity.Models.Speaker.EntityItem s = _speakerService.GetSpeaker(name);
            var s = _speakerService.GetSpeaker(name);
            if (s == null)
            {
                string userName = this.User.Identity.Name;
                var user = this.HttpContext.GetCurrentMembershipProvider().GetUser(userName, true);
                entityID = _speakerService.InsertSpeaker((Guid)user.ProviderUserKey, GlobalConstant.ROOT_ENTITY_ID, name, string.Empty, string.Empty);
            }
            else
            {
                entityID = s.entityID;
            }

            return new JsonResult { Data = new { name = name, id = entityID } };
          
        }
        #endregion

        #region Catalog

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult SaveCatalogItems(CatalogItem[] CatalogItems)
        {
            try
            {
                long portalID = this.HttpContext.GetCurrentPortal().portalID;
                var userName = this.User.Identity.Name;
                var isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);

                EntityItem[] catalogItems = CatalogItems.Select(i => new EntityItem
                {
                    entityID = i.CatalogItemID,
                    active = i.Active,
                    CurrentPortal = new CatalogItemXrefPortal
                    {
                        portalID = portalID,
                        isVisible = i.Visible ?? false,
                        isFree = i.IsFree ?? false,
                        isFreeOffer = i.IsFreeOffer ?? false
                    },
                    CatalogItemExtend = null
                    //CatalogItemExtend = new CatalogItemExtend
                    //{
                    //    notes = i.Notes
                    //}
                }).ToArray();

                _classService.UpdateCatalogItems(catalogItems, portalID, isSuperUser);
            }
            catch (Exception ex) 
            {
                this.ModelState.AddModelError("", "Unhandled Error is: " + ex.Message);
            }

            this.TempData["ViewMessage"] = ViewMessageEnum.UpdateSuccess;

            RouteValueDictionary rv = new RouteValueDictionary();

            foreach (string key in Request.QueryString.AllKeys)
                rv.Add(key, Request.QueryString[key]);

            return RedirectToAction("Index", rv);
        }

        #endregion

        #region Categories

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult Categories()
        {
            vw_Category[] model = null;
            try
            {
                model = _classService.GetAllCategories();
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Unhandled Error is: " + ex.Message);
            }

            return View(model);
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult CreateCategory(string category_name)
        {
            try
            {
                _classService.InsertTag(category_name, TagTypeEnum.Category);

                this.TempData["ViewMessage"] = ViewMessageEnum.CreateSuccess;
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Unhandled Error is: " + ex.Message);
            }

            return RedirectToAction("Categories");
        }

        [HttpGet]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult DeleteCategory(int category_id)
        {
            try
            {
                _classService.DeleteTag(category_id);

                this.TempData["ViewMessage"] = ViewMessageEnum.DeleteSuccess;
            }
            catch (BLToolkit.Data.DataException)
            {
                this.TempData["ViewMessage"] = ViewMessageEnum.DeleteError;
            }
            catch (Exception ex)
            {
                this.TempData["ViewErrorType"] = ex.GetType().ToString();
                this.TempData["ViewErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Categories");
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult UpdateCategory(int category_id, string category_name)
        {
            _classService.UpdateTag(category_id, category_name);

            this.TempData["ViewMessage"] = ViewMessageEnum.UpdateSuccess;

            return RedirectToAction("Categories");
        }

        #endregion

        
    }
}
