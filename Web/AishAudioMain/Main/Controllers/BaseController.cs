using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainEntity.Interfaces;
using Main.Common;
using System.Web.Security;
using MainCommon;

namespace Main.Controllers
{
    public class BaseController : Controller
    {
        protected IUserService UserService { get; private set; }
        protected IShoppingService ShoppingService { get; private set; }
        protected IClassService ClassService { get; private set; }
        protected ISpeakerService SpeakerService { get; private set; }
		protected IActivityLogService ActivityLogService{get;private set;}


        private bool _membershipUserInitialized;
        private MainEntity.Models.User.Membership _membershipUser;
        protected MainEntity.Models.User.Membership MembershipUser
        {
            get
            {
                if (!_membershipUserInitialized && _membershipUser == null && AspnetpUser != null)
                    _membershipUser = UserService.GetUser((Guid)AspnetpUser.ProviderUserKey);
                _membershipUserInitialized = true;

                return _membershipUser;
            }
        }
        protected MembershipUser AspnetpUser { get { return this.HttpContext.GetMembershipUser(); } }

		private MainEntity.Models.Shopping.Membership _subscriber;
		private bool _subscriberLoad;
		protected MainEntity.Models.Shopping.Membership Subscriber
		{
			get
			{
				if (AspnetpUser != null && !_subscriberLoad && _subscriber == null)
				{
					_subscriber = ShoppingService.GetMembershipAndActiveSubscribe((Guid)AspnetpUser.ProviderUserKey, DateTime.UtcNow);
					_subscriberLoad = true;
				}

				return _subscriber;
			}
		}
		//

        [Microsoft.Practices.Unity.InjectionMethod]
        public void Initialize(IUserService user_service, IShoppingService shopping_service, IClassService class_service, ISpeakerService speaker_service, IActivityLogService activity_log_service)
        {
            UserService = user_service;
            ClassService = class_service;
            ShoppingService = shopping_service;
            SpeakerService = speaker_service;
			ActivityLogService = activity_log_service;
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            filterContext.Controller.ViewData["MembershipUser"] = MembershipUser;
            filterContext.Controller.ViewData["IsAuthorized"] = MembershipUser != null;

            //This must be re-written to make sure that 1. we ARE using cache, and 2. it takes into account not only timespan but also a PortalID
            filterContext.Controller.ViewData["Speakers"] = this.HttpContext.Cache.GetValue(CacheEnum.TopSpeakers.ToString(), TimeSpan.FromMinutes(0), () =>
                SpeakerService.GetSpeakers(Main.GlobalConstant.ROOT_ENTITY_ID, true, true, 0, 10000, MainCommon.SortParametersEnum.Title, false, this.HttpContext.GetCurrentPortal().portalID));
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {            
            base.OnActionExecuted(filterContext);

            InitializeCart(filterContext);

            ValidateUser(filterContext);

        }

        private void ValidateUser(ActionExecutedContext filterContext)
        {
            if (MembershipUser!= null && MembershipUser.suspended &&
                !(filterContext.Controller.ControllerContext.RouteData.GetControllerName() == "Account" && filterContext.Controller.ControllerContext.RouteData.GetActionName() == "Suspended"))
            {
                var redirectUrl = new UrlHelper(filterContext.RequestContext).Action("Suspended", "Account");
                filterContext.Result = new RedirectResult(redirectUrl);
            }
        }

        private void InitializeCart(ActionExecutedContext filterContext)
        {
            List<string> cartContentTitles = new List<string>();

            long[] classProductInCartIDs = ShoppingService.GetClassProductInCartIDs(System.Web.HttpContext.Current);
            long subscribeInCartID = ShoppingService.GetSubscribeInCartID(System.Web.HttpContext.Current);
            long[] packageInCartIDs = ShoppingService.GetPackageInCartIDs(System.Web.HttpContext.Current);
            long unitsInCartID = ShoppingService.GetUnitsInCart(System.Web.HttpContext.Current);

            bool isCartEmpty = (classProductInCartIDs.Length == 0 && subscribeInCartID == 0 && unitsInCartID == 0 && packageInCartIDs.Length == 0);

            if (!isCartEmpty)
            {
                var products = ClassService.GetProductsList(classProductInCartIDs);
                foreach (var p in products)
                    cartContentTitles.Add(p.EntityItem.title.Replace("[Tape] ", "").Replace("[Disk] ", "").Replace("[File/Small Poster] ", "").Replace("[File] ", ""));

                var subscribe = UserService.GetSubscribePlan(subscribeInCartID);
                if (subscribe != null)
                    cartContentTitles.Add(subscribe.EntityItem.title.Replace("[Tape] ", "").Replace("[Disk] ", "").Replace("[File/Small Poster] ", "").Replace("[File] ", ""));

                var unitsItem = ShoppingService.GetProduct(unitsInCartID);
                int units = (unitsItem == null ? 0 : Convert.ToInt32(unitsItem.ProductEntity.price1));

                if (units > 0)
                    cartContentTitles.Add("UNITS");

                var packages = ClassService.GetProductsList(packageInCartIDs);
                foreach (var p in packages)
                    cartContentTitles.Add("[Package] " + p.EntityItem.title.Replace("[Tape] ", "").Replace("[Disk] ", "").Replace("[File/Small Poster] ", "").Replace("[File] ", ""));

            }

            string cartContentTitlesStr = string.Join("<br/>", cartContentTitles.ToArray());

            filterContext.Controller.ViewData["IsCartEmpty"] = isCartEmpty;
            filterContext.Controller.ViewData["CartContentTitles"] = cartContentTitlesStr;
        }

    }
}
