using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Main.Areas.Admin.Models.User
{
    public class EditCredits
    {
        public Guid UserID { get; set; }
        public decimal Credits { get; set; }

        public EditCredits(Guid user_id, decimal credits)
        {
            UserID = user_id;
            Credits = credits;
        }
             
    }
}