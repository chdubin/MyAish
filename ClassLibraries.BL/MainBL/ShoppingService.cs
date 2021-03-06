﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPartnerKinoBL.Extension;
using MainCommon;
using System.Web;
using MainEntity;
using MainEntity.Models.Shopping;

namespace MainBL
{
    public partial class ShoppingService : BaseBO, MainEntity.Interfaces.IShoppingService
    {
        public ShoppingService(string connection_name)
            : base(connection_name) { }


        #region Get

        public long GetShoppingID(Guid user_id, long from_entity_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ShoppingDbManager context) =>
               {
                   return context.SelectShopping(user_id, from_entity_id, new short[] { (short)ShoppingStateEnum.Prepaid, (short)ShoppingStateEnum.PaySuccessed }).Select(s => s.shoppingID).FirstOrDefault();
               });
        }


        public bool IsUserHaveSubscribePlan(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ShoppingDbManager context) =>
               {
                   return context.IsUserHaveSubscribePlan(user_id);
               });
        }

        public long[] SelectLibraryItemsIDs(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ShoppingDbManager context) =>
              {
                  return context.SelectShoppings(user_id, new short[] { (short)ShoppingStateEnum.Prepaid, (short)ShoppingStateEnum.PaySuccessed })
                      .Where(s => s.ProductTypeID == (short)ProductTypeEnum.File).Select(s => s.entityID).ToArray();
              });
        }

        public ShoppingClass[] SelectLibraryItems(Guid user_id, int start_row_index, int page_size)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ShoppingDbManager context) =>
              {
                  return context.SelectLibraryItems(user_id).Skip(start_row_index).Take(page_size).ToArray();
              });
        }

        public Shopping[] GetSuccessShopping(Guid user_id, long product_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ShoppingDbManager context) =>
              {
                  var states = new[] { (short)ShoppingStateEnum.Prepaid, (short)ShoppingStateEnum.PaySuccessed };
                  return context.SelectSuccessShopping(user_id, states).Where(s => s.entityID == product_id).ToArray();
              });
        }


        public int SelectLibraryItemsCount(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ShoppingDbManager context) =>
              {
                  return context.SelectLibraryItemsCount(user_id, new short[] { (short)ShoppingStateEnum.Prepaid, (short)ShoppingStateEnum.PaySuccessed });
              });
        }

        public long[] SelectLibraryItemsClassIds(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (ShoppingDbManager context) =>
                {
                    return context.SelectLibraryItems(user_id).Select(i => i.classID).ToArray();
                });
        }

        public KeyValuePair<long, DateTime>[] SelectLibraryItemsIDsWithShoppingDate(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ShoppingDbManager context) =>
              {
                  return context.SelectLibraryItemsIDsWithShoppingDate(user_id, new short[] { (short)ShoppingStateEnum.Prepaid, (short)ShoppingStateEnum.PaySuccessed }).ToArray();
              });
        }
 //this was added on feb2 from fersion i had added from github
 public MainEntity.Models.Activity.RoyaltyLog[] SelectUserIDsByClassPurchase(long classId, DateTime startDate, DateTime endDate)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ShoppingDbManager context) =>
              {
                  return context.SelectUserIDsByClassPurchase(classId, new short[] { (short)ShoppingStateEnum.Prepaid, (short)ShoppingStateEnum.PaySuccessed }, startDate, endDate).ToArray();
              });
        }
        public int SelectLibraryItemsCnt(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ShoppingDbManager context) =>
              {
                  return context.SelectLibraryItemsIDsWithShoppingDate(user_id, new short[] { (short)ShoppingStateEnum.Prepaid, (short)ShoppingStateEnum.PaySuccessed }).Count();
              });
        }

        public bool IsSubscribeCancel(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
             (ShoppingDbManager context) =>
             {
                 return context.IsSubscribeCancel(user_id);
             });
        }

        #endregion

        #region Select

        public int GetShoppingTransactionsCnt(Guid user_id, DateTime? since_date = null, DateTime? before_date = null, short[] transaction_state_list = null, string[] charge_type_list = null)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
             (ShoppingDbManager context) =>
             {
                 var transactions = context.GetShoppingTransactions(user_id, since_date, before_date);
                 if (transaction_state_list != null && transaction_state_list.Length > 0)
                     transactions = transactions.Where(t => transaction_state_list.Contains(t.transactionState));
                 if (charge_type_list != null && charge_type_list.Length > 0)
                     transactions = transactions.Where(t => charge_type_list.Contains(t.chargetype));


                 return transactions.Count();
             });
        }

        public UserShoppingTransaction[] GetShoppingTransactions(Guid user_id, int? start_row = null, int? max_row_count = null, DateTime? since_date = null, DateTime? before_date = null, short[] transaction_state_list = null, string[] charge_type_list = null)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
             (ShoppingDbManager context) =>
             {
                 var transactions = context.GetShoppingTransactions(user_id, since_date, before_date);
                 if (transaction_state_list != null && transaction_state_list.Length > 0)
                     transactions = transactions.Where(t => transaction_state_list.Contains(t.transactionState));
                 if (charge_type_list != null && charge_type_list.Length > 0)
                     transactions = transactions.Where(t => charge_type_list.Contains(t.chargetype));

                 transactions = transactions.OrderByDescending(t => t.createDate);

                 if (start_row != null) transactions = transactions.Skip(start_row.Value);
                 if (max_row_count != null) transactions = transactions.Take(max_row_count.Value);

                 var rval = transactions.ToArray();

                 return rval;
             });
        }

        public UserShoppingTransaction GetShoppingTransaction(long shopping_transaction_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
             (ShoppingDbManager context) =>
             {
                 var rval = context.GetShoppingTransaction(shopping_transaction_id);

                 return rval.SingleOrDefault();
             });
        }


        public ShoppingMembershipAddress[] GetShoppingAddresses(long[] from_transaction_ids)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
             (ShoppingDbManager context) =>
             {
                 var rval = context.SelectShoppingMembershipAddreses(from_transaction_ids).ToArray();
                 return rval;
             });
        }


        public int GetShoppingTransactionsCnt(DateTime? since_date = null, DateTime? before_date = null, short[] transaction_state_list = null, string[] charge_type_list = null)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
             (ShoppingDbManager context) =>
             {
                 var transactions = context.GetShoppingTransactionsCnt(since_date, before_date);
                 if (transaction_state_list != null && transaction_state_list.Length > 0)
                     transactions = transactions.Where(t => transaction_state_list.Contains(t.transactionState));
                 if (charge_type_list != null && charge_type_list.Length > 0)
                     transactions = transactions.Where(t => charge_type_list.Contains(t.chargetype));


                 return transactions.Count();
             });
        }

        public ReportShoppingTransaction[] GetShoppingTransactions(int? start_row = null, int? max_row_count = null, DateTime? since_date = null, DateTime? before_date = null, short[] transaction_state_list = null, string[] charge_type_list = null)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (ShoppingDbManager context) =>
                {
                    var transactions = context.GetShoppingTransactions(since_date, before_date);
                    if (transaction_state_list != null && transaction_state_list.Length > 0)
                        transactions = transactions.Where(t => transaction_state_list.Contains(t.TransactionState));
                    if (charge_type_list != null && charge_type_list.Length > 0)
                        transactions = transactions.Where(t => charge_type_list.Contains(t.TransactionType));

                    transactions = transactions.OrderByDescending(t => t.TransactionDate);

                    if (start_row != null) transactions = transactions.Skip(start_row.Value);
                    if (max_row_count != null) transactions = transactions.Take(max_row_count.Value);

                    var rval = transactions.ToArray();
                    var products = context.SelectShoppingProducts(rval.Select(t => t.TransactionID).ToArray());
                    foreach (var t in rval)
                        t.ProductTypes = products.Where(p => p.TransactionID == t.TransactionID)
                            .Select(p => (ProductTypeEnum)p.productTypeID).ToArray();

                    return rval;
                });
        }
 /// this was added on feb2 from version i had download from github
      public ReportShoppingTransaction[] GetShoppingTransactionsAll()
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (ShoppingDbManager context) =>
                {
                    var transactions = context.GetShoppingTransactions(null, null);
                    //if (transaction_state_list != null && transaction_state_list.Length > 0)
                    //    transactions = transactions.Where(t => transaction_state_list.Contains(t.TransactionState));
                    //if (charge_type_list != null && charge_type_list.Length > 0)
                    //    transactions = transactions.Where(t => charge_type_list.Contains(t.TransactionType));

                    transactions = transactions.OrderByDescending(t => t.TransactionDate);

                    //if (start_row != null) transactions = transactions.Skip(start_row.Value);
                    //if (max_row_count != null) transactions = transactions.Take(max_row_count.Value);

                    var rval = transactions.ToArray();
                    //var products = context.SelectShoppingProducts(rval.Select(t => t.TransactionID).ToArray());
                    //foreach (var t in rval)
                    //    t.ProductTypes = products.Where(p => p.TransactionID == t.TransactionID)
                    //        .Select(p => (ProductTypeEnum)p.productTypeID).ToArray();

                    return rval;
                });
        }

        public Shopping[] GetOrders(Guid user_id, long transaction_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
             (ShoppingDbManager context) =>
             {
                 return context.GetOrders(user_id, transaction_id, true).ToArray();
             });
        }


        //public EntityItem[] GetProducts(long parent_id, ProductTypeEnum product_type)
        //{
        //    return this.Exec(System.Data.IsolationLevel.Snapshot,
        //      (ShoppingDbManager context) =>
        //      {
        //          return context.GetPro(user_id).SingleOrDefault();
        //      });
        //}

        public long GetSubscribeInCartID(HttpContext context)
        {
            long rval = 0;
            rval = Convert.ToInt64(context.GetValue(CartItemTypeEnum.Subscribe, "0"));

            return rval;
        }

        public long GetUnitsInCart(HttpContext context)
        {
            long rval = 0;
            rval = Convert.ToInt64(context.GetValue(CartItemTypeEnum.Unit, "0"));
            return rval;
        }

        public long[] GetClassProductInCartIDs(HttpContext context)
        {
            return (context.GetValue(CartItemTypeEnum.Class, string.Empty))
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => long.Parse(s))
                .ToArray();
        }

        public long[] GetPackageInCartIDs(HttpContext context)
        {
            return (context.GetValue(CartItemTypeEnum.Package, string.Empty))
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => long.Parse(s))
                .ToArray();
        }

        public long[] GetAllProductsInCart(HttpContext context)
        {
            List<long> rval = new List<long>(GetClassProductInCartIDs(context));
            long unitsID = GetUnitsInCart(context);
            long cartSubscribeID = GetSubscribeInCartID(context);

            rval.AddRange(GetPackageInCartIDs(context));
            if (unitsID > 0) rval.Add(unitsID);
            if (cartSubscribeID > 0) rval.Add(cartSubscribeID);

            return rval.ToArray();
        }


        public SubscribePlanEntity GetUserSubscribePlan(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (ShoppingDbManager context) =>
                {
                    return context.SelectUserSubscribePlan(user_id).SingleOrDefault();
                });
        }

        public Membership GetMembership(Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ShoppingDbManager context) =>
              {
                  return context.SelectMembership(user_id).SingleOrDefault();
              });
        }

        public Membership GetMembershipAndActiveSubscribe(Guid user_id, DateTime current_date)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ShoppingDbManager context) =>
              {
                  return context.SelectMembershipAndActiveSubscribe(user_id, current_date.ToUniversalTime()).SingleOrDefault();
              });
        }

        #endregion

        #region Insert

        public int AddItemToShoppingCart(Guid? user_id, long item_id, CartItemTypeEnum item_type_id, HttpContext context, int cnt = 1)
        {
            var rval = 0;
            List<string> val = new List<string>();

            switch (item_type_id)
            {
                case CartItemTypeEnum.Subscribe:
                    {
                        rval = 1;
                        context.SetValue(CartItemTypeEnum.Subscribe, item_id.ToString());
                        break;
                    }

                case CartItemTypeEnum.Class:
                    {

                        var shopping = user_id != null ? this.GetSuccessShopping(user_id.Value, item_id) : null;
                        if (shopping == null || !shopping.Where(s => s.ProductTypeID == (int)ProductTypeEnum.File).Any())
                        {
                            bool canAdd = true;
                            if (user_id.HasValue)
                            {
                                var m = GetMembershipAndActiveSubscribe(user_id.Value, DateTime.Now);
                                DateTime dateLimit = m == null || !m.subscribeActivation.HasValue ? DateTime.Now.AddMonths(-2) : m.subscribeActivation.Value;
                                var log = this.Exec(System.Data.IsolationLevel.Snapshot,
                                (ActivityLogDbManager act) =>
                                {
                                    return act.SelectLibraryItem(user_id.Value, new ActivityLogTypeEnum[] { ActivityLogTypeEnum.DownloadClass }, (long)item_id, dateLimit).ToArray();
                                });
                                if (log != null && log.Where(l => l.Key == item_id).Any())
                                    canAdd = false;
                            }

                            if (canAdd)
                            {
                                rval = 1;
                                val = (context.GetValue(CartItemTypeEnum.Class, string.Empty))
                                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                                for (int i = 0; i < cnt; i++) val.Add(item_id.ToString());
                                val = val.ToList();
                                context.SetValue(CartItemTypeEnum.Class, string.Join(",", val.ToArray()));
                            }
                        }
                        break;
                    }

                case CartItemTypeEnum.Unit:
                    {
                        rval = 1;
                        context.SetValue(CartItemTypeEnum.Unit, item_id.ToString());
                        break;
                    }

                case CartItemTypeEnum.Package:
                    {
                        rval = 1;
                        val = (context.GetValue(CartItemTypeEnum.Package, string.Empty))
                            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                        val.Add(item_id.ToString());
                        //val = val.Distinct().ToList();
                        val = val.ToList();
                        context.SetValue(CartItemTypeEnum.Package, string.Join(",", val.ToArray()));
                        break;
                    }
            }

            return rval;
        }

        //public int BuyFreeOffer(long entity_id, Guid user_id, ShoppingStateEnum shopping_state,
        //    long shopping_transaction_id, decimal price)
        //{
        //    return this.Exec(System.Data.IsolationLevel.ReadCommitted,
        //       (ShoppingDbManager context) =>
        //       {
        //           int subscribePlanID = 0;

        //           var subscribePlan = context.SelectUserSubscribePlan(user_id).SingleOrDefault();
        //           if (subscribePlan.freeOfferCnt <= 2)
        //           {
        //               context.IncreaseFreeOfferPurchaseCnt(subscribePlanID);
        //               context.InsertShopping(entity_id, user_id, shopping_state, shopping_transaction_id, price);

        //               return 1;
        //           }
        //           return 0;
        //       });
        //}


        public int BuyFreeOffers(long[] entity_ids, Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (ShoppingDbManager context) =>
               {
                   var membership = context.SelectMembership(user_id).SingleOrDefault();

                   if (membership.freeOfferCnt >= entity_ids.Length)
                   {
                       foreach (long entity_id in entity_ids)
                       {
                           long transactionID = context.InsertTransaction(DateTime.UtcNow, 0, 0, null, null, null, "freeoffer", (short)ShoppingTransactionStateEnum.Complete);
                           context.InsertShopping(entity_id, user_id, ShoppingStateEnum.PaySuccessed, transactionID, 0, 0, 1);
                           context.DecraseFreeOfferPurchaseCnt(user_id);
                       }
                       return 1;
                   }
                   return 0;
               });
        }

        public int InsertFilesInLibrary(long[] product_file_ids, Guid user_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (ShoppingDbManager context) =>
               {
                   long tranID = context.InsertTransaction(DateTime.UtcNow, 0, 0, null, null, null, "admin", (short)ShoppingTransactionStateEnum.Complete);
                   foreach (var id in product_file_ids)
                       context.InsertShopping(id, user_id, ShoppingStateEnum.PaySuccessed, tranID, 0, 0, 1);
                   return 1;
               });
        }

        #endregion

        #region Update

        public void UpdateClassproductQuantity(int quantity, int product_id, HttpContext context)
        {
            var val = (context.GetValue(CartItemTypeEnum.Class, string.Empty))
                           .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            val = val.Where(p => p != product_id.ToString()).ToList();
            List<string> temp = new List<string>();
            for (var i = 0; i < quantity; i++)
                temp.Add(product_id.ToString());
            val.AddRange(temp);
            context.SetValue(CartItemTypeEnum.Class, string.Join(",", val.ToArray()));

            context.Profile.Save();
        }

        /*
        public int CancelSubscribe(Guid user_id) 
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (ShoppingDbManager context) =>
                {
                    return context.CancelSubscribe(user_id);
                });
        }
         * */

        public int UpdateShopping(Guid user_id, IDictionary<long, bool> shopping_list)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (ShoppingDbManager context) =>
                {
                    context.UpdateShopping(user_id, shopping_list.Where(sh => sh.Value == true).Select(sh => sh.Key).ToArray(), true);
                    return context.UpdateShopping(user_id, shopping_list.Where(sh => sh.Value == false).Select(sh => sh.Key).ToArray(), false);
                });
        }

        public int UpdateShoppingAvailable(Guid user_id, IDictionary<long, bool> shopping_list)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (ShoppingDbManager context) =>
                {
                    context.UpdateShoppingAvailable(user_id, shopping_list.Where(sh => sh.Value == true).Select(sh => sh.Key).ToArray(), true);
                    return context.UpdateShoppingAvailable(user_id, shopping_list.Where(sh => sh.Value == false).Select(sh => sh.Key).ToArray(), false);
                });
        }


        #endregion

        #region Delete

        public void EmptyCart(HttpContext context)
        {
            context.SetValue(CartItemTypeEnum.Subscribe, string.Empty);
            context.SetValue(CartItemTypeEnum.Class, string.Empty);
            context.SetValue(CartItemTypeEnum.Unit, string.Empty);
            context.SetValue(CartItemTypeEnum.Package, string.Empty);

            context.Profile.Save();
        }

        public void DeleteItemFromShoppingCart(CartItemTypeEnum item_type_id, HttpContext context, long item_id = -1)
        {
            List<string> val = new List<string>();


            switch (item_type_id)
            {
                case CartItemTypeEnum.Subscribe:
                    {
                        context.SetValue(CartItemTypeEnum.Subscribe, "0");
                        break;
                    }

                case CartItemTypeEnum.Class:
                    {
                        val = (context.GetValue(CartItemTypeEnum.Class, string.Empty))
                            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                        val = val.Where(p => p != item_id.ToString()).ToList();
                        context.SetValue(CartItemTypeEnum.Class, string.Join(",", val.ToArray()));

                        break;
                    }

                case CartItemTypeEnum.Unit:
                    {
                        context.SetValue(CartItemTypeEnum.Unit, "0");
                        break;
                    }

                case CartItemTypeEnum.Package:
                    {
                        val = (context.GetValue(CartItemTypeEnum.Package, string.Empty))
                            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                        val = val.Where(p => p != item_id.ToString()).ToList();
                        context.SetValue(CartItemTypeEnum.Package, string.Join(",", val.ToArray()));
                        break;
                    }
            }
        }

        #endregion

    }
}
