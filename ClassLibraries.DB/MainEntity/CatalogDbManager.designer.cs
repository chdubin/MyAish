using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data.Linq;
using MainEntity.Models.Catalog;

namespace MainEntity
{
    public partial class CatalogDbManager : EntityItemDbManager
    {
        public CatalogDbManager(string configuartion_name)
            : base(configuartion_name)
        {
        }

        public Table<EntityItem> EntityItems { get { return this.GetTable<EntityItem>(); } }

        public Table<ClassEntity> ClassEntities { get { return this.GetTable<ClassEntity>(); } }

        public Table<CatalogItemXrefPortal> CatalogItemXrefPortals { get { return this.GetTable<CatalogItemXrefPortal>(); } }

        public Table<CatalogItemExtend> CatalogItemExtends { get { return this.GetTable<CatalogItemExtend>(); } }

        public Table<ProductEntity> ProductEntities { get { return this.GetTable<ProductEntity>(); } }

        public Table<TagXrefEntity> TagXrefEntities { get { return this.GetTable<TagXrefEntity>(); } }

        public Table<Tag> Tags { get { return this.GetTable<Tag>(); } }

        public Table<FileEntity> FileEntities { get { return this.GetTable<FileEntity>(); } }

        public Table<ProductXrefEntity> ProductXrefEntities { get { return this.GetTable<ProductXrefEntity>(); } }

        public Table<SubscribePlanEntity> SubscribePlanEntities { get { return this.GetTable<SubscribePlanEntity>(); } }

        public Table<SubscribePlanXref> SubscribePlanXrefs { get { return this.GetTable<SubscribePlanXref>(); } }

        public Table<vw_Category> CategoryView { get { return this.GetTable<vw_Category>(); } }

        public Table<SpeakerEntity> SpeakerEntities { get { return this.GetTable<SpeakerEntity>(); } }

		public Table<ProductEntityShipping> ProductEntityShippings { get { return this.GetTable<ProductEntityShipping>(); } }

		public Table<ShippingLocation> ShippingLocations { get { return this.GetTable<ShippingLocation>(); } }
    }
}
