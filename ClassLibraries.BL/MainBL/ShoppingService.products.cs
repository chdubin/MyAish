using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainCommon;
using MainEntity.Models.Shopping;
using MainEntity;

namespace MainBL
{
    public partial class ShoppingService
    {
 
        public EntityItem[] GetProducts(long parent_id, ProductTypeEnum product_type)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ShoppingDbManager context) =>
               {
                   return context.GetProducts(new[]{parent_id}, new[]{(short)product_type}, true, true).ToArray();
               });

        }

        public EntityItem GetProduct(long product_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ShoppingDbManager context) =>
               {
                   return context.GetProduct(product_id, true, true).SingleOrDefault();
               });
        }

        public long[] ShoppingAtUnits(long[] product_ids, Guid user_id, DateTime transaction_date, decimal discount_percent, int max_units_on_subscribe)
		{
			return this.Exec(System.Data.IsolationLevel.ReadCommitted,
				(ShoppingDbManager context) =>
				{
                    var info = GetShoppingTransactionInfo(product_ids, user_id, true, discount_percent, false, max_units_on_subscribe, context);
					if (info.ShoppingPrice.Length > 0)
					{
                        var transactionID = ShoppingBegin(user_id, null, null, info, transaction_date, null, context);
                        ShoppingComplete(transactionID, "unit purchase", context);
					}

					return info.ShoppingPrice.Select(s => s.entityID).ToArray();
				});
		}

        public ShoppingTransactionInfo GetShoppingTransactionInfo(long[] product_ids, Guid user_id, bool shopping_only_user_balance, decimal discount_percent, int max_units_on_subscribe, bool subscribe_activation = false)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ShoppingDbManager context) =>
               {
                   return GetShoppingTransactionInfo(product_ids, user_id, shopping_only_user_balance, discount_percent, subscribe_activation, max_units_on_subscribe, context);
               });
        }

        private static ShoppingTransactionInfo GetShoppingTransactionInfo(long[] product_ids, Guid user_id, bool shopping_only_user_balance, decimal discount_percent, bool subscribe_activation, int max_units_on_subscribe, ShoppingDbManager context)
        {
            DateTime now = DateTime.UtcNow;
            var membership = context.SelectMembership(user_id).FirstOrDefault();
            var subscribe = context.SelectUserXrefSubscribePlan(user_id).FirstOrDefault();
            bool isSubscriber = subscribe != null && (
                (subscribe.cancelSubscribe && subscribe.startSubscribeDate <= now && subscribe.endSubscribeDate > now) ||
                (!subscribe.cancelSubscribe && !subscribe.declineSubscribe));

            var products = GetProducts(product_ids, isSubscriber, context);
            var productUnits = products.Where(p => p.ProductEntity.productTypeID == (short)ProductTypeEnum.Units).SingleOrDefault();
            var productSubscribe = products.Where(p => p.ProductEntity.productTypeID == (short)ProductTypeEnum.Subscribe).SingleOrDefault();
            var productFiles = products.Where(p => p.ProductEntity.productTypeID == (short)ProductTypeEnum.File).ToArray();
            var productOthers = product_ids.Select(p => products.Where(pp => pp.entityID == p).SingleOrDefault())
                .Where(p => p != null
                    && p.ProductEntity.productTypeID != (short)ProductTypeEnum.Subscribe
                    && p.ProductEntity.productTypeID != (short)ProductTypeEnum.Units
                    && p.ProductEntity.productTypeID != (short)ProductTypeEnum.File).ToArray();
            var productList = new List<EntityItem>(productFiles);
            productList.AddRange(productOthers);

            var productsPrice = productList
                .Select(p => new { entityID = p.entityID, price1 = p.ProductEntity.price1, price2 = p.ProductEntity.price2, title=p.title, productTypeID=p.ProductEntity.productTypeID, typeName=p.ProductTypeName, enity = p })
                .OrderBy(p => p.price2).ToArray();

			bool doSubscribe = productSubscribe != null && (!isSubscriber || subscribe_activation);
            bool canFreeSubscribe = membership == null || membership.subscribeActivation == null;
			var subscribePlanFree = doSubscribe && productSubscribe.active &&
                (productSubscribe.ProductEntity.price1 == null || productSubscribe.ProductEntity.price1 == 0) &&
                (productSubscribe.ProductEntity.price2 == null || productSubscribe.ProductEntity.price2 == 0);
			doSubscribe = doSubscribe && (!subscribePlanFree || canFreeSubscribe);
			var subscribePlanID = doSubscribe ? productSubscribe.entityID : 0;
            //first month if Monthly subscribe shall be free
            if (subscribe == null && productSubscribe != null)
            {
                productSubscribe.ProductEntity.price1 = 0;
            }

            var shoppingFromBalanceUnits = shopping_only_user_balance || (!isSubscriber && !doSubscribe);


            decimal unitsBuy = !shopping_only_user_balance && (isSubscriber || doSubscribe) && productUnits !=null? productUnits.ProductEntity.price2.Value : 0;
            decimal unitsInSubscribePlan = !shopping_only_user_balance && doSubscribe ? context.SelectSubscribePlan(subscribePlanID).Select(sp => sp.freeUnitsOnSubscribe).SingleOrDefault() : 0;
            decimal discount = isSubscriber || doSubscribe ? 1 - discount_percent / 100 : 1;
            decimal unitsBalance = membership != null ? membership.balance : 0;

            if (unitsBalance < max_units_on_subscribe) unitsInSubscribePlan = Math.Min(unitsInSubscribePlan + unitsBalance, max_units_on_subscribe) - unitsBalance;

            decimal amount = 0;
            decimal units = 0;
            var shoppingPrice1 = new Dictionary<long, Shopping>();
            var shoppingPrice2 = new Dictionary<long, Shopping>();

            foreach (var p in productsPrice)
            {
				Shopping shopping = null;
                if (p.price2 != null && p.price2 > 0 && (isSubscriber || doSubscribe || p.price2.Value == 1 || p.price2.Value == 2) &&
                    (!shoppingFromBalanceUnits || ((unitsBalance + unitsInSubscribePlan - units) >= p.price2.Value)))
                {
                    shopping = shoppingPrice2.GetOrAdd(p.entityID);
                    var price = p.price2.Value;
                    shopping.price2 += price;
                    units += price;
                }
                else if (p.price1 != null && p.price1 > 0 && !shopping_only_user_balance)
                {
                    shopping = shoppingPrice1.GetOrAdd(p.entityID);
                    var price = MainCommon.MyUtils.CeilingPrice(p.price1.Value * discount);
                    shopping.Price1WithoutDiscount += p.price1.Value;
                    shopping.price1 += price;
                    amount += price;
                }
                else if ((p.price1 == null || p.price1 == 0) && (p.price2 == null || p.price2 == 0))
                    shopping = shoppingPrice1.GetOrAdd(p.entityID);

				if (shopping != null)
				{
					shopping.cnt++;
					shopping.Title = p.title;
					shopping.ProductTypeID = p.productTypeID;
                    shopping.entityID = p.entityID;
                    shopping.EntityItem = p.enity;
                    shopping.TypeName = p.typeName;
				}
            }

            decimal holdUnits = (unitsBuy + unitsInSubscribePlan) - units;
            var shoppings = new List<Shopping>(shoppingPrice1.Each(k => k.Value.entityID = k.Key).Select(s => s.Value));
            shoppings.AddRange(shoppingPrice2.Each(k => k.Value.entityID = k.Key).Select(s => s.Value));

            if (doSubscribe && !shopping_only_user_balance)
            {
                var shopping = new Shopping()
                {
                    price1 = productSubscribe.ProductEntity.price1 ?? 0,
                    price2 = productSubscribe.ProductEntity.price2 ?? 0,
                    cnt = 1,
                    Title = productSubscribe.title,
                    ProductTypeID = productSubscribe.ProductEntity.productTypeID,
                    TypeName = productSubscribe.ProductTypeName,
                    entityID = productSubscribe.entityID,
                    EntityItem = productSubscribe
                };
                shoppings.Add(shopping);
                amount += shopping.price1;
            }
            if (unitsBuy > 0)
            {
                var shopping = new Shopping()
                {
                    price1 = productUnits.ProductEntity.price1 ?? 0,
                    price2 = 0,
                    cnt = Convert.ToInt32(productUnits.ProductEntity.price2??1),
                    Title = "Units",
                    ProductTypeID = productUnits.ProductEntity.productTypeID,
                    TypeName = productUnits.ProductTypeName,
                    entityID = productUnits.entityID,
                    EntityItem = productUnits
                };
                shoppings.Add(shopping);
                amount += shopping.price1;
            }

            return new ShoppingTransactionInfo(amount, holdUnits, shoppings.ToArray(), subscribePlanFree, unitsBuy > 0 ? productUnits : null, doSubscribe, isSubscriber);
        }

        private static EntityItem[] GetProducts(long[] product_ids, bool is_subscriber, ShoppingDbManager context)
        {
            var products = new List<EntityItem>(context.SelectProductEntities(product_ids.Distinct().ToArray(), true, true));
            var productPackages = products.Where(p => p.ProductEntity.productTypeID == (short)ProductTypeEnum.Package).Select(p=>p.entityID).ToArray();
            if (productPackages.Length > 0)
            {
                if (!is_subscriber)
                {
                    var subscribeOnPackage = context.GetProducts(productPackages, new[] { (short)ProductTypeEnum.Subscribe }, true, true).FirstOrDefault();
                    if (subscribeOnPackage != null)
                    {
                        var productSubscribe = products.Where(p => p.ProductEntity.productTypeID == (short)ProductTypeEnum.Subscribe).SingleOrDefault();
                        subscribeOnPackage.active = false;
                        products.Remove(productSubscribe);
                        products.Add(subscribeOnPackage);
                    }
                }

                var productsOnPackage = context.SelectProductsXrefEntity(productPackages);
                foreach(var product in productsOnPackage)
                    products.Remove(products.Where(f=>f.entityID == product.entityID).FirstOrDefault());
            }
            return products.ToArray();
        }

        public long ShoppingPay(Guid user_id, ShoppingTransactionInfo transaction_info, string charge_type, string card_id,
            KeyValuePair<long, long?>? addresses, Func<long, string> pay)
        {
            DateTime now = DateTime.UtcNow;
            var transactionID = ShoppingBegin(user_id, card_id, addresses, transaction_info, now, charge_type);
            try
            {
                string newTranID = null;
                if (transaction_info.Amount > 0)
                    newTranID = pay(transactionID);

                ShoppingPrepaid(transactionID, newTranID, "Y");
                if (transaction_info.Amount == 0) this.ShoppingComplete(transactionID, "Y");
            }
            catch (Exception ex)
            {
                this.ShoppingRollback(transactionID, ex.InnerException != null ? ex.InnerException.Message : ex.Message);

                throw;
            }
            return transactionID;
        }

        public long ShoppingPayAuthorize(ShoppingTransactionInfo transactionInfo, KeyValuePair<long, long?> addresses, string chargeType, string cardID, Guid userID,
            Func<long, string> authorize, Func<long, string, string> pay)
        {
            DateTime now = DateTime.UtcNow;
            var transactionID = ShoppingBegin(userID, cardID, addresses, transactionInfo, now, chargeType);

            try
            {
                var tranID = authorize(transactionID);


                string newTranID = null;
                if (transactionInfo.Amount > 0)
                    newTranID = pay(transactionID, tranID);

                ShoppingPrepaid(transactionID, newTranID, "Y");

            }
            catch (Exception ex)
            {
                ShoppingRollback(transactionID, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw;
            }
            return transactionID;
        }

        public long CreditCardAuthorize(KeyValuePair<long, long?> addresses, string new_card_id, Guid userID, Func<long, string> authorize)
        {
            DateTime now = DateTime.UtcNow;
            var transactionInfo = new ShoppingTransactionInfo(0, 0, new Shopping[]{}, false, null, false, false);
            var transactionID = ShoppingBegin(userID, new_card_id, addresses, transactionInfo, now, ShoppingTransactionTypeEnum.authorize.ToString());

            try
            {
                var sjTranID = authorize(transactionID);
                this.Exec(System.Data.IsolationLevel.ReadCommitted,
                    (ShoppingDbManager context) =>ShoppingComplete(transactionID, "Y", context, sjTranID));
            }
            catch (Exception ex)
            {
                ShoppingRollback(transactionID, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw;
            }
            return transactionID;
        }



        public long ShoppingBegin(Guid user_id, string membership_cart_id, KeyValuePair<long,long?>? addresses, ShoppingTransactionInfo shopping_info, DateTime transaction_date, string transaction_charge_type)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (ShoppingDbManager context) => ShoppingBegin(user_id, membership_cart_id, addresses, shopping_info, transaction_date, transaction_charge_type, context));
        }

        private static long ShoppingBegin(Guid user_id, string membership_cart_id, KeyValuePair<long, long?>? addresses, ShoppingTransactionInfo shopping_info, DateTime transaction_date, string transaction_charge_type, ShoppingDbManager context)
        {
            if (shopping_info.HoldUnits < 0)
            {
                decimal newBalance = context.SelectMembership(user_id).Select(m => m.balance).Single() + shopping_info.HoldUnits;
                context.UpdateMembershipBalance(user_id, newBalance);
            }

            var transactionID = context.InsertTransaction(transaction_date, shopping_info.HoldUnits, shopping_info.Amount, null, null, null,
                transaction_charge_type, (short)ShoppingTransactionStateEnum.Begint);

            if (!string.IsNullOrEmpty(membership_cart_id))
                context.InsertTransactionCartPayment(transactionID, user_id, membership_cart_id);
            if (addresses != null)
            {
                context.InsertTransactionMembershipAddress(transactionID, addresses.Value.Key);
                if (addresses.Value.Value != null)
                    context.InsertTransactionMembershipAddress(transactionID, addresses.Value.Value.Value);
            }


            foreach (var s in shopping_info.ShoppingPrice)
            {
                var shoppingID = context.InsertShopping(s.entityID, user_id, ShoppingStateEnum.PayInProccess, transactionID, s.price1, s.price2, s.cnt);
                context.InsertShoppingLog(shoppingID, transaction_date, ShoppingStateEnum.PayInProccess);
            }

            return transactionID;
        }


        public void ShoppingPrepaid(long transaction_id, string tran_id, string response)
        {
            this.Exec(System.Data.IsolationLevel.ReadCommitted,
           (ShoppingDbManager context) =>
           {
               var transaction = context.SelectTransaction(transaction_id).Single();
               if (transaction.transactionState != (short)ShoppingTransactionStateEnum.Complete ||
                    transaction.transactionState != (short)ShoppingTransactionStateEnum.Prepaid ||
                    transaction.transactionState != (short)ShoppingTransactionStateEnum.Rollback)
               {
                   var shoppings = context.SelectShoppings(transaction_id).ToArray();

                   ShoppingHoldBalance(transaction, ShoppingStateEnum.Prepaid, context);
                   context.UpdateShoppingTransaction(transaction_id, tran_id, response, (short)ShoppingTransactionStateEnum.Prepaid);

                   foreach (var shopping in shoppings)
                   {
                       context.InsertShoppingLog(shopping.shoppingID, DateTime.UtcNow, ShoppingStateEnum.Prepaid);
                       context.UpdateShoppingState(shopping.shoppingID, ShoppingStateEnum.Prepaid);

                       if ((EntityItemTypeEnum)shopping.EntityItem.typeID == EntityItemTypeEnum.SubscribePlanItem)
                       {
                           SubscribeActivation(shopping.entityID, shopping.UserId, context);
                       }
                       else if ((EntityItemTypeEnum)shopping.EntityItem.typeID == EntityItemTypeEnum.PackageItem)
                       {
                           var purchasedFiles = context.SelectShoppings(shopping.UserId, new[] { (short)ShoppingStateEnum.Prepaid, (short)ShoppingStateEnum.PaySuccessed })
                               .Where(s => s.ProductTypeID == (short)ProductTypeEnum.File).Select(s => s.entityID).ToArray();
                           var filesInPackage = context.SelectProductsXrefEntity(new[]{shopping.entityID}, (short)ProductTypeEnum.File).ToArray()
                               .Where(p => !purchasedFiles.Contains(p.entityID));
                           foreach (var file in filesInPackage)
                               context.InsertShopping(file.entityID, shopping.UserId, ShoppingStateEnum.Prepaid, transaction_id, 0, 0, 1);
                       }
                   }
               }
               else
                   throw new Exception("Internal error in: ShoppingService.ShoppingPrepaid: transation state is " + transaction.transactionState);

           });

        }

        public void ShoppingComplete(long transaction_id, string response)
        {
            this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (ShoppingDbManager context) => ShoppingComplete(transaction_id, response, context));
        }

        private static void ShoppingComplete(long transaction_id, string response, ShoppingDbManager context, string sj_tran_id=null)
        {
            var transaction = context.SelectTransaction(transaction_id).Single();
            if (transaction.transactionState != (short)ShoppingTransactionStateEnum.Complete ||
                transaction.transactionState != (short)ShoppingTransactionStateEnum.Rollback)
            {
                UpdateShoppings(transaction_id, ShoppingStateEnum.PaySuccessed, context);
                if(string.IsNullOrEmpty(sj_tran_id))
                    context.UpdateShoppingTransaction(transaction_id, response, (short)ShoppingTransactionStateEnum.Complete);
                else
                    context.UpdateShoppingTransaction(transaction_id, sj_tran_id, response, (short)ShoppingTransactionStateEnum.Complete);
            }
            else
                throw new Exception("Internal error in: ShoppingService.ShoppingComplete: transation state is " + transaction.transactionState);
        }

        public void ShoppingRollback(long transaction_id, string response)
        {
            this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (ShoppingDbManager context) =>
               {
                   var transaction = context.SelectTransaction(transaction_id).Single();
                   if (transaction.transactionState != (short)ShoppingTransactionStateEnum.Complete ||
                       transaction.transactionState != (short)ShoppingTransactionStateEnum.Rollback)
                   {
                       ShoppingHoldBalance(transaction, ShoppingStateEnum.PayError, context);
                       context.UpdateShoppingTransaction(transaction_id, null, response.Substring(0, Math.Min(response.Length, 1024)), (short)ShoppingTransactionStateEnum.Rollback);
                       UpdateShoppings(transaction_id, ShoppingStateEnum.PayError, context);
                   }
                   else
                       throw new Exception("Internal error in: ShoppingService.ShoppingRollback: transation state is " + transaction.transactionState);
               });
        }


        private static void ShoppingHoldBalance(ShoppingTransaction transaction, ShoppingStateEnum state_id, ShoppingDbManager context)
        {
            var balance = context.SelectMembership(transaction.ShoppingCartPayment.UserId).Single().balance;

            if (state_id == ShoppingStateEnum.Prepaid && transaction.holdBalance > 0)
                balance = balance + transaction.holdBalance;
            else if (state_id == ShoppingStateEnum.PayError && transaction.transactionState == (short)ShoppingTransactionStateEnum.Prepaid)
                balance = balance + transaction.holdBalance * -1;
            else if (state_id == ShoppingStateEnum.PayError && transaction.transactionState == (short)ShoppingTransactionStateEnum.Begint && transaction.holdBalance<0)
                balance = balance + Math.Abs(transaction.holdBalance);
            else if (state_id != ShoppingStateEnum.Prepaid && state_id != ShoppingStateEnum.PayError)
                throw new Exception("Internal error in ShoppingService.ShoppingPrepaid: only error, prepaid or success shopping state, " + state_id.ToString());

            context.UpdateMembershipBalance(transaction.ShoppingCartPayment.UserId, balance);
            //context.UpdateTransactionHoldBalance(transaction_id, 0);
        }

        private static void UpdateShoppings(long transaction_id, ShoppingStateEnum state_id, ShoppingDbManager context)
        {
            var shoppings = context.SelectShoppings(transaction_id).ToArray();
            foreach (var shopping in shoppings)
            {
                context.InsertShoppingLog(shopping.shoppingID, DateTime.UtcNow, state_id);
                context.UpdateShoppingState(shopping.shoppingID, state_id);
            }
        }



        private void SubscribeActivation(long subscribe_id,/* int max_units,*/ Guid user_id, ShoppingDbManager context)
        {
            var subscribe = context.GetSubscribe(subscribe_id).SingleOrDefault();
            var currentSubscribe = context.SelectUserXrefSubscribePlan(user_id).SingleOrDefault();

            if (subscribe != null)
            {
                var isChildSubscribe = context.GetSubscribe(subscribe.EntityItem.parentEntityID).Any();
                var start = currentSubscribe != null ? currentSubscribe.endSubscribeDate ?? DateTime.UtcNow : DateTime.UtcNow;
                var begin = isChildSubscribe && currentSubscribe != null && currentSubscribe.startSubscribeDate.HasValue ? currentSubscribe.startSubscribeDate.Value : start;
                var end = start.AddMonths(subscribe.durationInMonths).AddDays(subscribe.durationInDays);
                var membership = context.SelectMembership(user_id).Single();

                if (subscribe.durationInDays == 0 && membership.chargeDay != null && membership.chargeDay.Value > 0)
                {
                    var daysInChargeMonth = DateTime.DaysInMonth(end.Year, end.Month);
                    var endDay = Math.Min(daysInChargeMonth, membership.chargeDay.Value);
                    end = end.AddDays(endDay - end.Day);
                }

                //decimal balance = membership.balance;
                //if (balance > max_units)
                //    balance += (decimal)subscribe.freeUnitsOnSubscribe;
                //else
                //    balance = Math.Min((decimal)subscribe.freeUnitsOnSubscribe + balance, max_units);
                //context.UpdateMembershipBalance(user_id, balance);

                if (membership.subscribeActivation == null)
                    context.UpdateMembershipSubscribeActivation(user_id, begin, (byte)(subscribe.durationInDays > 0 ? end.Day : start.Day));

                context.DeleteMembershipXrefSubscribe(user_id);
                context.InsertMembershipXrefSubscribe(user_id, subscribe_id, begin, end, end);
            }

        }

        public ShoppingTransaction[] GetShoppingTransactions(ShoppingStateEnum shopping_state_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ShoppingDbManager context) =>
               {
                   return context.GetShoppingTransactions(shopping_state_id).ToArray();
               });
        }

    }
}
