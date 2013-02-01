using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Profile;
using MainEntity.Interfaces;
using Main.Models.Account;
using System.Text.RegularExpressions;
using Main.Common;
using MainCommon;
using Main.Models.ControllerView.Search;
using Main.Areas.Admin.Models.Common;
using Main.Common.Attributes;
using Main.Utilities;

namespace Main.Controllers
{
    [MyRequireHttpsAttribute]
    [AuthorizedOnlyPortal]
    public class HomeController : BaseController
    {
		//private ISpeakerService _speakerService;

		//public HomeController(ISpeakerService speaker_service)
		//{
		//    _speakerService = speaker_service;
		//}

		public ActionResult Index()
        {


            return View();
        }

        public ActionResult Results()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult SearchBig()
        {
            return View();
        }

		public ActionResult Free(int p = 1, int ps = 10, int rn = 0)
		{
			long currentPortalID = this.HttpContext.GetCurrentPortal().portalID;
			long[] classInCartIDs = ShoppingService.GetClassProductInCartIDs(System.Web.HttpContext.Current);
			int recordsCount;

			List<ClassListItem> model;

			recordsCount = this.ClassService.GetFullFreeClassesCnt(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID);
			var classes = this.ClassService.GetFullFreeClasses(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID, rn > 1 ? rn - 1 : (p - 1) * ps, ps);
			model = ClassListItem.GetForList(classes, classInCartIDs);

			this.ViewData["Pager"] = new PagingData(Request.Path, Request.QueryString, recordsCount, ps, p, rn - 1, 10);

			return View(model.ToArray());
		}

		[HttpPost]
		public ActionResult SubscribeNews(RegisterModel subscribe_email)
		{
			ActionResult rval;
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.ActivityLogService.LoggingUserEmail(MainCommon.ActivityLogTypeEnum.SubscribeToNewsOnEmail, this.Request.UserHostAddress, subscribe_email.Email,
                        this.AspnetpUser != null ? (Guid?)AspnetpUser.ProviderUserKey : null);


                    string from = string.Format(Resources.MailMessage.FromMailAddress);
                    string body = @"The easiest way to unsubscribe is to hit reply to this e-mail and write unsubscribe in the subject line.

Thank you for joining. We hope that aishaudio.com wisdom will give you a great lift.

Rabbi Aaron Dayan and the staff of aishaudio.com";
                    string subject = "Aishaudio.com subscription confirmation";

                    var userMessage = new System.Net.Mail.MailMessage(from, subscribe_email.Email, subject, body);
                    var adminMessage = new System.Net.Mail.MailMessage(from, Properties.Settings.Default.AdminEmailSubscribeNotification, subject, body);
                    MainCommon.Daemon.SubscribeDaemon.AddMessage(userMessage);
                    MainCommon.Daemon.SubscribeDaemon.AddMessage(adminMessage);

                    rval = this.View();
                }
                catch (Exception ex)
                {
                    rval = this.View(ex);
                }
            }
            else
            {
                rval = this.Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }

			return rval;
		}
    }
}
