using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using MainEntity.Models.Catalog;
using Main.Areas.Admin.Models.Catalog;

namespace Main.Areas.Admin.Models.ControllerView.Catalog
{
    public class CreateClass
    {
        #region Class
        
        public long ClassID { get; set; }

        public long SpeakerID { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public int Hour { get; set; }

        public int Min { get; set; }

		public int? NewOrder { get; set; }

        #endregion

        public string Description { get; set; }

        public bool IsFree { get; set; }

        public bool IsFreeOffer { get; set; }

        public bool IsVisible { get; set; }

		public bool IsFullFree { get; set; }

        public string SpeakerName { get; set; }

        public string Categories { get; set; }

        public string Code { get; set; }

        public string Notes { get; set; }

        public string AmazonFilePath { get; set; }

		public string AmazonFilePath2 { get; set; }

        public long DownloadFileID { get; set; }

        #region EntityItem

        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public bool Active { get; set; }

        #endregion

        #region Product
        public bool TapeAvailable { get; set; }
        public decimal TapePriceUSD { get; set; }

        public bool DiskAvailable { get; set; }
        public decimal DiskPriceUSD { get; set; }

        public bool DownloadAvailable { get; set; }
        public decimal DownloadPriceUSD { get; set; }
        public decimal DownloadPriceUnit { get; set; }

        #endregion


        public CatalogItemXPortal[] InPortals { get; set; }

        public SelectList Speakers { get; set; }

		public SelectList ShippingLocation { get; set; }

		public SelectList Level { get; set; }

		public int ShippingLocationID { get; set; }

		public int LevelTagID { get; set; }

        public int ImageType { get; set; }

        public string ImageUrl { get; set; }

		public void InitializeLists(EntityItem[] speakers, ShippingLocation[] shipping_locations,Tag[] levels)
		{
			var sl = new List<ShippingLocation>(shipping_locations);
			var lv = new List<Tag>(levels);

			this.Speakers = new SelectList(speakers, "entityID", "title", SpeakerID);
			this.ShippingLocation = new SelectList(sl, "shippingLocationID", "title", ShippingLocationID);
			this.Level = new SelectList(lv, "tagID", "name", LevelTagID);
		}

		public static CreateClass GetForCreate(MainEntity.Models.Portal.PortalEntity[] portals, long current_portal_id)
        {
			var rval = new CreateClass();
			
            var portal = portals.Select(p => new CatalogItemXPortal()
            {
                PortalID = p.portalID,
                Title = p.EntityItem.title,
                Selected = p.portalID == current_portal_id,
                IsVisible = p.portalID == current_portal_id,
            }).ToArray();

            rval.InPortals = portal;

            return rval;
        }
    }
}
