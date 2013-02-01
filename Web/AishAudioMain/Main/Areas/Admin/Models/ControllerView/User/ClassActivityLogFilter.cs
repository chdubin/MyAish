using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Main.Areas.Admin.Models.ControllerView.User
{
	public class ClassActivityLogFilter
	{
        public enum TypeFilter
        {
            ShowAll=0,
            ShowDownloadOnly=1,
            ShowStreamingOnly=2,
        }

        public ClassActivityLogFilter()
        {
            fgrouping = true;
        }

		public Guid user_id { get; set; }

		[DisplayName("From:")]
		[DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "MM/dd/yyyy")]
		public DateTime? fsincedata { get; set; }

		[DisplayName("To:")]
		[DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "MM/dd/yyyy")]
		public DateTime? fbeforedata { get; set; }

        [DisplayName("Hide Duplicates:")]
        [DefaultValue(true)]
        public bool fgrouping { get; set; }

        [DefaultValue(TypeFilter.ShowAll)]
        public TypeFilter ftype { get; set; }

    }
}