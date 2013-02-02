using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Main.Areas.Admin.Models.ControllerView.Transaction
{
    public class ShoppingTransactionsFilter
    {
        [DisplayName("From:")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "MM/dd/yyyy")]
        public DateTime? fsincedata { get; set; }

        [DisplayName("To:")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "MM/dd/yyyy")]
        public DateTime? fbeforedata { get; set; }

    }
}