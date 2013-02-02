using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Catalog;
using MainCommon;
using System.IO;
using System.Web;
using MainCommon.Models;

namespace MainEntity.Interfaces
{
    public interface ICatalogService
    {
        int GetCatalogItemsCnt(long from_root_id, bool with_only_active, bool with_only_nondeleted,
            long filter_portalid, int[] filter_categories, string filter_title, string filter_speaker, string filter_code, bool filter_new);

        EntityItem[] GetCatalogItems(long from_root_id, bool with_only_active, bool with_only_nondeleted, int start_row_index, int max_rows_count, SortParametersEnum sort, bool descending,
            long current_portal_id, long filter_portalid, int[] filter_categories, string filter_title, string filter_speaker, string filter_code, bool filter_new);

//        int GetPortalItemsCnt(long from_root_id, bool with_only_active, bool with_only_nondeleted, long current_portal_id, int filter_category_id);

//        EntityItem[] GetPortalItems(long from_root_id, bool with_only_active, bool with_only_nondeleted, int start_row_index, int max_rows_count, SortParametersEnum sort, bool descending, long current_portal_id, int filter_category_id);

        EntityItem GetCatalogItem(long from_catalog_id, bool with_only_nondeleted);


        #region Package

        EntityItem GetPackage(long from_catalog_id);

        EntityItem[] GetPackages(long from_root_id, long portal_id);

        #endregion


        EntityItem[] GetEntities(long from_parent_id, EntityItemTypeEnum entity_type_id, bool with_only_active, bool with_only_nondeleted);

		EntityItem[] GetSpeakers(long from_parent_id);

		long InsertClass(string title, string description, Guid creator_id, bool? active, long root_entity_id, long speaker_id, int? new_order,
			int level_tag_id,
			CatalogItemXrefPortal[] xref_portals,
			ProductEntity[] products, ImageInfo image,
            string categories, TimeSpan duration, string code, string notes);

		int UpdateClass(long class_id, string title, string description, Guid creator_id, bool? active, long root_entity_id, long speaker_id, int? new_order,
			int level_tag_id,
			CatalogItemXrefPortal[] xref_portals,
			ProductEntity[] products,
            ImageInfo image,
            string categories, TimeSpan duration, string code, string notes);

		int UpdatePackage(long product_id, string title, decimal price, string description, Guid creator_id, bool? active, long root_entity_id,
			CatalogItemXrefPortal[] xref_portals, int shipping_location_id,
            long[] attach_products_ids, short subscribe_plan_months, long monthly_subscribe_plan_id, int free_units_on_subscribe, int? free_units_on_next_subscribe,
            ImageInfo image, string categories, string code, bool unlimited_access_in_library, long speakerId);

		int InsertPackage(string title, bool? active, Guid creator_id, long root_entity_id, decimal price, string description,
			CatalogItemXrefPortal[] xref_portals, int shipping_location_id,
            long[] attach_products_ids, short subscribe_plan_months, long monthly_subscribe_plan_id, int free_units_on_subscribe, int? free_units_on_next_subscribe,
            ImageInfo image,
			string categories, string code, bool unlimited_access_in_library, long speakerId);

        long InsertOrUpdateSubscribePlanTree(long root_entity_id, long current_subscribe_id, Guid creator_id, int subscribe_plan_months, long end_subscribe_plan_id,
            int free_units_on_subscribe, int? free_units_on_next_subscribe, DateTime createDate);
            
        EntityItem[] GetClassProducts(long class_id);

        //string[] GetCatalogItemCategories(long catalog_item_id);

        string GetClassImage(long class_id);


        //EntityItem[] GetPackageClasses(long product_id);
        EntityItem[] GetProductXrefEntities(long product_id);

        int DeleteClass(long class_id);

        int DeletePackage(long product_id);

        //ClassEntity[] GetClassItems(int from_start_index, int and_page_size, string title, string speaker);
        //int GetClassItemsCount(string title, string speaker);

        ClassEntity[] GetClassItems(int from_start_index, int and_page_size, ProductTypeEnum include_product_type, string title, string speaker);

        int GetClassItemsCount(ProductTypeEnum include_product_type, string title, string speaker);

        EntityItem[] GetProductEntityItems(long[] entity_ids, ProductTypeEnum include_product_type);


        int UpdateCatalogItems(EntityItem[] items, long current_portal_id, bool isSuperUser);

        int UpdatePortalItems(EntityItem[] items, long current_portal_id);


        //SubscribePlanEntity GetSubscribePlan(long from_package_id);


        Tag[] GetAllTags(TagTypeEnum tag_type);

        int InsertTag(string name, TagTypeEnum tag_type);

        int UpdateTag(int tag_id, string tag_name);

        int DeleteTag(int tag_id);

        vw_Category[] GetAllCategories();


        EntityItem[] GetSpeakerClasses(long from_root_id, long speaker_id, int start_row_index, int max_rows_count, bool with_only_active,
            bool with_only_nondeleted, long portal_id, int filter_category_id, bool is_super_user);

        int GetSpeakerClassesCnt(long from_root_id, long speaker_id, bool with_only_active, bool with_only_nondeleted, long portal_id, int filter_category_id, bool is_super_user);

		ShippingLocation[] GetAllShippingLocations();

        ClassEntity[] GetClassEntities(long[] from_child_entity_ids, int start_row_index, int max_rows_count);


        int GetClassEntitiesWithFilesCount(long from_root_id, long[] except_class_entities_ids,
            long filter_portalid, int[] filter_categories, string filter_title, string filter_speaker, string filter_code, bool filter_new);

        EntityItem[] GetClassEntitiesWithFiles(long from_root_id, long[] except_class_entities_ids, int start_row_index, int max_rows_count,
            long filter_portalid, int[] filter_categories, string filter_title, string filter_speaker, string filter_code, bool filter_new);


        int GetClassEntitiesCount(long[] from_child_entity_ids);

        ProductEntity[] GetProductFilesInClasses(long[] class_entities_ids);

        int InsertCategories(string categories, long entity_id);
    }
}
