using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainCommon;

namespace Main.Models.ControllerView.Account
{
    public class DetailShoppingTransactions
    {
        public DateTime CreatedDate { get; set; }
        public long SelectedTransactionID { get; set; }
		public MainEntity.Models.Shopping.UserShoppingTransaction[] Transactions { get; set; }
        public MainEntity.Models.Shopping.Shopping[] ShoppingInSelectedTransaction { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Taxes { get; set; }
        public decimal FinalTotal { get; set; }

        public MainEntity.Models.Shopping.MembershipAddress BillingAddress{get;private set;}
        public MainEntity.Models.Shopping.MembershipAddress ShippingAddress{get;private set;}

		public DetailShoppingTransactions(MainEntity.Models.Shopping.UserShoppingTransaction[] transactions,
            MainEntity.Models.Shopping.Shopping[] orders, long cur_trans_id, DateTime create_date, MainEntity.Models.Shopping.ShoppingMembershipAddress[] addresses)
        {
            var transaction = transactions.Where(t => t.shoppingTransactionID == cur_trans_id).Single();

            Transactions = transactions;
            ShoppingInSelectedTransaction = orders.Where(sh => sh.ProductTypeID != (short)ProductTypeEnum.Shipping &&
                    sh.ProductTypeID != (short)ProductTypeEnum.Taxes).ToArray();
            SelectedTransactionID = cur_trans_id;
            CreatedDate = create_date;

            FinalTotal = transaction.amount ?? 0;

            var shippingOrders = orders.Where(o => o.ProductTypeID == (short)ProductTypeEnum.Shipping).ToArray();
            Shipping = shippingOrders == null ? 0 : shippingOrders.Sum(s => s.price1);

            Taxes = orders.Where(o => o.ProductTypeID == (short)ProductTypeEnum.Taxes).Sum(o => o.price1);

            SubTotal = FinalTotal - Taxes - Shipping;

            BillingAddress = addresses.Where(a => a!=null && a.MembershipAddress.isBillingAddress).Select(a => a.MembershipAddress).FirstOrDefault();
            ShippingAddress = addresses.Where(a => a != null && !a.MembershipAddress.isBillingAddress).Select(a => a.MembershipAddress).FirstOrDefault();
        }
            
    }
}