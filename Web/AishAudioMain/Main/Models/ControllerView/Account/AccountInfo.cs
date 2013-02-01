using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models.ControllerView.Account
{
    public class AccountInfo
    {
        public string EMail { get; set; }
        public string UserName { get; set; }
        public decimal UnitsCount { get; set; }
        public string SubscribePlanName { get; set; }
        public DateTime? DateMembershipBegan { get; set; }
        public int FreeOffersCnt { get; set; }
        public bool IsCancelSubscribe { get; set; }
        public DateTime? CancelledOnDate { get; set; }
    }
}