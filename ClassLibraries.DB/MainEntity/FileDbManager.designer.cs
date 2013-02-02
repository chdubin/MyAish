using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using MainEntity.Models.File;

namespace MainEntity
{
    public partial class FileDbManager : DbManager
    {
        public FileDbManager(string configuartion_name)
            : base(configuartion_name)
        {
        }

        public Table<FileEntity> FileEntity { get { return this.GetTable<FileEntity>(); } }

        public Table<ClassEntity> ClassEntity { get { return this.GetTable<ClassEntity>(); } }

        public Table<EntityItem> EntityItem { get { return this.GetTable<EntityItem>(); } }

        public Table<CatalogItemExtend> CatalogItemExtend { get { return this.GetTable<CatalogItemExtend>(); } }

		public Table<CatalogItemXrefPortal> CatalogItemXrefPortals { get { return this.GetTable<CatalogItemXrefPortal>(); } }
    }
}
