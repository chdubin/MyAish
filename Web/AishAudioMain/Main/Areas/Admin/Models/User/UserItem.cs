using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainEntity.Models.User;

namespace Main.Areas.Admin.Models.User
{
    public class UserItem
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string ReferrerCode { get; set; }
        public bool Validated { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string DayPhone { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }

        public DateTime CreatedDate { get; set; }
        public decimal Credits { get; set; }       
        public string MembershipType { get; set; }

        public long PlanID { get; set; }

        public DateTime? SubscribeActivation { get; set; }

        public DateTime? StartSubscribeDate { get; set; }
        public DateTime? EndSubscribeDate { get; set; }
        public byte? ChargeDay { get; set; }

        public bool IsCancelSubscribe { get; set; }
		public bool IsDeclinedSubscribe { get; set; }
		public bool IsSuspended { get; set; }

        public string RefferedBy { get; set; }
		public string RefferedCode { get; set; }
		public string LastRefer { get; set; }

        public bool IsLockedOut { get; set; }

        public UserItem()
        {

        }

        public UserItem(Membership data)
        {
            Email = data.Email;
            UserName = data.UserName;
            CreatedDate = data.CreatedDate;

            //MembershipAddress billingAddres = data.MembershipAddresses == null ? null : data.MembershipAddresses
            //    .Where(x => x.isBillingAddress == true).OrderByDescending(x=>x.addressID).FirstOrDefault();
            //if (billingAddres != null)
            //{                
            Address = data.postalAdderss;
            Phone = data.phone;
            DayPhone = data.dayPhone;

            City = data.city;
            Country = data.country;
            PostalCode = data.postalCode;
            State = data.state;
            //}

            MembershipType = data.MembershipType == "" ? "None" : data.MembershipType; 
            Name = data.firstName + " " + data.lastName;
            ReferrerCode = data.ReferrerCode;
			RefferedBy = data.RefferedBy;
			LastRefer = data.LastRefer;
            //Validated = data.activatedCart;
            Validated = data.IsApproved;
            this.PlanID = data.PlanID;
            Credits = Math.Round(data.balance,0);
            SubscribeActivation = data.subscribeActivation;
            StartSubscribeDate = data.StartSubscribeDate;
            EndSubscribeDate = data.EndSubscribeDate;
            IsCancelSubscribe = data.IsCancelSubscribe;
            ChargeDay = data.chargeDay;
            UserID = data.UserId;
			IsDeclinedSubscribe = data.IsDeclinedSubscribe;
			IsSuspended = data.suspended;

            IsLockedOut = data.IsLockedOut;
        }

	}
}