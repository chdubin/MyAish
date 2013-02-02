using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Custom;

namespace MainBL
{
    public static class ShoppingBL
    {
        public static MainEntity.Models.File.FileEntity[] GetLibraryItems(Func<long[]> GetLibraryItemsIDs,
            Func<long[], MainEntity.Models.File.FileEntity[]> GetLibraryItems) 
        {
            long[] ids = GetLibraryItemsIDs();

            return GetLibraryItems(ids);
        }


    }
}
