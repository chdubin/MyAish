using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.User;
using MainCommon;

namespace MainEntity.Interfaces
{
    public interface IUserService
    {
        //List<KeyValuePair<long, string>> GetSubscribePlanEntities();

        SubscribePlanEntity GetSubscribePlan(long plan_id);

        aspnet_User[] GetAspnetUsers(string cur_app_name, string role_name, int start_index, int page_size);

        Membership[] GetUsers(string cur_app_name, int start_index, int page_size, MembershipTypeEnum? membership_type,
            string filter_email, string filter_user_name, string filter_first_name, string filter_last_name, DateTime? filter_membership_start, DateTime? filter_membership_end,
            bool? filter_canceled, bool? filter_declined, int? filter_charge_in_days, bool? filter_charge_in_days_exact);

        int GetUsersCount(string cur_app_name, MembershipTypeEnum? membership_type,
            string filter_email, string filter_user_name, string filter_first_name, string filter_last_name, DateTime? filter_membership_start, DateTime? filter_membership_end,
            bool? filter_canceled, bool? filter_declined, int? filter_charge_in_days, bool? filter_charge_in_days_exact);

		MembershipCart GetLastMembershipCard(Guid user_id);

        int InsertOrUpdateMembershipCard(Guid user_id, DateTime? expiration_date, string card_type,
            string membership_card_id, CartStateEnum card_state);


        Membership GetUser(Guid user_id);

        Membership[] GetActivationSubscribers(int start_row_index, int max_rows);

        long GetNexSubscribePlan(long current_subscribe_plan);


        int UpdateUser(Guid user_id, decimal balance, string description, string first_name,
            string last_name, string referrer_code, string city, string country, string day_phone,
            string phone, string postal_address, string postal_code, string state, DateTime? start_subscribe_date, long plan_id,
            int free_offer_cnt, bool suspended, bool full_library_access);

        int LockUser(Guid user_id);

        int LockUser(Guid user_id, string email);

        int UpdateBalance(Guid user_id, decimal balance);

        int UnSuspend(Guid user_id);

        int UpdateProfile(Guid user_id, string first_name, string last_name, string city, string country, string day_phone, 
            string phone, string postal_address, string postal_code, string state);

        int UpdatePlanDates(Guid user_id, DateTime? start_date, DateTime? end_date, byte? charge_day);

        int UpdateEmail(Guid user_id, string email);

        int DeletePlan(Guid user_id);

        bool IsEmailUniqueForApplication(string application_name, string email);

        bool IsUserNameUniqueForApplication(string application_name, string user_name);

        int UpdateUserName(Guid user_id, string user_name);

		int InsertMembership(Guid user_id, string first_name, string last_name, decimal balance, int free_offer_cnt,
			string country, string state, string city, string postal_address, string postal_code, string phone, string day_phone, bool fullLibraryAccess,
			string referrer_code = null, long subscribe_plan_id = 0, DateTime? start_subscribe_date = null);

        KeyValuePair<long, long?> InsertAddesses(Guid user_id,
            string first_name, string last_name, string country, string state, string city, string postal_address, string postal_code, string phone, string day_phone=null, string description=null,
            string ship_first_name = null, string ship_last_name = null, string ship_country = null, string ship_state = null, string ship_city = null, string ship_postal_address = null, string ship_postal_code = null, string ship_phone = null, string ship_day_phone = null, string ship_description = null);

		int UpdateUserAfterCardAuthorization(Guid user_id, string cart_number, string sj_tran_id, long shopping_transaction_id);

        int UpdateMembershipCardState(Guid user_id, string card_number, CartStateEnum cart_state_id);
		//int UpdateMembershipCardTranID(Guid user_id, string card_number, string tran_id);

		int CancelSubscribe(Guid user_id);

        int DelaySubscribeActivation(Guid user_id, int delay_hours, byte max_tray_activation_cnt);

        System.Web.Security.MembershipUser CreateUser(System.Web.Security.MembershipProvider curProvider, string email, string password, bool is_approved,
            string first_name, string last_name, int free_offer_cnt, string country, string state, string city, string postal_address, string postal_code, string phone, string day_phone, bool full_library_access);
    }
}
