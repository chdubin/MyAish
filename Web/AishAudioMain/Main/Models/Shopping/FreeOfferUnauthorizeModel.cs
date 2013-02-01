using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models.Shopping
{
	public class FreeOfferUnauthorizeModel : Main.Models.Base.ValidateModel
	{
		public string PromoCode { get; set; }

		public override bool IsValidatePassword { get { return false; } }
		public override bool IsValidateConfirmPassword { get { return false; } }
		public override bool IsRequiredEmail { get { return true; } }

	}
}