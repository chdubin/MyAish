using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Catalog;
using MainCommon;

namespace MainEntity
{
    public partial class CatalogDbManager
    {
        /*public int GetClassesCntForCategory(int tag_id)
        {
            var rval = from e in this.EntityItems
                       join t in this.TagXrefEntities on e.entityID equals t.entityID
                       where t.tagID == tag_id
                       where e.typeID == (int)EntityItemTypeEnum.ClassItem
                           && e.active == true
                           && e.deleted == false
                       select e;

            return rval.Count();
        }

        public int GetClassesCntForCategorySearch(long portal_id, string[] category_names, string search_title, string search_description, string search_code)
        {
            var rval = from e in this.EntityItems
                       where e.typeID == (int)EntityItemTypeEnum.ClassItem
                           && e.active == true
                           && e.deleted == false
                       select e;
            rval = rval.Where(e => e.CatalogItemXrefPortals.Where(c => c.portalID == portal_id).Count() > 0);

            if (category_names != null && category_names.Length > 0)
                rval = rval.Where(e => e.TagXrefEntities.Where(t => category_names.Contains(t.Tag.name)).Count() > 0);
            if (!string.IsNullOrEmpty(search_title))
                rval = rval.Where(e => e.title.Contains(search_title));
            if (!string.IsNullOrEmpty(search_description))
                rval = rval.Where(e => e.ClassEntity.description.Contains(search_description));
            if (!string.IsNullOrEmpty(search_code))
                rval = rval.Where(e => e.CatalogItemExtend.code.Contains(search_code));

            return rval.Count();
        }*/

        /*public IQueryable<EntityItem> GetClassesForCategory(int tag_id, int start_row_index, int max_rows_count)
        {
            var rval = from e in this.EntityItems
                       join t in this.TagXrefEntities on e.entityID equals t.entityID
                       where t.tagID == tag_id
                       join c in this.ClassEntities on e.entityID equals c.classID
                       join e2 in this.EntityItems on c.speakerID equals e2.entityID
                       where e.typeID == (int)EntityItemTypeEnum.ClassItem
                           && e.active == true
                           && e.deleted == false

                       select new EntityItem
                       {
                           entityID = e.entityID,
                           title = e.title,

                           ClassEntity = new ClassEntity()
                           {
                               classID = c.classID,
                               description = c.description,
                               SpeakerName = e2.title
                           }
                       };

            return rval.Skip(start_row_index).Take(max_rows_count);
        }*/
        /*
        public IQueryable<EntityItem> GetClassesForCategorySearch(long portal_id, string[] category_names, int start_row_index, int max_rows_count,
            string search_title, string search_description, string search_code)
        {
            var rval = from e in this.EntityItems
                     
                       where e.typeID == (int)EntityItemTypeEnum.ClassItem
                           && e.active == true
                           && e.deleted == false

                       select new EntityItem
                       {
                           entityID = e.entityID,
                           title = e.title,
                           ClassEntity = e.ClassEntity
                       };

            rval = rval.Where(e => e.CatalogItemXrefPortals.Where(c => c.portalID == portal_id).Count() > 0);

            if (category_names != null && category_names.Length > 0)
                rval = rval.Where(e => e.TagXrefEntities.Where(t => category_names.Contains(t.Tag.name)).Count() > 0);
            if (!string.IsNullOrEmpty(search_title))
                rval = rval.Where(e => e.title.Contains(search_title));
            if (!string.IsNullOrEmpty(search_description))
                rval = rval.Where(e => e.ClassEntity.description.Contains(search_description));
            if (!string.IsNullOrEmpty(search_code))
                rval = rval.Where(e => e.CatalogItemExtend.code.Contains(search_code));

            return rval.Skip(start_row_index).Take(max_rows_count);
        }

        public IQueryable<EntityItem> GetProductsForClasses(long[] classes_ids, int[] products_types)
        {
            var rval = from e in this.EntityItems
                       join p in this.ProductEntities on e.entityID equals p.productID
                       where e.typeID == (int)EntityItemTypeEnum.ProductItem
                           && classes_ids.Contains(e.parentEntityID)
                           && e.active == true
                           && e.deleted == false
                           && products_types.Contains(p.productTypeID)
                       select new EntityItem
                       {
                           entityID = e.entityID,
                           parentEntityID = e.parentEntityID,

                           ProductEntity = p
                       };

            return rval;
        }*/

        /*public string[] GetSpeakerNamesForClasses(long[] classes_ids, int[] products_types)
        {
            var rval = from s in this.EntityItems
                       join p in this.ProductEntities on e.entityID equals p.productID
                       where e.typeID == (int)EntityItemTypeEnum.ProductItem
                           && classes_ids.Contains(e.parentEntityID)
                           && e.active == true
                           && e.deleted == false
                           && products_types.Contains(p.productTypeID)
                       select new EntityItem
                       {
                           entityID = e.entityID,
                           parentEntityID = e.parentEntityID,

                           ProductEntity = p
                       };

            return rval;
        }*/
    }
}
