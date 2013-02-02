using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using MainCommon;
using System.Web.Routing;

namespace Main.Areas.Admin.Models.Common
{
    public class AjaxPagingData
    {
        public int TotalRowsCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPageNum { get; set; }
        public int MaxPageNum { get; set; }
        
        public string ActionName { get; set; }
        public NameValueCollection Params { get; set; }
        public string UpdateTargetID { get; set; }

        public AjaxPagingData(int cur_page_num, int page_size, int total_rows_count, string action_name, 
            string update_target_id, NameValueCollection parameters)
        {
            CurrentPageNum = cur_page_num;
            PageSize = page_size;
            TotalRowsCount = total_rows_count;
            MaxPageNum = Convert.ToInt32(Math.Ceiling((double)total_rows_count / page_size));

            ActionName = action_name;
            Params = parameters;
            UpdateTargetID = update_target_id;
        }

        public RouteValueDictionary GetRouteValues(int page_num)
        {
            RouteValueDictionary rval = new RouteValueDictionary();
            foreach (var k in Params.AllKeys)
                rval.Add(k, Params[k]);

            rval.Add("random", DateTime.Now.Ticks);
            rval.Add("page_num", page_num);

            return rval;
        }
    }
}