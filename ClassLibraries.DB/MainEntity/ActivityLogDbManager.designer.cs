using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data.Linq;
using BLToolkit.Data;
using MainEntity.Models.Activity;

namespace MainEntity
{
	public partial class ActivityLogDbManager:DbManager
	{
		public ActivityLogDbManager(string configuartion_name)
            : base(configuartion_name)
        {
        }

		public Table<ActivityLogType> ActivityLogTypes { get { return this.GetTable<ActivityLogType>(); } }

		public Table<ActivityLog> ActivityLogs { get { return this.GetTable<ActivityLog>(); } }

		public Table<ActivityLogXrefUser> ActivityLogXrefUsers { get { return this.GetTable<ActivityLogXrefUser>(); } }

		public Table<ActivityLogXrefEmail> ActivityLogXrefEmails { get { return this.GetTable<ActivityLogXrefEmail>(); } }

		public Table<ActivityLogXrefEntityItem> ActivityLogXrefEntityItems { get { return this.GetTable<ActivityLogXrefEntityItem>(); } }

		public Table<ActivityLogXrefShopping> ActivityLogXrefShoppings { get { return this.GetTable<ActivityLogXrefShopping>(); } }

		public Table<Email> Emails { get { return this.GetTable<Email>(); } }

        public Table<EntityItem> EntityItems { get { return this.GetTable<EntityItem>(); } }

        public Table<ClassEntity> ClassEntities { get { return this.GetTable<ClassEntity>(); } }

        public Table<ProductEntity> ProductEntities { get { return this.GetTable<ProductEntity>(); } }

        public Table<Membership> Memberships { get { return this.GetTable<Membership>(); } }

	}
}
