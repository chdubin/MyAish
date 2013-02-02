using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Areas.Admin.Models.Catalog;
using MainEntity.Models.Catalog;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Main.Areas.Admin.Models.Catalog;
using MainEntity;
using System.Text;
using MyPartnerKinoBL.Extension;
using MainEntity.Interfaces;
using MainCommon;
using System.IO;
using MainCommon.Models;

namespace Main.Areas.Admin.Models.ControllerView.Catalog
{
    public class EditPackage
    {
        public bool Active { get; set; }
        public bool unlimitedAccessInLibrary { get; set; }

        [Required]
        public string Title { get; set; }
        public long SpeakerID { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public decimal PriceUSD { get; set; }
        public string Code { get; set; }

        public long PackageID { get; set; }
        public DateTime CreateDate { get; set; }

        public string ProductsToAdd { get; set; }
        public string ProductsToDelete { get; set; }
        public EntityItem[] Products { get; set; }
        public SelectList Speakers { get; set; }
        public string Categories { get; set; }
        //public bool EditMode { get; set; }
        [Required(ErrorMessage="You need assign this package to the branch")]
        public CatalogItemXPortal[] InPortals { get; set; }
        public string ClassImagePath { get; set; }

        [DisplayName("Attached Subscribe Plan in months")]
        public short SubscribePlanMonths { get; set; }
        [DisplayName("Free Units on Subscribe")]
        public int FreeUnitsOnSubscribe { get; set; }
        [DisplayName("Free Units per month")]
        public int FreeUnitsOnNextSubscribe { get; set; }


        public bool IsVisible { get; set; }

        public EditPackage()
        {
            Products = new EntityItem[0];
        }

		public SelectList ShippingLocation { get; set; }

        [Range(1,int.MaxValue,ErrorMessage="You can select Shipping Location")]
        public int ShippingLocationID { get; set; }

        public void InitializeLists(EntityItem[] speakers, ShippingLocation[] shipping_locations, MainEntity.Models.Portal.PortalEntity[] portals=null, long current_portal_id=0)
        {
            var sl = new List<ShippingLocation>(shipping_locations);
            sl.Insert(0, new ShippingLocation() { shippingLocationID = 0, title = "Select" });
            this.Speakers = new SelectList(speakers, "entityID", "title", SpeakerID);
            this.ShippingLocation = new SelectList(sl, "shippingLocationID", "title", ShippingLocationID);

            if (portals != null)
            {
                var portal = portals.Select(p => new CatalogItemXPortal()
                {
                    PortalID = p.portalID,
                    Title = p.EntityItem.title,
                    Selected = p.portalID == current_portal_id,
                    IsVisible = p.portalID == current_portal_id
                }).ToArray();

                InPortals = portal;
            }
        }


    
   }
}