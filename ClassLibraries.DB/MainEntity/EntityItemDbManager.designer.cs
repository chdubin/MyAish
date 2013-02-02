using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using MainEntity.Models.Entity;

namespace MainEntity
{
    public partial class EntityItemDbManager : DbManager
    {
        public EntityItemDbManager(string configuartion_name)
            : base(configuartion_name)
        {
        }

        public Table<EntityItem> BaseEntityItems { get { return this.GetTable<EntityItem>(); } }

        public Table<FileEntity> BaseFileEntities { get { return this.GetTable<FileEntity>(); } }

    }
}
