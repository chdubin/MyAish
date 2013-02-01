using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainCommon;

namespace Main.Areas.Admin.Models.ControllerView.Transaction
{
    public class TransactionDetail
    {
        public DateTime CreatedDate { get; set; }
        public long SelectedTransactionID { get; set; }
        public MainEntity.Models.Shopping.Shopping[] ShoppingInSelectedTransaction { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Taxes { get; set; }
        public decimal FinalTotal { get; set; }

        public MainEntity.Models.Shopping.MembershipAddress BillingAddress{get;private set;}
        public MainEntity.Models.Shopping.MembershipAddress ShippingAddress{get;private set;}

        public TransactionDetail()
        {
        }

        public TransactionDetail(MainEntity.Models.Shopping.UserShoppingTransaction transaction,
            MainEntity.Models.Shopping.Shopping[] orders, MainEntity.Models.Shopping.ShoppingMembershipAddress[] addresses)
        {
            ShoppingInSelectedTransaction = orders.Where(sh => sh.ProductTypeID != (short)ProductTypeEnum.Shipping &&
                    sh.ProductTypeID != (short)ProductTypeEnum.Taxes).ToArray();
            SelectedTransactionID = transaction.shoppingTransactionID;
            CreatedDate = transaction.createDate;

            FinalTotal = transaction.amount ?? 0;

            var shippingOrders = orders.Where(o => o.ProductTypeID == (short)ProductTypeEnum.Shipping).ToArray();
            Shipping = shippingOrders == null ? 0 : shippingOrders.Sum(s => s.price1);

            Taxes = orders.Where(o => o.ProductTypeID == (short)ProductTypeEnum.Taxes).Sum(o => o.price1);

            SubTotal = FinalTotal - Taxes - Shipping;

            BillingAddress = addresses.Where(a => a.MembershipAddress.isBillingAddress).Select(a => a.MembershipAddress).FirstOrDefault();
            ShippingAddress = addresses.Where(a => !a.MembershipAddress.isBillingAddress).Select(a => a.MembershipAddress).FirstOrDefault();
        }

    }
}