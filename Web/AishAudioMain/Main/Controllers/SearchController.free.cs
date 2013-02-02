using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Common;
using Main.Models.ControllerView.Search;
using Main.Common.Attributes;
using Main.Utilities;

namespace Main.Controllers
{
	public partial class SearchController
	{
        [MyRequireHttpsAttribute]
        [RequiresAuthorize]
		public ActionResult Free()
		{
			long currentPortalID = this.HttpContext.GetCurrentPortal().portalID;
			long[] classInCartIDs = ShoppingService.GetClassProductInCartIDs(System.Web.HttpContext.Current);

			var classes = ClassService.GetFreeClasses(Main.GlobalConstant.ROOT_ENTITY_ID, currentPortalID);
			var model = ClassListItem.GetForList(classes, classInCartIDs);

			var curProvider = this.HttpContext.GetCurrentMembershipProvider();
			var user = curProvider.GetUser(User.Identity.Name, false);
			var subscriber = ShoppingService.GetMembershipAndActiveSubscribe((Guid)user.ProviderUserKey, DateTime.Now);

			//if (subscriber == null)
			//    return this.RedirectToAction("Offerings", "Account");

			this.ViewData["ActiveSubscribe"] = subscriber != null;

			return View(model.ToArray());
		}
	}
}