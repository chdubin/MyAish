using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MainCommon;

namespace Main.Models.Shopping
{
    public class CartModel
    {
        public bool IsEmptyShoppingCart { get; set; }

        public MainEntity.Models.Shopping.Shopping[] ProductList1 { get; set; }

        public MainEntity.Models.Shopping.Shopping[] ProductList2 { get; set; }

        public MainEntity.Models.Shopping.Shopping ProductSubscribePlan { get; set; }

        public MainEntity.Models.Shopping.Shopping ProductUnits { get; set; }

        public bool DoSubscribe { get; set; }

        public bool IsSubscriber { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }


        public CartModel()
        {

        }

        public CartModel(MainEntity.Models.Shopping.ShoppingTransactionInfo info, decimal balance, MainEntity.Models.Shopping.EntityItem[] units_list)
        {
            this.ProductList1 = info.ShoppingPrice.Where(p => (p.price1 > 0 || (p.price1 == 0 && p.price2 == 0)) && p.ProductTypeID != (short)ProductTypeEnum.Subscribe && p.ProductTypeID != (short)ProductTypeEnum.Units).ToArray();
            this.ProductList2 = info.ShoppingPrice.Where(p => p.price2 > 0 && p.ProductTypeID != (short)ProductTypeEnum.Subscribe && p.ProductTypeID != (short)ProductTypeEnum.Units).ToArray();
            this.ProductSubscribePlan = info.ShoppingPrice.Where(p => p.ProductTypeID == (short)ProductTypeEnum.Subscribe && p.EntityItem.active).SingleOrDefault();
            if(info.UnitsBuy!=null)
                this.ProductUnits = new MainEntity.Models.Shopping.Shopping()
                {
                    price1 = info.UnitsBuy.ProductEntity.price1.Value,
                    price2 = info.UnitsBuy.ProductEntity.price2.Value,
                    cnt = 1,
                    Price1WithoutDiscount = info.UnitsBuy.ProductEntity.price1.Value,
                    entityID = info.UnitsBuy.entityID,
                    Title = info.UnitsBuy.title,
                    ProductTypeID = info.UnitsBuy.ProductEntity.productTypeID
                };
            if ((info.HoldUnits + balance) < 0)
            {
                var needUnits = Math.Abs(info.HoldUnits + balance) + (this.ProductUnits != null ? this.ProductUnits.price2 : 0);
                var productUnits = units_list.Where(u => u.ProductEntity.price2 >= needUnits).OrderBy(u => u.ProductEntity.price2).FirstOrDefault();
                var shopping = new MainEntity.Models.Shopping.Shopping() { price1 = productUnits.ProductEntity.price1 ?? 0,price2= productUnits.ProductEntity.price2 ?? 0, cnt = 1, Title = productUnits.title, ProductTypeID = productUnits.ProductEntity.productTypeID, entityID = productUnits.entityID };
                this.ProductUnits = shopping;
            }

            DoSubscribe = info.DoSubscribe;
            IsSubscriber = info.IsSubscriber;
            Amount = info.Amount;
            Balance = balance;
        }

    }
}