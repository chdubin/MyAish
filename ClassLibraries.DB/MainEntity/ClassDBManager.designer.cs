using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Class;
using BLToolkit.Data.Linq;

namespace MainEntity
{
    public partial class ClassDbManager : EntityItemDbManager
    {
        public ClassDbManager(string configuartion_name)
            : base(configuartion_name)
        {
        }

        public Table<EntityItem> EntityItems { get { return this.GetTable<EntityItem>(); } }

        public Table<ClassEntity> ClassEntities { get { return this.GetTable<ClassEntity>(); } }

        public Table<CatalogItemXrefPortal> CatalogItemXrefPortals { get { return this.GetTable<CatalogItemXrefPortal>(); } }

        public Table<CatalogItemExtend> CatalogItemExtends { get { return this.GetTable<CatalogItemExtend>(); } }

        public Table<ProductXrefEntity> ProductXrefEntities { get { return this.GetTable<ProductXrefEntity>(); } }

        public Table<ProductEntity> ProductEntities { get { return this.GetTable<ProductEntity>(); } }

        public Table<FileEntity> FileEntities { get { return this.GetTable<FileEntity>(); } }

		public Table<TagXrefEntity> TagXrefEntities { get { return this.GetTable<TagXrefEntity>(); } }

		public Table<Tag> Tags { get { return this.GetTable<Tag>(); } }

		public Table<ProductEntityShipping> ProductEntityShippings { get { return this.GetTable<ProductEntityShipping>(); } }
		public Table<ShippingLocation> ShippingLocations { get { return this.GetTable<ShippingLocation>(); } }

    }
}
