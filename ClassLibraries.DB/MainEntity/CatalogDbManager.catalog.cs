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
        #region Catalog

		public IQueryable<EntityItem> GetCatalogItems(long from_root_id, string and_code, int[] and_type_ids, bool with_only_active, bool with_only_nondeleted)
		{
			var rval =
				from e in this.EntityItems
				where e.rootEntityID == from_root_id
					&& e.CatalogItemExtend.code == and_code
					&& and_type_ids.Contains(e.typeID)
				select e;

			if (with_only_active)
				rval = rval.Where(e => e.active);
			if (with_only_nondeleted)
				rval = rval.Where(e => !e.deleted);

			return rval;
		}


		public int GetCatalogItemsCnt(long from_root_id, long parent_id, int[] catalog_entity_types, bool with_only_active, bool with_only_nondeleted,
			long filter_portalid, int[] filter_categories, string filter_title, string filter_speaker, string filter_code, bool filter_new)
		{
			IQueryable<EntityItem> rval =
				from e in this.EntityItems
				join c in this.ClassEntities on e.entityID equals c.classID into cc
				from ccc in cc.DefaultIfEmpty()
				join s in this.EntityItems on ccc.speakerID equals s.entityID into ss
				from sss in ss.DefaultIfEmpty()
                join p in this.CatalogItemXrefPortals.Where(x => x.portalID == filter_portalid) on e.entityID equals p.catalogItemID into pp
				from ppp in pp.DefaultIfEmpty()
				where e.rootEntityID == from_root_id
					&& e.parentEntityID == parent_id
					&& catalog_entity_types.Contains(e.typeID)
				select new EntityItem()
				{
					active = e.active,
					deleted = e.deleted,
					title = e.title,
					ClassSpeakerName = sss.title,
                    FilteredPortal = ppp
				};

			if (with_only_active)
				rval = rval.Where(e => e.active);
			if (with_only_nondeleted)
				rval = rval.Where(e => !e.deleted);
			if (filter_categories != null && filter_categories.Length > 0)
				rval = rval.Where(e => this.TagXrefEntities.Where(t => filter_categories.Contains(t.tagID) && t.entityID == e.entityID).Any());
			if (!string.IsNullOrEmpty(filter_title))
				rval = rval.Where(e => e.title.Contains(filter_title));
			if (!string.IsNullOrEmpty(filter_speaker))
				rval = rval.Where(e => e.ClassSpeakerName.Contains(filter_speaker));
			if (!string.IsNullOrEmpty(filter_code))
				rval = rval.Where(e => e.CatalogItemExtend.code.Contains(filter_code));
			if (filter_new)
				rval = rval.Where(e => e.ClassEntity.newOrder != null);
            if (filter_portalid > 0)
                rval = rval.Where(e => e.FilteredPortal != null);

			return rval.Count();
		}

        public IQueryable<EntityItem> GetCatalogItems(long from_root_id, long parent_id, int[] catalog_entity_types, bool with_only_active, bool with_only_nondeleted,
            long portal_id, long filter_portalid, int[] filter_categories, string filter_title, string filter_speaker, string filter_code, bool filter_new)
        {
            IQueryable<EntityItem> rval =
                from e in this.EntityItems
                join x in this.CatalogItemExtends on e.entityID equals x.entityID
                join c in this.ClassEntities on e.entityID equals c.classID into cc
                from ccc in cc.DefaultIfEmpty()
                join s in this.EntityItems on ccc.speakerID equals s.entityID into ss
                from sss in ss.DefaultIfEmpty()
                join p in this.CatalogItemXrefPortals.Where(x => x.portalID == portal_id) on e.entityID equals p.catalogItemID into pp
                from ppp in pp.DefaultIfEmpty()
                join p1 in this.CatalogItemXrefPortals.Where(x => x.portalID == filter_portalid) on e.entityID equals p1.catalogItemID into pp1
                from ppp1 in pp1.DefaultIfEmpty()
                where e.rootEntityID == from_root_id
                    && e.parentEntityID == parent_id
                    && catalog_entity_types.Contains(e.typeID)
                select new EntityItem()
                {
                    active = e.active,
                    createDate = e.createDate,
                    creatorID = e.creatorID,
                    deleted = e.deleted,
                    entityID = e.entityID,
                    hierarchiID = e.hierarchiID,
                    parentEntityID = e.parentEntityID,
                    rootEntityID = e.rootEntityID,
                    sortOrder = e.sortOrder,
                    title = e.title,
                    typeID = e.typeID,
                    CatalogItemExtend = x,
                    CurrentPortal = ppp,
                    FilteredPortal = ppp1,
                    ClassSpeakerName = sss.title,
                    ClassEntity = ccc
                };

            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);
			if (filter_categories != null && filter_categories.Length > 0)
                rval = rval.Where(e => this.TagXrefEntities.Where(t => t.entityID == e.entityID && filter_categories.Contains(t.tagID)).Any());
            if (!string.IsNullOrEmpty(filter_title))
                rval = rval.Where(e => e.title.Contains(filter_title));
            if (!string.IsNullOrEmpty(filter_speaker))
                rval = rval.Where(e => e.ClassSpeakerName.Contains(filter_speaker));
            if (!string.IsNullOrEmpty(filter_code))
                rval = rval.Where(e => e.CatalogItemExtend.code.Contains(filter_code));
			if (filter_new)
				rval = rval.Where(e => e.ClassEntity.newOrder != null);
            if (filter_portalid > 0)
                rval = rval.Where(e => e.FilteredPortal != null);

            return rval;
        }

        #endregion
        
        /*
        public int GetPortalItemsCnt_SuperUser_CategoryFilter(long from_root_id, long parent_id, int[] catalog_entity_types, bool with_only_active,
           bool with_only_nondeleted,long portal_id, int filter_category_id)
        {
            IQueryable<EntityItem> rval =
                from e in this.EntityItems
                join t in this.TagXrefEntities on e.entityID equals t.entityID
                where e.rootEntityID == from_root_id 
                    && e.parentEntityID == parent_id
                    && catalog_entity_types.Contains(e.typeID)
                    && t.tagID == filter_category_id
                select e;

            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval.Count();
        }

        public IQueryable<EntityItem> GetPortalItems_SuperUser_CategoryFilter(long from_root_id, long parent_id, int[] catalog_entity_types,
            bool with_only_active, bool with_only_nondeleted, long portal_id, int filter_category_id)
        {
            IQueryable<EntityItem> rval =
                from e in this.EntityItems
                join ext in this.CatalogItemExtends on e.entityID equals ext.entityID
                join t in this.TagXrefEntities on e.entityID equals t.entityID
                join p in this.CatalogItemXrefPortals.Where(x => x.portalID == portal_id) on e.entityID equals p.catalogItemID into pp
                from ppp in pp.DefaultIfEmpty()
                where e.rootEntityID == from_root_id
                    && e.parentEntityID == parent_id
                    && catalog_entity_types.Contains(e.typeID)
                    && t.tagID == filter_category_id
                select new EntityItem()
                {
                    active = e.active,
                    createDate = e.createDate,
                    creatorID = e.creatorID,
                    deleted = e.deleted,
                    entityID = e.entityID,
                    hierarchiID = e.hierarchiID,
                    parentEntityID = e.parentEntityID,
                    rootEntityID = e.rootEntityID,
                    sortOrder = e.sortOrder,
                    title = e.title,
                    typeID = e.typeID,
                    CatalogItemExtend = ext,
                    CurrentPortal = ppp
                };

            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }

        public int GetPortalItemsCnt_SuperUser(long from_root_id, long parent_id, int[] catalog_entity_types, bool with_only_active,
          bool with_only_nondeleted, long portal_id)
        {
            IQueryable<EntityItem> rval =
                from e in this.EntityItems
                where e.rootEntityID == from_root_id
                    && e.parentEntityID == parent_id
                    && catalog_entity_types.Contains(e.typeID)
                select e;

            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval.Count();
        }

        public IQueryable<EntityItem> GetPortalItems_SuperUser(long from_root_id, long parent_id, int[] catalog_entity_types,
            bool with_only_active, bool with_only_nondeleted, long portal_id)
        {
            IQueryable<EntityItem> rval =
                from e in this.EntityItems
                join ext in this.CatalogItemExtends on e.entityID equals ext.entityID
                join p in this.CatalogItemXrefPortals.Where(x => x.portalID == portal_id) on e.entityID equals p.catalogItemID into pp
                from ppp in pp.DefaultIfEmpty()
                where e.rootEntityID == from_root_id
                    && e.parentEntityID == parent_id
                    && catalog_entity_types.Contains(e.typeID)
                select new EntityItem()
                {
                    active = e.active,
                    createDate = e.createDate,
                    creatorID = e.creatorID,
                    deleted = e.deleted,
                    entityID = e.entityID,
                    hierarchiID = e.hierarchiID,
                    parentEntityID = e.parentEntityID,
                    rootEntityID = e.rootEntityID,
                    sortOrder = e.sortOrder,
                    title = e.title,
                    typeID = e.typeID,
                    CatalogItemExtend = ext,
                    CurrentPortal = ppp
                };

            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }
         * */


    }
}
