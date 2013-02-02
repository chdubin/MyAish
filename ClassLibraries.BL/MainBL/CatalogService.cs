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
using MainCommon.Models;

namespace MainBL
{
    public partial class CatalogService : BaseBO, ICatalogService
    {
        public CatalogService(string connection_name)
            : base(connection_name)
        {
        }


		#region Shippings

		#region Select

		public ShippingLocation[] GetAllShippingLocations()
		{
			return this.Exec(System.Data.IsolationLevel.ReadCommitted,
				(CatalogDbManager context) =>
				{
					return context.GetShippingLocations().OrderBy(l => l.shippingLocationID).ToArray();
				});
		}

		#endregion

		#region Insert

		public int InsertOrSkipShippingLocation(string title)
		{
			return this.Exec(System.Data.IsolationLevel.ReadCommitted,
				(CatalogDbManager context) =>
				{
					var shippingLocationID = context.GetShippingLocation(title).Select(s => s.shippingLocationID).SingleOrDefault();
					if (shippingLocationID == 0)
						shippingLocationID = context.InsertShippingLocation(title);
					return shippingLocationID;
				});
		}

		public int InsertProductShippingLocation(long product_id, int shipping_location_id)
		{
			return this.Exec(System.Data.IsolationLevel.ReadCommitted,
				(CatalogDbManager context) =>
				{
					return context.InsertProductShippingLocation(product_id,shipping_location_id);
				});
		}

		#endregion


		#region Delete

		public int DeleteProductShippingLocation(long product_id)
		{
			return this.Exec(System.Data.IsolationLevel.ReadCommitted,
				(CatalogDbManager context) =>
				{
					return context.DeleteProductShippingLocation(product_id);
				});
		}

		#endregion

		#endregion

		#region Select


        public int GetSpeakerClassesCnt(long from_root_id, long speaker_id, bool with_only_active, bool with_only_nondeleted, long portal_id, int filter_category_id, bool is_super_user)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    //var rval = context.GetSpeakerClassesCnt(from_root_id, from_root_id, speaker_id, with_only_active,
                    //    with_only_nondeleted, portal_id);

                    int rval = is_super_user ?
                        context.GetSpeakerClassesCnt_SuperUser(from_root_id, from_root_id, speaker_id, with_only_active, with_only_nondeleted, filter_category_id) :
                        context.GetSpeakerClassesCnt_Admin(from_root_id, from_root_id, speaker_id, with_only_active, with_only_nondeleted, portal_id, filter_category_id);

                    return rval;
                });
        }

        public EntityItem[] GetSpeakerClasses(long from_root_id, long speaker_id, int start_row_index, int max_rows_count, bool with_only_active,
            bool with_only_nondeleted, long portal_id, int filter_category_id, bool is_super_user)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    //return context.GetSpeakerClasses(from_root_id, from_root_id, speaker_id, with_only_active,
                    //    with_only_nondeleted, portal_id).Skip(start_row_index).Take(max_rows_count).ToArray();

                    var rval =  is_super_user ?
                        context.GetSpeakerClasses_SuperUser(from_root_id, from_root_id, speaker_id, with_only_active, with_only_nondeleted, filter_category_id) :
                        context.GetSpeakerClasses_Admin(from_root_id, from_root_id, speaker_id, with_only_active, with_only_nondeleted, portal_id, filter_category_id);

                    var rval2 = rval.Skip(start_row_index).Take(max_rows_count).ToArray();
                    var ids = rval.Select(e => e.entityID).ToArray();

                    PrepareCatalogItem(with_only_active, with_only_nondeleted, context, ids, rval2);

                    return rval2;
                });
        }



        public EntityItem GetCatalogItem(long from_catalog_id, bool with_only_nondeleted)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    var rval = context.GetCatalogItem(from_catalog_id, with_only_nondeleted).Single();

                    PrepareCatalogItem(false, with_only_nondeleted, context, new long[] { rval.entityID }, new EntityItem[] { rval });

                    return rval;
                });
        }




		public EntityItem[] GetEntities(long from_parent_id, EntityItemTypeEnum entity_type_id, bool with_only_active, bool with_only_nondeleted)
		{
			return this.Exec(System.Data.IsolationLevel.Snapshot,
				(CatalogDbManager context) =>
				{
					return context.GetEntityItems(from_parent_id, entity_type_id, with_only_active, with_only_nondeleted).ToArray();
				});
		}

		public EntityItem[] GetSpeakers(long from_parent_id)
		{
			return this.Exec(System.Data.IsolationLevel.Snapshot,
				(CatalogDbManager context) =>
				{
					return context.GetEntityItems(from_parent_id, EntityItemTypeEnum.SpeakerItem, true, true).OrderBy(e=>e.title).ToArray();
				});
		}


        public EntityItem[] GetClassProducts(long class_id) 
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    return context.GetClassProduct(class_id).ToArray();
                });
        }

        public string GetClassImage(long class_id) 
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    return context.GetClassImage(class_id).Select(f=>f.filePath).SingleOrDefault();
                });
        }

        public EntityItem[] GetProductXrefEntities(long product_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    return context.SelectProductXrefEntities(product_id).ToArray();
                });
        }

        public EntityItem[] GetProductEntityItems(long[] products_ids, ProductTypeEnum include_product_type)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
            (CatalogDbManager context) =>
            {
                return context.SelectProductEntityItems(products_ids, (short)include_product_type).ToArray();
            });
        }

        public ClassEntity[] GetClassItems(int from_start_index, int and_page_size,ProductTypeEnum include_product_type, string title, string speaker)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    return context.GetClassItems(from_start_index, and_page_size, (short)include_product_type, title, speaker).ToArray();
                });
        }

        public int GetClassItemsCount(ProductTypeEnum include_product_type, string title, string speaker)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    return context.GetClassItemsCount((short)include_product_type, title, speaker);
                });
        }

        public Tag[] GetAllTags(TagTypeEnum tag_type)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (CatalogDbManager context) =>
               {
                   return context.GetAllTags(tag_type).ToArray();
               });
        }

        public vw_Category[] GetAllCategories()
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (CatalogDbManager context) =>
               {
                   return context.GetAllCategories().ToArray();
               });
        }

		public EntityItem[] GetCatalogItems(long from_root_id, string and_code, bool with_only_active, bool with_only_nondeleted)
		{
			return this.Exec(System.Data.IsolationLevel.Snapshot,
			   (CatalogDbManager context) =>
			   {
				   return context.GetCatalogItems(from_root_id,and_code,
					   new int[] { (int)EntityItemTypeEnum.ClassItem, (int)EntityItemTypeEnum.PackageItem }, with_only_active, with_only_nondeleted).ToArray();
			   });
		}

        public ClassEntity[] GetClassEntities(long[] from_child_entity_ids, int start_row_index, int max_rows_count)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (CatalogDbManager context) =>
               {
                   return context.GetClassEntities(from_child_entity_ids).Skip(start_row_index).Take(max_rows_count).ToArray();
               });
        }

        public int GetClassEntitiesCount(long[] from_child_entity_ids)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (CatalogDbManager context) =>
               {
                   return context.GetClassEntitiesCount(from_child_entity_ids);
               });
        }

        public EntityItem[] GetClassEntitiesWithFiles(long from_root_id, long[] except_class_entities_ids, int start_row_index, int max_rows_count,
            long filter_portalid, int[] filter_categories, string filter_title, string filter_speaker, string filter_code, bool filter_new)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    var rval = context.GetClassEntitiesWithFiles(from_root_id, from_root_id, except_class_entities_ids,
                        filter_portalid, filter_categories, filter_title, filter_speaker, filter_code, filter_new);
                    if (start_row_index > 0) rval = rval.Skip(start_row_index);
                    if (max_rows_count < int.MaxValue) rval = rval.Take(max_rows_count);
                    return rval.ToArray();
                });
        }

        public int GetClassEntitiesWithFilesCount(long from_root_id, long[] except_class_entities_ids,
            long filter_portalid, int[] filter_categories, string filter_title, string filter_speaker, string filter_code, bool filter_new)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    return context.GetClassEntitiesWithFilesCount(from_root_id, from_root_id, except_class_entities_ids,
                        filter_portalid, filter_categories, filter_title, filter_speaker, filter_code, filter_new);
                });
        }

        public ProductEntity[] GetProductFilesInClasses(long[] class_entities_ids)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (CatalogDbManager context) =>
                {
                    return context.GetProductFilesInClasses(class_entities_ids).ToArray();
                });
        }

        #endregion

        #region Insert

        public int InsertTag(string name, TagTypeEnum tag_type)  
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (CatalogDbManager context) =>
               {
                   return context.InsertTag(name, tag_type); ;
               });
        }

		public int InsertOrSkipTag(string tag_name, TagTypeEnum tag_type)
		{
			return this.Exec(System.Data.IsolationLevel.ReadCommitted,
			   (CatalogDbManager context) =>
			   {
				   var tag = context.GetTag(tag_name, tag_type).SingleOrDefault();

				   return tag == null ? context.InsertTag(tag_name, tag_type) : tag.tagID;
			   });
		}

		public int InsertTagXrefEntity(int tag_id, long entity_id)
		{
			return this.Exec(System.Data.IsolationLevel.ReadCommitted,
			   (CatalogDbManager context) =>
			   {
				   return context.InsertTagXrefEntity(tag_id, entity_id);
			   });
		}

        public int InsertCategories(string categories, long entity_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (CatalogDbManager context) =>
               {
                   context.DeleteTagXrefEntity(entity_id, TagTypeEnum.Category);
                   InsertCategories(categories, entity_id, context);
                   return 1;
               });
        }

        private void InsertCategories(string categories, long entity_id, CatalogDbManager context)
        {
            if (!string.IsNullOrEmpty(categories) && categories.Trim().Length > 0)
            {
                string[] inputTags = categories.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).Distinct().ToArray();

                Tag[] tags = context.GetAllTags(TagTypeEnum.Category).ToArray();

                string[] tagsNamesToCreate = inputTags.Except(tags.Select(t => t.name).ToArray()).ToArray();

                Tag[] tagsToCreate = tagsNamesToCreate.Select(tn => new Tag { name = tn, tagTypeID = (short)TagTypeEnum.Category }).ToArray();

                foreach (Tag tag in tagsToCreate)
                {
                    tag.tagID = context.InsertTag(tag.name, TagTypeEnum.Category);
                    context.InsertTagXrefEntity(tag.tagID, entity_id);
                }

                foreach (Tag tag in tags.Where(t => inputTags.Contains(t.name)))
                    context.InsertTagXrefEntity(tag.tagID, entity_id);
            }
        }

		public long InsertClass(string title, string description, Guid creator_id, bool? active, long root_entity_id, long speaker_id, int? new_order,
			int level_tag_id,
            CatalogItemXrefPortal[] xref_portals,
            ProductEntity[] products, ImageInfo image,
            string categories, TimeSpan duration, string code, string notes)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (CatalogDbManager context) =>
                {
                    var createDate = DateTime.Now.ToUniversalTime();
					var classID = EntityItemService.InsertEnityItem(title, 0, active!=null ? active.Value : true, MainCommon.EntityItemTypeEnum.ClassItem,
                        creator_id, root_entity_id, root_entity_id, createDate, context);

					context.InsertClass(classID, speaker_id, description, duration, new_order);
                    context.InsertCatalogItemExtend(classID, code, notes);
					context.InsertTagXrefEntity(level_tag_id, classID);

                    foreach (var portal in xref_portals)
						context.InsertCatalogItemXrefPortal(portal.portalID, classID, portal.isVisible, portal.isFree, portal.isFreeOffer, portal.isFullFree);

                    foreach (var product in products)
                    {
                        var productName = "[" + (ProductTypeEnum)product.productTypeID + "] " + title;
                        var productID = EntityItemService.InsertEnityItem(productName, 0, true, EntityItemTypeEnum.ProductItem,
                            creator_id, root_entity_id, classID, createDate, context);
                        context.InsertProduct(productID, product.productTypeID, product.price1, product.price2, string.Empty, product.unlimitedAccessInLibrary);
						if (product.ShippingLocationID != 0)
							context.InsertProductShippingLocation(productID, product.ShippingLocationID);
                        if (product.productTypeID == (short)ProductTypeEnum.File)
                            context.InsertFile(productID, (FileTypeIDEnum)product.File.fileTypeID, product.File.filePath, product.File.alternateFilePath);
                    }

                    if (image != null)
                        InsertOtUpdateImage(classID, title, creator_id, root_entity_id, image, context, createDate);

                    InsertCategories(categories, classID, context);

                    return classID;
                });
        }

        //private static void SaveAs(Stream image, long image_lenght, string filePath)
        //{
        //    using (var file = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write))
        //    {
        //        var buff = new byte[65536];
        //        int len = 0;
        //        for (long size = 0; size < image_lenght; size += len)
        //        {
        //            len = image.Read(buff, 0, buff.Length);
        //            file.Write(buff, 0, len);
        //        }
        //    }
        //}

        public int InsertPackage(string title, bool? active, Guid creator_id, long root_entity_id, decimal price, string description,
			CatalogItemXrefPortal[] xref_portals, int shipping_location_id,
            long[] attach_products_ids, short subscribe_plan_months, long monthly_subscribe_plan_id, int free_units_on_subscribe, int? free_units_on_next_subscribe,
            ImageInfo image, string categories, string code, bool unlimited_access_in_library, long speaker_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (CatalogDbManager context) =>
                {
                    var createDate = DateTime.Now.ToUniversalTime();
					var productID = EntityItemService.InsertEnityItem(title, 0, active != null ? active.Value : true, MainCommon.EntityItemTypeEnum.PackageItem,
						creator_id, root_entity_id, root_entity_id, createDate, context);
                    
                    var rval = context.InsertProduct(productID, (short)ProductTypeEnum.Package, price, null, description, unlimited_access_in_library);
                    TimeSpan duration = new TimeSpan(0, 0, 0, 0, 0);
                    context.InsertClass(productID, speaker_id, description, duration, null);
                    context.InsertCatalogItemExtend(productID, code, string.Empty);
					if (shipping_location_id > 0)
						context.InsertProductShippingLocation(productID, shipping_location_id);

					foreach (var portal in xref_portals)
						context.InsertCatalogItemXrefPortal(portal.portalID, productID, portal.isVisible, portal.isFree, portal.isFreeOffer, portal.isFullFree);

                    foreach (var p in attach_products_ids)
                        context.InsertProductXrefEntity(productID, p);

                    context.InsertFile(productID, (FileTypeIDEnum)FileTypeIDEnum.S3File, "", "");

                    var currentSubscribe = context.GetEntityItems(productID, EntityItemTypeEnum.SubscribePlanItem, true, true).SingleOrDefault();
                    InsertOrUpdateSubscribePlanTree(productID, currentSubscribe, creator_id, subscribe_plan_months, monthly_subscribe_plan_id, free_units_on_subscribe, free_units_on_next_subscribe, createDate, context);

                    if (image != null)
                        InsertOtUpdateImage(productID, title, creator_id, root_entity_id, image, context, createDate);

                    InsertCategories(categories, productID, context);

                    return rval;
                });
        }

        #endregion

        #region Update

        public int UpdateClass(long class_id, string title, string description, Guid creator_id, bool? active, long root_entity_id, long speaker_id, int? new_order,
			int level_tag_id,
            CatalogItemXrefPortal[] xref_portals,
            ProductEntity[] products, ImageInfo image,
            string categories, TimeSpan duration, string code, string notes)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (CatalogDbManager context) =>
                {
                    var createDate = DateTime.Now.ToUniversalTime();
					var rval = context.UpdateClass(class_id, speaker_id, description, duration, new_order);
                    context.UpdateEntityTitle(class_id, title);
                    context.UpdateCatalogItemExtend(class_id, code, notes);

					context.DeleteTagXrefEntity(class_id, level_tag_id);
					if (level_tag_id != 0)
						context.InsertTagXrefEntity(level_tag_id, class_id);

					if(active!=null)
						context.UpdateEntityActivity(class_id, active.Value);

					foreach (var portal in xref_portals)
					{
						context.DeleteCatalogItemFromPortal(portal.portalID, class_id);
						context.InsertCatalogItemXrefPortal(portal.portalID, class_id, portal.isVisible, portal.isFree, portal.isFreeOffer, portal.isFullFree);
					}
                    
                    var classProducts = context.GetClassProduct(class_id).ToArray();
					var calssProductTypes = classProducts.Select(p=>p.ProductEntity.productTypeID).ToArray();
					foreach (var product in classProducts)
					{
						var updateProduct = products.Where(cp => cp.productTypeID == product.ProductEntity.productTypeID).SingleOrDefault();
						if (updateProduct != null)
							UpdateProduct(context, product.entityID, updateProduct.price1, updateProduct.price2, updateProduct.ShippingLocationID,
								updateProduct.productTypeID == (short)ProductTypeEnum.File ? updateProduct.File : null);
						else
							context.DeleteEntityItem(product.entityID);
					}
					foreach (var product in products.Where(p => !calssProductTypes.Contains(p.productTypeID)))
						InserProduct(class_id, title, creator_id, root_entity_id, context, createDate, product,
							product.productTypeID == (short)ProductTypeEnum.File ? product.File : null);

                    
                    if (image != null)
                        InsertOtUpdateImage(class_id, title, creator_id, root_entity_id, image, context, createDate);

					context.DeleteTagXrefEntity(class_id, TagTypeEnum.Category);
                    InsertCategories(categories, class_id, context);

                    return rval;
                });
        }

        public int UpdatePackage(long product_id, string title, decimal price, string description, Guid creator_id, bool? active, long root_entity_id, 
            CatalogItemXrefPortal[] xref_portals, int shipping_location_id,
            long[] attach_products_ids, short subscribe_plan_months, long monthly_subscribe_plan_id, int free_units_on_subscribe, int? free_units_on_next_subscribe,
            ImageInfo image, string categories, string code, bool unlimited_access_in_library, long speaker_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (CatalogDbManager context) =>
                {
                    var createDate = DateTime.Now.ToUniversalTime();
                    var rval = context.UpdatePackage(product_id, price, description, unlimited_access_in_library, speaker_id);
                    TimeSpan duration = new TimeSpan(0, 0, 0, 0, 0);
                    context.UpdateClass(product_id, speaker_id, description, duration, null);

                    context.UpdateEntityTitle(product_id, title);
                    context.UpdateCatalogItemExtend(product_id, code);
					
					context.DeleteProductShippingLocation(product_id);
					if (shipping_location_id > 0)
						context.InsertProductShippingLocation(product_id, shipping_location_id);

					if (active != null)
						context.UpdateEntityActivity(product_id, active.Value);

					foreach (var portal in xref_portals)
					{
						context.DeleteCatalogItemFromPortal(portal.portalID, product_id);
						context.InsertCatalogItemXrefPortal(portal.portalID, product_id, portal.isVisible, portal.isFree, portal.isFreeOffer, portal.isFullFree);
					}

                    var deleted = context.DeleteProductXrefEntity(product_id);
                    //var classes = context.GetClassItems(classesIds).ToArray();
                    foreach (var p in attach_products_ids)
                    {
                        context.InsertProductXrefEntity(product_id, p);
                    }
                  

                    var currentSubscribe = context.GetEntityItems(product_id, EntityItemTypeEnum.SubscribePlanItem, true, true).SingleOrDefault();
                    InsertOrUpdateSubscribePlanTree(product_id, currentSubscribe, creator_id, subscribe_plan_months, monthly_subscribe_plan_id, free_units_on_subscribe, free_units_on_next_subscribe, createDate, context);

                    if (image != null)
                        InsertOtUpdateImage(product_id, title, creator_id, root_entity_id, image, context, createDate);

					context.DeleteTagXrefEntity(product_id, TagTypeEnum.Category);
					InsertCategories(categories, product_id, context);

                    return rval;
                });
        }

        public long InsertOrUpdateSubscribePlanTree(long root_entity_id, long current_subscribe_id, Guid creator_id, int subscribe_plan_months, long end_subscribe_plan_id,
            int free_units_on_subscribe, int? free_units_on_next_subscribe, DateTime createDate)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (CatalogDbManager context) =>
                {
                    var currentSubscribe = context.GetEntityItem(current_subscribe_id, true, true).SingleOrDefault();

                    return InsertOrUpdateSubscribePlanTree(root_entity_id, currentSubscribe, creator_id, subscribe_plan_months, end_subscribe_plan_id,
                        free_units_on_subscribe, free_units_on_next_subscribe, createDate, context);
                });
        }


        private static long InsertOrUpdateSubscribePlanTree(long root_entity_id, EntityItem current_subscribe, Guid creator_id, int subscribe_plan_months, long end_subscribe_plan_id,
            int free_units_on_subscribe, int? free_units_on_next_subscribe, DateTime createDate, CatalogDbManager context)
        {
            long parentSubscribeID = 0;

            if (subscribe_plan_months > 0)
            {
                var subscribeDesc = subscribe_plan_months + "," + free_units_on_subscribe + "," + free_units_on_next_subscribe;
                if (current_subscribe == null)
                {
                    current_subscribe = new EntityItem()
                    {
                        entityID = InsertSubscribePlan(root_entity_id, root_entity_id, creator_id, 0, free_units_on_subscribe, free_units_on_next_subscribe, end_subscribe_plan_id, createDate, "[Prepaid subscribe] Subscribe at " + subscribe_plan_months + " months", subscribeDesc, context),
                        rootEntityID = root_entity_id,
                        parentEntityID = root_entity_id
                    };
                }
                else context.UpdateSubscribePlan(current_subscribe.entityID, free_units_on_subscribe, subscribeDesc);

                parentSubscribeID = current_subscribe.rootEntityID == current_subscribe.parentEntityID ? current_subscribe.entityID : current_subscribe.parentEntityID;
                context.DeleteEntityItem(parentSubscribeID, (int)EntityItemTypeEnum.SubscribePlanItem, subscribe_plan_months);
                context.UpdateChildSubscribePlan(parentSubscribeID, subscribeDesc);

                var childSubscribe = context.GetChildCatalogItems(new[] { parentSubscribeID }, new[] { (int)EntityItemTypeEnum.SubscribePlanItem }, true, true).ToArray();

                var prevSubscribeID = childSubscribe.OrderByDescending(e => e.sortOrder).Select(e => e.entityID).FirstOrDefault();
                if (prevSubscribeID == 0) prevSubscribeID = parentSubscribeID;
                context.UpdateSubscribePlanXref(prevSubscribeID, end_subscribe_plan_id);

                for (int i = childSubscribe.Length + 1; i < subscribe_plan_months; i++)
                {
                    var childSubscribePlanID = InsertSubscribePlan(root_entity_id, parentSubscribeID, creator_id, i, 0, free_units_on_next_subscribe, end_subscribe_plan_id, createDate, "[Prepaid subscribe] Subscribe at " + subscribe_plan_months + " months. Current month is " + (i + 1), null, context);
                    context.UpdateSubscribePlanXref(prevSubscribeID, childSubscribePlanID);
                    prevSubscribeID = childSubscribePlanID;
                }

                foreach (var child in childSubscribe)
                    context.UpdateSubscribePlanXref(child.entityID, free_units_on_next_subscribe);
            }
            else if (current_subscribe != null)
            {
                var subscribeID = current_subscribe.rootEntityID == current_subscribe.parentEntityID ? current_subscribe.entityID : current_subscribe.parentEntityID;

                context.DeleteEntityItem(subscribeID, (int)EntityItemTypeEnum.SubscribePlanItem, subscribe_plan_months);
                context.DeleteEntityItem(subscribeID);
            }

            return parentSubscribeID;
        }

        private static long InsertSubscribePlan(long root_entity_id, long parent_enity_id, Guid creator_id, int sort_order, int free_units_on_subscribe, int? free_units_on_next_subscribe, long end_subscribe_plan_id, DateTime createDate, string desc, string susbscribe_desc, CatalogDbManager context)
        {
            var subscribePlanID = EntityItemService.InsertEnityItem(desc, sort_order, true, EntityItemTypeEnum.SubscribePlanItem, creator_id, root_entity_id, parent_enity_id, createDate, context);
            context.InsertProduct(subscribePlanID, (short)ProductTypeEnum.Subscribe, 0, null, desc, true);
            context.InsertSubscribePlan(subscribePlanID, 0, 1, 0, free_units_on_subscribe, susbscribe_desc);
            context.InsertSubscribePlanXref(subscribePlanID, end_subscribe_plan_id, free_units_on_next_subscribe);

            return subscribePlanID;
        }

        //private static void InsertSubscribePlan(long product_id, Guid creator_id, long root_entity_id, short subscribe_plan_months, long monthly_subscribe_plan_id, CatalogDbManager context, DateTime createDate)
        //{
        //    var currentSubscribe = context.GetChildCatalogItems(new[] { product_id }, new[] { (int)EntityItemTypeEnum.SubscribePlanItem }, false, true).SingleOrDefault();
        //    var durationInMonths = currentSubscribe != null ? currentSubscribe.SubscribePlanEntity.durationInMonths : (short)0;
        //    if (durationInMonths != subscribe_plan_months)
        //    {
        //        var deleted = context.DeleteEntityItem(product_id, (short)EntityItemTypeEnum.SubscribePlanItem);
        //        if (subscribe_plan_months > 0)
        //        {
        //            string desc = "[Package subscribe] Subscribe at " + subscribe_plan_months + " months";
        //            var subscribePlanID = EntityItemService.InsertEnityItem(desc, 0, true, EntityItemTypeEnum.SubscribePlanItem, creator_id, root_entity_id, product_id, createDate, context);
        //            context.InsertProduct(subscribePlanID, (short)ProductTypeEnum.Subscribe, 0, null, desc);
        //            context.InsertSubscribePlan(subscribePlanID, 0, subscribe_plan_months, 2, 0, null);
        //            context.InsertSubscribePlanXref(subscribePlanID, monthly_subscribe_plan_id, 0);
        //        }
        //    }

        //}

		private static void UpdateProduct(CatalogDbManager context, long product_id, decimal? price1, decimal? price2, int shipping_location_id, FileEntity produc_file)
		{
			context.UpdateProduct(product_id, price1, price2);
			context.DeleteProductShippingLocation(product_id);
			if (produc_file != null)
				if (context.UpdateFile(product_id, produc_file.filePath, produc_file.alternateFilePath) == 0)
					context.InsertFile(product_id, (FileTypeIDEnum)produc_file.fileTypeID, produc_file.filePath, produc_file.alternateFilePath);
			if (shipping_location_id != 0)
				context.InsertProductShippingLocation(product_id, shipping_location_id);
		}

		private static void InserProduct(long class_id, string title, Guid creator_id, long root_entity_id, CatalogDbManager context, DateTime createDate, ProductEntity product, FileEntity produc_file)
		{
			var productName = "[" + (ProductTypeEnum)product.productTypeID + "] " + title;
			var productID = EntityItemService.InsertEnityItem(productName, 0, true, EntityItemTypeEnum.ProductItem,
				creator_id, root_entity_id, class_id, createDate, context);
			context.InsertProduct(productID, product.productTypeID, product.price1, product.price2, string.Empty, product.unlimitedAccessInLibrary);
			if (produc_file != null)
				context.InsertFile(productID, (FileTypeIDEnum)produc_file.fileTypeID, produc_file.filePath, produc_file.alternateFilePath);
			if (product.ShippingLocationID != 0)
				context.InsertProductShippingLocation(productID, product.ShippingLocationID);
		}


        private static void InsertOtUpdateImage(long parent_id, string title, Guid creator_id, long root_entity_id, ImageInfo image, CatalogDbManager context, DateTime createDate)
        {
            var oldFile = context.GetClassImage(parent_id).SingleOrDefault();
            if (oldFile != null)
            {
                var filePath = image.GetImageUrl(oldFile.fileID);
                context.UpdateFile(oldFile.fileID, filePath, null);

                image.Save(filePath);
            }
            else
            {
                var fileName = "[" + EntityItemTypeEnum.FileItem.ToString() + "/" + (FileTypeIDEnum.SmallPoster).ToString() + "] " + title;
                var fileID = EntityItemService.InsertEnityItem(fileName, 0, true, EntityItemTypeEnum.FileItem,
                    creator_id, root_entity_id, parent_id, createDate, context);
                string filePath = image.GetImageUrl(fileID);

                context.InsertFile(fileID, FileTypeIDEnum.SmallPoster, filePath, null);

                image.Save(filePath);
            }

        }

        public int UpdateCatalogItems(EntityItem[] items, long current_portal_id, bool isSuperUser)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (CatalogDbManager context) =>
                {
                    foreach (EntityItem item in items)
                    {
                        if (isSuperUser)
                        {
                            context.UpdateEntityActivity(item.entityID, item.active);
                            context.UpdateCatalogItemXrefPortal((current_portal_id == 0 ? 2 : current_portal_id), item.entityID, item.CurrentPortal.isVisible,
                                item.CurrentPortal.isFree, item.CurrentPortal.isFreeOffer, null);
                        }
                        else
                            context.UpdateCatalogItemXrefPortal(current_portal_id, item.entityID, item.CurrentPortal.isVisible,
                                item.CurrentPortal.isFree, item.CurrentPortal.isFreeOffer, null);

                        if(item.CatalogItemExtend!=null)
                            context.UpdateCatalogItemExtendNotes(item.entityID, item.CatalogItemExtend.notes);
                    }

                    return 1;
                });
        }

        public int UpdatePortalItems(EntityItem[] items, long current_portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (CatalogDbManager context) =>
                {
                    foreach (EntityItem item in items)
                    {
                        context.UpdateEntityActivity(item.entityID, item.active);

                        if (item.CurrentPortal.portalID == 0)
                            context.DeleteCatalogItemFromPortal(current_portal_id, item.entityID);
                        else
                            context.InsertOrUpdateCatalogItemXrefPortal(current_portal_id, item.entityID, item.CurrentPortal.isVisible,
                                item.CurrentPortal.isFree, item.CurrentPortal.isFreeOffer, item.CurrentPortal.isFullFree);

                        context.UpdateCatalogItemExtendNotes(item.entityID, item.CatalogItemExtend.notes);
                    }

                    //throw new Exception("makar exception");

                    return 1;
                });
        }

        public int UpdateTag(int tag_id, string tag_name) 
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (CatalogDbManager context) =>
                {
                    return context.UpdateTag(tag_id, tag_name);
                });
        }

        #endregion

        #region Delete

        public int DeleteClass(long class_id) 
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (CatalogDbManager context) =>
               {
                   //context.DeleteCatalogItemFromPortals(class_id);

                   //context.DeleteTagXrefEntity(class_id);

                   var classProducts = context.GetClassProduct(class_id).ToArray();
                   foreach (var product in classProducts)
                       context.DeleteEntityItem(product.entityID);

                   var image = context.GetFileForCatalogItem(class_id).SingleOrDefault();
                   if (image != null)
                   {
                       //context.CompletelyDeleteFileEntityItem(image.entityID);
					   context.DeleteEntityItem(image.entityID);

					   //if (!string.IsNullOrEmpty(image.FileEntity.filePath))
					   //    System.IO.File.Delete(image.FileEntity.filePath);
                   }
                   //throw new Exception("makar exception");
                   return context.DeleteEntityItem(class_id);
               });
        }

        public int DeletePackage(long product_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (CatalogDbManager context) =>
               {
                   //context.DeleteCatalogItemFromPortals(product_id);

				   //context.DeleteTagXrefEntity(product_id);

                   //context.DeleteClassesFromPackage(product_id);

                   var image = context.GetFileForCatalogItem(product_id).SingleOrDefault();
                   if (image != null)
                   {
                       //context.CompletelyDeleteFileEntityItem(image.entityID);
					   context.DeleteEntityItem(image.entityID);

					   //if (!string.IsNullOrEmpty(image.FileEntity.filePath))
					   //    System.IO.File.Delete(image.FileEntity.filePath);
                   }
                   
                   return context.DeleteEntityItem(product_id);
               });
        }

        public int DeleteTag(int tag_id) 
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (CatalogDbManager context) =>
               {
                   return context.DeleteTag(tag_id);
               });
        }

		public int DeleteTagXrefEntity(long from_entity_id, TagTypeEnum tag_type)
		{
			return this.Exec(System.Data.IsolationLevel.Snapshot,
			   (CatalogDbManager context) =>
			   {
				   return context.DeleteTagXrefEntity(from_entity_id, tag_type);
			   });
		}


        #endregion


        private static void PrepareCatalogItem(bool with_only_active, bool with_only_nondeleted, CatalogDbManager context, long[] ids, EntityItem[] items)
        {
            var tags = context.GetTags(ids, new short[] { (short)TagTypeEnum.Category, (short)TagTypeEnum.ClassLevel }).ToArray();
            var enitities = context.GetChildCatalogItems(ids,
                new int[] { (int)EntityItemTypeEnum.FileItem, (int)EntityItemTypeEnum.ProductItem, (int)EntityItemTypeEnum.SubscribePlanItem },
                with_only_active, with_only_nondeleted).ToArray();
            var xrefPortals = context.GetCatalogItemXrefPortals(ids).ToArray();

            foreach (var catalogEntity in items)
            {
                catalogEntity.TagXrefEntities.AddRange(tags.Where(t => t.entityID == catalogEntity.entityID).OrderBy(t=>t.Tag.name));
                catalogEntity.ChildEnities = enitities.Where(e => e.parentEntityID == catalogEntity.entityID).ToArray();
                catalogEntity.CatalogItemXrefPortals.AddRange(xrefPortals.Where(p => p.catalogItemID == catalogEntity.entityID));
            }
        }
    }
}
