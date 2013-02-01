using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using MainEntity.Models.Portal;

namespace MainEntity
{
    public partial class PortalDbManager : EntityItemDbManager
    {
        public PortalDbManager(string configuartion_name)
            : base(configuartion_name)
        {
        }

        public Table<EntityItem> EntityItems { get { return this.GetTable<EntityItem>(); } }

        public Table<PortalEntity> PortalEntities { get { return this.GetTable<PortalEntity>(); } }

        public Table<PortalAlias> PortalAliases { get { return this.GetTable<PortalAlias>(); } }

    }
}
