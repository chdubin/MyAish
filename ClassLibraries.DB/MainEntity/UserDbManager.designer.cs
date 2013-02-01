using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data.Linq;
using MainEntity.Models.User;

namespace MainEntity
{
    public partial class UserDbManager : EntityItemDbManager
    {
        public UserDbManager(string configuartion_name)
            : base(configuartion_name)
        {
        }

        public Table<aspnet_Membership> AspnetMembershipItems { get { return this.GetTable<aspnet_Membership>(); } }

        public Table<aspnet_User> AspnetUserItems { get { return this.GetTable<aspnet_User>(); } }

        public Table<aspnet_Application> AspnetAppItems { get { return this.GetTable<aspnet_Application>(); } }

        public Table<aspnet_Role> AspnetRoles { get { return this.GetTable<aspnet_Role>(); } }

        public Table<aspnet_UsersInRole> AspnetUsersInRoles { get { return this.GetTable<aspnet_UsersInRole>(); } }


        public Table<EntityItem> EntityItems { get { return this.GetTable<EntityItem>(); } }

        public Table<Membership> MembershipItems { get { return this.GetTable<Membership>(); } }

        public Table<MembershipXrefSubscribePlan> MembershipXSubscrPlanItems { get { return this.GetTable<MembershipXrefSubscribePlan>(); } }

        public Table<MembershipAddress> MembershipAddressItems { get { return this.GetTable<MembershipAddress>(); } }

        public Table<ShoppingMembershipAddress> ShoppingMembershipAddreses { get { return this.GetTable<ShoppingMembershipAddress>(); } }

        public Table<MembershipCart> MembershipCartsItems { get { return this.GetTable<MembershipCart>(); } }

        public Table<MembershipXrefReferrer> MembershipXReferrerItems { get { return this.GetTable<MembershipXrefReferrer>(); } }

        public Table<SubscribePlanEntity> SubscribePlanEntityItems { get { return this.GetTable<SubscribePlanEntity>(); } }

        public Table<SubscribePlanXref> SubscribePlanXrefItems { get { return this.GetTable<SubscribePlanXref>(); } }

        public Table<ProductEntity> ProductEntityItems { get { return this.GetTable<ProductEntity>(); } }

        public Table<MembershipCartState> MembershipCartStates { get { return this.GetTable<MembershipCartState>(); } }

    }
}

