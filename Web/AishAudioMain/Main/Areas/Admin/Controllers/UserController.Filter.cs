using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Areas.Admin.Models.ControllerView.User;
using Main.Common;

namespace Main.Areas.Admin.Controllers
{
	public partial class UserController
	{
		[ChildActionOnly]
		public ActionResult UserFilter(UserFilter filter)
		{
            //var plansList = this.HttpContext.Cache.GetValue(MainCommon.CacheEnum.SubscribePlanCache.ToString(),
            //    TimeSpan.FromSeconds(GlobalConstant.SUBSCRIBEPLAN_IN_CACHE_SEC), () => _userService.GetSubscribePlanEntities().ToArray());

			filter.Initialize();

			return PartialView(filter);
		}

        public ActionResult EditCredits(Guid user_id, decimal credits)
        {
            return PartialView(new Main.Areas.Admin.Models.User.EditCredits(user_id, credits));
        }

	}
}