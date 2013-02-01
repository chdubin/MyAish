using System;
using System.Collections.Generic;
namespace MainEntity
{
	partial class UserEntityDataContext
	{
	}
}

namespace MainEntity.Models.User
{
    public partial class Membership
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }

        public string Description { get; set; }

        public string ReferrerCode { get; set; }
		public string RefferedBy { get; set; }
		public string LastRefer { get; set; }

        //public MembershipAddress Address { get; set; }
        public DateTime CreatedDate { get; set; }

        public string MembershipType { get; set; }

        public DateTime? StartSubscribeDate { get; set; }
        public DateTime? EndSubscribeDate { get; set; }
        public bool IsCancelSubscribe { get; set; }
        public bool IsDeclinedSubscribe { get; set; }

        public long PlanID { get; set; }
        public string PlanName { get; set; }
        //public List<KeyValuePair<long, string>> PlansList;

        public long NextSubscribePlanID { get; set; }
        public DateTime? SubscribeActivation { get; set; }
        //public ProductEntity NextSubscribeProduct { get; set; }

        public bool IsLockedOut { get; set; }
    }
}
