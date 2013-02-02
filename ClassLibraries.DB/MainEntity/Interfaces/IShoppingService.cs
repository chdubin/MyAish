using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using MainCommon;
using MainEntity.Models.Shopping;

namespace MainEntity.Interfaces
{
    public interface IShoppingService
    {
        int AddItemToShoppingCart(Guid? user_id, long item_id, CartItemTypeEnum item_type_id, HttpContext context, int cnt = 1);

        void DeleteItemFromShoppingCart(CartItemTypeEnum item_type_id, HttpContext context, long item_id = -1);

        void UpdateClassproductQuantity(int quantity, int product_id, HttpContext context);

        void EmptyCart(HttpContext context);

        long GetSubscribeInCartID(HttpContext context);

        long GetUnitsInCart(HttpContext context);

        long[] GetClassProductInCartIDs(HttpContext context);

        long[] GetPackageInCartIDs(HttpContext context);

        long[] GetAllProductsInCart(HttpContext context);

        //void SetUnitsCountInCart(int count, HttpContext context);

        int BuyFreeOffers(long[] entity_ids, Guid user_id);

        SubscribePlanEntity GetUserSubscribePlan(Guid user_id);

        Membership GetMembership(Guid user_id);

        bool IsUserHaveSubscribePlan(Guid user_id);

        long[] SelectLibraryItemsIDs(Guid user_id);

        //int CancelSubscribe(Guid user_id);

        bool IsSubscribeCancel(Guid user_id);

        KeyValuePair<long, DateTime>[] SelectLibraryItemsIDsWithShoppingDate(Guid user_id);//,int start_row_index, int max_rows_count);
// this was added on feb 2 from version i had download from git hube  
MainEntity.Models.Activity.RoyaltyLog[] SelectUserIDsByClassPurchase(long classId, DateTime startDate, DateTime endDate);

        EntityItem[] GetProducts(long parent_id, ProductTypeEnum product_type);

        EntityItem GetProduct(long product_id);

        //KeyValuePair<long, decimal> ShoppingBegin(Dictionary<long, int> product_ids, Guid user_id, string membership_cart_id, long address_id, decimal unit_rate, DateTime transaction_date);

        ShoppingTransactionInfo GetShoppingTransactionInfo(long[] product_ids, Guid user_id, bool shopping_only_user_balance, decimal discount_percent, int max_units_on_subscribe, bool subscribe_activation = false);

		long ShoppingBegin(Guid user_id, string membership_cart_id, KeyValuePair<long, long?>? addresses, ShoppingTransactionInfo shopping_info, DateTime transaction_date, string transaction_charge_type);

        void ShoppingPrepaid(long transaction_id, string tran_id, string response);

		void ShoppingComplete(long transaction_id, string response);

        ShoppingTransaction[] GetShoppingTransactions(ShoppingStateEnum shopping_state_id);

        long[] ShoppingAtUnits(long[] product_ids, Guid user_id, DateTime transaction_date, decimal discount_percent, int max_units_on_subscribe);

		Membership GetMembershipAndActiveSubscribe(Guid user_id, DateTime current_date);

        ShoppingClass[] SelectLibraryItems(Guid user_id, int start_row_index, int page_size);

        int SelectLibraryItemsCount(Guid user_id);

        long[] SelectLibraryItemsClassIds(Guid user_id);

        int SelectLibraryItemsCnt(Guid user_id);

        int UpdateShopping(Guid user_id, IDictionary<long, bool> shopping_list);

        int UpdateShoppingAvailable(Guid user_id, IDictionary<long, bool> shopping_list);

        int InsertFilesInLibrary(long[] product_file_ids, Guid user_id);

        Shopping[] GetOrders(Guid user_id, long transaction_id);

        
        int GetShoppingTransactionsCnt(Guid guid, DateTime? since_date = null, DateTime? before_date = null, short[] transaction_state_list = null, string[] charge_type_list = null);

        UserShoppingTransaction[] GetShoppingTransactions(Guid user_id, int? start_row = null, int? max_row_count = null, DateTime? since_date = null, DateTime? before_date = null, short[] transaction_state_list = null, string[] charge_type_list = null);

        UserShoppingTransaction GetShoppingTransaction(long shopping_transaction_id);

        ShoppingMembershipAddress[] GetShoppingAddresses(long[] from_transaction_ids);


        int GetShoppingTransactionsCnt(DateTime? since_date = null, DateTime? before_date = null, short[] transaction_state_list = null, string[] charge_type_list = null);

        ReportShoppingTransaction[] GetShoppingTransactions(int? start_row = null, int? max_row_count = null, DateTime? since_date = null, DateTime? before_date = null, short[] transaction_state_list = null, string[] charge_type_list = null);

        long GetShoppingID(Guid user_id, long from_entity_id);

        void ShoppingRollback(long transaction_id, string response);

        long ShoppingPay(Guid user_id, ShoppingTransactionInfo transaction_info, string charge_type, string card_id,
            KeyValuePair<long, long?>? addresses, Func<long, string> pay);

        long ShoppingPayAuthorize(ShoppingTransactionInfo transactionInfo, KeyValuePair<long, long?> addresses,
            string chargeType, string cardID, Guid userID,
            Func<long, string> authorize, Func<long, string, string> pay);

        long CreditCardAuthorize(KeyValuePair<long, long?> addresses, string new_card_id, Guid userID, Func<long, string> authorize);

    }
}
