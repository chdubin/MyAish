using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MainEntity.Models.User;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Main.Models.Base;

namespace Main.Areas.Admin.Models.User
{
    public class UserEditModel : ValidateModel
    {
        public List<SelectListItem> CountryList = new List<SelectListItem>();

        public long? PortalID { get; set; }

        public string UserName { get; set; }
        public string UserID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string City { get; set; }
        public string Address { get; set; }

        public string Country { get; set; }

        public string Description { get; set; }
        public string ReferrerCode { get; set; }
        public string PostalCode { get; set; }


        public string Phone { get; set; }
        public string DayPhone { get; set; }

        public string State { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Balance { get; set; }

        public int FreeOfferCnt { get; set; }
        public bool Suspended { get; set; }

        public bool FullLibraryAccess { get; set; }

        //public string MembershipType { get; set; }
        public long PlanID { get; set; }
        public string PlanName { get; set; }
        //public List<KeyValuePair<long, string>> PlansList { get; set; } 
        [DisplayName("months count")]
        public int PrepaidPlanMonths { get; set; }
        [DisplayName("units per month")]
        public int PrepaidPlanUnitsPerMonth { get; set; }

        public string StartDate { get; set; }
        //public string PaidThrough { get; set; }

        public List<string> Roles { get; set; }
        public List<string> UserRoles { get; set; }

        public UserEditModel()
        {

        }

        public UserEditModel(/*List<KeyValuePair<long, string>> plans_list,*/ string[] roles, List<string> user_roles = null)
        {
            InitPlanList(/*plans_list, */roles, user_roles);
        }


        public UserEditModel(Membership user, string[] roles, List<string> user_roles = null)
        {
            UserID = user.UserId.ToString();
            UserName = user.UserName;
            Password = user.Password;
            ConfirmPassword = user.Password;
            Email = user.Email;
            UserID = user.UserId.ToString();
            FirstName = user.firstName;
            LastName = user.lastName;
            FreeOfferCnt = user.freeOfferCnt;
            Suspended = user.suspended;
            FullLibraryAccess = user.fullLibraryAccess;

			City = user.city;
			Address = user.postalAdderss;
			Country = user.country;
			PostalCode = user.postalCode;
			Phone = user.phone;
			DayPhone = user.dayPhone;
			State = user.state;

            Description = user.Description;
            ReferrerCode = user.ReferrerCode;
            CreatedDate = user.CreatedDate;
            Balance = user.balance;

            StartDate = GetStringFromDate(user.StartSubscribeDate);
            //PaidThrough = GetStringFromDate(user.EndSubscribeDate);

            var subscribePlanDesc = (user.MembershipXrefSubscribePlan != null ? user.MembershipXrefSubscribePlan.SubscribePlanEntity.description : "0").Split(',');
            int subscribePlanMonths=0, freeUnitsOnNextSubscribe = 0;
            int.TryParse(subscribePlanDesc[0], out subscribePlanMonths);
            if (subscribePlanDesc.Length > 2) int.TryParse(subscribePlanDesc[2], out freeUnitsOnNextSubscribe);
            PlanID = user.PlanID;
            PlanName = user.PlanName;
            PrepaidPlanMonths = subscribePlanMonths;
            PrepaidPlanUnitsPerMonth = freeUnitsOnNextSubscribe;

            InitPlanList(/*user.PlansList, */roles, user_roles);

        }


        public void InitPlanList(/*List<KeyValuePair<long, string>> plans_list, */string[] roles, List<string> user_roles = null)
        {
            this.Roles = roles.ToList();
            this.UserRoles = user_roles ?? new List<string>();
            //this.PlansList = plans_list;
            //this.PlansList.Add(new KeyValuePair<long, string>(0, "None"));
            //this.PlansList = this.PlansList.OrderBy(s => s.Key).ToList();
        }


        private string GetStringFromDate(DateTime? date)
        {
            string str = null;
            try
            {
                str = date.Value.Month + "/" + date.Value.Day + "/" + date.Value.Year;
            }
            catch { }

            return str;
        }
          
    }
}
