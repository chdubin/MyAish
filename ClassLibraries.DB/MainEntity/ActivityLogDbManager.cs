using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Activity;
using MainCommon;
using BLToolkit.Data.Linq;

namespace MainEntity
{
	public partial class ActivityLogDbManager
	{
		#region Select

		public IQueryable<Email> GetEmail(string from_email)
		{
			var rval = from e in this.Emails
					   where e.email1 == from_email
					   select e;
			return rval;
		}


        public IQueryable<ActivityLog> SelectActivityLog(Guid user_id, ActivityLogTypeEnum[] activity_log_types, DateTime? since, DateTime? before)
        {
            var types = activity_log_types.Select(t => (short)t).ToArray();
            var rval = from a in this.ActivityLogs
                       join au in this.ActivityLogXrefUsers on a.activityLogID equals au.activityLogID
                       join ae in this.ActivityLogXrefEntityItems on a.activityLogID equals ae.activityLogID
                       join ef in this.EntityItems on ae.entityID equals ef.entityID
                       join pe in this.ProductEntities on ef.entityID equals pe.productID
                       join e in this.EntityItems on ef.parentEntityID equals e.entityID
                       join ce in this.ClassEntities on e.entityID equals ce.classID
                       join se in this.EntityItems on ce.speakerID equals se.entityID
                       where au.UserId == user_id && types.Contains(a.activityLogTypeID)
                       select new ActivityLog()
                       {
                           activityLogID = a.activityLogID,
                           activityLogTypeID = a.activityLogTypeID,
                           createDate = a.createDate,
                           requestIP = a.requestIP,

                           ClassID = ce.classID,
                           Title = e.title,
                           Speaker = se.title,
                           Units = pe.price2
                       };

            if (since != null)
                rval = rval.Where(a => a.createDate >= since.Value);
            if (before != null)
                rval = rval.Where(a => a.createDate <= before.Value);

            return rval;
        }

        public IQueryable<ActivityLog> SelectActivityLog(Guid user_id, long logId, ActivityLogTypeEnum[] activity_log_types, DateTime? since, DateTime? before)
        {
            var types = activity_log_types.Select(t => (short)t).ToArray();
            var rval = from a in this.ActivityLogs
                       join au in this.ActivityLogXrefUsers on a.activityLogID equals au.activityLogID
                       join ae in this.ActivityLogXrefEntityItems on a.activityLogID equals ae.activityLogID
                       join ef in this.EntityItems on ae.entityID equals ef.entityID
                       join pe in this.ProductEntities on ef.entityID equals pe.productID
                       join e in this.EntityItems on ef.parentEntityID equals e.entityID
                       join ce in this.ClassEntities on e.entityID equals ce.classID
                       join se in this.EntityItems on ce.speakerID equals se.entityID
                       where au.UserId == user_id && types.Contains(a.activityLogTypeID)
                       select new ActivityLog()
                       {
                           activityLogID = a.activityLogID,
                           activityLogTypeID = a.activityLogTypeID,
                           createDate = a.createDate,
                           requestIP = a.requestIP,

                           ClassID = ce.classID,
                           Title = e.title,
                           Speaker = se.title,
                           Units = pe.price2
                       };

            if (since != null)
                rval = rval.Where(a => a.createDate >= since.Value);
            if (before != null)
                rval = rval.Where(a => a.createDate <= before.Value);

            return rval;
        }


        public IQueryable<KeyValuePair<long, DateTime>> SelectLibraryItemsIDsWithShoppingDate(Guid user_id, ActivityLogTypeEnum[] activity_types, DateTime dateLimit)
        {
            var types = activity_types.Select(t => (short)t).ToArray();
            return from a in this.ActivityLogs
                   join au in this.ActivityLogXrefUsers on a.activityLogID equals au.activityLogID
                   join ae in this.ActivityLogXrefEntityItems on a.activityLogID equals ae.activityLogID
                   join ef in this.EntityItems on ae.entityID equals ef.entityID
                   join pe in this.ProductEntities on ef.entityID equals pe.productID
                   join e in this.EntityItems on ef.parentEntityID equals e.entityID
                   join ce in this.ClassEntities on e.entityID equals ce.classID
                   join se in this.EntityItems on ce.speakerID equals se.entityID
                   where au.UserId == user_id && types.Contains(a.activityLogTypeID)
                    && (a.createDate > dateLimit || pe.unlimitedAccessInLibrary)
                   group a.createDate.Value by ae.entityID into g
                   select new KeyValuePair<long, DateTime>(g.Key, g.Min());
        }

        public IQueryable<KeyValuePair<long, DateTime>> SelectLibraryItem(Guid user_id, ActivityLogTypeEnum[] activity_types, long prod_id, DateTime dateLimit)
        {
            var types = activity_types.Select(t => (short)t).ToArray();
            return from a in this.ActivityLogs
                   join au in this.ActivityLogXrefUsers on a.activityLogID equals au.activityLogID
                   join ae in this.ActivityLogXrefEntityItems on a.activityLogID equals ae.activityLogID
                   join ef in this.EntityItems on ae.entityID equals ef.entityID
                   join pe in this.ProductEntities on ef.entityID equals pe.productID
                   join e in this.EntityItems on ef.parentEntityID equals e.entityID
                   join ce in this.ClassEntities on e.entityID equals ce.classID
                   join se in this.EntityItems on ce.speakerID equals se.entityID
                   where au.UserId == user_id && types.Contains(a.activityLogTypeID)
                    && (a.createDate > dateLimit || pe.unlimitedAccessInLibrary)
                    && ae.entityID == prod_id
                   select new KeyValuePair<long, DateTime>(ae.entityID, a.createDate.Value);
        }
// this was added on feb 2 from version i had download from git hub
        public IQueryable<RoyaltyLog> SelectUserIDsByClassLog(long classId, ActivityLogTypeEnum[] activity_types, DateTime startDate, DateTime endDate)
        {
            var types = activity_types.Select(t => (short)t).ToArray();
            var ret = from a in this.ActivityLogs
                   join ae in this.ActivityLogXrefEntityItems on a.activityLogID equals ae.activityLogID
                   join au in this.ActivityLogXrefUsers on a.activityLogID equals au.activityLogID
                   join m in this.Memberships on au.UserId equals m.UserId
                    join ef in this.EntityItems on ae.entityID equals ef.entityID
                    join pe in this.ProductEntities on ef.entityID equals pe.productID
                    join e in this.EntityItems on ef.parentEntityID equals e.entityID
                    join ce in this.ClassEntities on e.entityID equals ce.classID
                    where e.entityID == classId && types.Contains(a.activityLogTypeID)
                    && ((a.createDate < new DateTime(2012, 6, 11) || a.createDate > new DateTime(2012, 6, 9)) && a.requestIP != null)
                    && (a.createDate > startDate && a.createDate < endDate.AddDays(1))
                   //group au.UserId by ae.entityID into g
                   select new RoyaltyLog { ClassID = ae.entityID, UserId = au.UserId, Title = e.title, FirstName = m.firstName, LastName = m.lastName, Price = -1 };

            return ret;
        }

        public IQueryable<LastActivityLog> SelectLastActivityLog(Guid[] user_ids, ActivityLogTypeEnum[] activity_log_types)
        {
            var types = activity_log_types.Select(t => (short)t).ToArray();

            var rval = from m in this.Memberships
                       let a = (from au in this.ActivityLogXrefUsers
                                join aa in this.ActivityLogs on au.activityLogID equals aa.activityLogID
                                join ae in this.ActivityLogXrefEntityItems on au.activityLogID equals ae.activityLogID
                                join ef in this.EntityItems on ae.entityID equals ef.entityID
                                join e in this.EntityItems on ef.parentEntityID equals e.entityID
                                where types.Contains(aa.activityLogTypeID) && au.UserId == m.UserId
                                orderby aa.createDate descending
                                select new { Title = e.title + "|"+ aa.createDate.ToString() })
                       where user_ids.Contains(m.UserId)
                       select new LastActivityLog { UserId = m.UserId, Title = a.First().Title };

            return rval;
        }


		#endregion


		#region Insert

		public long InsertActivityLog(ActivityLogTypeEnum type_id, DateTime create_date, string request_ip)
		{
			return Convert.ToInt64(this.ActivityLogs.InsertWithIdentity(() => 
				new ActivityLog() { activityLogTypeID = (short)type_id, createDate = create_date, requestIP = request_ip }));
		}

		public int InsertEmail(string email)
		{
			return Convert.ToInt32(this.Emails.InsertWithIdentity(() =>
				new Email() { email1 = email }));
		}

		public int InsertActivityLogXrefEmail(long activity_log_id, int email_id)
		{
			return this.ActivityLogXrefEmails.Insert(() =>
				new ActivityLogXrefEmail() { activityLogID = activity_log_id, emailID = email_id });
		}

		public int InsertActivityLogXrefUsers(long activity_log_id, Guid user_id)
		{
			return this.ActivityLogXrefUsers.Insert(() =>
				new ActivityLogXrefUser() { activityLogID = activity_log_id, UserId = user_id });
		}

		public int InsertActivityLogXrefEntityItem(long activity_log_id, long entity_id)
		{
			return this.ActivityLogXrefEntityItems.Insert(() =>
				new ActivityLogXrefEntityItem() { activityLogID = activity_log_id, entityID = entity_id });
		}

		public int InsertActivityLogXrefShopping(long activity_log_id, long shopping_id)
		{
			return this.ActivityLogXrefShoppings.Insert(() =>
				new ActivityLogXrefShopping() { activityLogID = activity_log_id, shoppingID = shopping_id });
		}

		#endregion

	}
}
