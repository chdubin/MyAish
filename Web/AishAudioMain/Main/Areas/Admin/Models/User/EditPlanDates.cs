using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Areas.Admin.Models.User
{
    public class EditPlanDates
    {
        public Guid UserID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte? ChargeDay { get; set; }

        public EditPlanDates()
        {

        }

        public EditPlanDates(Guid user_id, DateTime? start_date, DateTime? end_date, byte? charge_day)
        {
            this.UserID = user_id;
            this.StartDate = start_date;
            this.EndDate = end_date;
            ChargeDay = charge_day;
        }

    }
}