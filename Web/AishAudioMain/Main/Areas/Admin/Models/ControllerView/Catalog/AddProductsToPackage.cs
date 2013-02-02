using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainEntity.Models.Catalog;

namespace Main.Areas.Admin.Models.ControllerView.Catalog
{
    public class AddProductsToPackage
    {
        public ClassEntity[] Classes { get; set; }
        public string TitleFilter { get; set; }
        public string SpeakerFilter { get; set; }
        public long[] SelectedProductsIds { get; set; }

        public AddProductsToPackage(ClassEntity[] classes, string title_filter, string speaker_filter, long[] selected_products_ids)
        {
            Classes = classes;
            TitleFilter = title_filter;
            SpeakerFilter = speaker_filter;
            SelectedProductsIds = selected_products_ids;
        }
    }
}