using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainEntity.Interfaces;
using Main.Models;
using System.Web.Security;
using Main.Common.Validation;
using Main.Common;
using Main.Models.Account;
using MainCommon.Daemon;
using System.Net.Mail;
using System.Text;
using System.IO;
using MainCommon;
using System.Configuration;
using System.Web.UI;
using Main.Areas.Admin.Models.Common;
using MainCommon.Skipjack;
using Main.Common.Attributes;
using System.Web.Routing;
using Main.Utilities;


namespace Main.Controllers
{
    [HandleError]
    [MyRequireHttpsAttribute]
    [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
    public partial class AccountController : BaseController
    {
        private IFormsAuthenticationService _formsService;
        private IMembershipService _membershipService;
        private IShoppingService _shoppingService;
        private IActivityLogService _activityLogService;
        private IFileService _fileService;
        private IUserService _userService;

        private bool newCustomer;

        public AccountController(IFormsAuthenticationService forms_service, IMembershipService membership_service,
            IShoppingService shopping_service, IFileService file_service, IUserService user_service, IActivityLogService log_activity_service)
        {
            _formsService = forms_service;
            _membershipService = membership_service;
            _shoppingService = shopping_service;
            _fileService = file_service;
            _userService = user_service;
            _activityLogService = log_activity_service;
        }

        #region LogOn

        //public ActionResult LogOn()
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
           
            ViewData["returnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var membershipProvider = this.HttpContext.GetCurrentMembershipProvider();
                if (_membershipService.ValidateUser(model.UserName, model.Password, membershipProvider))
                {
                    var unauthorizedCartSubscribe = ShoppingService.GetSubscribeInCartID(System.Web.HttpContext.Current);

                    _formsService.SignIn(model.UserName, membershipProvider.ApplicationName, this.Response.Cookies);

                    var userID = (Guid)this.HttpContext.GetCurrentMembershipProvider().GetUser(model.UserName, false).ProviderUserKey;
                    var user = ShoppingService.GetMembershipAndActiveSubscribe(userID, DateTime.Now);

                    /*********************   THIS IS A HACK TO REDIRECT ADMIN TO BACKOFFICE ***********************/

                    //if (model.UserName.Equals("srdjanMladenovic", StringComparison.CurrentCultureIgnoreCase))
                    if (this.HttpContext.GetCurrentRoleProvider().IsUserInRole(model.UserName, UserRoles.SUPERUSER_ROLE) ||
                        this.HttpContext.GetCurrentRoleProvider().IsUserInRole(model.UserName, UserRoles.PORTALADMIN_ROLE))
                        return RedirectToAction("Index", "Home", new { area = "admin" });

                    /********************************************************************************************/

                    if (unauthorizedCartSubscribe > 0 && user == null)
                    {
                        return RedirectToAction("Cart", "Shopping");
                    }
                    else if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        model.Password = "";
                        return RedirectToAction("Index", "Account", model);
                    }
                }
                else
                {
                    var user = this.HttpContext.GetCurrentMembershipProvider().GetUser(model.UserName, false);
                    if (user != null && !user.IsApproved)
                        return RedirectToAction("RegisterCustomMembershipValidation", new { email = model.UserName });

                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    return RedirectToAction("IncorrectLogin", "Account", model);
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return RedirectToAction("IncorrectLogin", "Account", model);
            }

        }

        public ActionResult LogOff(string returnUrl)
        {
            _formsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Change Password

        //[RequiresAuthorize, AllowAuthorizePortal]
        //public ActionResult ChangePassword()
        //{
        //    ViewData["PasswordLength"] = this.HttpContext.GetCurrentMembershipProvider().MinRequiredPasswordLength;
        //    return View();
        //}

        //[HttpPost]
        //[RequiresAuthorize, AllowAuthorizePortal]
        //public ActionResult ChangePassword(ChangePasswordModel model, string redirectUrl)
        //{
        //    ChangePassword(model, this.ModelState);

        //    if (!string.IsNullOrEmpty(redirectUrl))
        //        return Redirect(redirectUrl);
        //    else
        //        return Redirect(this.HttpContext.Request.RawUrl);

        //}

        private void ChangePassword(ChangePasswordModel model, ModelStateDictionary model_state)
        {
            if (model_state.IsValid)
            {
                var curProvider = this.HttpContext.GetCurrentMembershipProvider();
                var aspnetUser = curProvider.GetUser(this.HttpContext.User.Identity.Name, false);
                model.OldPassword = aspnetUser.ResetPassword();

                if (!_membershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.Password, this.HttpContext.GetCurrentMembershipProvider()))
                {
                    model_state.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }

            }
        }

        //[RequiresAuthorize, AllowAuthorizePortal]
        //public ActionResult ChangePasswordSuccess()
        //{
        //    return View();
        //}

        #endregion

        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult Index()
        {
            var model = new Main.Models.ControllerView.Account.AccountInfo()
            {
                UserName = this.MembershipUser.UserName,
                EMail = this.AspnetpUser.Email,
                UnitsCount = this.MembershipUser.balance,
                DateMembershipBegan = this.Subscriber != null ? this.Subscriber.subscribeActivation : this.AspnetpUser.CreationDate,
                CancelledOnDate = this.Subscriber != null ? this.Subscriber.EndSubscribeDate : null,
                SubscribePlanName = this.Subscriber != null ? UserService.GetSubscribePlan(this.Subscriber.SubscribePlanID).EntityItem.title : "Free Listening",
                FreeOffersCnt = this.MembershipUser.freeOfferCnt,
                IsCancelSubscribe = this.MembershipUser.IsCancelSubscribe,
            };

            this.ViewData["IsUserHaveSubscribePlan"] = _shoppingService.IsUserHaveSubscribePlan(this.MembershipUser.UserId);
            ViewData["UnitsQuantity"] = ShoppingService.GetProducts(GlobalConstant.ROOT_ENTITY_ID, ProductTypeEnum.Units).ToDictionary(item => item.entityID, item => item.title);


            return View(model);
        }

        [HttpGet]
        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult Suspended()
        {
            return ChangeCreditCard();
        }

        [HttpPost]
        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult Suspended(ChangeCreditCard model, bool cancel_subscribe = false)
        {
            ActionResult rval;

            if (cancel_subscribe)
            {
                _userService.CancelSubscribe(this.MembershipUser.UserId);
                Mailer.AddMessage(Mailer.OnMonthlyMembershipCancellation(
                        Properties.Settings.Default.FromEmailNotification, this.AspnetpUser.Email, this.MembershipUser.firstName + " " + this.MembershipUser.lastName, this.AspnetpUser.UserName),
                    Properties.Settings.Default.AdminEmailSubscribeNotification);
                _userService.UnSuspend(this.MembershipUser.UserId);
                rval = RedirectToAction("Index");
            }
            else
            {
                rval = ChangeCreditCard(model);
                if (this.ModelState.IsValid)
                {
                    _userService.UnSuspend(this.MembershipUser.UserId);
                    ChargeMonthlyFee(this.MembershipUser);
                }
            }

            return rval;
        }

        private bool ChargeMonthlyFee(MainEntity.Models.User.Membership subscriber)
        {
            var sjLog = new StringBuilder();
            long transactionID = 0;
            
            var transactionInfo = _shoppingService.GetShoppingTransactionInfo(new[] { subscriber.PlanID == 238 ? 238 : subscriber.NextSubscribePlanID },
                subscriber.UserId, false, (decimal)Properties.Settings.Default.SubscriberDiscount, Properties.Settings.Default.MaxUnitsOnSubscribe, true);

            try
            {
                var card = _userService.GetLastMembershipCard(subscriber.UserId);
                var chargeType = ShoppingTransactionTypeEnum.monthly.ToString();
                var tranID = card.tranID;
                var cardID = card.membershipCartID;
                var authShoppingTransactionID = card.shoppingTransactionID;
                KeyValuePair<long, long?>? addresses = null;

                _shoppingService.ShoppingPay(subscriber.UserId, transactionInfo, chargeType, cardID, addresses,
                    (transaction_id) =>
                    {
                        transactionID = transaction_id;
                        return SkipjackExecutor.Pay(tranID, transaction_id, authShoppingTransactionID, transactionInfo.Amount,
                        Properties.Settings.Default.SJSerialNumber, Properties.Settings.Default.SJDeveloperSerialNumber,
                        Properties.Settings.Default.SJChangeStatusUrl, Properties.Settings.Default.SJGetStatusUrl, sjLog);
                    });

                Mailer.AddMessage(Mailer.ChargeOnSkipjackIsSuccess(Properties.Settings.Default.FromEmailNotification, Properties.Settings.Default.AdminEmailSubscribeNotification, transactionInfo.Amount.ToString(), sjLog.ToString()));
            }
            catch (SJTimeoutException)
            {
                Mailer.AddMessage(Mailer.NoResponseFromTheSkipjack(
                    Properties.Settings.Default.FromEmailNotification, Properties.Settings.Default.AdminEmailSubscribeNotification, subscriber.UserId.ToString(), transactionID.ToString(), transactionInfo.Amount.ToString(), sjLog.ToString()));
                throw;
            }
            catch (SJDeclineException)
            {
                Mailer.AddMessage(Mailer.ChargeOnSkipjackIsDeclined(
                    Properties.Settings.Default.FromEmailNotification, Properties.Settings.Default.AdminEmailSubscribeNotification, sjLog.ToString()));
                throw;
            }
            catch (SJUnhandledException)
            {
                Mailer.AddMessage(Mailer.SkipjackFailsBecauseNoResponseInGivenTime(
                    Properties.Settings.Default.FromEmailNotification, Properties.Settings.Default.AdminEmailSubscribeNotification, sjLog.ToString()));
                throw;
            }
          
            return true;
        }

        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult ShoppingTransactions()
        {
            var user = this.HttpContext.GetMembershipUser();
            Guid userID = (Guid)user.ProviderUserKey;

            var transactions = _shoppingService.GetShoppingTransactions(userID, null, null, null, null, new short[] { (short)ShoppingTransactionStateEnum.Complete, (short)ShoppingTransactionStateEnum.Prepaid }, null)
                .Where(t => t.chargetype != ShoppingTransactionTypeEnum.authorize.ToString()).ToArray();
            return View(transactions);
        }

        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult DetailShoppingTransaction(long transaction_id)
        {
            var user = this.HttpContext.GetMembershipUser();
            Guid userID = (Guid)user.ProviderUserKey;

            var transactions = _shoppingService.GetShoppingTransactions(userID, null, null, null, null, new short[] { (short)ShoppingTransactionStateEnum.Complete, (short)ShoppingTransactionStateEnum.Prepaid }, null);
            var orders = _shoppingService.GetOrders(userID, transaction_id);
            var addresess = _shoppingService.GetShoppingAddresses(new[] { transaction_id });

            var model = new Main.Models.ControllerView.Account.DetailShoppingTransactions(transactions.Where(t => t.chargetype != ShoppingTransactionTypeEnum.authorize.ToString()).ToArray(), orders,
                transaction_id, transactions.Where(t => t.shoppingTransactionID == transaction_id).Select(r => r.createDate).First(), addresess);

            return View(model);
        }



        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult MyLibrary(int p = 1, int ps = 0, int rn = 0, string sort = null)
        {
            var s = sort != null ? (MyLibraryClassSortEnum)Enum.Parse(typeof(MyLibraryClassSortEnum), sort) : MyLibraryClassSortEnum.Date_desc;

            string userName = this.HttpContext.User.Identity.Name;
            var provider = this.HttpContext.GetCurrentMembershipProvider();
            MembershipUser user = provider.GetUser(userName, false);
            Guid userID = (Guid)user.ProviderUserKey;
            var myUser = _userService.GetUser(userID);

            if (ps > 0) this.HttpContext.SetValue(SessionEnum.CatalogPageSize, ps.ToString());
            else ps = int.Parse(this.HttpContext.GetValue(SessionEnum.CatalogPageSize, "10"));

            int startRowIndex = rn > 1 ? rn - 1 : (p - 1) * ps;
            int maxRowCount = ps;
            var shoppings = _shoppingService.SelectLibraryItemsIDsWithShoppingDate(userID);//, startRowIndex, maxRowCount);

            var genMem = _shoppingService.GetMembership(userID);
            var m = _shoppingService.GetMembershipAndActiveSubscribe(userID, DateTime.Now);
            DateTime dateLimit = genMem != null && genMem.fullLibraryAccess ? new DateTime(2002, 1, 1) : 
                m == null || !m.subscribeActivation.HasValue ? DateTime.Now.AddMonths(-2) : m.subscribeActivation.Value;
            
            var logs = _activityLogService.SelectLibraryItemsIDsWithShoppingDate(userID, new ActivityLogTypeEnum[] { ActivityLogTypeEnum.DownloadClass }, dateLimit);//, startRowIndex, maxRowCount);

            var fileEntities = new List<MainEntity.Models.File.FileEntity>();
            for (int i = 0; i < shoppings.Length; i += 100)
                fileEntities.AddRange(_fileService.SelectFilesByIDs(shoppings.Select(f => f.Key).Skip(i).Take(100).ToArray()));

            
            for (int i = 0; i < logs.Length; i += 100)
                fileEntities.AddRange(_fileService.SelectFilesByIDs(logs.Select(f => f.Key).Skip(i).Take(100).ToArray(), fileEntities));

            int recordsCount = fileEntities.Count();

            this.ViewData["SortOrder"] = s;
            this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, rn - 1, 10);


            this.ViewData["balance"] = myUser.balance;
            this.ViewData["UserName"] = (myUser.firstName + " " + myUser.lastName).Trim();

            foreach (var item in fileEntities)
                item.EntityItem.ShoppingDate = shoppings.Where(f => f.Key == item.fileID).Select(f => f.Value).FirstOrDefault();
            
            foreach (var item in fileEntities.Where(x => x.EntityItem.ShoppingDate.Equals(DateTime.MinValue)))
                item.EntityItem.ShoppingDate = logs.Where(f => f.Key == item.fileID).Select(f => f.Value).FirstOrDefault();
            
            IEnumerable<MainEntity.Models.File.FileEntity> model = null;

            if (s == MyLibraryClassSortEnum.Code_asc)
                model = fileEntities.OrderBy(f => f.EntityItem.FileClassEntity.CatalogItemExtend.code.Substring(3));
            else if (s == MyLibraryClassSortEnum.Code_desc)
                model = fileEntities.OrderByDescending(f => f.EntityItem.FileClassEntity.CatalogItemExtend.code.Substring(3));
            else if (s == MyLibraryClassSortEnum.Speaker_asc)
                model = fileEntities.OrderBy(f => f.EntityItem.FileClassEntity.SpeakerEntityItem.title);
            else if (s == MyLibraryClassSortEnum.Speaker_desc)
                model = fileEntities.OrderByDescending(f => f.EntityItem.FileClassEntity.SpeakerEntityItem.title);
            else if (s == MyLibraryClassSortEnum.Title_asc)
                model = fileEntities.OrderBy(f => f.EntityItem.FileClassEntity.ClassEntityItem.title);
            else if (s == MyLibraryClassSortEnum.Title_desc)
                model = fileEntities.OrderByDescending(f => f.EntityItem.FileClassEntity.ClassEntityItem.title);
            else if (s == MyLibraryClassSortEnum.Date_asc)
                model = fileEntities.OrderBy(f => f.EntityItem.ShoppingDate);
            else model = fileEntities.OrderByDescending(f => f.EntityItem.ShoppingDate);


            return View(model.Skip(startRowIndex).Take(maxRowCount).ToArray());
        }

        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult MyLibraryList()
        {
            string userName = this.HttpContext.User.Identity.Name;
            var provider = this.HttpContext.GetCurrentMembershipProvider();
            MembershipUser user = provider.GetUser(userName, false);
            Guid userID = (Guid)user.ProviderUserKey;
            var myUser = _userService.GetUser((Guid)user.ProviderUserKey);

            //KeyValuePair<long, DateTime>[] freeFilesIDs =
            //    _shoppingService.SelectLibraryItemsIDsWithShoppingDate(userID);

            //var fileEntities = _fileService.SelectFilesByIDs(freeFilesIDs.Select(f => f.Key).ToArray());

            var shoppings = _shoppingService.SelectLibraryItemsIDsWithShoppingDate(userID);//, startRowIndex, maxRowCount);

            var genMem = _shoppingService.GetMembership(userID);
            var m = _shoppingService.GetMembershipAndActiveSubscribe(userID, DateTime.Now);
            DateTime dateLimit = genMem != null && genMem.fullLibraryAccess ? new DateTime(2002, 1, 1) :
                m == null || !m.subscribeActivation.HasValue ? DateTime.Now.AddMonths(-2) : m.subscribeActivation.Value;

            var logs = _activityLogService.SelectLibraryItemsIDsWithShoppingDate(userID, new ActivityLogTypeEnum[] { ActivityLogTypeEnum.DownloadClass }, dateLimit);//, startRowIndex, maxRowCount);

            var fileEntities = new List<MainEntity.Models.File.FileEntity>();
            for (int i = 0; i < shoppings.Length; i += 100)
                fileEntities.AddRange(_fileService.SelectFilesByIDs(shoppings.Select(f => f.Key).Skip(i).Take(100).ToArray()));


            for (int i = 0; i < logs.Length; i += 100)
                fileEntities.AddRange(_fileService.SelectFilesByIDs(logs.Select(f => f.Key).Skip(i).Take(100).ToArray(), fileEntities));


            return View(fileEntities.OrderBy(x => x.EntityItem.FileClassEntity.CatalogItemExtend.code.Substring(3)).ToArray()); //.Select(f => f.EntityItem.FileClassEntity.ClassEntityItem.title).ToArray());
        }

        public ActionResult IncorrectLogin(LogOnModel model, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (model == null)
                model = new LogOnModel();
            return View(model);
        }


        #region Edit profile

        [HttpGet]
        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult Profile()
        {
            var model = GetProfileModel();

            return View(model);
        }

        [HttpGet]
        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult ProfileConfirmation(string crypt)
        {
            UTF8Encoding enc = new UTF8Encoding();

            byte[] b = HttpServerUtility.UrlTokenDecode(crypt);
            b = MyUtils.DecodeBytes(b);
            var userID = enc.GetString(b, 1, b[0]);
            var email = enc.GetString(b, b[0] + 2, b[b[0] + 1]);

            var curProvider = this.HttpContext.GetCurrentMembershipProvider();
            MembershipUser user = curProvider.GetUser(User.Identity.Name, false);

            if (new Guid(userID) == (Guid)user.ProviderUserKey)
            {
                user.Email = email;
                curProvider.UpdateUser(user);
            }
            else
            {
                // TODO ошибка
            }


            return View();
        }

        private EditProfileModel GetProfileModel()
        {
            var curProvider = this.HttpContext.GetCurrentMembershipProvider();
            var aspnetUser = curProvider.GetUser(this.HttpContext.User.Identity.Name, false);
            var user = UserService.GetUser((Guid)aspnetUser.ProviderUserKey);
            var cart = UserService.GetLastMembershipCard(user.UserId);

            EditProfileModel model = new EditProfileModel(user.UserId, user.firstName, user.lastName, user.Email, user.UserName, cart != null ? cart.membershipCartID : null, cart != null ? cart.expirationDate : null)
            {
                City1 = user.city,
                Country1 = user.country,
                DayPhone1 = user.dayPhone,
                Phone1 = user.phone,
                PostalAddress1 = user.postalAdderss,
                PostalCode1 = user.postalCode,
                State1 = user.state
            };

            return model;
        }

        [HttpPost]
        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult ChangePasswordProfile(ChangePasswordModel password_model)
        {
            ChangePassword(password_model, this.ModelState);

            if (this.ModelState.IsValid)
            {
                return this.RedirectToAction("ProfileUpdateSuccessfull");
            }
            else
            {
                var model = GetProfileModel();
                return View("Profile", model);
            }
        }

        [HttpPost]
        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult Profile(EditProfileModel profile)
        {
            if (ModelState.IsValid)
            {
                var curProvider = this.HttpContext.GetCurrentMembershipProvider();
                var aspnetUser = curProvider.GetUser(this.HttpContext.User.Identity.Name, false);

                UserService.UpdateProfile((Guid)aspnetUser.ProviderUserKey, profile.FirstName, profile.LastName,
                    profile.City1, profile.Country1, profile.DayPhone1, profile.Phone1, profile.PostalAddress1,
                    profile.PostalCode1, profile.State1);


                bool isEmailUniqueForApp = UserService.IsEmailUniqueForApplication(curProvider.ApplicationName, profile.Email);
                if (aspnetUser.Email != profile.Email)
                {
                    if (isEmailUniqueForApp)
                    {
                        UTF8Encoding enc = new UTF8Encoding();

                        List<byte> resBytes = new List<byte>();
                        var userIdBytes = enc.GetBytes(aspnetUser.ProviderUserKey.ToString());
                        var mailBytes = enc.GetBytes(profile.Email.ToString());
                        resBytes.Add((byte)userIdBytes.Length);
                        resBytes.AddRange(userIdBytes);
                        resBytes.Add((byte)mailBytes.Length);
                        resBytes.AddRange(mailBytes);
                        string crypt = HttpServerUtility.UrlTokenEncode(MyUtils.EncodeBytes(resBytes.ToArray()));

                        string link = "http://" + HttpContext.Request.ServerVariables["HTTP_HOST"] + Url.Action("ProfileConfirmation", "Account") + "?crypt=" + crypt;
                        string body = string.Format(Resources.MailMessage.EditProfileMessageBody, User.Identity.Name, link);

                        MailMessage message = new MailMessage(Resources.MailMessage.FromMailAddress, profile.Email,
                            Resources.MailMessage.EditProfileMessageSubject, body);
                        SubscribeDaemon.AddMessage(message);
                    }
                    else
                    {
                        string errorMsg = "An account with this email address already exists.";
                        this.ModelState.AddModelError("", errorMsg);
                    }
                }


                if (aspnetUser.UserName != profile.UserName)
                {
                    bool isUserNameUniqueForApp = _userService.IsUserNameUniqueForApplication(curProvider.ApplicationName, profile.UserName);
                    if (isUserNameUniqueForApp)
                    {
                        _userService.UpdateUserName((Guid)aspnetUser.ProviderUserKey, profile.UserName);
                        _formsService.SignOut();
                        _formsService.SignIn(profile.UserName, curProvider.ApplicationName, this.Response.Cookies);
                    }
                    else
                    {
                        ModelState.AddModelError("", "An account with this username already exists.");
                    }
                }
            }

            if (this.ModelState.IsValid)
                return this.RedirectToAction("ProfileUpdateSuccessfull");



            return View(profile);
        }

        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult ProfileUpdateSuccessfull()
        {
            return this.View();
        }

        [HttpGet]
        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult ChangeCreditCard()
        {
            var creditCard = this.UserService.GetLastMembershipCard(this.MembershipUser.UserId);

            var model = new ChangeCreditCard();
            if (creditCard != null)
            {
                model.CurrentExpirationDate = creditCard.expirationDate;
                model.CurrentMembershipCartID = creditCard.membershipCartID;
            };

            model.City = this.MembershipUser.city;
            model.Country = this.MembershipUser.country;
            model.Email = this.MembershipUser.Email;
            model.FirstName = this.MembershipUser.firstName;
            model.LastName = this.MembershipUser.lastName;
            model.Phone = this.MembershipUser.phone;
            model.PostalAddress = this.MembershipUser.postalAdderss;
            model.PostalCode = this.MembershipUser.postalCode;
            model.State = this.MembershipUser.state;

            return View(model);
        }

        [HttpPost]
        [RequiresAuthorize, AllowAuthorizePortal]
        public ActionResult ChangeCreditCard(ChangeCreditCard model)
        {
            var creditCard = this.UserService.GetLastMembershipCard(this.MembershipUser.UserId);

            if (this.ModelState.IsValid)
            {
                var sjLog = new StringBuilder();
                try
                {
                    var cardID = model.CreditCardNumber.Substring(model.CreditCardNumber.Length - 4);
                    var expirationDate = new DateTime(model.ExpirationDateYear, model.ExpirationDateMonth, 1);
                    string chargeType = ShoppingTransactionTypeEnum.authorize.ToString();
                    var oldCardID = creditCard != null ? creditCard.membershipCartID : null;
                    var addresses = _userService.InsertAddesses(this.MembershipUser.UserId, model.FirstName, model.LastName, model.Country, model.State, model.City, model.PostalAddress, model.PostalCode, model.Phone, null, null);
                    _userService.InsertOrUpdateMembershipCard(this.MembershipUser.UserId, expirationDate, model.CreditCard, cardID, CartStateEnum.Active);
                    _shoppingService.CreditCardAuthorize(addresses, cardID, this.MembershipUser.UserId,
                        (transaction_id) => AuthorizeCreditCard(
                                transaction_id, model.FirstName, model.LastName, model.Email,
                                model.PostalAddress, model.City, model.State, model.PostalCode, model.Country,
                                model.Phone, long.Parse(model.CreditCardNumber), expirationDate, this.MembershipUser.UserId, cardID, oldCardID, sjLog, this.UserService));

                    Mailer.AddMessage(Mailer.OnSomeoneChangeCreditCard(
                        Properties.Settings.Default.FromEmailNotification, this.MembershipUser.Email, model.FirstName + " " + model.LastName));


                    return View("ChangeCreditCardSuccessfull");
                }
                catch (SJUnhandledException)
                {
                    Mailer.AddMessage(Mailer.SkipjackFailsBecauseNoResponseInGivenTime(
                        Properties.Settings.Default.FromEmailNotification, Properties.Settings.Default.AdminEmailSubscribeNotification, sjLog.ToString()));

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


        #region RegisterCustom

        [HttpGet]
        public ActionResult RegisterCustom(string returnUrl)
        {
            //Url.Action("RegisterCustom", "Account", new RouteValueDictionary { { "returnUrl", Request.Url.AbsoluteUri } });
            ActionResult rval;
            var model = new RegisterCustomModel();
            ViewData["returnUrl"] = returnUrl;

            if (string.Compare(returnUrl, "/search/free", true) == 0)
                rval = View("RegisterFree", model);
            else
                rval = View(model);

            return rval;
        }

        [HttpPost]
        public ActionResult LogOnPasswordProtection(LogOnModel model, string returnUrl)
        {
            if (!string.IsNullOrEmpty(this.HttpContext.GetCurrentPortal().passwordProtection))
            {
                var membershipProvider = this.HttpContext.GetCurrentMembershipProvider();
                var userName = string.Format(Properties.Settings.Default.PasswordProtectionUserName, this.HttpContext.GetCurrentPortal().portalID);

                if (ModelState.IsValid && _membershipService.ValidateUser(userName, model.Password, membershipProvider))
                {
                    _formsService.SignIn(userName, membershipProvider.ApplicationName, this.Response.Cookies);

                    return string.IsNullOrEmpty(returnUrl) ? RedirectToAction("Index", "Home") : (ActionResult)Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "The password provided is incorrect.");
                    return RedirectToAction("RegisterCustom");
                }
            }
            else return RedirectToAction("LogOn", "Account");
        }


        private string GeneratePassw(int length)
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, length);
        }

        [HttpPost]
        [AllowRegisterPortal]
        public ActionResult RegisterCustom(string returnUrl, RegisterCustomModel model)
        {
            this.HttpContext.SetValue(SessionEnum.ReturnUrl, returnUrl);
            if (ModelState.IsValid)
            {
                var curProvider = this.HttpContext.GetCurrentMembershipProvider();
                string autoPassw = GeneratePassw(8);
                try
                {
                    var newUser = _userService.CreateUser(curProvider, model.Email, autoPassw, false,
                        model.FirstName, model.LastName, 2, null, null, null, null, null, null, null, false);

                    UTF8Encoding encoding = new UTF8Encoding();

                    List<byte> resBytes = new List<byte>();
                    resBytes = new List<byte>();
                    resBytes = AddDataToByteArray(autoPassw, resBytes);
                    resBytes = AddDataToByteArray(model.Email, resBytes);

                    string link = "http://" + HttpContext.Request.ServerVariables["HTTP_HOST"] + Url.Action("RegisterCustomAuthorize", "Account") + "?e=" + HttpServerUtility.UrlTokenEncode(resBytes.ToArray());
                    Mailer.AddMessage(Mailer.OnRegisterFreeUser(Resources.MailMessage.FromMailAddress, model.Email, model.FirstName + " " + model.LastName, autoPassw, link),
                        Properties.Settings.Default.AdminEmailSubscribeNotification);
                    return View("RegisterCustomInfo", model);
                }
                catch (ArgumentException)
                {
                    return RedirectToAction("CheckoutLogin", "Shopping");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "Some fields are incorrect.");
            }

            ActionResult rval = null;
            ViewData["returnUrl"] = returnUrl;
            if (string.Compare(returnUrl, "/search/free", true) == 0)
                rval = View("RegisterFree", model);
            else
                rval = View(model);

            return rval;
        }

        [HttpGet]
        [AllowRegisterPortal]
        public ActionResult RegisterCustomInfo(RegisterCustomModel model)
        {


            return View(model);
        }

        [HttpGet]
        [AllowRegisterPortal]
        public ActionResult RegisterCustomAuthorize(string e)
        {
            try
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] b = HttpServerUtility.UrlTokenDecode(e);
                string passw = encoding.GetString(b, 1, b[0]);
                string email = encoding.GetString(b, b[0] + 2, b[b[0] + 1]);

                var curProvider = this.HttpContext.GetCurrentMembershipProvider();
                if (curProvider.ChangePassword(email, passw, passw))
                {
                    ViewData["email"] = email;
                    ViewData["old_password"] = passw;
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect password.");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Incorrect code in url.");
            }

            return View();
        }

        [HttpPost]
        [AllowRegisterPortal]
        public ActionResult RegisterCustomAuthorize(string email, string old_password, string password, string confirm_password, string came_from,
            string fill_in)
        {
            var curProvider = this.HttpContext.GetCurrentMembershipProvider();
            var aspnetUser = curProvider.GetUser(email, false);


            if (aspnetUser != null)
            {
                string errorChangePassword = "Cannot confirm authorize.";
                bool isChange = false;
                try
                {
                    isChange = aspnetUser.ChangePassword(old_password, password);
                }
                catch (ArgumentException)
                {
                    errorChangePassword = "Password must be longer than 6 characters.";
                }
                catch (Exception ex)
                {
                    errorChangePassword = ex.Message;
                }

                if (isChange)
                {
                    aspnetUser.Comment = came_from + " " + fill_in;
                    aspnetUser.IsApproved = true;
                    curProvider.UpdateUser(aspnetUser);
                    _formsService.SignIn(aspnetUser.UserName, curProvider.ApplicationName, this.Response.Cookies);
                    string returnUrl = HttpContext.GetValue(SessionEnum.ReturnUrl, null);
                    if (returnUrl != null)
                    {
                        HttpContext.SetValue(SessionEnum.ReturnUrl, null);
                        Response.Redirect(returnUrl);
                    }
                    if (_shoppingService.SelectLibraryItemsCount((Guid)aspnetUser.ProviderUserKey) > 0)
                        return RedirectToAction("MyLibrary");
                    else
                        return RedirectToAction("RegisterCustomAuthorizeSuccess");
                }
                else
                {
                    ModelState.AddModelError("", errorChangePassword);
                    ViewData["email"] = email;
                    ViewData["old_password"] = old_password;
                    return View();
                }

            }
            else
            {
                ModelState.AddModelError("", "Some error in create user.");
                ViewData["email"] = email;
                ViewData["old_password"] = old_password;
                return View();
            }
        }

        [HttpGet]
        [AllowRegisterPortal]
        public ActionResult RegisterCustomAuthorizeSuccess()
        {
            return View();
        }

        [HttpGet]
        [AuthorizedOnlyPortal]
        public ActionResult FreeReferredBy()
        {

            return View();
        }

        [HttpGet]
        [AllowRegisterPortal]
        public ActionResult RegisterCustomMembershipValidation(string email, string first_name, string last_name)
        {
            var model = new RegisterCustomModel(first_name, last_name, email);
            return View(model);
        }

        [HttpPost]
        [AllowRegisterPortal]
        public ActionResult RegisterCustomValidateMembership(RegisterCustomModel model)
        {
            if (!string.IsNullOrEmpty(model.Password))
            {
                var curProvider = this.HttpContext.GetCurrentMembershipProvider();
                var user = curProvider.GetUser(model.Email, false);
                bool isValid = (user != null && user.ChangePassword(model.Password, model.Password));

                if (isValid)
                {
                    List<byte> resBytes = new List<byte>();
                    resBytes = AddDataToByteArray(model.Password, resBytes);
                    resBytes = AddDataToByteArray(user.Email, resBytes);

                    return RedirectToAction("RegisterCustomAuthorize", "Account", new { e = HttpServerUtility.UrlTokenEncode(resBytes.ToArray()) });
                }
                else
                {
                    ViewData["ValidateMembershipError"] = "The password you entered is incorrect.";
                }
            }
            else
            {
                ViewData["ValidateMembershipError"] = "You not enter password.";
            }

            return View("RegisterCustomMembershipValidation", model);
        }

        [HttpPost]
        [AllowRegisterPortal]
        public ActionResult RegisterCustomResendRegistrationCode(RegisterCustomModel model)
        {
            var curProvider = this.HttpContext.GetCurrentMembershipProvider();
            string autoPassw = GeneratePassw(8);
            var user = curProvider.GetUser(model.Email, false);

            if (user == null)
            {
                try
                {
                    var newUser = _userService.CreateUser(curProvider, model.Email, autoPassw, false,
                        string.Empty, string.Empty, 2, null, null, null, null, null, null, null, false);

                    UTF8Encoding encoding = new UTF8Encoding();

                    List<byte> resBytes = new List<byte>();
                    resBytes = AddDataToByteArray(autoPassw, resBytes);
                    resBytes = AddDataToByteArray(model.Email, resBytes);

                    string link = "http://" + HttpContext.Request.ServerVariables["HTTP_HOST"] + Url.Action("RegisterCustomAuthorize", "Account") + "?e=" + HttpServerUtility.UrlTokenEncode(resBytes.ToArray());
                    Mailer.AddMessage(Mailer.OnRegisterFreeUser(Resources.MailMessage.FromMailAddress, model.Email, model.FirstName + " " + model.LastName, autoPassw, link),
                        Properties.Settings.Default.AdminEmailSubscribeNotification);

                    return View("RegisterCustomInfo", model);

                }
                catch (Exception ex)
                {
                    ViewData["ResendRegistrationCodeError"] = ex.Message;
                    return RedirectToAction("RegisterCustomMembershipValidation", new { email = model.Email, first_name = model.FirstName, last_name = model.LastName });
                }

            }
            else if (!user.IsApproved)
            {
                var temp = user.ResetPassword();
                if (user.ChangePassword(temp, autoPassw))
                {
                    UTF8Encoding encoding = new UTF8Encoding();

                    List<byte> resBytes = new List<byte>();
                    resBytes = AddDataToByteArray(autoPassw, resBytes);
                    resBytes = AddDataToByteArray(model.Email, resBytes);

                    string link = "http://" + HttpContext.Request.ServerVariables["HTTP_HOST"] + Url.Action("RegisterCustomAuthorize", "Account") + "?e=" + HttpServerUtility.UrlTokenEncode(resBytes.ToArray());
                    Mailer.AddMessage(Mailer.OnRegisterFreeUser(Resources.MailMessage.FromMailAddress, model.Email, model.FirstName + " " + model.LastName, autoPassw, link),
                        Properties.Settings.Default.AdminEmailSubscribeNotification);
                }

                return View("RegisterCustomInfo", model);
            }

            return View("RegisterCustomMembershipValidation", model);
        }

        #endregion


        #region Register

        [HttpGet]
        [AllowRegisterPortal]
        public ActionResult Register()
        {
            var model = new RegisterModel();

            return View(model);
        }

        [HttpPost]
        [AllowRegisterPortal]
        public ActionResult Register(RegisterModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.ActivityLogService.LoggingUserEmail(MainCommon.ActivityLogTypeEnum.SpecialOfferRegisterEmail, this.Request.UserHostAddress, model.Email,
                    this.AspnetpUser != null ? (Guid?)AspnetpUser.ProviderUserKey : null);

                return RedirectToAction("Offerings", new { email = model.Email });
            }

            return View(model);
        }

        #endregion


        [HttpGet, AuthorizedOnlyPortal]
        public ActionResult Offerings(string email)
        {
            long subscribeID = ShoppingService.GetSubscribeInCartID(System.Web.HttpContext.Current);
            var subscribe = subscribeID == 0 ? null : UserService.GetSubscribePlan(subscribeID);

            if (subscribe == null)
                return View();
            else
                return RedirectToAction("Checkout", new { email = email });
        }


        #region Checkout

        [HttpGet, AuthorizedOnlyPortal]
        public ActionResult Checkout(string email, bool add_monthly_membership = false)
        {
            long cartSubscribeID;

            if (add_monthly_membership)
            {
                if (this.Subscriber == null)
                {
                    cartSubscribeID = long.Parse(ConfigurationManager.AppSettings["MonthlyMembershipSubscribeID"]);
                    if (this.MembershipUser != null && this.MembershipUser.subscribeActivation != null)
                        cartSubscribeID = _userService.GetNexSubscribePlan(cartSubscribeID);

                    _shoppingService.AddItemToShoppingCart(this.MembershipUser != null ? (Guid?)this.MembershipUser.UserId : null,
                        cartSubscribeID, CartItemTypeEnum.Subscribe, System.Web.HttpContext.Current);
                    System.Web.HttpContext.Current.Profile.Save();

                    TempData["already_member"] = false;
                }
                else
                {
                    cartSubscribeID = 0;
                    TempData["already_member"] = true;
                }
            }
            else cartSubscribeID = ShoppingService.GetSubscribeInCartID(System.Web.HttpContext.Current);

            decimal balance = 0;

            var productIDs = ShoppingService.GetClassProductInCartIDs(System.Web.HttpContext.Current);
            var unitsID = ShoppingService.GetUnitsInCart(System.Web.HttpContext.Current);

            if (this.MembershipUser == null && UserExist(email)) return RedirectToAction("CheckoutLogin", "Shopping");

            if (this.MembershipUser != null && this.MembershipUser.balance > 0)
            {
                var completeProducts = _shoppingService.ShoppingAtUnits(productIDs, this.MembershipUser.UserId, DateTime.UtcNow, (decimal)Properties.Settings.Default.SubscriberDiscount, Properties.Settings.Default.MaxUnitsOnSubscribe);
                productIDs = productIDs.Remove(completeProducts);
                foreach (var productID in completeProducts)
                    _shoppingService.DeleteItemFromShoppingCart(CartItemTypeEnum.Class, System.Web.HttpContext.Current, productID);
                System.Web.HttpContext.Current.Profile.Save();

                if (productIDs.Length == 0 && cartSubscribeID == 0 && unitsID == 0) return this.RedirectToAction("MyLibrary");
                balance = UserService.GetUser(this.MembershipUser.UserId).balance;
            }


            var totalIDs = ShoppingService.GetAllProductsInCart(System.Web.HttpContext.Current);
            var info = _shoppingService.GetShoppingTransactionInfo(totalIDs, this.MembershipUser != null ? this.MembershipUser.UserId : Guid.Empty, false, (decimal)Properties.Settings.Default.SubscriberDiscount, Properties.Settings.Default.MaxUnitsOnSubscribe);

            CheckoutModel model;
            object tempModel = null;
            if (this.TempData.TryGetValue("CheckoutModel", out tempModel))
            {
                model = (CheckoutModel)tempModel;
                model.Membership = this.MembershipUser;
                model.TransactionInfo = info;
            }
            else
                model = new CheckoutModel() { Membership = this.MembershipUser, TransactionInfo = info };

            return View(model);
        }

        [HttpPost, AuthorizedOnlyPortal]
        public ActionResult Checkout(CheckoutModel checkout_model)
        {
            if (UserExist(checkout_model.Email)) return RedirectToAction("CheckoutLogin", "Shopping");

            if (ModelState.IsValid)
            {
                object tempModel = null;
                if (this.TempData.TryGetValue("EnterCCInfoModel", out tempModel))
                    TempData["EnterCCInfoModel"] = null;

                if (this.TempData.TryGetValue("CheckoutModel", out tempModel))
                    TempData["CheckoutModel"] = null;

                this.TempData.Add("CheckoutModel", checkout_model);
                return RedirectToAction("EnterCCInfo");
            }
            else
            {
                var totalIDs = new List<long>(ShoppingService.GetClassProductInCartIDs(System.Web.HttpContext.Current));
                totalIDs.Add(ShoppingService.GetSubscribeInCartID(System.Web.HttpContext.Current));
                totalIDs.Add(ShoppingService.GetUnitsInCart(System.Web.HttpContext.Current));

                checkout_model.TransactionInfo = _shoppingService.GetShoppingTransactionInfo(totalIDs.ToArray(), checkout_model.Authorized ? this.MembershipUser.UserId : Guid.Empty, false, (decimal)Properties.Settings.Default.SubscriberDiscount, Properties.Settings.Default.MaxUnitsOnSubscribe);
                //ModelState.AddModelError("", "Some fields are not filled or entered incorrectly.");
            }

            return View(checkout_model);
        }

        [HttpGet, AuthorizedOnlyPortal]
        public ActionResult EnterCCInfo()
        {
            object tempModel = null;
            if (!this.TempData.TryGetValue("CheckoutModel", out tempModel))
                return this.RedirectToAction("Checkout");
            var checkoutModel = (CheckoutModel)tempModel;
            TempData["CheckoutModel"] = checkoutModel;

            EnterCCInfoModel model;
            //if (!this.TempData.TryGetValue("EnterCCInfoModel", out tempModel))
                model = new EnterCCInfoModel(checkoutModel);
            //else
            //    model = (EnterCCInfoModel)tempModel;
            if (model.Error != null)
            {
                ModelState.AddModelError("", model.Error);
                model.Error = null;
            }
            var curProvider = this.HttpContext.GetCurrentMembershipProvider();
            var aspnetUser = curProvider.GetUser(this.HttpContext.User.Identity.Name, false);
            if (aspnetUser != null)
            {
                var user = UserService.GetUser((Guid)aspnetUser.ProviderUserKey);
                if (user != null)
                {
                    model.FirstName1 = user.firstName;
                    model.LastName1 = user.lastName;
                    model.City1 = user.city;
                    model.Country1 = user.country;
                    model.DayPhone1 = user.dayPhone;
                    model.Phone1 = user.phone;
                    model.PostalAddress1 = user.postalAdderss;
                    model.PostalCode1 = user.postalCode;
                    model.State1 = user.state;
                }
            }

            if (this.MembershipUser != null && this.MembershipUser.activatedCart)
            {
                var card = _userService.GetLastMembershipCard(this.MembershipUser.UserId);
                if (card != null)
                {
                    model.CreditCard = card.cartTypeID;
                    model.CreditCardNumber = string.Format("xxxx-xxxx-xxxx-{0}", card.membershipCartID);
                    model.ExpirationDateMonth = card.expirationDate.Value.Month;
                    model.ExpirationDateYear = card.expirationDate.Value.Year;
                    model.CreditCardValidated = true;
                }
            }

            return View(model);
        }

        [HttpPost, AuthorizedOnlyPortal]
        public ActionResult EnterCCInfo(EnterCCInfoModel model)
        {
            TempData["SuccessKey"] = true;
            TempData["CheckoutModel"] = new CheckoutModel(model);
            TempData["EnterCCInfoModel"] = model;

            if (ModelState.IsValid)
            {
                var curProvider = this.HttpContext.GetCurrentMembershipProvider();
                MembershipUser aspnetUser = curProvider.GetUser(this.HttpContext.User.Identity.Name, false);
                if (aspnetUser != null)
                {
                    var user = UserService.GetUser((Guid)aspnetUser.ProviderUserKey);
                    user.firstName = model.FirstName1;
                    user.lastName = model.LastName1;
                    user.city = model.City1;
                    user.country = model.Country1;
                    user.dayPhone = model.DayPhone1;
                    user.phone = model.Phone1;
                    user.postalAdderss = model.PostalAddress1;
                    user.postalCode = model.PostalCode1;
                    user.state = model.State1;
                    UserService.UpdateProfile(user.UserId, model.FirstName1, model.LastName1, model.City1, model.Country1, model.DayPhone1, model.Phone1, model.PostalAddress1, model.PostalCode1, model.State1);
                }
                return RedirectToAction("CheckoutConfirm");
            }
            else
                return View(model);
        }

        [HttpGet, AuthorizedOnlyPortal]
        public ActionResult CheckoutConfirm()
        {
            TempData["SuccessKey"] = true;

            object tempModel = null;
            if (!this.TempData.TryGetValue("CheckoutModel", out tempModel))
                return this.RedirectToAction("Checkout");
            var checkoutModel = (CheckoutModel)tempModel;
            TempData["CheckoutModel"] = checkoutModel;

            if (!this.TempData.TryGetValue("EnterCCInfoModel", out tempModel))
                return this.RedirectToAction("EnterCCInfo");
            var enterCCInfoModel = (EnterCCInfoModel)tempModel;
            TempData["EnterCCInfoModel"] = enterCCInfoModel;

            long[] productIds = ShoppingService.GetAllProductsInCart(System.Web.HttpContext.Current);
            var transactionInfo = _shoppingService.GetShoppingTransactionInfo(productIds, this.AspnetpUser != null ? (Guid)this.AspnetpUser.ProviderUserKey : Guid.Empty, false, (decimal)Properties.Settings.Default.SubscriberDiscount, Properties.Settings.Default.MaxUnitsOnSubscribe);

            var billingAddress = new MainEntity.Models.Shopping.ShoppingMembershipAddress()
            {
                addressID = 0,
                MembershipAddress = new MainEntity.Models.Shopping.MembershipAddress()
                {
                    isBillingAddress = true,
                    addressID = 0,

                    city = enterCCInfoModel.City1,
                    country = enterCCInfoModel.Country1,
                    dayPhone = enterCCInfoModel.DayPhone1,
                    description = enterCCInfoModel.Description1,
                    firstName = enterCCInfoModel.FirstName1,
                    lastName = enterCCInfoModel.LastName1,
                    phone = enterCCInfoModel.Phone1,
                    postalAdderss = enterCCInfoModel.PostalAddress1,
                    postalCode = enterCCInfoModel.PostalCode1,
                    state = enterCCInfoModel.State1,
                }
            };
            var shippingAddress = string.IsNullOrEmpty(enterCCInfoModel.PostalAddress2) ? null : new MainEntity.Models.Shopping.ShoppingMembershipAddress()
            {
                addressID = 0,
                MembershipAddress = new MainEntity.Models.Shopping.MembershipAddress()
                {
                    isBillingAddress = false,
                    addressID = 0,

                    city = enterCCInfoModel.City2,
                    country = enterCCInfoModel.Country2,
                    dayPhone = enterCCInfoModel.DayPhone2,
                    description = enterCCInfoModel.Description2,
                    firstName = enterCCInfoModel.FirstName2,
                    lastName = enterCCInfoModel.LastName2,
                    phone = enterCCInfoModel.Phone2,
                    postalAdderss = enterCCInfoModel.PostalAddress2,
                    postalCode = enterCCInfoModel.PostalCode2,
                    state = enterCCInfoModel.State2,
                }
            };

            var transaction = new MainEntity.Models.Shopping.UserShoppingTransaction()
            {
                amount = transactionInfo.Amount,
                shoppingTransactionID = 0
            };

            var model = new Main.Models.ControllerView.Account.DetailShoppingTransactions(new[] { transaction }, transactionInfo.ShoppingPrice,
                0, DateTime.UtcNow, new[] { billingAddress, shippingAddress });

            return View(model);
        }

        [HttpGet, AuthorizedOnlyPortal]
        public ActionResult CheckoutComplete()
        {
            TempData["SuccessKey"] = true;

            object tempModel = null;
            if (!this.TempData.TryGetValue("CheckoutModel", out tempModel))
                return this.RedirectToAction("Checkout");
            var checkoutModel = (CheckoutModel)tempModel;
            TempData["CheckoutModel"] = checkoutModel;

            if (!this.TempData.TryGetValue("EnterCCInfoModel", out tempModel))
                return this.RedirectToAction("EnterCCInfo");
            var model = (EnterCCInfoModel)tempModel;
            TempData["EnterCCInfoModel"] = model;

            long transactionID = 0;

            var curProvider = this.HttpContext.GetCurrentMembershipProvider();
            var user = this.AspnetpUser;
            var createUser = user == null;

            long[] productIds = ShoppingService.GetAllProductsInCart(System.Web.HttpContext.Current);
            long[] classesIds = ShoppingService.GetClassProductInCartIDs(System.Web.HttpContext.Current);
            long subscribePlaID = _shoppingService.GetSubscribeInCartID(System.Web.HttpContext.Current);

            try
            {
                if (createUser)
                    user = _userService.CreateUser(curProvider, model.Email, model.Password, true, model.FirstName1, model.LastName1, 2,
                        model.Country1, model.State1, model.City1, model.PostalAddress1, model.PostalCode1, model.Phone1, model.DayPhone1, true);
                else
                    model.Email = user.Email;

                var transactionInfo = _shoppingService.GetShoppingTransactionInfo(productIds, (Guid)user.ProviderUserKey, false, (decimal)Properties.Settings.Default.SubscriberDiscount, Properties.Settings.Default.MaxUnitsOnSubscribe);
                var card = _userService.GetLastMembershipCard((Guid)user.ProviderUserKey);
                var addresses = _userService.InsertAddesses((Guid)user.ProviderUserKey,
                    model.FirstName1, model.LastName1, model.Country1, model.State1, model.City1, model.PostalAddress1, model.PostalCode1, model.Phone1, model.DayPhone1, model.Description1,
                    model.FirstName2, model.LastName2, model.Country2, model.State2, model.City2, model.PostalAddress2, model.PostalCode2, model.Phone2, model.DayPhone2, model.Description2);

                try
                {
                    transactionID = Purchase(model, user, createUser, transactionInfo, card, addresses);
                }
                catch
                {
                    if (createUser) _userService.LockUser((Guid)user.ProviderUserKey);
                    throw;
                }

                if (createUser)
                {
                    _formsService.SignIn(user.UserName, curProvider.ApplicationName, this.Response.Cookies);

                    if (subscribePlaID > 0)
                        Mailer.AddMessage(Mailer.OnRegisterMonthlyUser(Resources.MailMessage.FromMailAddress, model.Email, model.FirstName1 + " " + model.LastName1, model.Password),
                            Properties.Settings.Default.AdminEmailSubscribeNotification);
                    else
                        Mailer.AddMessage(Mailer.OnRegisterFreeUserWithoutEmailValidate(Resources.MailMessage.FromMailAddress, model.Email, model.FirstName1 + " " + model.LastName1, model.Password),
                            Properties.Settings.Default.AdminEmailSubscribeNotification);

                }
            }
            catch (SJTimeoutException e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("EnterCCInfo", model);
            }
            catch (SJDeclineException e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("EnterCCInfo", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "Your credit card could not be authenticated. Please check the information and try again.");//ex.Message);
                model.Error = "Your credit card could not be authenticated. Please check the information and try again." + (ex.InnerException != null ? " " + ex.InnerException.Message : "");
                return RedirectToAction("EnterCCInfo", model);
            }
            _shoppingService.EmptyCart(System.Web.HttpContext.Current);
            //if file was purchased then redirect to My Labrary
            var products = ClassService.GetProductsList(productIds);
            if (products.Count(prod => prod.productTypeID == (short)ProductTypeEnum.File) > 0)
            {
                return RedirectToAction("MyLibrary");
            }
            return RedirectToAction("Index");
        }

        #endregion


        #region ForgotPassword

        [HttpGet, AllowAuthorizePortal]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost, AllowAuthorizePortal]
        public ActionResult ForgotPassword(string email)
        {
            string appName = this.HttpContext.GetCurrentMembershipProvider().ApplicationName;
            var users = UserService.GetUsers(appName, 0, 10000, null, email, null, null, null, null, null, null, null, null, null).ToList();
            var curProvider = this.HttpContext.GetCurrentMembershipProvider();

            ViewData["ForgotPasswordStatus"] = "NotFoundAccount";

            foreach (var userItem in users)
            {

                MembershipUser user = curProvider.GetUser(userItem.UserName, false);
                var userMembership = UserService.GetUser((Guid)user.ProviderUserKey);

                if (userMembership != null)
                {
                    string newPassw = GeneratePassw(8);
                    user.ChangePassword(user.ResetPassword(), newPassw);
                    string body = string.Format(Resources.MailMessage.ForgotPasswordBody, userMembership.firstName, userMembership.lastName,
                        userMembership.UserName, newPassw);
                    MailMessage message = new MailMessage(Resources.MailMessage.FromMailAddress, email,
                        Resources.MailMessage.ForgotPasswordSubject, body);
                    SubscribeDaemon.AddMessage(message);

                    ViewData["ForgotPasswordStatus"] = "FoundAccount";
                }
            }



            return View("ForgotPassword");
        }

        #endregion


        #region Private methods

        private bool UserExist(string email)
        {
            bool userIsPresent = false;
            if (!string.IsNullOrEmpty(email))
            {
                var curProvider = this.HttpContext.GetCurrentMembershipProvider();
                userIsPresent = curProvider.GetUser(email, false) != null;
            }
            return userIsPresent;
        }


        private List<byte> AddDataToByteArray(string data, List<byte> bytes)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            var b = encoding.GetBytes(data);
            bytes.Add((byte)b.Length);
            bytes.AddRange(b);

            return bytes;
        }

        #endregion
    }
}
