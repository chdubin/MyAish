using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainEntity.Models.Catalog;

namespace Main.Areas.Admin.Models.ControllerView.Catalog
{
    public class ProductsInPackage
    {
        public EntityItem[] Products { get; set; }

        public ProductsInPackage(EntityItem[] products)
        {
            Products = products;
        }
    }
}