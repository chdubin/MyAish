using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPartnerKinoBL.Extension;
using MainEntity.Interfaces;
using MainEntity;
using MainEntity.Models.Catalog;
using MainCommon;
using System.Web;
using System.IO;

namespace MainBL
{
    public partial class CatalogService
    {
        #region Get

        public EntityItem GetPackage(long from_catalog_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    var rval = context.GetPackage(from_catalog_id, true).Single();

                    PrepareCatalogItem(true, true, context, new long[] { rval.entityID }, new EntityItem[] { rval });

                    return rval;
                });
        }

        public EntityItem[] GetPackages(long from_root_id, long portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    var rval = context.GetPackages(from_root_id, portal_id, true, true).ToArray();
                    var ids = rval.Select(e => e.entityID).ToArray();
                    var posters = context.GetFiles(ids, true, true).Where(f => f.fileTypeID == (short)FileTypeIDEnum.SmallPoster).ToArray();

                    rval.Each(e => e.FileEntity = posters.Where(p => p.EntityItem.parentEntityID == e.entityID).FirstOrDefault());
                    PrepareCatalogItem(true, true, context, ids, rval);

                    return rval;
                });
        }

        #endregion


        #region Private


        #endregion
    }
}
