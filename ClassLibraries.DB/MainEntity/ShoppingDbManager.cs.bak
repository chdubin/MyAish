using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Shopping;
using BLToolkit.Data.Linq;
using MainCommon;

namespace MainEntity
{
    public partial class ShoppingDbManager
    {
        #region Get

        public bool IsUserHaveSubscribePlan(Guid user_id)
        {
            return (from p in this.MembershipXrefSubscribePlan
                    where p.UserId == user_id && !p.cancelSubscribe //p.endSubscribeDate >= DateTime.Now
                    select p).Count() > 0;
        }

        public bool IsSubscribeCancel(Guid user_id)
        {
            return (from p in this.MembershipXrefSubscribePlan
                    where p.UserId == user_id && p.cancelSubscribe == true
                    select p).Count() > 0;
        }

        #endregion

        #region Select

        public IQueryable<SubscribePlanEntity> SelectUserSubscribePlan(Guid user_id)
        {
            var rval = from sp in this.SubscribePlanEntity
                       join msp in this.MembershipXrefSubscribePlan on sp.subscribePlanID equals msp.subscribePlanID
                       where msp.UserId == user_id
                       select sp;

            return rval;
        }

        public IQueryable<MembershipXrefSubscribePlan> SelectUserXrefSubscribePlan(Guid user_id)
        {
            var rval = from msp in this.MembershipXrefSubscribePlan
                       where msp.UserId == user_id
                       select msp;

            return rval;
        }

        public IQueryable<Membership> SelectMembership(Guid user_id)
        {
            return from m in this.Membership
                   where m.UserId == user_id
                   select new Membership()
                   {
                       activatedCart = m.activatedCart,
                       activationCartDate = m.activationCartDate,
                       balance = m.balance,
                       firstName = m.firstName,
                       freeOfferCnt = m.freeOfferCnt,
                       lastName = m.lastName,
                       subscribeActivation = m.subscribeActivation,
                       fullLibraryAccess = m.fullLibraryAccess,
                       UserId = m.UserId,
                       StartSubscribeDate = m.MembershipXrefSubscribePlan.startSubscribeDate,
                       EndSubscribeDate = m.MembershipXrefSubscribePlan.endSubscribeDate,
                       SubscribePlanID = m.MembershipXrefSubscribePlan.subscribePlanID,
                       IsDeclinedSubscribe = m.MembershipXrefSubscribePlan.declineSubscribe,
                       IsCanceledSubscribe = m.MembershipXrefSubscribePlan.cancelSubscribe,

                       city = m.city,
                       country = m.country,
                       dayPhone = m.dayPhone,
                       phone = m.phone,
                       postalAdderss = m.postalAdderss,
                       postalCode = m.postalCode,
                       refferedBy = m.refferedBy,
                       state = m.state
                   };
        }

        public IQueryable<Membership> SelectMembershipAndActiveSubscribe(Guid user_id, DateTime current_date)
        {

            return SelectMembership(user_id).Where(m =>
                (m.IsCanceledSubscribe == true && m.StartSubscribeDate <= current_date && m.EndSubscribeDate > current_date && m.IsDeclinedSubscribe == false) ||
                (m.IsCanceledSubscribe == false && m.IsDeclinedSubscribe == false));
        }

        public IQueryable<SubscribePlanEntity> SelectSubscribePlan(long entity_id)
        {
            var rval = from sp in this.SubscribePlanEntity
                       where sp.subscribePlanID == entity_id
                       select sp;

            return rval;
        }

        public IQueryable<Shopping> SelectShoppings(Guid user_id, short[] shopping_states)
        {
            return from sh in this.Shopping
                   join p in this.ProductEntitys on sh.entityID equals p.productID
                   where sh.UserId == user_id
                    && shopping_states.Contains(sh.shoppingStateID)
                   select new Shopping()
                   {
                        cnt = sh.cnt,
                        deleted = sh.deleted,
                        entityID = sh.entityID,
                        price1 = sh.price1,
                        price2 = sh.price2,
                        shoppingID = sh.shoppingID,
                        shoppingTransactionID = sh.shoppingTransactionID,
                        UserId = sh.UserId,

                        ProductTypeID = p.productTypeID
                   };
        }

        public IQueryable<Shopping> SelectSuccessShopping(Guid user_id, short[] shopping_states)
        {
            return from s in this.Shopping
                   where s.UserId == user_id && shopping_states.Contains(s.shoppingStateID) && !s.deleted
                   select new Shopping()
                   {
                       cnt = s.cnt,
                       deleted = s.deleted,
                       entityID = s.entityID,
                       price1 = s.price1,
                       price2 = s.price2,
                       shoppingID = s.shoppingID,
                       shoppingStateID = s.shoppingStateID,

                       ProductTypeID = s.EntityItem.ProductEntity.productTypeID,
                   };
        }

        public IQueryable<KeyValuePair<long, DateTime>> SelectLibraryItemsIDsWithShoppingDate(Guid user_id, short[] shopping_states)
        {
            var genMem = SelectMembership(user_id).SingleOrDefault();
            var m = SelectMembershipAndActiveSubscribe(user_id, DateTime.Now).SingleOrDefault();

            DateTime dateLimit = genMem != null && genMem.fullLibraryAccess ? new DateTime(2002, 1, 1) : m == null || !m.subscribeActivation.HasValue ? DateTime.Now.AddMonths(-2) : m.subscribeActivation.Value;

            var shop = from sh in this.Shopping
                   join tl in this.ShoppingTransaction on sh.shoppingTransactionID equals tl.shoppingTransactionID
                   join mem in this.Membership on sh.UserId equals mem.UserId
                   join pr in this.ProductEntitys on sh.entityID equals pr.productID
                   where sh.UserId == user_id
                   && shopping_states.Contains(sh.shoppingStateID)
                   && !sh.deleted
                   && (tl.createDate >= dateLimit || sh.unlimitedAccessInLibrary || pr.unlimitedAccessInLibrary)
                   select new KeyValuePair<long, DateTime>(sh.entityID, tl.createDate);

            return shop;
        }

        public IQueryable<ShoppingClass> SelectLibraryItems(Guid user_id)
        {
            var rval = from sh in this.ShoppingClass
                       where sh.UserId == user_id
                       select sh;

            return rval;
        }

        public int SelectLibraryItemsCount(Guid user_id, short[] shopping_states)
        {
            var rval = from sh in this.ShoppingClass
                       where sh.UserId == user_id && shopping_states.Contains(sh.shoppingStateID)
                       select sh;

            return rval.Count();
        }

        public IQueryable<Shopping> GetOrders(Guid user_id, long transaction_id, bool exclude_deleted)
        {
            return from sh in this.Shopping
                   join ss in this.ShoppingStateItems on sh.shoppingStateID equals ss.shoppingStateID
                   join e in this.EntityItems on sh.entityID equals e.entityID
                   join p in this.ProductEntitys on e.entityID equals p.productID
                   join pe in this.EntityItems on e.parentEntityID equals pe.entityID
                   join pc in this.ClassEntityItems on pe.entityID equals pc.classID into pcc
                   from pc in pcc.DefaultIfEmpty()
                   join pcs in this.EntityItems on pc.speakerID equals pcs.entityID into pcss
                   from pcs in pcss.DefaultIfEmpty()
                   join pex in this.CatalogItemExtendItems on pe.entityID equals pex.entityID into pexx
                   from pex in pexx.DefaultIfEmpty()
                   where sh.UserId == user_id && (!exclude_deleted || !sh.deleted)
                   && sh.shoppingTransactionID == transaction_id
                   select new MainEntity.Models.Shopping.Shopping()
                   {
                       entityID = sh.entityID,
                       price1 = sh.price1,
                       price2 = sh.price2,
                       shoppingID = sh.shoppingID,
                       ShoppingStateName = ss.name,
                       deleted = sh.deleted,
                       cnt = sh.cnt,
                       Title = e.title,
                       ParentTitle = pe.title,
                       TypeName = p.ProductType.name,
                       Speaker = pcs.title,
                       ItemNumber = pex.code,
                       ProductTypeID = p.productTypeID,
                       ProductID = pe.entityID,
                   };
        }

        public IQueryable<UserShoppingTransaction> GetShoppingTransactions(Guid user_id, DateTime? since_date = null, DateTime? before_date = null)
        {
            return from st in this.UserShoppingTransactions
                   where st.UserId == user_id &&
                         (since_date == null || st.createDate >= since_date) && (before_date == null || st.createDate <= before_date)
                   select st;
        }

        public IQueryable<UserShoppingTransaction> GetShoppingTransactionsCnt(DateTime? since_date = null, DateTime? before_date = null)
        {
            return from st in this.UserShoppingTransactions
                   where (since_date == null || st.createDate >= since_date) && (before_date == null || st.createDate <= before_date)
                   select st;
        }

        public IQueryable<UserShoppingTransaction> GetShoppingTransaction(long transaction_id)
        {
            return from st in this.UserShoppingTransactions
                   where st.shoppingTransactionID == transaction_id
                   select st;
        }


        public IQueryable<ReportShoppingTransaction> GetShoppingTransactions(DateTime? since_date = null, DateTime? before_date = null)
        {

            return from st in this.UserShoppingTransactions
                   join m in this.Membership on st.UserId equals m.UserId
                   join u in this.AspnetUser on m.UserId equals u.UserId
                   join xs in this.MembershipXrefSubscribePlan on m.UserId equals xs.UserId into xss
                   from xs in xss.DefaultIfEmpty()
                   join sp in this.EntityItems on xs.subscribePlanID equals sp.entityID into spp
                   from sp in spp.DefaultIfEmpty()
                   where (since_date == null || st.createDate >= since_date) && (before_date == null || st.createDate <= before_date)
                   select new ReportShoppingTransaction()
                   {
                       Amount = st.amount,
                       CardID = st.membershipCartID,
                       CurrentSubscribeTitle = sp.title,
                       CurrentSubscribeID = sp.entityID,
                       CurrentSubscribeEndDate = xs.endSubscribeDate,
                       MembershipFirstName = m.firstName,
                       MembershipLastName = m.lastName,
                       MembershipUserName = u.UserName,
                       UserId = u.UserId,
                       TransactionID = st.shoppingTransactionID,
                       TransactionDate = st.createDate,
                       TransactionType = st.chargetype,
                       TransactionState = st.transactionState,
                       Response = st.response
                   };
        }

        public IQueryable<ProductEntity> SelectShoppingProducts(long[] from_transaction_ids)
        {
            return from s in this.Shopping
                   join p in this.ProductEntitys on s.entityID equals p.productID
                   where from_transaction_ids.Contains(s.shoppingTransactionID)
                   select new ProductEntity()
                   {
                       Count = s.cnt,
                       description = p.description,
                       price1 = p.price1,
                       price2 = p.price2,
                       productID = p.productID,
                       productTypeID = p.productTypeID,
                       TransactionID = s.shoppingTransactionID
                   };
        }

        public IQueryable<ShoppingMembershipAddress> SelectShoppingMembershipAddreses(long[] from_transaction_ids)
        {
            var rval = from xa in this.ShoppingMembershipAddress
                       join a in this.MembershipAddreses on xa.addressID equals a.addressID
                       where from_transaction_ids.Contains(xa.shoppingTransactionID)
                       select new ShoppingMembershipAddress()
                       {
                           addressID = xa.addressID,
                           shoppingTransactionID = xa.shoppingTransactionID,
                           MembershipAddress = a,
                       };
            return rval;
        }

        #endregion

        #region Update

        //public int CancelSubscribe(Guid user_id) 
        //{
        //    return this.MembershipXrefSubscribePlan.Where(x => x.UserId == user_id)
        //        .Update(x => new MembershipXrefSubscribePlan()
        //        {
        //            cancelSubscribe = true
        //        });
        //}

        public int UpdateShopping(Guid user_id, long[] entity_ids, bool deleted)
        {
            return this.ShoppingClass.Where(sh => sh.UserId == user_id && entity_ids.Contains(sh.classID))
                .Update(sh => new ShoppingClass()
                {
                    deleted = deleted
                });
        }

        public int UpdateShoppingAvailable(Guid user_id, long[] entity_ids, bool unlimited_access_in_library)
        {
            return this.ShoppingClass.Where(sh => sh.UserId == user_id && entity_ids.Contains(sh.classID))
                .Update(sh => new ShoppingClass()
                {
                    unlimitedAccessInLibrary = unlimited_access_in_library
                });
        }

        public int UpdateShoppingTransaction(long transaction_id, string tran_id, string response, short transaction_state)
        {
            return this.ShoppingTransaction.Where(sh => sh.shoppingTransactionID == transaction_id)
                .Update(sh => new ShoppingTransaction()
                {
                    tranID = tran_id,
                    response = response,
                    transactionState = transaction_state
                });
        }

        public int UpdateShoppingTransaction(long transaction_id, decimal hold_balance, short transaction_state)
        {
            return this.ShoppingTransaction.Where(sh => sh.shoppingTransactionID == transaction_id)
                .Update(sh => new ShoppingTransaction()
                {
                    holdBalance = hold_balance,
                    transactionState = transaction_state
                });
        }

        public int UpdateShoppingTransaction(long transaction_id, string response, short transaction_state)
        {
            return this.ShoppingTransaction.Where(sh => sh.shoppingTransactionID == transaction_id)
                .Update(sh => new ShoppingTransaction()
                {
                    response = response,
                    transactionState = transaction_state
                });
        }

        public int DecraseFreeOfferPurchaseCnt(Guid user_id)
        {
            return this.Membership.Where(m => m.UserId == user_id)
                .Update(m => new Membership()
                {
                    freeOfferCnt = m.freeOfferCnt - 1
                });
        }

        #endregion

        #region Insert

        public long InsertShopping(long entity_id, Guid user_id, ShoppingStateEnum shopping_state,
            long shopping_transaction_id, decimal price1, decimal price2, int count)
        {
            var rval = Convert.ToInt64(this.Shopping.InsertWithIdentity(() => new Shopping()
            {
                entityID = entity_id,
                UserId = user_id,
                shoppingStateID = (short)shopping_state,
                shoppingTransactionID = shopping_transaction_id,
                price1 = price1,
                price2 = price2,
                cnt = count
            }));

            return rval;
        }

        public long InsertTransaction(DateTime transaction_date, decimal hold_balance, decimal amount, string tran_id, string comment, string response, string charge_type, short transaction_state)
        {
            return Convert.ToInt64(this.ShoppingTransaction.InsertWithIdentity(() => new ShoppingTransaction()
            {
                createDate = transaction_date,
                holdBalance = hold_balance,
                amount = amount,
                tranID = tran_id,
                comment = comment,
                response = response,
                chargetype = charge_type,
                transactionState = transaction_state
            }));
        }

        public void InsertTransactionCartPayment(long transaction_id, Guid user_id, string cart_id)
        {
            this.ShoppingCartPayments.Insert(() => new ShoppingCartPayment()
            {
                membershipCartID = cart_id,
                shoppingTransactionID = transaction_id,
                UserId = user_id
            });
        }

        public void InsertTransactionMembershipAddress(long transaction_id, long address_id)
        {
            this.ShoppingMembershipAddress.Insert(() => new ShoppingMembershipAddress()
            {
                shoppingTransactionID = transaction_id,
                addressID = address_id
            });
        }

        public long InsertShoppingLog(long shopping_id, DateTime create_date, ShoppingStateEnum state)
        {
            return Convert.ToInt64(this.ShoppingLog.InsertWithIdentity(() => new ShoppingLog()
            {
                createDate = create_date,
                shoppingID = shopping_id,
                shoppingStateID = (short)state
            }));
        }

        #endregion
    }
}
