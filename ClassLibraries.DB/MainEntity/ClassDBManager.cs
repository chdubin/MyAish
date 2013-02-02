using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Class;
using MainCommon;
using BLToolkit.Data.Linq;

namespace MainEntity
{
    public partial class ClassDbManager
	{

		#region Select

		public IQueryable<ClassEntity> SelectClass(long from_child_id)
		{
			var rval = from c in this.ClassEntities
					   join ch in this.EntityItems on c.classID equals ch.parentEntityID
					   where ch.entityID == from_child_id
					   select c;
			return rval;
		}


		public IQueryable<ProductEntity> GetProductsList(long[] ids)
        {
            var rval = from p in this.ProductEntities
                       where ids.Contains(p.productID)
                       select new ProductEntity()
                       {
                           productID = p.productID,
                           description = p.description,
                           price1 = p.price1,
                           price2 = p.price2,
                           productTypeID = p.productTypeID,                           
                           EntityItem = new EntityItem()
                           {
                               entityID = p.EntityItem.entityID,
                               active = p.EntityItem.active,
                               title = p.EntityItem.title,
                               typeID = p.EntityItem.typeID,
                               deleted = p.EntityItem.deleted,
                               sortOrder = p.EntityItem.sortOrder
                           }
                       };
            return rval;
        }


		public IQueryable<ClassEntity> GetClasses(long from_root_id, long portal_id, bool with_only_active, bool with_only_nondeleted)
        {
            var rval = from c in this.ClassEntities
					   join x in this.CatalogItemXrefPortals on c.classID equals x.catalogItemID
					   join e in this.EntityItems on c.classID equals e.entityID
					   join s in this.EntityItems on c.speakerID equals s.entityID
					   join i in this.CatalogItemExtends on c.classID equals i.entityID
                       where x.portalID == portal_id
                           && e.rootEntityID == from_root_id
                           && ((e.typeID == (int)EntityItemTypeEnum.ClassItem) || (e.typeID == (int)EntityItemTypeEnum.PackageItem)) 
                       select new ClassEntity
                       {
                           classID = c.classID,
                           description = c.description,
                           duration = c.duration,
                           SpeakerName = s.title,

                           EntityItem = e,
                           CatalogItemExtend = i,
						   IsFree = x.isFree,

						   Code = i.code,
						   Title = e.title,
						   IsActive = e.active,
						   IsDelete = e.deleted,
						   newOrder = c.newOrder
                       };

			if (with_only_active)
				rval = rval.Where(e => e.IsActive);
			if (with_only_nondeleted)
				rval = rval.Where(e => !e.IsDelete);

			return rval;
        }

        public IQueryable<ClassEntity> GetFilteredClasses(long from_root_id, long portal_id, bool with_only_active, 
            bool with_only_nondeleted, int[] product_type_ids, int class_level)
        {
            var rval = from c in this.ClassEntities
                       join x in this.CatalogItemXrefPortals on c.classID equals x.catalogItemID
                       join e in this.EntityItems on c.classID equals e.entityID
                       join s in this.EntityItems on c.speakerID equals s.entityID
                       join i in this.CatalogItemExtends on c.classID equals i.entityID
                       where x.portalID == portal_id
                           && e.rootEntityID == from_root_id
                           && e.typeID == (int)EntityItemTypeEnum.ClassItem
                       select new ClassEntity
                       {
                           classID = c.classID,
                           description = c.description,
                           duration = c.duration,
                           SpeakerName = s.title,
                           newOrder = c.newOrder,

                           EntityItem = e,
                           CatalogItemExtend = i,
                           IsFree = x.isFree,

                           Code = i.code,
                           Title = e.title,
                           IsActive = e.active,
                           IsDelete = e.deleted
                       };

            if (class_level != 0)
                rval = rval.Where(c =>
                    (from t2 in this.TagXrefEntities
                     where t2.entityID == c.classID && t2.tagID == class_level
                     select t2).Any());

            if (product_type_ids.Length > 0)
                rval = rval.Where(c =>
                    (from e2 in this.EntityItems
                     join p2 in this.ProductEntities on e2.entityID equals p2.productID
                     where e2.parentEntityID == c.classID && product_type_ids.Contains(p2.productTypeID)
                     select e2).Any());

            if (with_only_active)
                rval = rval.Where(e => e.IsActive);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.IsDelete);

            return rval;
        }


		public IQueryable<ClassEntity> GetClassCnt(long from_root_id, long portal_id, bool with_only_active, bool with_only_nondeleted)
		{
			var rval = from c in this.ClassEntities
					   join x in this.CatalogItemXrefPortals on c.classID equals x.catalogItemID
					   join e in this.EntityItems on c.classID equals e.entityID
					   join s in this.EntityItems on c.speakerID equals s.entityID
					   join i in this.CatalogItemExtends on c.classID equals i.entityID
					   where x.portalID == portal_id
						   && e.rootEntityID == from_root_id
                           && ((e.typeID == (int)EntityItemTypeEnum.ClassItem) || (e.typeID == (int)EntityItemTypeEnum.PackageItem))
					   select new ClassEntity
					   {
						   Code = i.code,
						   Title = e.title,
						   IsActive = e.active,
						   IsDelete = e.deleted
					   };

			if (with_only_active)
				rval = rval.Where(e => e.IsActive);
			if (with_only_nondeleted)
				rval = rval.Where(e => !e.IsDelete);
			return rval;
		}
		 


		public IQueryable<ClassEntity> GetClass(long from_root_id, long portal_id, long class_id,
			bool with_only_active, bool with_only_nondeleted)
		{
			var rval = from c in this.ClassEntities
					   where c.CatalogItemXrefPortal.portalID == portal_id
						   && c.EntityItem.rootEntityID == from_root_id
						   && c.classID == class_id

					   select new ClassEntity
					   {
						   classID = c.classID,
						   description = c.description,
						   duration = c.duration,
						   SpeakerName = c.EntityItemSpeaker.title,
                           newOrder = c.newOrder,

						   EntityItem = c.EntityItem,
						   CatalogItemExtend = c.CatalogItemExtend,

						   IsFree = c.CatalogItemXrefPortal.isFree

					   };
			if (with_only_active)
				rval = rval.Where(e => e.EntityItem.active);
			if (with_only_nondeleted)
				rval = rval.Where(e => !e.EntityItem.deleted);


			return rval;
		}

		public IQueryable<EntityItem> GetSpeaker(long from_root_id, string from_name,
			bool with_only_active, bool with_only_nondeleted)
		{
			var rval = from e in this.EntityItems
					   where e.rootEntityID == from_root_id && e.typeID == (int)EntityItemTypeEnum.SpeakerItem && e.title == from_name
					   select e;
			if (with_only_active)
				rval = rval.Where(e => e.active);
			if (with_only_nondeleted)
				rval = rval.Where(e => !e.deleted);


			return rval;
		}


		public IQueryable<Tag> GetTag(string from_name, TagTypeEnum tag_type)
		{
			var rval = from t in this.Tags
					   where t.name == from_name && t.tagTypeID == (short)tag_type
					   select t;

			return rval;
		}

		public IQueryable<EntityItem> GetProductsForClasses(long[] classes_ids, int[] products_types)
        {
			var rval = from e in this.EntityItems
					   join p in this.ProductEntities on e.entityID equals p.productID
                       where //e.typeID == (int)EntityItemTypeEnum.ProductItem                       
                           (classes_ids.Contains(e.parentEntityID) || (classes_ids.Contains(e.entityID) && p.productTypeID == 4))
                           && e.active == true
                           && e.deleted == false
                           && products_types.Contains(p.productTypeID)
                      
					   select new EntityItem
                       {
						   parentEntityID = e.parentEntityID,
						   entityID = e.entityID,
						   ProductEntity = p,

						   ShippingLocationName = p.ProductEntityShipping.ShippingLocation.title
                       };

            return rval;
        }

		public IQueryable<TagXrefEntity> GetTagsForClasses(long[] class_ids, short[] tag_types)
		{
			var rval = from t in this.TagXrefEntities
					   where class_ids.Contains(t.entityID)  
						   && tag_types.Contains(t.Tag.tagTypeID)
					   select new TagXrefEntity()
					   {
						   entityID = t.entityID,
						   tagID = t.tagID,
						   Tag = t.Tag
					   };

			return rval;
		}


        public EntityItem[] GetFilesForClasses(long[] classes_ids)
        {
            var rval = from f in this.EntityItems
                       where classes_ids.Contains(f.parentEntityID)
                           && f.active == true
                           && f.deleted == false
                           && f.FileEntity.fileTypeID == (short)FileTypeIDEnum.S3File
                       select new EntityItem
                       {
                           entityID = f.entityID,
                           title = f.title,
                           parentEntityID = f.parentEntityID,

                           FileEntity = f.FileEntity
                       };

            return rval.ToArray();
        }

        public Dictionary<long, string> GetSmallPostersForClasses(long[] classes_ids)
        {
            var rval = from f in this.FileEntities
                       where classes_ids.Contains(f.EntityItem.parentEntityID)
                           && f.EntityItem.active == true
                           && f.EntityItem.deleted == false
                           && f.fileTypeID == (short)FileTypeIDEnum.SmallPoster
                       select new
                       {
                           f.EntityItem.parentEntityID,
                           f.filePath
                       };

            return rval.AsEnumerable().ToDictionary(kvp => kvp.parentEntityID, kvp => kvp.filePath);
        }

        public IQueryable<ClassEntity> GetFreeOfferClasses(long from_root_id, long parent_id, long portal_id) 
        {
            var rval = from c in this.ClassEntities
                       where c.EntityItem.active == true
                           && c.EntityItem.deleted == false
                           && c.CatalogItemXrefPortal.portalID == portal_id
                           && c.EntityItem.rootEntityID == from_root_id
                           && c.EntityItem.parentEntityID == parent_id
                           && c.CatalogItemXrefPortal.isFreeOffer == true

                       select new ClassEntity
                       {
                           classID = c.classID,
                           description = c.description,
                           duration = c.duration,
                           SpeakerName = c.EntityItemSpeaker.title,
                           newOrder = c.newOrder,

                           EntityItem = c.EntityItem,
                           CatalogItemExtend = c.CatalogItemExtend
                       };

            return rval;
        }

        public int GetFreeOfferClassesCnt(long[] entity_ids, long from_root_id, long parent_id, long portal_id) 
        {
            var rval = from c in this.ClassEntities
                       where c.EntityItem.active == true
                           && c.EntityItem.deleted == false
                           && c.CatalogItemXrefPortal.portalID == portal_id
                           && c.EntityItem.rootEntityID == from_root_id
                           && c.EntityItem.parentEntityID == parent_id
                           && c.CatalogItemXrefPortal.isFreeOffer == true
                           && entity_ids.Contains(c.classID)

                       select c;

            return rval.Count();
        }


		public IQueryable<ClassEntity> GetFullFreeClasses(long from_root_id, long portal_id)
		{
			var rval = from c in this.ClassEntities
					   where c.EntityItem.active == true
						   && c.EntityItem.deleted == false
						   && c.CatalogItemXrefPortal.portalID == portal_id
						   && c.EntityItem.rootEntityID == from_root_id
						   && c.CatalogItemXrefPortal.isFullFree == true

					   select new ClassEntity
					   {
						   classID = c.classID,
						   description = c.description,
						   duration = c.duration,
						   SpeakerName = c.EntityItemSpeaker.title,
						   newOrder = c.newOrder,

						   EntityItem = c.EntityItem,
						   CatalogItemExtend = c.CatalogItemExtend
					   };
			return rval;
		}

		public int GetFullFreeClassesCnt(long from_root_id, long portal_id)
		{
			var rval = from c in this.ClassEntities
					   where c.EntityItem.active == true
						   && c.EntityItem.deleted == false
						   && c.CatalogItemXrefPortal.portalID == portal_id
						   && c.EntityItem.rootEntityID == from_root_id
						   && c.CatalogItemXrefPortal.isFullFree == true

					   select c;
			return rval.Count();
		}


		#endregion


		#region Update

		public int IncreaseStatCnt(long class_id, int increase_download_cnt, int increase_listen_cnt)
		{
			return this.ClassEntities.Where(c => c.classID == class_id)
				.Update(c => new ClassEntity() { statDownloadCnt = c.statDownloadCnt + increase_download_cnt, statListenCnt = c.statListenCnt + increase_listen_cnt });
		}

		#endregion

	}
}
