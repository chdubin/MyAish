using System;
namespace MainEntity.Models.File
{
    public partial class EntityItem
    {
        public DateTime ShoppingDate { get; set; }

    }

	public partial class FileEntity
	{
		public EntityItem FileEntityItem { get; set; }

		public EntityItem CatalogItem { get; set; }

		public CatalogItemXrefPortal CatalogItemInPortal { get; set; }
	}
}
