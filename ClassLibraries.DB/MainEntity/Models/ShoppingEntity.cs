using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainCommon;

namespace MainEntity.Models.Shopping
{
	public partial class Membership
	{
		public DateTime? StartSubscribeDate { get; set; }

		public DateTime? EndSubscribeDate { get; set; }

        public bool IsDeclinedSubscribe { get; set; }

        public bool IsCanceledSubscribe { get; set; }

		public long SubscribePlanID { get; set; }
	}

    public partial class Shopping
    {
        public string ShoppingStateName { get; set; }
        public string Speaker { get; set; }
        public string Title { get; set; }
        public string ParentTitle { get; set; }
        public string TypeName { get; set; }
        public string ItemNumber { get; set; }
        public short ProductTypeID { get; set; }
        public long ProductID { get; set; }

        public decimal Price1WithoutDiscount { get; set; }
    }

    public partial class ProductEntity
    {
        public int Count { get; set; }

        public long TransactionID { get; set; }
    }

    public partial class ShoppingTransaction
    {
        public decimal Amount { get; set; }

        public string PaymentCartID { get; set; }

    }

    public partial class UserShoppingTransaction
    {
        public MembershipAddress BillingAddress { get; set; }

        public MembershipAddress ShippingAdress { get; set; }
    }

    public class ShoppingTransactionInfo
    {
        public decimal Amount { get; private set; }

        public decimal HoldUnits { get; private set; }

        public Shopping[] ShoppingPrice { get; private set; }

		public bool SubscribePlanFree { get; private set; }

        public bool DoSubscribe { get; set; }

        public bool IsSubscriber { get; set; }

		public EntityItem UnitsBuy { get; private set; }

        public ShoppingTransactionInfo(decimal amount, decimal hold_units, Shopping[] shopping_price, bool subscribe_plan_free, EntityItem units_buy, bool do_subscribe, bool is_subscriber)
        {
            Amount = amount;
            HoldUnits = hold_units;
            ShoppingPrice = shopping_price;
			SubscribePlanFree = subscribe_plan_free;
			UnitsBuy = units_buy;

            DoSubscribe = do_subscribe;
            IsSubscriber = is_subscriber;
        }
    }

    public class ReportShoppingTransaction
    {
        public Guid UserId { get; set; }

        public string MembershipFirstName { get; set; }

        public string MembershipLastName { get; set; }

        public string MembershipUserName { get; set; }

        public string CurrentSubscribeTitle { get; set; }

        public long? CurrentSubscribeID { get; set; }

        public DateTime? CurrentSubscribeEndDate { get; set; }

        public string CardID { get; set; }

        public long TransactionID { get; set; }

        public decimal? Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string TransactionType { get; set; }

        public short TransactionState { get; set; }

        public ProductTypeEnum[] ProductTypes { get; set; }

        public string Response { get; set; }
    }

    public partial class EntityItem
    {
        public short ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }

        public string CodeParent { get; set; }
        public string Code { get; set; }
    }
}
