using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainEntity.Interfaces;
using Main.Areas.Admin.Models.User;
using Main.Common;
using System.Web.Security;
using Main.Common.Attributes;
using Main.Areas.Admin.Models.Common;
using System.Collections.Specialized;
using MainCommon;
using Main.Areas.Admin.Models.ControllerView.User;
using System.Text;
using Main.Utilities;

namespace Main.Areas.Admin.Controllers
{
    [MyRequireHttps]
    public partial class UserController : Controller
    {
        private IUserService _userService;
        private IMembershipService _membershipService;
        private IShoppingService _shoppingService;
        private ICatalogService _catalogService;
        private IActivityLogService _activityLogService;
        private IPortalService _portalService;

        public UserController(IUserService user_service, IMembershipService membership_service, IShoppingService shopping_service,
            ICatalogService catalog_service, IActivityLogService activity_log_service, IPortalService portal_service)
        {
            _userService = user_service;
            _membershipService = membership_service;
            _shoppingService = shopping_service;
            _catalogService = catalog_service;
            _activityLogService = activity_log_service;
            _portalService = portal_service;
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult Index(int ps = 100, int p = 1,
            Main.Areas.Admin.Models.ControllerView.User.UserFilter filter = null)
        {
            string appName = this.HttpContext.GetCurrentMembershipProvider().ApplicationName;

            filter = filter ?? new Main.Areas.Admin.Models.ControllerView.User.UserFilter();

            var recordsCount = _userService.GetUsersCount(appName, filter.ssubscribe,
                filter.semail, filter.susername, filter.sfirstname, filter.slastname, filter.msdate, filter.medate,
                filter.scanceled ? (bool?)filter.scanceled : null, filter.sdeclined ? (bool?)filter.sdeclined : null,
                filter.schargeindays.HasValue ? (int?)filter.schargeindays.Value : null, filter.schargeindaysexactly ? (bool?)filter.schargeindaysexactly : null);
            var users = _userService.GetUsers(appName, (p - 1) * ps, ps, filter.ssubscribe,
                filter.semail, filter.susername, filter.sfirstname, filter.slastname, filter.msdate, filter.medate,
                filter.scanceled ? (bool?)filter.scanceled : null, filter.sdeclined ? (bool?)filter.sdeclined : null,
                filter.schargeindays.HasValue ? (int?)filter.schargeindays.Value : null, filter.schargeindaysexactly ? (bool?)filter.schargeindaysexactly : null);

            this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, 1, 100);

            //foreach (var u in users)
            //    u.MembershipAddresses.AddRange(_userService.GetMembershipAddresses(u.UserId));                           

            List<UserItem> list = new List<UserItem>();

            foreach (var user in users)
                list.Add(new UserItem(user));

            var model = new Main.Areas.Admin.Models.ControllerView.User.Index(list.ToArray());

            return View(model);
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult ViewFiles(Guid user_id)
        {
            var mUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(user_id, false);

            var model = new Main.Areas.Admin.Models.ControllerView.User.ViewFiles(null, null, (Guid)mUser.ProviderUserKey);
            return View(model);
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult UpdateViewFiles(Guid user_id, IDictionary<long, bool> shopping_class_available, IDictionary<long, bool> shopping_class_items, IDictionary<long, bool> shopping_class_items_state,
            IDictionary<long, bool> class_items_to_add)
        {
            var mUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(user_id, false);

            if (shopping_class_items != null)
            {
                IDictionary<long, bool> diff = shopping_class_items.Except(shopping_class_items_state).ToDictionary(i => i.Key, i => !i.Value);
                foreach (var item in diff)
                    _shoppingService.UpdateShopping((Guid)mUser.ProviderUserKey, diff);
            }

            if (shopping_class_available != null)
            {
                IDictionary<long, bool> available = shopping_class_available.ToDictionary(i => i.Key, i => i.Value);
                foreach (var item in available)
                    _shoppingService.UpdateShoppingAvailable((Guid)mUser.ProviderUserKey, available);
            }

            var products = _catalogService.GetProductFilesInClasses(class_items_to_add.Where(c => c.Value == true).Select(c => c.Key).ToArray());
            _shoppingService.InsertFilesInLibrary(products.Select(p => p.productID).ToArray(), (Guid)mUser.ProviderUserKey);

            return RedirectToAction("ViewFiles", new { user_id = user_id });
        }


        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult ClassActivityLog(Main.Areas.Admin.Models.ControllerView.User.ClassActivityLogFilter filter, int ps = 100, int p = 1,
            string sort = null)
        {
            var mUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(filter.user_id, false);
            var user = _userService.GetUser((Guid)mUser.ProviderUserKey);
            var s = sort != null ? (ClassActivityLogSortEnum)Enum.Parse(typeof(ClassActivityLogSortEnum), sort) : ClassActivityLogSortEnum.Date_desc;
            var activity = new ActivityLogTypeEnum[] { ActivityLogTypeEnum.DownloadClass, ActivityLogTypeEnum.StreamingClass, ActivityLogTypeEnum.StreamingFreeClass };
            if (filter.ftype == Models.ControllerView.User.ClassActivityLogFilter.TypeFilter.ShowDownloadOnly)
                activity = new ActivityLogTypeEnum[] { ActivityLogTypeEnum.DownloadClass };
            else if (filter.ftype == Models.ControllerView.User.ClassActivityLogFilter.TypeFilter.ShowStreamingOnly)
                activity = new ActivityLogTypeEnum[] { ActivityLogTypeEnum.StreamingClass, ActivityLogTypeEnum.StreamingFreeClass };

            int recordsCount = _activityLogService.GetActivityLogCnt((Guid)mUser.ProviderUserKey, filter.fsincedata, filter.fbeforedata, activity, filter.fgrouping);
            var model = _activityLogService.GetActivityLog(s, (Guid)mUser.ProviderUserKey, filter.fsincedata, filter.fbeforedata, activity, filter.fgrouping, (p - 1) * ps, ps);
            this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, 1, 100);
            this.ViewData["SortOrder"] = s.ToString();
            this.ViewData["UserName"] = string.IsNullOrEmpty(user.firstName + user.lastName) ? user.Email : user.firstName + " " + user.lastName;
            this.ViewData["Filter"] = filter;

            return View(model);
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult ViewShoppingTransactions(Main.Areas.Admin.Models.ControllerView.User.ShoppingTransactionsFilter filter, int ps = 100, int p = 1)
        {
            short[] tranState;
            string[] tranType;
            GetTransactionFilter(filter, out tranState, out tranType);

            var mUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(filter.user_id, false);
            var user = _userService.GetUser((Guid)mUser.ProviderUserKey);
            int recordsCount = _shoppingService.GetShoppingTransactionsCnt((Guid)mUser.ProviderUserKey, filter.fsincedata, filter.fbeforedata, tranState, tranType);
            var model = _shoppingService.GetShoppingTransactions((Guid)mUser.ProviderUserKey, (p - 1) * ps, ps, filter.fsincedata, filter.fbeforedata, tranState, tranType);
            filter.Initialize();

            this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, 1, 100);
            this.ViewData["UserName"] = string.IsNullOrEmpty(user.firstName + user.lastName) ? user.Email : user.firstName + " " + user.lastName;
            this.ViewData["Filter"] = filter;

            return View(model);
        }

        private static void GetTransactionFilter(Main.Areas.Admin.Models.ControllerView.User.ShoppingTransactionsFilter filter, out short[] tran_state, out string[] tran_type)
        {
            tran_state = new[] { (short)ShoppingTransactionStateEnum.Prepaid, (short)ShoppingTransactionStateEnum.Complete };
            tran_type = null;

            switch (filter.ftranstate)
            {
                case ShoppingTransactionsFilter.TransactionStateEnum.AllSuccessfulMonthlyTransactions:
                    tran_type = new[] { 
                        ShoppingTransactionTypeEnum.monthlyfee.ToString(), ShoppingTransactionTypeEnum.authorize_monthlyfee.ToString(),
                        ShoppingTransactionTypeEnum.authorize_monthlyfee_purchase.ToString(), ShoppingTransactionTypeEnum.monthlyfee_purchase.ToString() };
                    break;
                case ShoppingTransactionsFilter.TransactionStateEnum.AllSuccessfulPurchaseTransactions:
                    tran_type = new[] { ShoppingTransactionTypeEnum.purchase.ToString(), ShoppingTransactionTypeEnum.authorize_purchase.ToString() };
                    break;
                case ShoppingTransactionsFilter.TransactionStateEnum.AllUnsuccessfulMonthlyTransactions:
                    tran_type = new[] { 
                        ShoppingTransactionTypeEnum.monthlyfee.ToString(), ShoppingTransactionTypeEnum.authorize_monthlyfee.ToString(),
                        ShoppingTransactionTypeEnum.authorize_monthlyfee_purchase.ToString(), ShoppingTransactionTypeEnum.monthlyfee_purchase.ToString()};
                    tran_state = new[] { (short)ShoppingTransactionStateEnum.Rollback };
                    break;
                default:
                    tran_state = null;
                    break;
            }
        }


        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult ClassesInLibrary(Guid user_id, int page_num = 1, int page_size = 10)
        {
            var mUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(user_id, false);
            var libraryItems = _shoppingService.SelectLibraryItems((Guid)mUser.ProviderUserKey, (page_num - 1) * page_size, page_size);
            var libraryItemsCount = _shoppingService.SelectLibraryItemsCount((Guid)mUser.ProviderUserKey);
            var model = new Main.Areas.Admin.Models.ControllerView.User.ViewFiles(libraryItems, null, (Guid)mUser.ProviderUserKey);

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("user_id", mUser.ProviderUserKey.ToString());
            this.ViewData["Pager1"] = new AjaxPagingData(page_num, page_size, libraryItemsCount, "ClassesInLibrary", "existing", parameters);

            return View(model);
        }

        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult Classes(Guid user_id, int page_num = 1, int page_size = 10,
            string stitle = null, int[] scategory = null, string sspeaker = null, string scode = null, long sportal = 0)
        {
            var mUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(user_id, false);
            var exceptClassesIds = _shoppingService.SelectLibraryItemsClassIds((Guid)mUser.ProviderUserKey);
            string userName = this.User.Identity.Name;
            bool isSuperUser = this.HttpContext.GetCurrentRoleProvider().IsUserInRole(userName, UserRoles.SUPERUSER_ROLE);

            sportal = isSuperUser ? sportal : this.HttpContext.GetCurrentPortal().portalID;
            var filterNew = scategory != null && scategory.Contains(-1);
            if (filterNew) scategory = scategory.Except(new int[] { -1 }).ToArray();


            var otherClasses = _catalogService.GetClassEntitiesWithFiles(GlobalConstant.ROOT_ENTITY_ID, exceptClassesIds, (page_num - 1) * page_size, page_size,
                sportal, scategory, stitle, sspeaker, scode, filterNew);
            var otherClassesCount = _catalogService.GetClassEntitiesWithFilesCount(GlobalConstant.ROOT_ENTITY_ID, exceptClassesIds,
                sportal, scategory, stitle, sspeaker, scode, filterNew);
            var model = new Main.Areas.Admin.Models.ControllerView.User.ViewFiles(null, otherClasses, (Guid)mUser.ProviderUserKey);

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("user_id", mUser.ProviderUserKey.ToString());
            this.ViewData["Pager2"] = new AjaxPagingData(page_num, page_size, otherClassesCount, "Classes", "other", parameters);

            return View(model);
        }


        #region Create User

        [HttpGet]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult CreateUser(long? portal_id = null)
        {
            RoleProvider role;
            MembershipProvider membership;

            GetProviders(portal_id, out role, out membership);

            string[] roles = role.GetAllRoles();

            var model = new UserEditModel(/*_userService.GetSubscribePlanEntities(), */roles) { PortalID = portal_id };
            if (portal_id.HasValue) model.UserRoles.Add(UserRoles.PORTALADMIN_ROLE);

            return View(model);
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult CreateUser(UserEditModel user)
        {
            string userName = user.Email;

            RoleProvider role;
            MembershipProvider membership;

            GetProviders(user.PortalID, out role, out membership);

            if (this.ModelState.IsValid)
            {
                MembershipCreateStatus status;
                Guid userKey = Guid.NewGuid();
                var mUser = membership.CreateUser(userName, user.Password, user.Email, "yes", "yes", true,
                    userKey, out status);

                if (status == MembershipCreateStatus.Success)
                {
                    mUser.Comment = user.Description;
                    membership.UpdateUser(mUser);

                    try
                    {
                        var userRoles = role.GetRolesForUser(userName);
                        if (userRoles != null && userRoles.Length != 0)
                            role.RemoveUsersFromRoles(new string[] { userName }, userRoles);
                        if (user.UserRoles != null)
                            role.AddUsersToRoles(new string[] { userName }, user.UserRoles.ToArray());

                        membership.UpdateUser(mUser);

                        DateTime? startSubscribeDate = GetDateFromString(user.StartDate);
                        if (user.PlanID > 238)
                        {
                            var currUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(this.User.Identity.Name, false);
                            var parentSubscribeID = _catalogService.InsertOrUpdateSubscribePlanTree(GlobalConstant.ROOT_ENTITY_ID, user.PlanID, (Guid)currUser.ProviderUserKey, user.PrepaidPlanMonths, 238, 0, user.PrepaidPlanUnitsPerMonth, DateTime.UtcNow);
                            if (user.PlanID == int.MaxValue) user.PlanID = parentSubscribeID;
                        }

                        _userService.InsertMembership(userKey, user.FirstName, user.LastName, user.Balance, 2,
                            user.Country, user.State, user.City, user.Address, user.PostalCode, user.Phone, user.DayPhone, user.FullLibraryAccess, user.ReferrerCode,
                            user.PlanID, startSubscribeDate);

                        user.InitPlanList(/*_userService.GetSubscribePlanEntities(), */role.GetAllRoles(), role.GetRolesForUser(userName).ToList());

                        TempData["created"] = true;
                        user.UserRoles = role.GetRolesForUser(mUser.UserName).ToList();
                        user.Roles = role.GetAllRoles().ToList();
                        user.UserID = userKey.ToString();
                        user.UserName = mUser.UserName;

                        return View("EditUserInfo", user);
                    }
                    catch (Exception e)
                    {
                        membership.DeleteUser(userName, true);
                        string errorMsg = "Cannot create user. Error message: " + e.Message;
                        this.ModelState.AddModelError("", errorMsg);

                        var curRolesProvider = this.HttpContext.GetCurrentRoleProvider();
                        string[] roles = curRolesProvider.GetAllRoles();

                        ViewData.Model = new UserEditModel(/*_userService.GetSubscribePlanEntities(), */roles);
                        return View();
                    }
                }
                else
                {
                    string errorMsg = "Cannot create user";
                    switch (status)
                    {
                        case MembershipCreateStatus.DuplicateEmail:
                            {
                                errorMsg = "An account already exists with this email address";
                                break;
                            }
                        case MembershipCreateStatus.DuplicateUserName:
                            {
                                errorMsg = "An account already exists with this email address";
                                break;
                            }
                        case MembershipCreateStatus.InvalidEmail:
                            {
                                errorMsg = "Email format is incorrect";
                                break;
                            }
                        case MembershipCreateStatus.InvalidPassword:
                            {
                                errorMsg = "Password must be at least 6 characters long";
                                break;
                            }
                        case MembershipCreateStatus.InvalidUserName:
                            {
                                errorMsg = "Email format is incorrect";
                                break;
                            }
                    }

                    this.ModelState.AddModelError("", errorMsg);
                    user.InitPlanList(/*_userService.GetSubscribePlanEntities(), */role.GetAllRoles());
                }
            }
            else
            {
                user.InitPlanList(/*_userService.GetSubscribePlanEntities(), */role.GetAllRoles(), user.UserRoles);
            }

            return View("CreateUser", user);
        }

        private void GetProviders(long? from_portal_id, out RoleProvider role, out MembershipProvider membership)
        {
            if (from_portal_id.HasValue)
            {

                var applicationName = _portalService.GetPortal(from_portal_id.Value, false, true).applicationName;
                role = Roles.Providers.Cast<RoleProvider>().
                    Where(m => m.ApplicationName.Equals(applicationName, StringComparison.InvariantCultureIgnoreCase)).Single();
                membership = Membership.Providers.Cast<MembershipProvider>().
                    Where(m => m.ApplicationName.Equals(applicationName, StringComparison.InvariantCultureIgnoreCase)).Single();
            }
            else
            {
                role = this.HttpContext.GetCurrentRoleProvider();
                membership = this.HttpContext.GetCurrentMembershipProvider();
            }
        }

        #endregion

        #region Edit User

        [HttpGet]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public string EditCredits(string credits, string user_id)
        {
            var mUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(new Guid(user_id), false);
            var balance = decimal.Parse(credits);
            _userService.UpdateBalance((Guid)mUser.ProviderUserKey, balance);

            return decimal.Round(balance, 2).ToString("N0") + " units";
        }

        [HttpGet]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public string LockUser(string user_id)
        {
            var mUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(new Guid(user_id), false);

            int ret = -1;
            string textRet = "ock User";

            if (mUser.IsLockedOut)
            {
                ret = mUser.UnlockUser() ? 1 : 0;
                if (ret > -1)
                    textRet = "L" + textRet;
            }
            else
            {
                ret = _userService.LockUser(new Guid(user_id), mUser.Email);
                if (ret > -1)
                    textRet = "Unl" + textRet;
            }

            return textRet;
        }

        [HttpGet]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult EditPlanDates(Guid user_id, DateTime? start_subscribe_date, DateTime? end_subscribe_date, byte? charge_day)
        {
            var mUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(user_id, false);
            return PartialView(new Main.Areas.Admin.Models.User.EditPlanDates((Guid)mUser.ProviderUserKey, start_subscribe_date, end_subscribe_date, charge_day));
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult UpdatePlanDates(Guid user_id, DateTime? start_date, DateTime? end_date, byte? charge_day)
        {
            var mUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(user_id, false);
            _userService.UpdatePlanDates((Guid)mUser.ProviderUserKey, start_date, end_date, charge_day);

            return PartialView(new Main.Areas.Admin.Models.User.EditPlanDates(user_id, start_date, end_date, charge_day));
        }

        [HttpGet]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult EditUserInfo(Guid user_id)
        {
            var mUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(user_id, false);
            var user = _userService.GetUser((Guid)mUser.ProviderUserKey);
            //user.PlansList = _userService.GetSubscribePlanEntities();
            var curRoleProvider = this.HttpContext.GetCurrentRoleProvider();
            var model = new Main.Areas.Admin.Models.User.UserEditModel(user, curRoleProvider.GetAllRoles(), curRoleProvider.GetRolesForUser(user.UserName).ToList());

            return View(model);
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult EditUserInfo(Main.Areas.Admin.Models.User.UserEditModel user)
        {
            var curRoleProvider = this.HttpContext.GetCurrentRoleProvider();
            var curProvider = this.HttpContext.GetCurrentMembershipProvider();
            var userID = new Guid(user.UserID);
            MembershipUser mUser = curProvider.GetUser(userID, false);
            string userName = mUser.UserName;

            if (!this.ModelState.IsValidField("Password"))
            {
                this.ModelState.Where(w => w.Key == "Password").First().Value.Errors.Clear();
                this.ModelState.Where(w => w.Key == "ConfirmPassword").First().Value.Errors.Clear();
            }
            else
            {

                if (user.Password == user.ConfirmPassword)
                {
                    var oldPassword = mUser.ResetPassword();
                    if (!_membershipService.ChangePassword(mUser.UserName, oldPassword, user.Password, this.HttpContext.GetCurrentMembershipProvider()))
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Password and ConfirmPassword is not equal.");
                }
            }

            if (this.ModelState.IsValid)
            {
                if (user.Email != mUser.Email)
                {
                    bool isEmailUniqueForApp = _userService.IsEmailUniqueForApplication(curProvider.ApplicationName, user.Email);
                    if (isEmailUniqueForApp)
                        _userService.UpdateEmail(userID, user.Email);
                    else
                        ModelState.AddModelError("", "Account with this email address already exist.");
                }

                if (user.UserName != mUser.UserName)
                {
                    bool isUserNameUniqueForApp = _userService.IsUserNameUniqueForApplication(curProvider.ApplicationName, user.UserName);
                    if (isUserNameUniqueForApp)
                        _userService.UpdateUserName(new Guid(user.UserID), user.UserName);
                    else
                        ModelState.AddModelError("", "An account with this username already exists.");
                }

                if (ModelState.IsValid)
                {
                    if ((user.UserName != mUser.UserName) || (user.Email != mUser.Email))
                        mUser = curProvider.GetUser(userID, false);

                    mUser.Comment = user.Description;
                    var userRoles = curRoleProvider.GetRolesForUser(userName);
                    if (userRoles != null && userRoles.Length != 0)
                        curRoleProvider.RemoveUsersFromRoles(new string[] { userName }, userRoles);
                    if (user.UserRoles != null)
                        curRoleProvider.AddUsersToRoles(new string[] { userName }, user.UserRoles.ToArray());

                    curProvider.UpdateUser(mUser);

                    DateTime? startSubscribeDate = GetDateFromString(user.StartDate);
                    if (user.PlanID > 238)
                    {
                        var currUser = curProvider.GetUser(this.User.Identity.Name, false);
                        var parentSubscribeID = _catalogService.InsertOrUpdateSubscribePlanTree(GlobalConstant.ROOT_ENTITY_ID, user.PlanID, (Guid)currUser.ProviderUserKey, user.PrepaidPlanMonths, 238, 0, user.PrepaidPlanUnitsPerMonth, DateTime.UtcNow);
                        if (user.PlanID == int.MaxValue) user.PlanID = parentSubscribeID;
                    }

                    _userService.UpdateUser(new Guid(user.UserID), user.Balance, user.Description, user.FirstName, user.LastName,
                        user.ReferrerCode, user.City, user.Country, user.DayPhone, user.Phone, user.Address, user.PostalCode, user.State,
                        startSubscribeDate, user.PlanID, user.FreeOfferCnt, user.Suspended, user.FullLibraryAccess);

                    TempData["success"] = true;
                }

                user.InitPlanList(/*_userService.GetSubscribePlanEntities(), */curRoleProvider.GetAllRoles(), curRoleProvider.GetRolesForUser(userName).ToList());
            }
            else
            {
                user.InitPlanList(/*_userService.GetSubscribePlanEntities(), */curRoleProvider.GetAllRoles(), curRoleProvider.GetRolesForUser(userName).ToList());
            }

            TempData["created"] = false;
            return View("EditUserInfo", user);
        }

        private DateTime? GetDateFromString(string str)
        {
            DateTime? date = null;
            try
            {
                string[] temp = str.Split('/');
                date = new DateTime(int.Parse(temp[2]), int.Parse(temp[0]), int.Parse(temp[1]));
            }
            catch { }

            return date;
        }

        #endregion

        [HttpGet]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_PORTALADMIN_ROLES)]
        public ActionResult CancelMembership(Guid user_id)
        {
            var mUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(user_id, false);
            //_userService.DeletePlan((Guid)mUser.ProviderUserKey);

            _userService.CancelSubscribe((Guid)mUser.ProviderUserKey);

            return RedirectToAction("Index");
        }


        #region Delete User

        [HttpGet]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult Delete(Guid user_id)
        {
            var mUser = this.HttpContext.GetCurrentMembershipProvider().GetUser(user_id, false);
            _userService.LockUser((Guid)mUser.ProviderUserKey);

            return RedirectToAction("Index");
        }

        #endregion

        #region Credit Card

        [HttpGet]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult ChangeCreditCard(Guid user_id)
        {
            var creditCard = _userService.GetLastMembershipCard(user_id);
            var user = _userService.GetUser(user_id);

            var model = new Main.Models.Account.ChangeCreditCard();
            if (creditCard != null)
            {
                model.CurrentExpirationDate = creditCard.expirationDate;
                model.CurrentMembershipCartID = creditCard.membershipCartID;
            };

            model.UserID = user_id;
            model.City = user.city;
            model.Country = user.country;
            model.Email = user.Email;
            model.FirstName = user.firstName;
            model.LastName = user.lastName;
            model.Phone = user.phone;
            model.PostalAddress = user.postalAdderss;
            model.PostalCode = user.postalCode;
            model.State = user.state;

            return View(model);
        }

        [HttpPost]
        [PortalAuthorize(Roles = UserRoles.SUPERUSER_ROLE)]
        public ActionResult ChangeCreditCard(Main.Models.Account.ChangeCreditCard model, bool AddNewCard, decimal? AmountToCharge, bool InActive = false)
        {
            var creditCard = _userService.GetLastMembershipCard(model.UserID);
            var tran = _shoppingService.GetShoppingTransactionInfo(new long[] { 53850 }, (Guid)model.UserID, false, (decimal)Properties.Settings.Default.SubscriberDiscount, Properties.Settings.Default.MaxUnitsOnSubscribe);
            var transactionInfo = new MainEntity.Models.Shopping.ShoppingTransactionInfo(
                AmountToCharge.HasValue ? AmountToCharge.Value : 0,
                tran.HoldUnits,
                tran.ShoppingPrice,
                tran.SubscribePlanFree,
                tran.UnitsBuy,
                tran.DoSubscribe,
                tran.IsSubscriber
            );

            if (InActive) _userService.UpdateMembershipCardState(model.UserID, model.CreditCardNumber, CartStateEnum.Deleted);

            var addresses = _userService.InsertAddesses(model.UserID, model.FirstName, model.LastName, model.Country, model.State, model.City, model.PostalAddress, model.PostalCode, model.Phone, null, null);
            string chargeType = ShoppingTransactionTypeEnum.manual.ToString();

            var sjLog = new StringBuilder();
            long transactionID = 0;

            if (AddNewCard && this.ModelState.IsValid)
            {
                try
                {
                    var cardID = model.CreditCardNumber.Substring(model.CreditCardNumber.Length - 4);
                    var expirationDate = new DateTime(model.ExpirationDateYear, model.ExpirationDateMonth, 1);
                    var oldCardID = creditCard != null ? creditCard.membershipCartID : null;
                    _userService.InsertOrUpdateMembershipCard(model.UserID, expirationDate, model.CreditCard, cardID, CartStateEnum.Deleted);

                    if (AmountToCharge.HasValue && AmountToCharge.Value > 0)
                    {
                        transactionID = _shoppingService.ShoppingPayAuthorize(transactionInfo, addresses, chargeType, cardID, model.UserID,
                            (transaction_id) =>
                                Main.Controllers.AccountController.AuthorizeCreditCard(
                                    transaction_id, model.FirstName, model.LastName, model.Email,
                                    model.PostalAddress, model.City, model.State, model.PostalCode, model.Country,
                                    model.Phone, long.Parse(model.CreditCardNumber), expirationDate, model.UserID, cardID, oldCardID, sjLog, this._userService),
                            (transaction_id, tran_id) =>
                            {
                                transactionID = transaction_id;
                                return SkipjackExecutor.Pay(tran_id, transaction_id, transaction_id, AmountToCharge.Value, Properties.Settings.Default.SJSerialNumber, Properties.Settings.Default.SJDeveloperSerialNumber, Properties.Settings.Default.SJChangeStatusUrl, Properties.Settings.Default.SJGetStatusUrl, sjLog);
                            });
                    }
                    else
                    {
                        chargeType = ShoppingTransactionTypeEnum.authorize.ToString();
                        _shoppingService.CreditCardAuthorize(addresses, cardID, model.UserID,
                            (transaction_id) => Main.Controllers.AccountController.AuthorizeCreditCard(
                                    transaction_id, model.FirstName, model.LastName, model.Email,
                                    model.PostalAddress, model.City, model.State, model.PostalCode, model.Country,
                                    model.Phone, long.Parse(model.CreditCardNumber), expirationDate, model.UserID, cardID, oldCardID, sjLog, this._userService));

                    }

                    Mailer.AddMessage(Mailer.OnSomeoneChangeCreditCard(
                        Properties.Settings.Default.FromEmailNotification, model.Email, model.FirstName + " " + model.LastName));

                    TempData["success"] = true;
                }
                catch (MainCommon.Skipjack.SJUnhandledException)
                {
                    this.ModelState.AddModelError("", "Your card could not be authenticated. Please check the information you've entered and try again");
                }
                catch
                {
                    this.ModelState.AddModelError("", "Temporary error, try again later");
                }
            }
            else if (!AddNewCard && AmountToCharge.HasValue && AmountToCharge.Value > 0)
            {
                this.ModelState.Clear();
                string tranID = creditCard.tranID;
                string cardID = creditCard.membershipCartID;
                var authShoppingTransactionID = creditCard.shoppingTransactionID;

                try
                {
                    transactionID = _shoppingService.ShoppingPay((Guid)model.UserID, transactionInfo, chargeType, cardID, addresses,
                        (transaction_id) =>
                        {
                            transactionID = transaction_id;
                            return SkipjackExecutor.Pay(tranID, transaction_id, authShoppingTransactionID, AmountToCharge.Value, Properties.Settings.Default.SJSerialNumber, Properties.Settings.Default.SJDeveloperSerialNumber, Properties.Settings.Default.SJChangeStatusUrl, Properties.Settings.Default.SJGetStatusUrl, sjLog);
                        });
                }
                catch (MainCommon.Skipjack.SJUnhandledException)
                {
                    this.ModelState.AddModelError("", "Your card could not be authenticated. Please check the information you've entered and try again");
                }
                catch
                {
                    this.ModelState.AddModelError("", "Temporary error, try again later");
                }
            }

            model.CurrentExpirationDate = creditCard != null ? creditCard.expirationDate : null;
            model.CurrentMembershipCartID = creditCard != null ? creditCard.membershipCartID : null;

            return View(model);

        }
        #endregion

    }
}