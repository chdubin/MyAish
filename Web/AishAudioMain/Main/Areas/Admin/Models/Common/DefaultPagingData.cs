using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using MainCommon;

namespace Main.Areas.Admin.Models.Common
{
    public class DefaultPagingData
    {
        public int CurrentPageNum { get; set; }
        public int TotalRowsCount { get; set; }
        public int PageSize { get; set; }
        public int MaxPageNum { get; set; }

        public string ActionName { get; set; }
        public string UpdateTargetId { get; set; }
        public string AddParams { get; set; }

        public DefaultPagingData(int cur_page_num, int total_rows_count, int page_size, string action_name, string update_target_id, NameValueCollection query_string)
        {
            CurrentPageNum = cur_page_num;
            TotalRowsCount = total_rows_count;
            PageSize = page_size;
            MaxPageNum = Convert.ToInt32(Math.Ceiling((double)total_rows_count / page_size));

            ActionName = action_name;
            UpdateTargetId = update_target_id;

            List<KeyValuePair<string, string>> qs = MyUtils.ExcludeQueryParam(query_string, new string[] { "page_num", "page_size", "products_to_add", "products_to_delete" });
            AddParams = "?" + MyUtils.GetKeyValuePairString(qs);
        }
    }
}