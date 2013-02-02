using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainCommon;
using MainEntity.Interfaces;
using Main.Models.Shopping;
using Main.Common;
using MainEntity.Models.Class;
using Main.Models.ControllerView.Search;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Text;
using System.Net.Mail;
using MainCommon.Daemon;
using System.Web.UI;
using Main.Common.Attributes;
using Main.Utilities;

namespace Main.Controllers
{
    [MyRequireHttpsAttribute]
    [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
    [AuthorizedOnlyPortal]
	public class ShoppingController : BaseController
    {
         private IShoppingService _shoppingCartService;
         private IClassService _classService;
         private IUserService _userService;
         private IFileService _fileService;
         private ICatalogService _catalogService;

         public ShoppingController(IShoppingService shopping_cart_service, IClassService class_service, 
             IUserService user_service, IFileService file_service, ICatalogService catalog_service)
        {
            _shoppingCartService = shopping_cart_service;
            _classService = class_service;
            _userService = user_service;
            _fileService = file_service;
            _catalogService = catalog_service;
        }

        [HttpGet]
        public ActionResult Cart()
        {
            var allProducts = new List<long>(ShoppingService.GetAllProductsInCart(System.Web.HttpContext.Current));
            var unitsList = ShoppingService.GetProducts(GlobalConstant.ROOT_ENTITY_ID, ProductTypeEnum.Units);
            var balance = this.MembershipUser != null ? this.MembershipUser.balance : 0;
            var info = ShoppingService.GetShoppingTransactionInfo(allProducts.ToArray(), this.MembershipUser != null ? this.MembershipUser.UserId : Guid.Empty, false, Properties.Settings.Default.SubscriberDiscount, Properties.Settings.Default.MaxUnitsOnSubscribe);
            var model = new CartModel(info, balance,unitsList);

            ShoppingService.EmptyCart(System.Web.HttpContext.Current);
            model.ProductList1.Each(p => ShoppingService.AddItemToShoppingCart(null, p.entityID, p.ProductTypeID == (short)ProductTypeEnum.Package ? CartItemTypeEnum.Package : CartItemTypeEnum.Class, System.Web.HttpContext.Current, p.cnt));
            model.ProductList2.Each(p => ShoppingService.AddItemToShoppingCart(null, p.entityID, p.ProductTypeID == (short)ProductTypeEnum.Package ? CartItemTypeEnum.Package : CartItemTypeEnum.Class, System.Web.HttpContext.Current, p.cnt));
            if (model.ProductSubscribePlan != null)
                ShoppingService.AddItemToShoppingCart(null, model.ProductSubscribePlan.entityID, CartItemTypeEnum.Subscribe, System.Web.HttpContext.Current);
            if (model.ProductUnits != null)
                ShoppingService.AddItemToShoppingCart(null, model.ProductUnits.entityID, CartItemTypeEnum.Unit, System.Web.HttpContext.Current);
            System.Web.HttpContext.Current.Profile.Save();

            var units = unitsList.ToDictionary(k => k.entityID, v => v.title);
            units.Add(0, "Add units");
            ViewData["UnitsQuantity"] = units.OrderBy(u=>u.Key).ToArray();
            
            return View(model);            
            
        }

        public ActionResult DeleteUnitsFromCart(int units_are_needed_for_purchase = 0)
        {
            _shoppingCartService.DeleteItemFromShoppingCart(CartItemTypeEnum.Unit, System.Web.HttpContext.Current);
            System.Web.HttpContext.Current.Profile.Save();

            return RedirectToAction("Cart", "Shopping");

        }

		[OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult AddItemToShoppingCart(int item_id, CartItemTypeEnum item_type_id)
        {
            var userID = this.MembershipUser != null ? (Guid?)this.MembershipUser.UserId : null;
            var added = _shoppingCartService.AddItemToShoppingCart(userID, item_id, item_type_id, System.Web.HttpContext.Current);
            System.Web.HttpContext.Current.Profile.Save();

            return added > 0 ? (ActionResult)View("~/Views/Shopping/HeaderCartSection.ascx") : new EmptyResult();
        }

        [HttpGet]
		public ActionResult DeleteItemFromCart(int item_id, CartItemTypeEnum item_type_id)
        {
            _shoppingCartService.DeleteItemFromShoppingCart(item_type_id, System.Web.HttpContext.Current, item_id);
            System.Web.HttpContext.Current.Profile.Save();

            return RedirectToAction("Cart", "Shopping");
        }

        [HttpGet]
		public ActionResult EmptyCart()
        {
            _shoppingCartService.EmptyCart(System.Web.HttpContext.Current);

            return RedirectToAction("Cart", "Shopping");
        }

        [HttpPost]
		public ActionResult UpdateQuantity(int[] quantity, int[] id, int units_quantity = 0)
        {
            if(units_quantity > 0)
                _shoppingCartService.AddItemToShoppingCart(null, units_quantity, CartItemTypeEnum.Unit, System.Web.HttpContext.Current);

            if(quantity !=null)
                for(var i =0;i<quantity.Length;i++)
                    _shoppingCartService.UpdateClassproductQuantity(quantity[i], id[i], System.Web.HttpContext.Current);

            System.Web.HttpContext.Current.Profile.Save();
            return RedirectToAction("Cart", "Shopping");
        }

        public ActionResult OrderUnits(long unitsID = 0) 
        {
            var userID = this.MembershipUser != null ? (Guid?)this.MembershipUser.UserId : null;

            _shoppingCartService.AddItemToShoppingCart(null, unitsID, CartItemTypeEnum.Unit, System.Web.HttpContext.Current);
            System.Web.HttpContext.Current.Profile.Save();

            return RedirectToAction("Cart");
        }

		public ActionResult AddPackage(long package_id)
        {
            _shoppingCartService.AddItemToShoppingCart(null, package_id, CartItemTypeEnum.Package, System.Web.HttpContext.Current);
            System.Web.HttpContext.Current.Profile.Save();

            return RedirectToAction("CheckoutLogin");
        }

		public ActionResult CheckoutLogin()
        {
            return View();
        }

		public ActionResult FreeOffer() 
        {
            long currentPortalID = this.HttpContext.GetCurrentPortal().portalID;

            string userName = this.User.Identity.Name;
            var provider = this.HttpContext.GetCurrentMembershipProvider();
			int freeOfreeCnt = 2;
            var user = provider.GetUser(userName, false);
			if (user != null)
				freeOfreeCnt = _shoppingCartService.GetMembership((Guid)user.ProviderUserKey).freeOfferCnt;

			if (freeOfreeCnt > 0)
            {
                ClassEntity[] classes = _classService.GetFreeOfferClasses(GlobalConstant.ROOT_ENTITY_ID, currentPortalID);
				var shoppedItemsIDs = user != null ? _shoppingCartService.SelectLibraryItemsIDs((Guid)user.ProviderUserKey) : new long[] { };

                List<ClassListItem> model = ClassListItem.GetForList(classes.Where(c => c.File!=null && !shoppedItemsIDs.Contains(c.File.entityID)).ToArray(), new long[0]);

				this.ViewData["FreeOfferCnt"] = freeOfreeCnt;
				this.ViewData["IsAuthorized"] = user != null;
                return View(model.ToArray());
            }
            else
            {
                return View();
            }
        }

		[HttpPost]
		public ActionResult FreeOffer(long[] free_file_ids, FreeOfferUnauthorizeModel model)
		{
			ViewData["free_file_ids"] = free_file_ids;
			var currentPortalID = this.HttpContext.GetCurrentPortal().portalID;
			string userName = this.User.Identity.Name;
			var provider = this.HttpContext.GetCurrentMembershipProvider();
			var user = provider.GetUser(userName, false);
            var promo = _catalogService.GetEntities(GlobalConstant.ROOT_ENTITY_ID, EntityItemTypeEnum.PromoItem, true, true)
                .Where(p => string.Compare(p.title, model.PromoCode, true) == 0).FirstOrDefault();
            var maxFreeCnt = promo != null ? promo.sortOrder : 2;

			if (free_file_ids == null || free_file_ids.Length == 0)
				ModelState.AddModelError("free_file_ids", "No box was checked.");
            else if (free_file_ids.Length > maxFreeCnt)
                ModelState.AddModelError("free_file_ids", string.Format("You are only allowed to check {0}.", maxFreeCnt));
			if (user != null)
			{
                /********  this modelstate check causes Email Required error *******/
                /******** Commented out in a hurry ******************/

                //if (ModelState.IsValid) 
                //{
					if (_shoppingCartService.BuyFreeOffers(free_file_ids, (Guid)user.ProviderUserKey) == 0)
					{
						this.TempData["TooMuchOffersSelected"] = true;

						return RedirectToAction("FreeOffer");
					}
					else
						return RedirectToAction("MyLibrary", "Account");
                //}
			}
			else
			{
				if (ModelState.IsValid)
				{
					string name = provider.GetUserNameByEmail(model.Email);
					if (string.IsNullOrEmpty(name))
					{
						ActivityLogService.LoggingUserEmail(ActivityLogTypeEnum.SpecialOfferRegisterEmail, this.Request.UserHostAddress, model.Email, null);

                        return RedirectToAction("ChooseFreeFiles", new { email = model.Email, free_file_ids = string.Join(",",free_file_ids.Select(id=>id.ToString()).ToArray()), promo_code = model.PromoCode });

					}
					else
						ModelState.AddModelError("Email", "You are already an Aishaudio.com Member. To choose your 2 free downloads, please login and click the 'My Account' link at the top of any page.");

				}
			}

			return FreeOffer();
		}

		[ChildActionOnly]
		public ActionResult FreeOfferUnauthorize()
		{
			var model = new FreeOfferUnauthorizeModel();
			return PartialView(model);
		}

		[ChildActionOnly]
		[HttpPost]
		public ActionResult FreeOfferUnauthorize(FreeOfferUnauthorizeModel model)
		{
			return PartialView(model);
		}

		[HttpGet]
        public ActionResult ChooseFreeFiles(string email, string free_file_ids, string promo_code)
		{
			if (!string.IsNullOrEmpty(email))
			{
				var curProvider = this.HttpContext.GetCurrentMembershipProvider();
				string autoPassw = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);

                try
                {
                    var currentPortalID = this.HttpContext.GetCurrentPortal().portalID;
                    var promo = _catalogService.GetEntities(GlobalConstant.ROOT_ENTITY_ID, EntityItemTypeEnum.PromoItem, true, true)
                        .Where(p => string.Compare(p.title, promo_code, true) == 0).FirstOrDefault();
                    var maxFreeCnt = promo != null ? promo.sortOrder : 2;

                    var newUser = _userService.CreateUser(curProvider, email, autoPassw, false,
                        string.Empty, string.Empty, maxFreeCnt, null, null, null, null, null, null, null, false);
                    if (free_file_ids != null)
                    {
                        var freeFileIds = _classService.GetFreeOfferClasses(GlobalConstant.ROOT_ENTITY_ID, currentPortalID)
                            .Select(c=>c.File.entityID).Intersect(free_file_ids.Split(',').Select(id => long.Parse(id))).ToArray();
                        if(freeFileIds.Length > 0) _shoppingCartService.BuyFreeOffers(freeFileIds, (Guid)newUser.ProviderUserKey);
                    }

                    UTF8Encoding encoding = new UTF8Encoding();

                    var resBytes = new List<byte>().AddTextToArray(autoPassw).AddTextToArray(email);

                    string link = "http://" + HttpContext.Request.ServerVariables["HTTP_HOST"] + Url.Action("RegisterCustomAuthorize", "Account") + "?e=" + HttpServerUtility.UrlTokenEncode(resBytes.ToArray());
                    Mailer.AddMessage(Mailer.OnRegister2FreeDownloadsUser(Resources.MailMessage.FromMailAddress, email, autoPassw, link),
                        Properties.Settings.Default.AdminEmailSubscribeNotification);
                }
                catch (Exception ex)
                {
                }

			}

			return this.View((object)email);
		}

        [RequiresAuthorize]
        [HttpGet]
        public ActionResult CancelSubscribe()
        {
            string userName = this.User.Identity.Name;
            var provider = this.HttpContext.GetCurrentMembershipProvider();
            var user = provider.GetUser(userName, false);

            this.TempData["NoSubscribe"] = _shoppingCartService.IsSubscribeCancel((Guid)user.ProviderUserKey);

            return View();
        }

        [RequiresAuthorize]
        [HttpPost]
        public ActionResult DoCancelSubscribe() 
        {
            string userName = this.User.Identity.Name;
            var provider = this.HttpContext.GetCurrentMembershipProvider();
            var user = provider.GetUser(userName, false);

			UserService.CancelSubscribe((Guid)user.ProviderUserKey);
            Mailer.AddMessage(Mailer.OnMonthlyMembershipCancellation(
                    Properties.Settings.Default.FromEmailNotification, this.AspnetpUser.Email, this.MembershipUser.firstName + " " + this.MembershipUser.lastName, this.AspnetpUser.UserName),
                Properties.Settings.Default.AdminEmailSubscribeNotification);

            this.TempData["NoSubscribe"] = true;

            return RedirectToAction("CancelSubscribe");
        }

        #region Static

        public ActionResult FreeMP3() 
        {

            return View();
        }

        public ActionResult IPodOffer()
        {
            return View();
        }
        #endregion
    }
}
