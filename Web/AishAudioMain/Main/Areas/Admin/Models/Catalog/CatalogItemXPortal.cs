using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainEntity.Models.Catalog;

namespace Main.Areas.Admin.Models.Catalog
{
    public class CatalogItemXPortal
    {
        public bool Selected { get; set; }

        public bool IsFree { get; set; }

        public bool IsFreeOffer { get; set; }

        public bool IsVisible { get; set; }

		public bool IsFullFree { get; set; }

        public long PortalID { get; set; }

        #region Portal

        public string Title { get; set; }

        #endregion

        public static CatalogItemXPortal[] GetPortalsForCatalogItemEdit(MainEntity.Models.Portal.PortalEntity[] all_portals, CatalogItemXrefPortal[] catalog_item_portals)
        {
            var rval = from p in all_portals
                       join pp in catalog_item_portals on p.portalID equals pp.portalID into ppp
                       select new CatalogItemXPortal()
                       {
                           PortalID = p.portalID,
                           Title = p.EntityItem.title,
                           Selected = ppp.SingleOrDefault() != null,
                           IsFree = ppp.SingleOrDefault() != null ? ppp.SingleOrDefault().isFree : false,
                           IsFreeOffer = ppp.SingleOrDefault() != null ? ppp.SingleOrDefault().isFreeOffer : false,
						   IsFullFree = ppp.SingleOrDefault() != null ? ppp.SingleOrDefault().isFullFree : false,
                           IsVisible = ppp.SingleOrDefault() != null ? ppp.SingleOrDefault().isVisible : false
                       };

            return rval.ToArray();
        }
    }
}