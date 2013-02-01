using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using MainEntity.Models.Shopping;

namespace MainEntity
{
    public partial class ShoppingDbManager : DbManager
    {
        public ShoppingDbManager(string configuartion_name)
            : base(configuartion_name)
        {
        }

        public Table<Shopping> Shopping { get { return this.GetTable<Shopping>(); } }

        public Table<ShoppingLog> ShoppingLog { get { return this.GetTable<ShoppingLog>(); } }

        public Table<SubscribePlanEntity> SubscribePlanEntity { get { return this.GetTable<SubscribePlanEntity>(); } }

        public Table<MembershipXrefSubscribePlan> MembershipXrefSubscribePlan { get { return this.GetTable<MembershipXrefSubscribePlan>(); } }

        public Table<Membership> Membership { get { return this.GetTable<Membership>(); } }

        public Table<MembershipAddress> MembershipAddreses { get { return this.GetTable<MembershipAddress>(); } }

        public Table<aspnet_User> AspnetUser { get { return this.GetTable<aspnet_User>(); } }

        public Table<ShoppingTransaction> ShoppingTransaction { get { return this.GetTable<ShoppingTransaction>(); } }

        public Table<EntityItem> EntityItems { get { return this.GetTable<EntityItem>(); } }

        public Table<ProductEntity> ProductEntitys { get { return this.GetTable<ProductEntity>(); } }

        public Table<ProductType> ProductTypes { get { return this.GetTable<ProductType>(); } }

        public Table<ProductXrefEntity> ProductXrefEntitys { get { return this.GetTable<ProductXrefEntity>(); } }

        public Table<ShoppingCartPayment> ShoppingCartPayments { get { return this.GetTable<ShoppingCartPayment>(); } }

        public Table<ShoppingMembershipAddress> ShoppingMembershipAddress { get { return this.GetTable<ShoppingMembershipAddress>(); } }

        public Table<ShoppingClass> ShoppingClass { get { return this.GetTable<ShoppingClass>(); } }

        public Table<ShoppingState> ShoppingStateItems { get { return this.GetTable<ShoppingState>(); } }

        public Table<EntityType> EntityTypeItems { get { return this.GetTable<EntityType>(); } }

        public Table<ClassEntity> ClassEntityItems { get { return this.GetTable<ClassEntity>(); } }

        public Table<SpeakerEntity> SpeakerEntityItems { get { return this.GetTable<SpeakerEntity>(); } }

        public Table<CatalogItemExtend> CatalogItemExtendItems { get { return this.GetTable<CatalogItemExtend>(); } }

		public Table<UserShoppingTransaction> UserShoppingTransactions { get { return this.GetTable<UserShoppingTransaction>(); } }
    }
}
