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
// this was added on feb 2 from version i had download from git hub
public partial class FileRoyaltiesEntity
    {
        public Guid UserId { get; set; }
        public long fileId { get; set; }
        public EntityItem FileEntityItem { get; set; }

        public EntityItem CatalogItem { get; set; }

        public CatalogItemXrefPortal CatalogItemInPortal { get; set; }
    }
}
