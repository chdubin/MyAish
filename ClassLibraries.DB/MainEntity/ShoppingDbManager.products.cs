using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Shopping;
using MainCommon;
using BLToolkit.Data.Linq;

namespace MainEntity
{
    public partial class ShoppingDbManager
    {
        public IQueryable<EntityItem> GetProducts(long[] parent_ids, short[] product_types,
            bool with_only_active, bool with_only_nondeleted)
        {
            var rval = from e in this.EntityItems
                       join p in this.ProductEntitys on e.entityID equals p.productID
                       where parent_ids.Contains(e.parentEntityID)
                       select new EntityItem()
                       {
                           entityID = e.entityID,
                           title = e.title,
                           active = e.active,
                           deleted = e.deleted,

                           ProductTypeID = p.productTypeID,
                           ProductEntity = p
                       };

            if (product_types != null && product_types.Length > 0)
                rval = rval.Where(e => product_types.Contains(e.ProductTypeID));

            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);


            return rval;
        }


        public IQueryable<EntityItem> GetProduct(long product_id,
            bool with_only_active, bool with_only_nondeleted)
        {
            var rval = from i in this.EntityItems
                       where i.entityID == product_id
                       select new EntityItem()
                       {
                           entityID = i.entityID,
                           title = i.title,
                           active = i.active,
                           deleted = i.deleted,

                           ProductEntity = i.ProductEntity
                       };

            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }

        //public IQueryable<EntityItem> FindEntityItem(long[] from_packages, EntityItemTypeEnum search_entity_type,
        //        bool with_only_active, bool with_only_nondeleted)
        //{
        //    var rval = from i in this.EntityItems
        //               join p in this.ProductXrefEntitys on i.entityID equals p.entityID
        //               where from_packages.Contains(p.productID) && i.typeID == (int)search_entity_type
        //               select new EntityItem()
        //               {
        //                   entityID = i.entityID,
        //                   title = i.title,
        //                   active = i.active,
        //                   deleted = i.deleted,

        //                   ProductEntity = i.ProductEntity,
        //                   SubscribePlanEntity = i.SubscribePlanEntity
        //               };
        //    if (with_only_active)
        //        rval = rval.Where(e => e.active);
        //    if (with_only_nondeleted)
        //        rval = rval.Where(e => !e.deleted);

        //    return rval;
        //}

        public IQueryable<EntityItem> SelectProductEntities(long[] from_products,
            bool with_only_active, bool with_only_nondeleted)
        {
            var rval = from e in this.EntityItems
                       join p in this.ProductEntitys on e.entityID equals p.productID
                       join t in this.ProductTypes on p.productTypeID equals t.productTypeID
                       let e1 = (from ex in this.CatalogItemExtendItems where ex.entityID == e.entityID select ex.code).FirstOrDefault()
                       let e2 = (from ex in this.CatalogItemExtendItems where ex.entityID == e.parentEntityID select ex.code).FirstOrDefault()
                       where from_products.Contains(e.entityID)
                       select new EntityItem()
                       {
                           entityID = e.entityID,
                           title = e.title,
                           active = e.active,
                           deleted = e.deleted,
                           typeID = e.typeID,
                           Code = e1,
                           CodeParent = e2,

                           ProductEntity = p,
                           ProductTypeName =t.name
                       };
            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }

        public IQueryable<EntityItem> SelectProductsXrefEntity(long[] product_ids, short? product_type_id=null, bool with_only_active = true, bool with_only_nondeleted = true)
        {
            var rval = from e in this.EntityItems
                       join x in this.ProductXrefEntitys on e.entityID equals x.entityID
                       join p in this.ProductEntitys on e.entityID equals p.productID
                       where product_ids.Contains(x.productID)
                       select new EntityItem()
                       {
                           entityID = e.entityID,
                           title = e.title,
                           active = e.active,
                           deleted = e.deleted,
                           typeID = e.typeID,

                           ProductTypeID = p.productTypeID,
                           ProductEntity = p,
                       };
            if (product_type_id.HasValue)
                rval = rval.Where(e => e.ProductTypeID == product_type_id);

            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }


        public IQueryable<SubscribePlanEntity> GetSubscribe(long subscribe_id)
        {
            var rval = from i in this.SubscribePlanEntity
                       join e in this.EntityItems on i.subscribePlanID equals e.entityID
                       where i.subscribePlanID == subscribe_id
                       select new SubscribePlanEntity()
                       {
                           description = i.description,
                           durationInDays = i.durationInDays,
                           durationInMonths = i.durationInMonths,
                           freeOfferCnt = i.freeOfferCnt,
                           freeUnitsOnSubscribe = i.freeUnitsOnSubscribe,
                           subscribePlanID = i.subscribePlanID,
                           EntityItem = e
                       };

            return rval;
        }


        public IQueryable<Shopping> SelectShoppings(long from_transaction_id)
        {
            var rval = from s in this.Shopping
                       where s.shoppingTransactionID==from_transaction_id
                       select new Shopping()
                       {
                           entityID = s.entityID,
                           price1 = s.price1,
                           price2 = s.price2,
                           shoppingID = s.shoppingID,
                           shoppingStateID = s.shoppingStateID,
                           shoppingTransactionID = s.shoppingTransactionID,
                           UserId = s.UserId,

                           EntityItem = s.EntityItem
                       };

            return rval;
        }

        public IQueryable<ShoppingTransaction> SelectTransaction(long from_transaction_id)
        {
            var rval = from t in this.ShoppingTransaction
                       where t.shoppingTransactionID == from_transaction_id
                       select new ShoppingTransaction()
                       {
                           createDate = t.createDate,
                           holdBalance = t.holdBalance,
                           shoppingTransactionID = t.shoppingTransactionID,

                           ShoppingCartPayment = t.ShoppingCartPayment,
                       };

            return rval;
        }

        public IQueryable<ShoppingTransaction> GetShoppingTransactions(ShoppingStateEnum shopping_state_id)
        {
            return (from s in this.Shopping
                    where s.shoppingStateID == (short)shopping_state_id
                    select s.ShoppingTransaction).Distinct();
        }

        #region Update

        public int UpdateMembershipBalance(Guid user_id, decimal new_balance)
        {
            return this.Membership.Where(x => x.UserId == user_id)
                .Update(x => new Membership()
                {
                    balance = new_balance
                });
        }

        public int UpdateMembershipSubscribeActivation(Guid user_id, DateTime subscribe_activation, byte charge_day)
        {
            return this.Membership.Where(x => x.UserId == user_id)
                .Update(x => new Membership()
                {
                    chargeDay = charge_day,
                    subscribeActivation = subscribe_activation,
                    suspended = false
                });
        }

        public int UpdateShoppingState(long shopping_id, ShoppingStateEnum state_id)
        {
            return this.Shopping.Where(s => s.shoppingID == shopping_id)
                .Update(x => new Shopping()
                {
                    shoppingStateID = (short)state_id
                });
        }

        public int UpdateTransactionHoldBalance(long transaction_id, decimal hold_balance)
        {
            return this.ShoppingTransaction.Where(t => t.shoppingTransactionID == transaction_id)
                .Update(x => new ShoppingTransaction()
                {
                    holdBalance = hold_balance
                });
        }


        #endregion


        #region Insert

        public int InsertMembershipXrefSubscribe(Guid user_id, long subscribe_plan_id, DateTime start_subscribe_date, DateTime end_subscribe_date, DateTime next_activation_subscribe)
        {
            return this.MembershipXrefSubscribePlan.Insert(()=>new MembershipXrefSubscribePlan()
            {
                cancelSubscribe = false,
                declineSubscribe = false,
                endSubscribeDate = end_subscribe_date,
                startSubscribeDate = start_subscribe_date,
                activationDate = next_activation_subscribe,
                subscribePlanID = subscribe_plan_id,
                UserId = user_id
            });
        }

        #endregion



        #region Delete

        public int DeleteMembershipXrefSubscribe(Guid user_id)
        {
            return this.MembershipXrefSubscribePlan.Where(m => m.UserId == user_id).Delete();
        }

        #endregion
    }
}
