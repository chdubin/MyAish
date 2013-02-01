using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;


namespace Main.Areas.Admin.Models.ControllerView.Catalog
{
    public class ClassFilter
    {
        [DisplayName("Speaker:")]
        public string sspeaker { get; set; }

        [DisplayName("Branch:")]
        public SelectList sportal { get; set; }

        [DisplayName("Tile:")]
        public string stitle { get; set; }

        [DisplayName("Code:")]
        public string scode { get; set; }

        [DisplayName("Category:")]
        public MultiSelectList scategory { get; set; }

        public long portal_id { get; set; }

        public Guid? user_id { get; set; }

        public ClassFilter(string selected_speaker, long portal_id, string selected_title, string selected_code, int[] selected_categories, MainEntity.Models.Portal.PortalEntity[] portals, MainEntity.Models.Tag.Tag[] categories, 
            long current_portal_id=0, Guid? userid=null)
        {
            sspeaker = selected_speaker;
            stitle = selected_title;
            scode = selected_code;
            portal_id = current_portal_id;
            user_id = userid;

            var portal = new List<MainEntity.Models.Portal.PortalEntity>(portals);
            portal.Insert(0, new MainEntity.Models.Portal.PortalEntity() { portalID = 0, EntityItem = new MainEntity.Models.Portal.EntityItem() { title = "All branches" } });

            sportal = new SelectList(portal, "portalID", "EntityItem.title", portal_id);

			var category = new List<MainEntity.Models.Tag.Tag>(categories);
			category.Insert(0, new MainEntity.Models.Tag.Tag() { name = "New", tagID = -1 });
			scategory = new MultiSelectList(category, "tagID", "name", selected_categories);

        }
    }
}