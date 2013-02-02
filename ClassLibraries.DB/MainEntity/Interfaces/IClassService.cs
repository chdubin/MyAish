using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Class;
using MainCommon;

namespace MainEntity.Interfaces
{
    public interface IClassService
    {
        int IncreaseStatDownloadCnt(long file_id);

        int IncreaseStatListenCnt(long file_id);

        int SearchClassesCnt(long from_root_id, long portal_id, string category_name, string[] search_words, string filter_code, string word_in_title, long[] class_ids, int? include_product_type_id = null);

        int SearchClassesCnt(long from_root_id, long portal_id, string speaker_name, int? include_product_type_id = null);

        int GetClassesCnt(long from_root_id, long portal_id, int? include_product_type_id = null);

        ClassEntity[] SearchClasses(long from_root_id, long portal_id, string category_name, int start_row_index, int max_rows_count, string[] search_words, string filter_code, string word_in_title, long[] class_ids, int? include_product_type_id = null, SortClassesEnum sort = (SortClassesEnum)(-1));

        ClassEntity[] SearchClasses(long from_root_id, long portal_id, string speaker_name, int start_row_index, int max_rows_count, int? include_product_type_id = null, SortClassesEnum sort = (SortClassesEnum)(-1));

        ClassEntity[] GetClasses(long from_root_id, long portal_id, int start_row_index, int max_rows_count, int? include_product_type_id = null);

        ClassEntity[] GetFreeClasses(long from_root_id, long portal_id);

        ClassEntity GetClass(long from_root_id, long portal_id, long class_id);

        ProductEntity[] GetProductsList(long[] ids);

        ClassEntity[] GetFreeOfferClasses(long from_root_id, long portal_id);

        int GetFreeOfferClassesCnt(long[] entity_ids, long from_root_id, long portal_id);

        EntityItem[] GetProductsForClasses(long[] classes_ids, int[] product_types_ids);

        EntityItem[] GetFilesForClasses(long[] classes_ids);

        int GetFilteredClassesCnt(long from_root_id, long portal_id,
         string filter_title,
         string filter_category,
         string filter_category_equals,
         string filter_code,
         string filter_speaker,
         int filter_level,
         int[] product_type_ids, int? include_product_type_id = null);

        MainEntity.Models.Class.ClassEntity[] GetFilteredClasses(long from_root_id, long portal_id, int start_row_index, int max_rows_count,
            string filter_title,
            string filter_category,
            string filter_category_equals,
            string filter_code,
            string filter_speaker,
            int filter_level,
            int[] product_type_ids, int? include_product_type_id = null);


        int GetSortedClassesCnt(long from_root_id, long portal_id, SortClassesEnum sort, int? include_product_type_id = null);

        MainEntity.Models.Class.ClassEntity[] GetSortedClasses(long from_root_id, long portal_id, int start_row_index, int max_rows_count, SortClassesEnum sort, int? include_product_type_id = null);


        int GetFullFreeClassesCnt(long from_root_id, long portal_id);

        MainEntity.Models.Class.ClassEntity[] GetFullFreeClasses(long from_root_id, long portal_id, int start_row_index, int max_rows_count);


    }
}
