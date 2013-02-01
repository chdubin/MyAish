using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainCommon;
using MainEntity.Models.Catalog;
using MainEntity;

namespace MainBL
{
    public partial class CatalogService // CATALOG
    {
        #region Select

        public int GetCatalogItemsCnt(long from_root_id, bool with_only_active, bool with_only_nondeleted,
            long filter_portalid, int[] filter_categories, string filter_title, string filter_speaker, string filter_code, bool filter_new)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    int rval = context.GetCatalogItemsCnt(from_root_id, from_root_id, 
                        new int[] { (int)EntityItemTypeEnum.ClassItem, (int)EntityItemTypeEnum.PackageItem }, with_only_active, with_only_nondeleted,
                        filter_portalid, filter_categories, filter_title, filter_speaker, filter_code, filter_new);

                    return rval;
                });
        }

        public EntityItem[] GetCatalogItems(long from_root_id, bool with_only_active, bool with_only_nondeleted, int start_row_index, int max_rows_count, SortParametersEnum sort, bool descending,
            long current_portal_id, long filter_portalid, int[] filter_categories, string filter_title, string filter_speaker, string filter_code, bool filter_new)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {

                    IQueryable<EntityItem> rvalQuery = context.GetCatalogItems(from_root_id, from_root_id, 
                        new int[] { (int)EntityItemTypeEnum.ClassItem, (int)EntityItemTypeEnum.PackageItem }, with_only_active, with_only_nondeleted,
                        current_portal_id, filter_portalid, filter_categories, filter_title, filter_speaker, filter_code, filter_new);

                    switch (sort)
                    {
                        case SortParametersEnum.Title: rvalQuery = descending ? rvalQuery.OrderByDescending(e => e.title) : rvalQuery.OrderBy(e => e.title); break;
                        case SortParametersEnum.Date: rvalQuery = descending ? rvalQuery.OrderByDescending(e => e.createDate) : rvalQuery.OrderBy(e => e.createDate); break;
                        case SortParametersEnum.Active: rvalQuery = descending ? rvalQuery.OrderByDescending(e => e.active) : rvalQuery.OrderBy(e => e.active); break;
                        case SortParametersEnum.Visible: rvalQuery = descending ? rvalQuery.OrderByDescending(e => e.CurrentPortal.isVisible) : rvalQuery.OrderBy(e => e.CurrentPortal.isVisible); break;
                        case SortParametersEnum.Free: rvalQuery = descending ?
                            rvalQuery
                                .OrderByDescending(e => e.CurrentPortal.isFree)
                                .ThenBy(e => e.typeID) :
                            rvalQuery
                                .OrderBy(e => e.CurrentPortal.isFree)
                                .ThenByDescending(e => e.typeID);
                            ; break;
                        case SortParametersEnum.FreeOffer: rvalQuery = descending ?
                            rvalQuery
                                .OrderByDescending(e => e.CurrentPortal.isFreeOffer)
                                .ThenBy(e => e.typeID) :
                            rvalQuery
                                .OrderBy(e => e.CurrentPortal.isFreeOffer)
                                .ThenByDescending(e => e.typeID);
                            break;

						default: rvalQuery = descending ? rvalQuery.OrderByDescending(e => e.CatalogItemExtend.code) : rvalQuery.OrderBy(e => e.CatalogItemExtend.code); break;
                    }

                    var rval = rvalQuery.Skip(start_row_index).Take(max_rows_count).ToArray();
                    var ids = rval.Select(e => e.entityID).ToArray();

                    PrepareCatalogItem(with_only_active, with_only_nondeleted, context, ids, rval);

                    return rval;
                });
        }

        #endregion
    }
}
