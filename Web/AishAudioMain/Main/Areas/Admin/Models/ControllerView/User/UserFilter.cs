using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Common.Localize;
using MainCommon;

namespace Main.Areas.Admin.Models.ControllerView.User
{
	public class UserFilter
	{
		public UserFilter()
		{

		}

		[DisplayName("Email:")]
		public string semail { get; set; }

		[DisplayName("User name:")]
		public string susername { get; set; }

		[DisplayName("Fisrt name:")]
		public string sfirstname { get; set; }

		[DisplayName("Last name:")]
		public string slastname { get; set; }

		[DisplayName("Subscribe plan")]
        [DefaultValue(MembershipTypeEnum.AllMonthly)]
        public MembershipTypeEnum ssubscribe { get; set; }

        //[DisplayName("Subscribe plan")]
        //public long? splanid { get; set; }

        [DisplayName("Membership Start Date From")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "MM/dd/yyyy")]
        public DateTime? msdate { get; set; }

        [DisplayName("Until")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "MM/dd/yyyy")]
        public DateTime? medate { get; set; }

        [DisplayName("Cancelled monthly")]
        public bool scanceled { get; set; }

        [DisplayName("Monthly with declined credit card")]
        public bool sdeclined { get; set; }

        [DisplayName("To Charge in # Days")]
        public int? schargeindays { get; set; }

        [DisplayName("Show ONLY this charge day")]
        public bool schargeindaysexactly { get; set; }

        public SelectList SubscribePlan { get; set; }

		public void Initialize()//(KeyValuePair<long,string>[] subscribe_plans)
		{
            SubscribePlan = ssubscribe.ToSelectList();

            //var plans = new List<KeyValuePair<long, string>>(subscribe_plans);
            //plans.Insert(0, new KeyValuePair<long, string>(0, "All subscribe plan"));
            //plans.Insert(1, new KeyValuePair<long, string>(-1, "Free listening"));
            //SubscribePlan = new SelectList(plans, "Key", "Value", splanid);
		}

	}
}