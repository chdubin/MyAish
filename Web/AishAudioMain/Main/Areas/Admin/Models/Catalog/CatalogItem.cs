using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MainEntity.Models.Catalog;
using MainCommon;

namespace Main.Areas.Admin.Models.Catalog
{
    public class CatalogItem
    {
        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public bool Active { get; set; }

        public long CatalogItemID { get; set; }

        public EntityItemTypeEnum TypeID { get; set; }

        public bool? Visible { get; set; }

        public bool? IsFree { get; set; }

        public bool? IsFreeOffer { get; set; }

        public bool? IsFullFree { get; set; }

        public string Notes { get; set; }

        public bool Selected { get; set; }

        public string Code { get; set; }

        public FileEntity DownloadFileItem { get; set; }

        public bool? IsNew { get; set; }

        public IEnumerable<KeyValuePair<long, string>> Categories { get; set; }

        public static List<CatalogItem> GetForList(long currentPortalID, EntityItem[] catalogItems)
        {
            var model = new List<CatalogItem>();
            foreach (var c in catalogItems)
            {
                var itemInPortal = c.CatalogItemXrefPortals.Where(p => p.portalID == (currentPortalID == 0 ? 2 : currentPortalID) && p.catalogItemID == c.entityID).SingleOrDefault();
                model.Add(new CatalogItem()
                {
                    Active = c.active,
                    CatalogItemID = c.entityID,
                    CreateDate = c.createDate,
                    Title = c.title,
                    TypeID = (EntityItemTypeEnum)c.typeID,
                    Notes = c.CatalogItemExtend != null ? c.CatalogItemExtend.notes : string.Empty,
                    Selected = itemInPortal != null,
                    Visible = itemInPortal != null ? (bool?)itemInPortal.isVisible : null,
                    IsFree = itemInPortal != null ? (bool?)itemInPortal.isFree : null,
                    IsFreeOffer = itemInPortal != null ? (bool?)itemInPortal.isFreeOffer : null,
                    IsFullFree = itemInPortal != null ? (bool?)itemInPortal.isFullFree : null,
                    Code = c.CatalogItemExtend != null ? c.CatalogItemExtend.code : string.Empty,
                    DownloadFileItem = c.ChildEnities != null ? c.ChildEnities.Where(che => che.FileEntity != null)
                        .Select(she => she.FileEntity)
                        .Where(fe => fe.fileTypeID == (short)FileTypeIDEnum.S3File).FirstOrDefault() : null,

                    IsNew = c.ClassEntity == null ? null : (bool?)(c.ClassEntity.newOrder > 0),
                    Categories = c.TagXrefEntities
                        .Where(t => t.Tag.tagTypeID == (short)TagTypeEnum.Category)
                        .Select(t => new KeyValuePair<long, string>(t.tagID, t.Tag.name)).ToArray()
                });
            }
            return model;
        }
    }
}