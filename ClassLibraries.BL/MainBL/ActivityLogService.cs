using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPartnerKinoBL.Extension;
using MainEntity.Interfaces;
using System.Data;
using MainEntity;
using MainCommon;
using MainEntity.Models.Activity;

namespace MainBL
{
    public class ActivityLogService : BaseBO, IActivityLogService
    {
        public ActivityLogService(string connection_name)
            : base(connection_name)
        {
        }

        public int LoggingUserEmail(ActivityLogTypeEnum log_type_id, string request_ip, string email, Guid? user_id)
        {
            return this.Exec(IsolationLevel.ReadCommitted,
                (ActivityLogDbManager context) =>
                {
                    var logID = context.InsertActivityLog(log_type_id, DateTime.UtcNow, request_ip);

                    var emailID = context.GetEmail(email).Select(e => e.emailID).SingleOrDefault();
                    if (emailID == 0)
                        emailID = context.InsertEmail(email);

                    var rval = context.InsertActivityLogXrefEmail(logID, emailID);
                    if (user_id != null)
                        context.InsertActivityLogXrefUsers(logID, user_id.Value);

                    return rval;
                });
        }


        public int LoggingEntityItem(ActivityLogTypeEnum log_type_id, string request_ip, long entity_id, Guid? user_id)
        {
            return this.Exec(IsolationLevel.ReadCommitted,
                (ActivityLogDbManager context) =>
                {
                    var logID = context.InsertActivityLog(log_type_id, DateTime.UtcNow, request_ip);
                    var rval = context.InsertActivityLogXrefEntityItem(logID, entity_id);
                    if (user_id != null)
                        context.InsertActivityLogXrefUsers(logID, user_id.Value);

                    return rval;
                });
        }

        public int LoggingShopping(ActivityLogTypeEnum log_type_id, string request_ip, long shopping_id, long entity_id, Guid user_id)
        {
            return this.Exec(IsolationLevel.ReadCommitted,
                (ActivityLogDbManager context) =>
                {
                    var logID = context.InsertActivityLog(log_type_id, DateTime.UtcNow, request_ip);
                    var rval = context.InsertActivityLogXrefShopping(logID, shopping_id);
                    context.InsertActivityLogXrefEntityItem(logID, entity_id);
                    context.InsertActivityLogXrefUsers(logID, user_id);

                    return rval;
                });
        }

        public int GetActivityLogCnt(Guid user_id, DateTime? since, DateTime? before, ActivityLogTypeEnum[] activity_log_types, bool grouping_class)
        {
            return this.Exec(IsolationLevel.Snapshot,
                (ActivityLogDbManager context) =>
                {
                    int rval;
                    var query = context.SelectActivityLog(user_id, activity_log_types, since, before);

                    if (grouping_class)
                    {
                        var query2 = from res1 in
                                         (from res in query
                                          group res by res.ClassID into g
                                          select new { id = 1 })
                                     group res1 by res1.id into g1
                                     select g1.Count();
                        rval = query2.FirstOrDefault();

                    }
                    else rval = query.Count();

                    return rval;
                });

        }

        public long GetLogID(Guid user_id, long from_entity_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ActivityLogDbManager context) =>
               {
                   return context.SelectActivityLog(user_id, from_entity_id, new ActivityLogTypeEnum[] { ActivityLogTypeEnum.DownloadClass }, null, null).Select(x => x.ClassID).FirstOrDefault();
               });
        }

        public KeyValuePair<long, DateTime>[] SelectLibraryItemsIDsWithShoppingDate(Guid user_id, ActivityLogTypeEnum[] activity_log_types, DateTime dateLimit)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ActivityLogDbManager context) =>
              {
                  return context.SelectLibraryItemsIDsWithShoppingDate(user_id, activity_log_types, dateLimit).ToArray();
              });
        }

        public KeyValuePair<long, DateTime>[] SelectLibraryItem(Guid user_id, ActivityLogTypeEnum[] activity_log_types, long prod_id, DateTime dateLimit)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ActivityLogDbManager context) =>
              {
                  return context.SelectLibraryItem(user_id, activity_log_types, prod_id, dateLimit).ToArray();
              });
        }

        public ActivityLog[] GetActivityLog(ClassActivityLogSortEnum sort, Guid user_id, DateTime? since, DateTime? before, ActivityLogTypeEnum[] activity_log_types,
            bool grouping_class,
            int start_row_index, int max_rows_count)
        {
            return this.Exec(IsolationLevel.Snapshot,
                (ActivityLogDbManager context) =>
                {
                    var rval = context.SelectActivityLog(user_id, activity_log_types, since, before);

                    if (grouping_class)
                        rval = rval.GroupBy(a => a.ClassID).Select(g => new ActivityLog()
                        {
                            activityLogID = (long)g.Average(a => a.activityLogID),
                            activityLogTypeID = (short)g.Average(a => a.activityLogTypeID),
                            createDate = g.Min(a => a.createDate),
                            requestIP = g.Max(a => a.requestIP),

                            ClassID = g.Key,
                            Title = g.Max(a => a.Title),
                            Speaker = g.Max(a => a.Speaker),
                            Units = g.Max(a => a.Units)
                        });

                    switch (sort)
                    {
                        case ClassActivityLogSortEnum.ClassID_asc:
                            rval = rval.OrderBy(a => a.ClassID);
                            break;
                        case ClassActivityLogSortEnum.ClassID_desc:
                            rval = rval.OrderByDescending(a => a.ClassID);
                            break;
                        case ClassActivityLogSortEnum.Date_asc:
                            rval = rval.OrderBy(a => a.createDate);
                            break;
                        case ClassActivityLogSortEnum.Date_desc:
                            rval = rval.OrderByDescending(a => a.createDate);
                            break;
                        case ClassActivityLogSortEnum.Speaker_asc:
                            rval = rval.OrderBy(a => a.Speaker);
                            break;
                        case ClassActivityLogSortEnum.Speaker_desc:
                            rval = rval.OrderByDescending(a => a.Speaker);
                            break;
                        case ClassActivityLogSortEnum.Title_asc:
                            rval = rval.OrderBy(a => a.Title);
                            break;
                        case ClassActivityLogSortEnum.Title_desc:
                            rval = rval.OrderByDescending(a => a.Title);
                            break;
                        case ClassActivityLogSortEnum.Type_asc:
                            rval = rval.OrderBy(a => a.activityLogTypeID);
                            break;
                        case ClassActivityLogSortEnum.Type_desc:
                            rval = rval.OrderByDescending(a => a.activityLogTypeID);
                            break;
                        case ClassActivityLogSortEnum.Units_asc:
                            rval = rval.OrderBy(a => a.Units);
                            break;
                        case ClassActivityLogSortEnum.Units_desc:
                            rval = rval.OrderByDescending(a => a.Units);
                            break;
                        default:
                            break;
                    }



                    return rval.Skip(start_row_index).Take(max_rows_count).ToArray();
                });
        }


        public LastActivityLog[] GetLastActivity(Guid[] user_ids, ActivityLogTypeEnum[] activity_log_types)
        {
            return this.Exec(IsolationLevel.Snapshot,
                (ActivityLogDbManager context) =>
                {
                    var rval = context.SelectLastActivityLog(user_ids, activity_log_types);

                    var data = rval.ToArray().Each(a =>
                    {
                        var parts = a.Title.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length > 0) a.Title = parts[0];
                        if (parts.Length > 1) a.Date = DateTime.ParseExact(parts[1].Replace(" ", "T"), "o", null);
                    });

                    return data.ToArray();
                });
        }

    }
}
