using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainCommon;
using MainEntity.Models.Activity;

namespace MainEntity.Interfaces
{
	public interface IActivityLogService
	{
		int LoggingUserEmail(ActivityLogTypeEnum log_type_id, string request_ip, string email, Guid? user_id);

		int LoggingEntityItem(ActivityLogTypeEnum log_type_id, string request_ip, long entity_id, Guid? user_id);

        int LoggingShopping(ActivityLogTypeEnum log_type_id, string request_ip, long shopping_id, long entity_id, Guid user_id);

        int GetActivityLogCnt(Guid user_id, DateTime? since, DateTime? before, ActivityLogTypeEnum[] activity_log_types, bool grouping_class);
        ActivityLog[] GetActivityLog(ClassActivityLogSortEnum sort, Guid user_id, DateTime? since, DateTime? before, ActivityLogTypeEnum[] activity_log_types, bool grouping_class, int start_row_index, int max_rows_count);
        long GetLogID(Guid user_id, long from_entity_id);

        KeyValuePair<long, DateTime>[] SelectLibraryItemsIDsWithShoppingDate(Guid user_id, ActivityLogTypeEnum[] activity_log_types, DateTime dateLimit);//,int start_row_index, int max_rows_count);
        KeyValuePair<long, DateTime>[] SelectLibraryItem(Guid user_id, ActivityLogTypeEnum[] activity_types, long prod_id, DateTime dateLimit);

        LastActivityLog[] GetLastActivity(Guid[] user_ids, ActivityLogTypeEnum[] activity_log_types);
    }
}
