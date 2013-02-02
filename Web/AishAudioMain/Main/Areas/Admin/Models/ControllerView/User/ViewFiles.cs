using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainEntity.Models.Catalog;
using MainEntity.Models.Shopping;

namespace Main.Areas.Admin.Models.ControllerView.User
{
    public class ViewFiles
    {
        public ShoppingClass[] Classes { get; set; }
        public MainEntity.Models.Catalog.EntityItem[] OtherClasses { get; set; }
        public Guid UserID { get; set; }

        public ViewFiles(ShoppingClass[] shopping_classes, MainEntity.Models.Catalog.EntityItem[] other_classes, Guid user_id)
        {
            Classes = shopping_classes;
            OtherClasses = other_classes;
            UserID = user_id;
        }
    }
}