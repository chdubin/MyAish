using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Text;
using MainCommon;

namespace Main.Areas.Admin.Models.Common
{
    public class PagingData
    {
        // p - page num
        // ps - page size
        // rn - start row number
        public string ClearPagerPath { get; set; }
        public string PagerPathWithSize { get; set; }

        public string FirsPageUrl { get; set; }
        public string PrevPageUrl { get; set; }
        public string NextPageUrl { get; set; }
        public string LastPageUrl { get; set; }
        public string LastFullPageUrl { get; set; }

        public int CurrentPageNum { get; set; }
        public int TotalPageCnt { get; set; }
        public int TotalRowCnt { get; set; }
        public int StartRowIndex { get; set; }
        public int EndPageRowIndex { get; set; } 
        public int PageSize { get; set; }
        public int StartRowForLastFullPage { get; set; }

        public PagingData(string path, NameValueCollection query_string, int total_row_cnt, int page_size, int current_page_num, 
            int current_row_number, int default_page_size)
        {
            
            TotalPageCnt = (int)Math.Ceiling((double)total_row_cnt / page_size);
            TotalRowCnt = total_row_cnt;
            PageSize = page_size;
            CurrentPageNum = current_page_num;

            if (current_row_number > 1)
                StartRowIndex = current_row_number;
            else
                StartRowIndex = (current_page_num - 1) * page_size;

            if (total_row_cnt < page_size)
                StartRowForLastFullPage = 0;
            else
                StartRowForLastFullPage = total_row_cnt - page_size;

            if (current_row_number <= 1)
            {
                if (StartRowIndex + page_size <= total_row_cnt)
                    EndPageRowIndex = StartRowIndex + page_size;
                else
                    EndPageRowIndex = StartRowIndex + total_row_cnt % page_size;
            }
            else 
            {
                EndPageRowIndex = current_row_number + page_size;
            }

            List<KeyValuePair<string, string>> qs = MyUtils.ExcludeQueryParam(query_string, new string[] { "p", "ps", "rn" });

            ClearPagerPath = (path + "?" + MyUtils.GetKeyValuePairString(qs));

            if (page_size != default_page_size)
                qs.Add(new KeyValuePair<string, string>("ps", page_size.ToString()));

            PagerPathWithSize = (path + "?" + MyUtils.GetKeyValuePairString(qs));

            FirsPageUrl = (path + "?" + MyUtils.GetKeyValuePairString(qs)).TrimEnd(new char[] { '?', '&' });

            if (current_row_number <= 1)
            {
                PrevPageUrl = (path + "?" + MyUtils.GetKeyValuePairString(qs) + 
                    (CurrentPageNum > 2 ? "p=" + (CurrentPageNum - 1).ToString() : string.Empty)).TrimEnd(new char[] { '?', '&' });
                NextPageUrl = path + "?" + MyUtils.GetKeyValuePairString(qs) + "p=" + (CurrentPageNum < TotalPageCnt ? CurrentPageNum + 1 : TotalPageCnt);
            }
            else 
            {
                PrevPageUrl = (path + "?" + MyUtils.GetKeyValuePairString(qs) +
                    (StartRowIndex > PageSize ? "rn=" + (StartRowIndex - PageSize + 1).ToString() : string.Empty)).TrimEnd(new char[] { '?', '&' });
                NextPageUrl = path + "?" + MyUtils.GetKeyValuePairString(qs) + "rn=" + (StartRowIndex < StartRowForLastFullPage ? StartRowIndex + PageSize + 1 : StartRowForLastFullPage + 1);
            }

            LastPageUrl = path + "?" + MyUtils.GetKeyValuePairString(qs) + "p=" + TotalPageCnt;
            LastFullPageUrl = path + "?" + MyUtils.GetKeyValuePairString(qs) + "rn=" + (StartRowForLastFullPage + 1);
        }
    }
}