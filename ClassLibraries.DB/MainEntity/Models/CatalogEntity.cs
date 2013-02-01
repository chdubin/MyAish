using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainCommon;
using System.ComponentModel.DataAnnotations;

namespace MainEntity.Models.Catalog
{
    public partial class ClassEntity
    {
        public string SpeakerName { get; set; }

        public EntityItem Type { get; set; }
        public EntityItem Disc { get; set; }
        public EntityItem File { get; set; }
        public FileEntity FileEntity { get; set; }

        public ProductEntity Product { get; set; }
        public CatalogItemXrefPortal FilteredPortal { get; set; }
        // for LEFT JOIN
        //[BLToolkit.Mapping.Association(Storage = "_CatalogItemExtend", ThisKey = "entityID", OtherKey = "entityID", CanBeNull = true)]
        //public CatalogItemExtend CatalogItemExtend2 { get; set; }
    }

    public partial class EntityItem
    {
        public EntityItem[] ChildEnities { get; set; }

        public CatalogItemXrefPortal CurrentPortal { get; set; }

        public CatalogItemXrefPortal FilteredPortal { get; set; }

        public string ClassSpeakerName { get; set; }

    }

    public partial class ProductEntity
    {
        public FileEntity File { get; set; }

		public int ShippingLocationID { get; set; }

        public string ShippingLocationTitle { get; set; }
    }
}
