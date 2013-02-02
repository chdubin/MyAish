using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Catalog;
using MainCommon;
using BLToolkit.Data.Linq;

namespace MainEntity
{
    public partial class CatalogDbManager
    {
		#region Shippings

		#region Select

		public IQueryable<ShippingLocation> GetShippingLocations()
		{
			var rval = from l in this.ShippingLocations
					   select l;

			return rval;
		}

		public IQueryable<ShippingLocation> GetShippingLocation(string from_title)
		{
			var rval = from l in this.ShippingLocations
					   where l.title == from_title
					   select l;

			return rval;
		}

		public IQueryable<ProductEntityShipping> GetProductShippingLocation(long from_product_id, int and_shipping_location_id)
		{
			var rval = from l in this.ProductEntityShippings
					   where l.productID == from_product_id && l.shippingLocationID == and_shipping_location_id
					   select l;

			return rval;
		}

		#endregion


		#region Insert

		public int InsertShippingLocation(string title)
		{
			return Convert.ToInt32(this.ShippingLocations.InsertWithIdentity(() => new ShippingLocation()
			{
				title = title
			}));
		}

		public int InsertProductShippingLocation(long product_id, int shipping_location_id)
		{
			return this.ProductEntityShippings.Insert(() => new ProductEntityShipping()
			{
				productID = product_id,
				shippingLocationID = shipping_location_id
			});
		}

		#endregion


		#region Delete

		public int DeleteProductShippingLocation(long product_id)
		{
			return this.ProductEntityShippings
				.Where(p => p.productID == product_id)
				.Delete();
		}

		#endregion


		#endregion


		#region Select

		public IQueryable<EntityItem> GetPortalItems(long from_root_id, long parent_id, int[] catalog_entity_types, bool with_only_active,
            bool with_only_nondeleted, long current_portal_id)
        {
            var rval = from e in this.EntityItems
                       join ex in this.CatalogItemExtends on e.entityID equals ex.entityID
                       join p in this.CatalogItemXrefPortals.Where(x => x.portalID == current_portal_id) on e.entityID equals p.catalogItemID into cp
                       from cc in cp.DefaultIfEmpty()
                       where e.rootEntityID == from_root_id && e.parentEntityID == parent_id &&
                           catalog_entity_types.Contains(e.typeID)

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
                           CurrentPortal = cc,
                           CatalogItemExtend = ex,
                           ClassEntity = new ClassEntity()
                           {
                               classID = e.ClassEntity.classID,
                               speakerID = e.ClassEntity.speakerID,
                               SpeakerName = e.ClassEntity.SpeakerEntityItem.title
                           }
                       };
            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }

        public int GetPortalItemsCnt(long from_root_id, long parent_id, int[] catalog_entity_types, bool with_only_active, bool with_only_nondeleted, long current_portal_id)
        {
            var rval = from e in this.EntityItems
                       join p in this.CatalogItemXrefPortals.Where(x => x.portalID == current_portal_id) on e.entityID equals p.catalogItemID into cp
                       from cc in cp.DefaultIfEmpty()
                       where e.rootEntityID == from_root_id && e.parentEntityID == parent_id && catalog_entity_types.Contains(e.typeID)
                       select new EntityItem()
                       {
                           active = e.active,
                           deleted = e.deleted
                       };
            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval.Count();
        }

        public int GetSpeakerClassesCnt(long from_root_id, long parent_id, long speaker_id, bool? with_only_active,
           bool with_only_nondeleted, long portal_id)
        {
            var rval = from e in this.EntityItems
                       join p in this.CatalogItemXrefPortals.Where(x => x.portalID == portal_id) on e.entityID equals p.catalogItemID into cp
                       from cc in cp.DefaultIfEmpty()
                       where e.rootEntityID == from_root_id && e.parentEntityID == parent_id &&
                       e.typeID == (int)EntityItemTypeEnum.ClassItem && e.ClassEntity.speakerID == speaker_id &&
                       (cc != null || portal_id == 0)
                       select new EntityItem()
                       {
                           active = e.active,
                           deleted = e.deleted
                       };

            if (with_only_active != null && with_only_active.Value)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval.Count();
        }

        public IQueryable<EntityItem> GetSpeakerClasses(long from_root_id, long parent_id, long speaker_id, bool? with_only_active,
            bool with_only_nondeleted, long portal_id)
        {
            var rval = from e in this.EntityItems
                       join ex in this.CatalogItemExtends on e.entityID equals ex.entityID
                       join p in this.CatalogItemXrefPortals.Where(x => x.portalID == portal_id) on e.entityID equals p.catalogItemID into cp
                       from cc in cp.DefaultIfEmpty()
                       where e.rootEntityID == from_root_id && e.parentEntityID == parent_id &&
                       e.typeID == (int)EntityItemTypeEnum.ClassItem && e.ClassEntity.speakerID == speaker_id &&
                       (cc != null || portal_id == 0)
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
                           CatalogItemExtend = ex,

                           ClassEntity = new ClassEntity()
                           {
                               classID = e.ClassEntity.classID,
                               description = e.ClassEntity.description,
                               duration = e.ClassEntity.duration,
                           }

                       };


            if (with_only_active != null && with_only_active.Value)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }


        public int GetSpeakerClassesCnt_SuperUser(long from_root_id, long parent_id, long speaker_id, bool with_only_active,
          bool with_only_nondeleted, int filter_category_id)
        {
            IQueryable<EntityItem> rval =
                from e in this.EntityItems
                where e.rootEntityID == from_root_id
                    && e.parentEntityID == parent_id
                    && e.typeID == (int)EntityItemTypeEnum.ClassItem 
                    && e.ClassEntity.speakerID == speaker_id
                select new EntityItem { entityID = e.entityID };

            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);
            if (filter_category_id != 0)
                rval = rval.Where(e => e.TagXrefEntities.Where(t => t.tagID == filter_category_id).Any());

            return rval.Count();
        }

        public IQueryable<EntityItem> GetSpeakerClasses_SuperUser(long from_root_id, long parent_id, long speaker_id,
            bool with_only_active, bool with_only_nondeleted, int filter_category_id)
        {
            IQueryable<EntityItem> rval =
                from e in this.EntityItems
                where e.rootEntityID == from_root_id
                    && e.parentEntityID == parent_id
                    && e.typeID == (int)EntityItemTypeEnum.ClassItem
                    && e.ClassEntity.speakerID == speaker_id
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
                    CatalogItemExtend = e.CatalogItemExtend
                };

            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);
            if (filter_category_id != 0)
                rval = rval.Where(e => e.TagXrefEntities.Where(t => t.tagID == filter_category_id).Any());

            return rval;
        }

        public int GetSpeakerClassesCnt_Admin(long from_root_id, long parent_id, long speaker_id, bool with_only_active,
          bool with_only_nondeleted, long portal_id, int filter_category_id)
        {
            IQueryable<EntityItem> rval =
                from e in this.EntityItems
                join p in this.CatalogItemXrefPortals on e.entityID equals p.catalogItemID
                join c in this.ClassEntities on e.entityID equals c.classID
                where e.rootEntityID == from_root_id
                    && e.parentEntityID == parent_id
                    && e.typeID == (int)EntityItemTypeEnum.ClassItem
                    && c.speakerID == speaker_id
                    && p.portalID == portal_id
                select new EntityItem { entityID = e.entityID };

            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);
            if (filter_category_id != 0)
                rval = rval.Where(e => e.TagXrefEntities.Where(t => t.tagID == filter_category_id).Any());

            return rval.Count();
        }

        public IQueryable<EntityItem> GetSpeakerClasses_Admin(long from_root_id, long parent_id, long speaker_id,
            bool with_only_active, bool with_only_nondeleted, long portal_id, int filter_category_id)
        {
            IQueryable<EntityItem> rval =
                from e in this.EntityItems
                join p in this.CatalogItemXrefPortals on e.entityID equals p.catalogItemID
                where e.rootEntityID == from_root_id
                    && e.parentEntityID == parent_id
                    && e.typeID == (int)EntityItemTypeEnum.ClassItem
                    && e.ClassEntity.speakerID == speaker_id
                    && p.portalID == portal_id
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
                    CatalogItemExtend = e.CatalogItemExtend,
                    CurrentPortal = p
                };

            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);
            if (filter_category_id != 0)
                rval = rval.Where(e => e.TagXrefEntities.Where(t => t.tagID == filter_category_id).Any());

            return rval;
        }


        public IQueryable<EntityItem> GetCatalogItem(long from_catalog_item_id, bool with_only_nondeleted)
        {
            var rval = from e in this.EntityItems
                       where e.entityID == from_catalog_item_id
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
						   
                           CatalogItemExtend = e.CatalogItemExtend,
                           ClassEntity = new ClassEntity()
                           {
                               classID = e.ClassEntity.classID,
                               speakerID = e.ClassEntity.speakerID,
                               SpeakerName = e.ClassEntity.SpeakerEntityItem.title,
                               description = e.ClassEntity.description,
                               duration = e.ClassEntity.duration,
							   newOrder = e.ClassEntity.newOrder,
                           },
                           FileEntity = e.FileEntity,
                       };
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }

        public IQueryable<EntityItem> GetPackage(long from_catalog_item_id, bool with_only_nondeleted)
        {
            var rval = from e in this.EntityItems
                       where e.entityID == from_catalog_item_id
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

                           CatalogItemExtend = e.CatalogItemExtend,
                           ProductEntity = new ProductEntity()
                           {
                               productID = e.ProductEntity.productID,
                               productTypeID = e.ProductEntity.productTypeID,
                               price1 = e.ProductEntity.price1,
                               description = e.ProductEntity.description,
							   ShippingLocationID = e.ProductEntity.ProductEntityShipping.shippingLocationID,
                               unlimitedAccessInLibrary = e.ProductEntity.unlimitedAccessInLibrary
                           }
                       };
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }

        public IQueryable<EntityItem> GetPackages(long from_root_id, long current_portal_id, bool with_only_active, bool with_only_nondeleted)
        {
            var typeID = (int)EntityItemTypeEnum.PackageItem;
            var rval = from e in this.EntityItems
                       join ex in this.CatalogItemExtends on e.entityID equals ex.entityID
                       join x in this.CatalogItemXrefPortals on e.entityID equals x.catalogItemID
                       join p in this.ProductEntities on e.entityID equals p.productID
                       join sx in this.ProductEntityShippings on p.productID equals sx.productID into sxx
                       from sx in sxx.DefaultIfEmpty()
                       join s in this.ShippingLocations on sx.shippingLocationID equals s.shippingLocationID into ss
                       from s in ss.DefaultIfEmpty()
                       where e.rootEntityID == from_root_id && e.typeID == typeID && x.portalID == current_portal_id
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

                           CatalogItemExtend = ex,
                           ProductEntity = new ProductEntity()
                           {
                               description = p.description,
                               price1 = p.price1,
                               price2 =p.price2,
                               productID = p.productID,
                               productTypeID = p.productTypeID,
                               ShippingLocationID = sx.shippingLocationID,
                               ShippingLocationTitle = s.title
                           },
                       };
            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);


            return rval;
        }


		public IQueryable<EntityItem> GetEntityItems(long from_parent_id, EntityItemTypeEnum entity_type_id, bool with_only_active, bool with_only_nondeleted)
		{
			var rval = from e in this.EntityItems
					   where e.parentEntityID == from_parent_id && e.typeID == (int)entity_type_id
					   select e;
			if (with_only_active)
				rval = rval.Where(e => e.active);
			if (with_only_nondeleted)
				rval = rval.Where(e => !e.deleted);

			return rval;
		}

        public IQueryable<EntityItem> GetEntityItem(long entity_id, bool with_only_active, bool with_only_nondeleted)
        {
            var rval = from e in this.EntityItems
                       where e.entityID == entity_id
                       select e;
            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }


        //public IQueryable<EntityItem> SelectChildCatalogItems(long from_root_id, int[] entity_types_id, bool with_only_active, bool with_only_nondeleted)
        //{
        //    var rval = from e in this.EntityItems
        //               join p in this.ProductEntities on e.entityID equals p.productID
        //               //join f in this.FileEntities on e.entityID equals f.fileID into ff
        //               //from f in ff.DefaultIfEmpty()
        //               join s in this.SubscribePlanEntities on e.entityID equals s.subscribePlanID into ss
        //               from s in ss.DefaultIfEmpty()
        //               where from_root_id == e.rootEntityID && entity_types_id.Contains(e.typeID)
        //               select new EntityItem()
        //               {
        //                   active = e.active,
        //                   createDate = e.createDate,
        //                   creatorID = e.creatorID,
        //                   deleted = e.deleted,
        //                   entityID = e.entityID,
        //                   hierarchiID = e.hierarchiID,
        //                   parentEntityID = e.parentEntityID,
        //                   rootEntityID = e.rootEntityID,
        //                   sortOrder = e.sortOrder,
        //                   title = e.title,
        //                   typeID = e.typeID,

        //                   SubscribePlanEntity = s,
        //                   //FileEntity = f,
        //                   ProductEntity = new ProductEntity()
        //                   {
        //                       productID = p.productID,
        //                       productTypeID = p.productTypeID,
        //                       price1 = p.price1,
        //                       price2 = p.price2,
        //                       description = p.description,
        //                       ShippingLocationID = p.ProductEntityShipping.shippingLocationID,
        //                   }

        //               };
        //    if (with_only_active)
        //        rval = rval.Where(e => e.active);
        //    if (with_only_nondeleted)
        //        rval = rval.Where(e => !e.deleted);

        //    return rval;
        //}



        public IQueryable<EntityItem> GetChildCatalogItems(long[] from_parent_ids, int[] entity_types_id, bool with_only_active, bool with_only_nondeleted)
        {
            var rval = from e in this.EntityItems
					   join p in this.ProductEntities on e.entityID equals p.productID
                       join f in this.FileEntities on e.entityID equals f.fileID into ff
                       from f in ff.DefaultIfEmpty()
                       join s in this.SubscribePlanEntities on e.entityID equals s.subscribePlanID into ss
                       from s in ss.DefaultIfEmpty()
                       where from_parent_ids.Contains(e.parentEntityID) && entity_types_id.Contains(e.typeID)
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

                           SubscribePlanEntity = s,
                           FileEntity = f,
						   ProductEntity = new ProductEntity()
						   {
                               productID = p.productID,
                               productTypeID = p.productTypeID,
                               price1 = p.price1,
                               price2 = p.price2,
                               description = p.description,
                               ShippingLocationID = p.ProductEntityShipping.shippingLocationID,
						   }

                       };
            if (with_only_active)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }

        public IQueryable<CatalogItemXrefPortal> GetCatalogItemPortals(long catalog_item_id) 
        {
            var rval = from c in this.CatalogItemXrefPortals
                       where c.catalogItemID == catalog_item_id
                       select new CatalogItemXrefPortal()
                       {
                           portalID = c.portalID,
                           catalogItemID = c.catalogItemID,
                           isVisible = c.isVisible,
                           isFree = c.isFree,
                           isFreeOffer = c.isFreeOffer,
						   isFullFree = c.isFullFree
                       };

            return rval;
        }

        public IQueryable<FileEntity> GetFiles(long[] from_parent_ids, bool with_only_active, bool with_only_nondeleted)
        {
            var rval = from f in this.FileEntities
                       where f.EntityItem.typeID == (int)EntityItemTypeEnum.FileItem && from_parent_ids.Contains(f.EntityItem.parentEntityID)
                       select new FileEntity()
                       {
                           fileID = f.fileID,
                           filePath = f.filePath,
                           fileTypeID = f.fileTypeID,

                           EntityItem = f.EntityItem
                       };
            if (with_only_active)
                rval = rval.Where(c => c.EntityItem.active);
            if (with_only_nondeleted)
                rval = rval.Where(c => !c.EntityItem.deleted);

            return rval;
        }

		public IQueryable<Tag> GetTag(string tag_name, TagTypeEnum tag_type)
		{
			var rval = from t in this.Tags
					   where t.tagTypeID == (short)tag_type && t.name == tag_name
					   select t;
			
			return rval;
		}

		public IQueryable<TagXrefEntity> GetTags(long[] entity_ids, short[] tag_type_ids)
		{
			var rval = from x in this.TagXrefEntities
					   join t in this.Tags on x.tagID equals t.tagID
					   where tag_type_ids.Contains(t.tagTypeID) && entity_ids.Contains(x.entityID)
					   select new TagXrefEntity()
					   {
						   entityID = x.entityID,
						   tagID = x.tagID,

						   Tag = t
					   };
			if (entity_ids != null)
				rval = rval.Where(t => entity_ids.Contains(t.entityID));

			return rval;
		}


        public IQueryable<Tag> GetAllTags(TagTypeEnum tag_type)
        {
            var rval = from t in this.Tags
                       where t.tagTypeID == (short)tag_type
                       orderby t.name
                       select t;

            return rval;
        }

        public IQueryable<vw_Category> GetAllCategories()
        {
            return from c in this.CategoryView
                   select c;
        }

        public IQueryable<CatalogItemXrefPortal> GetCatalogItemXrefPortals(long[] catalog_entity_ids)
        {
            var rval = from c in this.CatalogItemXrefPortals
                       where catalog_entity_ids.Contains(c.catalogItemID)
                       select c;
            return rval;
        }

        public IQueryable<EntityItem> GetClassProduct(long class_id)
        {
            var rval = from e in this.EntityItems
                       where e.parentEntityID == class_id && e.deleted == false && e.typeID == (int)EntityItemTypeEnum.ProductItem
                       select new EntityItem
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

                           CatalogItemExtend = e.CatalogItemExtend,
                           ProductEntity = new ProductEntity()
                           {
                               productID = e.ProductEntity.productID,
                               productTypeID = e.ProductEntity.productTypeID,
                               price1 = e.ProductEntity.price1,
                               price2 = e.ProductEntity.price2,
                               description = e.ProductEntity.description,
							   File = e.FileEntity
                           },
                       };

            return rval;
        }

        public IQueryable<string> GetCatalogItemCategories(long catalog_item_id) 
        {
            return from txr in this.TagXrefEntities
                   join
                       t in this.Tags on txr.tagID equals t.tagID
                   where txr.entityID == catalog_item_id
                   select t.name;
        }

        public IQueryable<FileEntity> GetClassImage(long class_id)
        {
            var rval = from e in this.EntityItems
                       where e.parentEntityID == class_id && e.typeID == (int)EntityItemTypeEnum.FileItem
                       && e.FileEntity.fileTypeID == (int)FileTypeIDEnum.SmallPoster && e.deleted == false
                       select e.FileEntity;

            return rval;
        }

        public IQueryable<EntityItem> GetFileForCatalogItem(long catalog_item_id) 
        {
            var rval = from e in this.EntityItems
                       where e.parentEntityID == catalog_item_id && e.typeID == (int)EntityItemTypeEnum.FileItem && e.deleted == false
                       select new EntityItem
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

                           FileEntity = new FileEntity
                           {
                               filePath = e.FileEntity.filePath,
                               fileTypeID = e.FileEntity.fileTypeID
                           }
                       };

            return rval;

        }

        public IQueryable<EntityItem> GetClassItems(long[] entity_ids)
        {
            return from e in this.EntityItems
                   join c in this.ClassEntities on e.entityID equals c.classID
                   where entity_ids.Contains(e.entityID)
                   select new EntityItem
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

                       ClassEntity = new ClassEntity()
                       {
                           classID = e.ClassEntity.classID,
                           description = e.ClassEntity.description
                       }
                   };
        }

        //public IQueryable<EntityItem> GetPackageClasses(long from_product_id)
        //{
        //    return from x in this.ProductXrefEntities
        //           join e in this.EntityItems on x.entityID equals e.entityID
        //           join c in this.ClassEntities on e.entityID equals c.classID
        //           where x.productID == from_product_id
        //           select new EntityItem()
        //           {
        //               active = e.active,
        //               createDate = e.createDate,
        //               creatorID = e.creatorID,
        //               deleted = e.deleted,
        //               entityID = e.entityID,
        //               hierarchiID = e.hierarchiID,
        //               parentEntityID = e.parentEntityID,
        //               rootEntityID = e.rootEntityID,
        //               sortOrder = e.sortOrder,
        //               title = e.title,
        //               typeID = e.typeID,

        //               ClassEntity = new ClassEntity()
        //               {
        //                   classID = e.ClassEntity.classID,
        //                   description = e.ClassEntity.description
        //               }
        //           };
        //}

        public IQueryable<EntityItem> SelectProductXrefEntities(long from_product_id)
        {
            return from x in this.ProductXrefEntities
                   join e in this.EntityItems on x.entityID equals e.entityID
                   join p in this.ProductEntities on e.entityID equals p.productID
                   where x.productID == from_product_id
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

                       ProductEntity = p
                   };
        }


        public IQueryable<ClassEntity> GetClassItems(int from_start_index, int and_page_size, short include_product_type, string title, string speaker)
        {
            return (from c in this.ClassEntities
                    join e in this.EntityItems on c.classID equals e.entityID
                    join s in this.EntityItems on c.speakerID equals s.entityID
                    join ce in this.EntityItems on c.classID equals ce.parentEntityID
                    join p in this.ProductEntities on ce.entityID equals p.productID
                    where e.active == true && e.deleted == false &&
                        e.title.Contains(title) && s.title.Contains(speaker) && p.productTypeID == include_product_type
                    select new ClassEntity()
                    {
                        classID = c.classID,
                        description = c.description,
                        duration = c.duration,
                        speakerID = c.speakerID,

                        Product = p,
                        SpeakerEntityItem = s,
                        EntityItem = e,
                    }).Skip(from_start_index).Take(and_page_size);
        }

        public int GetClassItemsCount(short include_product_type, string title, string speaker)
        {
            return (from c in this.ClassEntities
                    where c.EntityItem.active == true && c.EntityItem.deleted == false &&
                        c.EntityItem.title.Contains(title) && c.SpeakerEntityItem.title.Contains(speaker) && c.ParentClassEntity.ProductEntity.productTypeID == include_product_type
                    select c).Count();
        }

        public IQueryable<EntityItem> SelectProductEntityItems(long[] products_ids, short include_product_type)
        {
            return from e in this.EntityItems
                   join p in this.ProductEntities on e.entityID equals p.productID
                   where e.active == true && e.deleted == false && p.productTypeID == include_product_type && products_ids.Contains(p.productID)
                   select e;

        }

        //public IQueryable<SubscribePlanEntity> GetSubscribePlan(long from_package_id)
        //{
        //    var rval= from s in this.SubscribePlanEntities
        //           join x in this.ProductXrefEntities on s.subscribePlanID equals x.entityID
        //           where x.productID == from_package_id
        //           select s;

        //    return rval;
        //}

        public IQueryable<SubscribePlanEntity> SelectSubscribePlan(long entity_id)
        {
            var rval = from s in this.SubscribePlanEntities
                       where s.subscribePlanID == entity_id
                       select s;

            return rval;
        }

        public IQueryable<ClassEntity> GetClassEntities(long[] from_child_entity_ids)
        {
            var rval = from c in this.ClassEntities
                       join e in this.EntityItems on c.classID equals e.parentEntityID
                       where from_child_entity_ids.Contains(e.entityID)
                       select new ClassEntity()
                       {
                           classID = c.classID,
                           description = c.description,
                           duration = c.duration,
                           speakerID = c.speakerID,

                           SpeakerEntityItem = c.SpeakerEntityItem,
                           EntityItem = c.EntityItem
                       };

            return rval;
        }

        public int GetClassEntitiesCount(long[] from_child_entity_ids)
        {
            var rval = (from c in this.ClassEntities
                       join e in this.EntityItems on c.classID equals e.parentEntityID
                       where from_child_entity_ids.Contains(e.entityID)
                       select c).Count();

            return rval;
        }

        public IQueryable<EntityItem> GetClassEntitiesWithFiles(long from_root_id, long parent_id, long[] except_class_entities_ids,
            long filter_portalid, int[] filter_categories, string filter_title, string filter_speaker, string filter_code, bool filter_new)
        {
            IQueryable<EntityItem> rval =
                            from e in this.EntityItems
                            join x in this.CatalogItemExtends on e.entityID equals x.entityID
                            join ec in this.EntityItems on e.entityID equals ec.parentEntityID
                            join ecp in this.ProductEntities on ec.entityID equals ecp.productID
                            join ecf in this.FileEntities on ec.entityID equals ecf.fileID
                            join c in this.ClassEntities on e.entityID equals c.classID
                            join s in this.EntityItems on c.speakerID equals s.entityID
                            join se in this.EntityItems on c.speakerID equals se.entityID
                            join p in this.CatalogItemXrefPortals.Where(x => x.portalID == filter_portalid) on e.entityID equals p.catalogItemID into pp
                            from ppp in pp.DefaultIfEmpty()
                            where e.rootEntityID == from_root_id
                                && e.parentEntityID == parent_id && e.active == true && e.deleted == false &&
                                ecp.productTypeID == (short)ProductTypeEnum.File
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
                                FilteredPortal = ppp,
                                ClassSpeakerName = s.title,
                                ClassEntity = c,
                                FileEntity = ecf
                            };

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

            return rval;
        }

        public int GetClassEntitiesWithFilesCount(long from_root_id, long parent_id, long[] except_class_entities_ids,
            long filter_portalid, int[] filter_categories, string filter_title, string filter_speaker, string filter_code, bool filter_new)
        {
            IQueryable<EntityItem> rval =
                            from e in this.EntityItems
                            join ec in this.EntityItems on e.entityID equals ec.parentEntityID
                            join ecp in this.ProductEntities on ec.entityID equals ecp.productID
                            join ecf in this.FileEntities on ec.entityID equals ecf.fileID
                            join c in this.ClassEntities on e.entityID equals c.classID
                            join s in this.EntityItems on c.speakerID equals s.entityID
                            join se in this.EntityItems on c.speakerID equals se.entityID
                            join p in this.CatalogItemXrefPortals.Where(x => x.portalID == filter_portalid) on e.entityID equals p.catalogItemID into pp
                            from ppp in pp.DefaultIfEmpty()
                            where e.rootEntityID == from_root_id
                                && e.parentEntityID == parent_id && e.active == true && e.deleted == false &&
                                ecp.productTypeID == (short)ProductTypeEnum.File
                            select new EntityItem()
                            {
                                title = e.title,

                                ClassSpeakerName = s.title,
                                FilteredPortal = ppp,
                            };

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


        //public IQueryable<ClassEntity> GetClassEntitiesWithFiles(long[] except_class_entities_ids)
        //{
        //    var rval = from c in this.ClassEntities
        //               join e in this.EntityItems on c.classID equals e.parentEntityID
        //               join p in this.ProductEntities on e.entityID equals p.productID
        //               join f in this.FileEntities on p.productID equals f.fileID
        //               join s in this.SpeakerEntities on c.speakerID equals s.speakerID
        //               join es in this.EntityItems on s.speakerID equals es.entityID
        //               join ec in this.EntityItems on c.classID equals ec.entityID
        //               where p.productTypeID == (short)ProductTypeEnum.File && !except_class_entities_ids.Contains(c.classID) &&
        //                    ec.deleted == false && ec.active == true
        //               orderby c.classID
        //               select new ClassEntity()
        //               {
        //                   classID = c.classID,
        //                   description = c.description,
        //                   duration = c.duration,
        //                   speakerID = c.speakerID,

        //                   SpeakerEntityItem = es,
        //                   EntityItem = ec,
        //                   FileEntity = f
        //               };

        //    return rval;
        //}


        //public int GetClassEntitiesWithFilesCount(long from_root_id, long parent_id, long[] except_class_entities_ids,
        //    long filter_portalid, int[] filter_categories, string filter_title, string filter_speaker, string filter_code, bool filter_new)
        //{
        //    var rval = from c in this.ClassEntities
        //               join e in this.EntityItems on c.classID equals e.parentEntityID
        //               join p in this.ProductEntities on e.entityID equals p.productID
        //               join ec in this.EntityItems on c.classID equals ec.entityID
        //               join p1 in this.CatalogItemXrefPortals.Where(x => x.portalID == filter_portalid) on ec.entityID equals p1.catalogItemID into pp1
        //               from ppp1 in pp1.DefaultIfEmpty()

        //               where p.productTypeID == (short)ProductTypeEnum.File && !except_class_entities_ids.Contains(c.classID) &&
        //                    ec.deleted == false && ec.active == true
        //               select c.classID;

        //    return rval.Count();
        //}

        public IQueryable<ProductEntity> GetProductFilesInClasses(long[] class_entities_ids)
        {
            var rval = from e in this.EntityItems
                       join p in this.ProductEntities on e.entityID equals p.productID
                       where class_entities_ids.Contains(e.parentEntityID)
                            && p.productTypeID == (short)ProductTypeEnum.File
                       select p;

            return rval;
        }

        #endregion

        #region Insert

        public int InsertClass(long class_id, long speaker_id, string description, TimeSpan duration, int? new_order)
        {
            return this.ClassEntities.Insert(() => new ClassEntity() { classID = class_id, speakerID = speaker_id, description = description, duration = duration, newOrder=new_order });
        }

        public int InsertProduct(long product_id, short product_type_id, decimal? price1, decimal? price2, string description, bool unlimitedAccessInLibrary)
        {
            return this.ProductEntities.Insert(() => new ProductEntity() { productID = product_id, price1 = price1, price2 = price2, productTypeID = product_type_id, description = description, unlimitedAccessInLibrary = unlimitedAccessInLibrary });
        }

        public int InsertFile(long file_id, FileTypeIDEnum file_type, string file_path, string alternate_file_path)
        {
			return this.FileEntities.Insert(() => new FileEntity() { fileID = file_id, fileTypeID = (short)file_type, filePath = file_path, alternateFilePath = alternate_file_path });
        }

        public int InsertTagXrefEntity(int tag_id, long entity_id)
        {
            return this.TagXrefEntities.Insert(() => new TagXrefEntity() { entityID = entity_id, tagID = tag_id });
        }

		public int InsertCatalogItemXrefPortal(long portal_id, long catalog_item_id, bool is_visible, bool is_free, bool is_free_offer, bool is_full_free)
        {
            return this.CatalogItemXrefPortals.Insert(() => new CatalogItemXrefPortal() {
				portalID = portal_id,
				catalogItemID = catalog_item_id,
				isFree = is_free,
				isVisible = is_visible,
				isFreeOffer = is_free_offer,
				isFullFree = is_full_free
			});
        }

        public int InsertCatalogItemExtend(long entity_id, string code, string notes)
        {
            return this.CatalogItemExtends.Insert(() => new CatalogItemExtend() { entityID = entity_id, code = code, notes = notes });
        }

        public int InsertTag(string name, TagTypeEnum tag_type)
        {
            return Convert.ToInt32(this.Tags.InsertWithIdentity(() => new Tag() { name = name, tagTypeID = (short)tag_type }));
        }

        public int InsertProductXrefEntity(long product_id, long entity_id)
        {
            return this.ProductXrefEntities.Insert(() => new ProductXrefEntity() { productID = product_id, entityID = entity_id });
        }

        public int InsertSubscribePlan(long subscribe_plan_id, short duration_in_days, short duration_in_months, int free_offer_cnt, int free_units_on_subscribe, string description)
        {
            return this.SubscribePlanEntities.Insert(() => new SubscribePlanEntity()
            {
                subscribePlanID = subscribe_plan_id,
                durationInDays = duration_in_days,
                durationInMonths = duration_in_months,
                freeOfferCnt = free_offer_cnt,
                freeUnitsOnSubscribe = free_units_on_subscribe,
                description = description
            });
        }

        public int InsertSubscribePlanXref(long subscribe_plan_id, long next_subscribe_plan_id, int? free_units_on_next_subscribe)
        {
            return this.SubscribePlanXrefs.Insert(() => new SubscribePlanXref()
            {
                subscribePlanID = subscribe_plan_id,
                nextSubscribePlanID = next_subscribe_plan_id,
                freeUnitsOnNextSubscribe = free_units_on_next_subscribe,
            });
        }

        #endregion

        #region Delete

        public int DeleteEntityItem(long entity_id)
        {
            return this.EntityItems.Where(e => e.entityID == entity_id)
                .Update(e => new EntityItem()
              {
                  deleted = true
              });
        }

        public int DeleteEntityItem(long parent_entity_id, short type_id)
        {
            return this.EntityItems.Where(e => e.parentEntityID == parent_entity_id && e.typeID == type_id)
                .Update(e => new EntityItem()
                {
                    deleted = true
                });
        }

        public int DeleteEntityItem(long parent_entity_id, short type_id, int min_sort_order)
        {
            return this.EntityItems.Where(e => e.parentEntityID == parent_entity_id && e.typeID == type_id && e.sortOrder >= min_sort_order)
                .Update(e => new EntityItem()
                {
                    deleted = true
                });
        }



        public int CompletelyDeleteFileEntityItem(long file_id) 
        {
            return this.FileEntities.Where(f => f.fileID == file_id).Delete();
        }

        public int DeleteCatalogItemFromPortals(long catalog_item_id)
        {
            return this.CatalogItemXrefPortals.Where(p => p.catalogItemID == catalog_item_id).Delete();
        }

        public int DeleteCatalogItemFromPortal(long portal_id, long catalog_item_id)
        {
            return this.CatalogItemXrefPortals.Where(p => p.catalogItemID == catalog_item_id && p.portalID == portal_id).Delete();
        }

		public int DeleteTagXrefEntity(long entity_id, TagTypeEnum tag_type) 
        {
			return this.TagXrefEntities.Where(t => t.entityID == entity_id && t.Tag.tagTypeID == (short)tag_type).Delete();
        }

		public int DeleteTagXrefEntity(long entity_id, int tag_id)
		{
			return this.TagXrefEntities.Where(t => t.entityID == entity_id && t.tagID == tag_id).Delete();
		}

		public int DeleteTagXrefEntity(long entity_id)
		{
			return this.TagXrefEntities.Where(t => t.entityID == entity_id).Delete();
		}


        public int DeleteProductXrefEntity(long product_id)
        {
            var products = from x in this.ProductXrefEntities
                           join e in EntityItems on x.entityID equals e.entityID
                           where x.productID == product_id
                           select x;

            return products.Delete();
        }


        public int DeleteTag(int tag_id) 
        {
            return this.Tags.Where(t => t.tagID == tag_id).Delete();
        }

        #endregion

        #region Update

        public int UpdateSubscribePlanXref(long subscribe_plan_id, long next_subscribe_plan_id)
        {
            return this.SubscribePlanXrefs.Where(x => x.subscribePlanID == subscribe_plan_id).Update(x => new SubscribePlanXref(){nextSubscribePlanID = next_subscribe_plan_id});
        }

        public int UpdateSubscribePlanXref(long subscribe_plan_id, int? free_units_on_next_subscribe)
        {
            return this.SubscribePlanXrefs.Where(x => x.subscribePlanID == subscribe_plan_id).Update(x => new SubscribePlanXref() { freeUnitsOnNextSubscribe = free_units_on_next_subscribe });
        }

        public int UpdateSubscribePlan(long subscribe_plan_id, int free_units_on_subscribe, string desc)
        {
            return this.SubscribePlanEntities.Where(s => s.subscribePlanID == subscribe_plan_id).Update(s => new SubscribePlanEntity() { freeUnitsOnSubscribe = free_units_on_subscribe, description = desc });
        }

        public int UpdateChildSubscribePlan(long parent_subscribe_plan_id, string desc)
        {
            return (from s in this.SubscribePlanEntities
                    where s.EntityItem.parentEntityID == parent_subscribe_plan_id && s.EntityItem.active == true && s.EntityItem.deleted == false
                    select s).Update(s => new SubscribePlanEntity() { description = desc });
        }


        public int UpdateProduct(long product_id, decimal? price1, decimal? price2)
        {
			return this.ProductEntities.Where(p => p.productID == product_id).
                Update(p => new ProductEntity()
                {
                    price1 = price1,
                    price2 = price2
                });
        }

        public int UpdateFile(long file_id,string path,string alternate_file_path)
        {
            return this.FileEntities.Where(f => f.fileID == file_id).
                Update(f => new FileEntity()
                {
                   filePath = path,
				   alternateFilePath = alternate_file_path
                });
        }

        public int UpdateClass(long class_id, long speaker_id, string description, TimeSpan duration, int? new_order)
        {
            return this.ClassEntities.Where(e => e.classID == class_id)
                .Update(p => new ClassEntity()
               {
                   speakerID = speaker_id,
                   description = description,
                   duration = duration,
				   newOrder = new_order
               });
        }

        public int UpdateEntity(long class_id, string title, bool active)
        {
            return this.EntityItems.Where(e => e.entityID == class_id)
                .Update(p => new EntityItem()
                {
                    title = title,
                    active = active
                });
        }

        public int UpdateEntityTitle(long class_id, string title)
        {
            return this.EntityItems.Where(e => e.entityID == class_id)
                .Update(p => new EntityItem()
                {
                    title = title
                });
        }

        public int UpdateEntityActivity(long entity_id, bool active)
        {
            return this.EntityItems.Where(e => e.entityID == entity_id)
                .Update(e => new EntityItem()
                {
                    active = active
                });
        }

        public int UpdateCatalogItemExtend(long entity_id, string code, string notes)
        {
            return this.CatalogItemExtends.Where(e => e.entityID == entity_id)
                .Update(p => new CatalogItemExtend()
                {
                    code = code,
                    notes = notes
                });
        }

        public int UpdateCatalogItemExtend(long entity_id, string code)
        {
            return this.CatalogItemExtends.Where(e => e.entityID == entity_id)
                .Update(p => new CatalogItemExtend()
                {
                    code = code
                });
        }

        public int UpdateCatalogItemExtendNotes(long entity_id, string notes)
        {
            return this.CatalogItemExtends.Where(e => e.entityID == entity_id)
                .Update(p => new CatalogItemExtend()
                {
                    notes = notes
                });
        }

        public int UpdatePackage(long product_id, decimal price, string description, bool unlimited_access_in_library, long speaker_Id)
        {
            return this.ProductEntities.Where(e => e.productID == product_id)
                .Update(p => new ProductEntity()
                {
                    price1 = price,
                    description = description,
                    unlimitedAccessInLibrary = unlimited_access_in_library,
                   
                });
        }

        public int UpdateCatalogItemXrefPortal(long portal_id, long catalog_item_id, bool is_visible,
                                bool is_free, bool is_free_offer, bool? is_full_free=null) 
        {
			if (is_full_free == null)
			{
				return this.CatalogItemXrefPortals.Where(c => c.portalID == portal_id && c.catalogItemID == catalog_item_id)
				   .Update(c => new CatalogItemXrefPortal()
				   {
					   isVisible = is_visible,
					   isFree = is_free,
					   isFreeOffer = is_free_offer
				   });
			}
			else
			{
				return this.CatalogItemXrefPortals.Where(c => c.portalID == portal_id && c.catalogItemID == catalog_item_id)
				   .Update(c => new CatalogItemXrefPortal()
				   {
					   isVisible = is_visible,
					   isFree = is_free,
					   isFreeOffer = is_free_offer,
					   isFullFree = is_full_free.Value
				   });
			}
        }

        public int UpdateTag(int tag_id, string tag_name) {
            return this.Tags.Where(t => t.tagID == tag_id)
               .Update(t => new Tag()
               {
                   name = tag_name
               });
        }

        #endregion

        #region InsertOrUpdate

        public int InsertOrUpdateCatalogItemXrefPortal(long portal_id, long catalog_item_id,
			bool is_visible, bool is_free, bool is_free_offer, bool is_full_free)
        {
            // не тестировал
            int rval = this.CatalogItemXrefPortals.Where(e => e.portalID == portal_id && e.catalogItemID == catalog_item_id)
               .Update(p => new CatalogItemXrefPortal()
               {
                   isFree = is_free,
                   isFreeOffer = is_free_offer,
                   isVisible = is_visible,
				   isFullFree = is_full_free
               });

            if (rval == 0)
            {
                rval = this.CatalogItemXrefPortals.Insert(() => new CatalogItemXrefPortal()
               {
                   portalID = portal_id,
                   catalogItemID = catalog_item_id,
                   isFree = is_free,
                   isVisible = is_visible,
                   isFreeOffer = is_free_offer,
				   isFullFree = is_full_free
               });
            }

            return rval;
        }

        #endregion
    }
}
