using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPartnerKinoBL.Extension;
using MainEntity.Interfaces;
using MainEntity;
using MainEntity.Models.Class;
using MainCommon;
using MainCommon.Extension;
using System.Linq.Expressions;

namespace MainBL
{
    public partial class ClassService : BaseBO, IClassService
    {
        public ClassService(string connection_name)
            : base(connection_name)
        {
        }

        #region Select

        public int SearchClassesCnt(long from_root_id, long portal_id, string category_name, string[] search_words, string filter_code, string word_in_title, long[] class_ids, int? include_product_type_id = null)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (ClassDbManager context) =>
                {
                    int categoryID = 0;
                    if (!string.IsNullOrEmpty(category_name))
                        categoryID = context.GetTag(category_name, TagTypeEnum.Category).Select(t => t.tagID).FirstOrDefault();

                    var rval = context.GetClassCnt(from_root_id, portal_id, true, true);
                    if (include_product_type_id != null)
                        rval = rval.Where(c =>
                            (from e2 in context.EntityItems
                             join p2 in context.ProductEntities on e2.entityID equals p2.productID
                             where e2.parentEntityID == c.classID && include_product_type_id == p2.productTypeID
                             select e2).Any());


                    var pr = GetSearchClassPredicate(context, categoryID, search_words, filter_code, word_in_title, class_ids);
                    rval = rval.Where(pr);
                    var cnt = rval.Count();

                    return cnt;
                });
        }

        public MainEntity.Models.Class.ClassEntity[] SearchClasses(long from_root_id, long portal_id,
            string category_name, int start_row_index, int max_rows_count, string[] search_words, string filter_code, string word_in_title, long[] class_ids, int? include_product_type_id = null, SortClassesEnum sort = (SortClassesEnum) (-1))
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ClassDbManager context) =>
               {
                   int categoryID = 0;
                   if (!string.IsNullOrEmpty(category_name))
                       categoryID = context.GetTag(category_name, TagTypeEnum.Category).Select(t => t.tagID).FirstOrDefault();
                   var rval = context.GetClasses(from_root_id, portal_id, true, true);
                   if (include_product_type_id != null)
                       rval = rval.Where(c =>
                           (from e2 in context.EntityItems
                            join p2 in context.ProductEntities on e2.entityID equals p2.productID
                            where e2.parentEntityID == c.classID && include_product_type_id == p2.productTypeID
                            select e2).Any());
                   if (sort != (SortClassesEnum)(-1))
                   {
                       switch (sort)
                       {
                           case SortClassesEnum.New:
                               {
                                   rval = rval.Where(c => c.newOrder != null).OrderByDescending(s => s.EntityItem.createDate);
                                   break;
                               }
                           case SortClassesEnum.Top:
                               {
                                   rval = rval.OrderByDescending(s => s.statDownloadCnt);
                                   break;
                               }
                           case SortClassesEnum.Code:
                               {
                                   rval = rval.OrderBy(s => s.Code);
                                   break;
                               }
                       }
                   }

                   var pr = GetSearchClassPredicate(context, categoryID, search_words, filter_code, word_in_title, class_ids);

                   rval = rval.Where(pr).Skip(start_row_index).Take(max_rows_count);
                   string q = rval.ToString();
                   var classes = rval.ToArray();

                   LoadClassData(context, classes);

                   return classes;
               });
        }

        public int GetSortedClassesCnt(long from_root_id, long portal_id, SortClassesEnum sort, int? include_product_type_id = null)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ClassDbManager context) =>
               {
                   var rval = context.GetClassCnt(from_root_id, portal_id, true, true);

                   if (include_product_type_id != null)
                       rval = rval.Where(c =>
                           (from e2 in context.EntityItems
                            join p2 in context.ProductEntities on e2.entityID equals p2.productID
                            where e2.parentEntityID == c.classID && include_product_type_id == p2.productTypeID
                            select e2).Any());


                   //switch (sort)
                   //{
                   //    case SortClassesEnum.New:
                   //        {
                   //            rval = rval.Where(c => c.newOrder != null);
                   //            break;
                   //        }
                   //    default:
                   //        {
                   //            throw new NotSupportedException();
                   //        }
                   //}

                   return rval.Count();
               });
        }

        public MainEntity.Models.Class.ClassEntity[] GetSortedClasses(long from_root_id, long portal_id, int start_row_index,
            int max_rows_count, SortClassesEnum sort, int? include_product_type_id = null)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ClassDbManager context) =>
              {
                  var rval = context.GetClasses(from_root_id, portal_id, true, true);

                  if (include_product_type_id != null)
                      rval = rval.Where(c =>
                          (from e2 in context.EntityItems
                           join p2 in context.ProductEntities on e2.entityID equals p2.productID
                           where e2.parentEntityID == c.classID && include_product_type_id == p2.productTypeID
                           select e2).Any());

                  switch (sort)
                  {
                      case SortClassesEnum.New:
                          {
                              rval = rval.Where(c => c.newOrder != null).OrderByDescending(s => s.EntityItem.createDate);
                              break;
                          }

                      case SortClassesEnum.Top:
                          {
                              rval = rval.OrderByDescending(s => s.statDownloadCnt);
                              break;
                          }
                      case SortClassesEnum.Code:
                          {
                              rval = rval.OrderBy(s => s.Code);
                              break;
                          }
                      default:
                          {
                              throw new NotSupportedException();
                          }
                  }
                  var classes = rval.Skip(start_row_index).Take(max_rows_count).ToArray();

                  LoadClassData(context, classes);

                  return classes;
              });
        }


        public MainEntity.Models.Class.ClassEntity[] GetFilteredClasses(long from_root_id, long portal_id, int start_row_index, int max_rows_count,
            string filter_title,
            string filter_category,
            string filter_category_equals,
            string filter_code,
            string filter_speaker,
            int filter_level,
            int[] product_type_ids, int? include_product_type_id = null)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ClassDbManager context) =>
               {
                   var rval = context.GetClasses(from_root_id, portal_id, true, true);

                   #region filtering

                   if (!string.IsNullOrEmpty(filter_title))
                       rval = rval.Where(c => c.Title.Contains(filter_title));

                   if (filter_level != 0)
                       rval = rval.Where(c =>
                           (from t2 in context.TagXrefEntities
                            where t2.entityID == c.classID && t2.tagID == filter_level
                            select t2).Any());

                   if (product_type_ids.Length > 0)
                       rval = rval.Where(c =>
                           (from e2 in context.EntityItems
                            join p2 in context.ProductEntities on e2.entityID equals p2.productID
                            where e2.parentEntityID == c.classID && product_type_ids.Contains(p2.productTypeID)
                            select e2).Any());

                   if (include_product_type_id != null)
                       rval = rval.Where(c =>
                           (from e2 in context.EntityItems
                            join p2 in context.ProductEntities on e2.entityID equals p2.productID
                            where e2.parentEntityID == c.classID && include_product_type_id == p2.productTypeID
                            select e2).Any());

                   if (!string.IsNullOrEmpty(filter_category))
                   {
                       rval = rval.Where(c =>
                           (from tx in context.TagXrefEntities
                            join t in context.Tags on tx.tagID equals t.tagID
                            where t.tagTypeID == (short)TagTypeEnum.Category && tx.entityID == c.classID && t.name.Contains(filter_category)
                            select tx).Any());
                   }

                   if (!string.IsNullOrEmpty(filter_category_equals))
                   {
                       rval = rval.Where(c =>
                           (from tx in context.TagXrefEntities
                            join t in context.Tags on tx.tagID equals t.tagID
                            where t.tagTypeID == (short)TagTypeEnum.Category && tx.entityID == c.classID && t.name == filter_category_equals
                            select tx).Any());
                   }

                   if (!string.IsNullOrEmpty(filter_code))
                   {
                       rval = rval.Where(c => c.Code.Contains(filter_code));
                   }

                   if (!string.IsNullOrEmpty(filter_speaker))
                       rval = rval.Where(c => c.SpeakerName.Contains(filter_speaker));

                   #endregion

                   var classes = rval.Skip(start_row_index).Take(max_rows_count).ToArray();

                   LoadClassData(context, classes);

                   return classes;
               });
        }

        public int GetFilteredClassesCnt(long from_root_id, long portal_id,
            string filter_title,
            string filter_category,
            string filter_category_equals,
            string filter_code,
            string filter_speaker,
            int filter_level,
            int[] product_type_ids, int? include_product_type_id = null)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ClassDbManager context) =>
               {
                   var rval = context.GetClassCnt(from_root_id, portal_id, true, true);

                   #region filtering

                   if (!string.IsNullOrEmpty(filter_title))
                       rval = rval.Where(c => c.Title.Contains(filter_title));

                   if (filter_level != 0)
                       rval = rval.Where(c =>
                           (from t2 in context.TagXrefEntities
                            where t2.entityID == c.classID && t2.tagID == filter_level
                            select t2).Any());

                   if (product_type_ids.Length > 0)
                       rval = rval.Where(c =>
                           (from e2 in context.EntityItems
                            join p2 in context.ProductEntities on e2.entityID equals p2.productID
                            where e2.parentEntityID == c.classID && product_type_ids.Contains(p2.productTypeID)
                            select e2).Any());

                   if (include_product_type_id != null)
                       rval = rval.Where(c =>
                           (from e2 in context.EntityItems
                            join p2 in context.ProductEntities on e2.entityID equals p2.productID
                            where e2.parentEntityID == c.classID && include_product_type_id == p2.productTypeID
                            select e2).Any());

                   if (!string.IsNullOrEmpty(filter_category))
                   {
                       rval = rval.Where(c =>
                           (from tx in context.TagXrefEntities
                            join t in context.Tags on tx.tagID equals t.tagID
                            where t.tagTypeID == (short)TagTypeEnum.Category && tx.entityID == c.classID && t.name.Contains(filter_category)
                            select tx).Any());
                   }

                   if (!string.IsNullOrEmpty(filter_category_equals))
                   {
                       rval = rval.Where(c =>
                           (from tx in context.TagXrefEntities
                            join t in context.Tags on tx.tagID equals t.tagID
                            where t.tagTypeID == (short)TagTypeEnum.Category && tx.entityID == c.classID && t.name == filter_category_equals
                            select tx).Any());
                   }

                   if (!string.IsNullOrEmpty(filter_code))
                   {
                       rval = rval.Where(c => c.Code.Contains(filter_code));
                   }

                   if (!string.IsNullOrEmpty(filter_speaker))
                   {
                       rval = rval.Where(c =>
                           (from e in context.EntityItems
                            where e.entityID == c.speakerID && e.title.Contains(filter_speaker)
                            select e).Any());
                   }

                   #endregion

                   return rval.Count();
               });
        }


        public int SearchClassesCnt(long from_root_id, long portal_id, string speaker_name, int? include_product_type_id = null)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (ClassDbManager context) =>
                {
                    var speakerID = context.GetSpeaker(from_root_id, speaker_name, true, true).Select(s => s.entityID).FirstOrDefault();
                    var rval = context.GetClassCnt(from_root_id, portal_id, true, true);
                    rval = rval.Where(e => e.speakerID == speakerID);
                    if (include_product_type_id != null)
                        rval = rval.Where(c =>
                            (from e2 in context.EntityItems
                             join p2 in context.ProductEntities on e2.entityID equals p2.productID
                             where e2.parentEntityID == c.classID && include_product_type_id == p2.productTypeID
                             select e2).Any());

                    var cnt = rval.Count();

                    return cnt;
                });
        }

        public MainEntity.Models.Class.ClassEntity[] SearchClasses(long from_root_id, long portal_id,
            string speaker_name, int start_row_index, int max_rows_count, int? include_product_type_id = null, SortClassesEnum sort = (SortClassesEnum) (-1))
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ClassDbManager context) =>
               {
                   var speakerID = context.GetSpeaker(from_root_id, speaker_name, true, true).Select(s => s.entityID).FirstOrDefault();
                   var data = context.GetClasses(from_root_id, portal_id, true, true);
                   data = data.Where(c => c.speakerID == speakerID);
                   if (include_product_type_id != null)
                       data = data.Where(c =>
                           (from e2 in context.EntityItems
                            join p2 in context.ProductEntities on e2.entityID equals p2.productID
                            where e2.parentEntityID == c.classID && include_product_type_id == p2.productTypeID
                            select e2).Any());
                   if (sort != (SortClassesEnum)(-1))
                   {
                       switch (sort)
                       {
                           case SortClassesEnum.New:
                               {
                                   data = data.Where(c => c.newOrder != null).OrderByDescending(s => s.EntityItem.createDate);
                                   break;
                               }
                           case SortClassesEnum.Top:
                               {
                                   data = data.OrderByDescending(s => s.statDownloadCnt);
                                   break;
                               }
                           case SortClassesEnum.Code:
                               {
                                   data = data.OrderBy(s => s.Code);
                                   break;
                               }
                       }
                   }
                   var rval = data.Skip(start_row_index).Take(max_rows_count).ToArray();

                   LoadClassData(context, rval);

                   return rval;
               });
        }

        public int GetClassesCnt(long from_root_id, long portal_id, int? include_product_type_id = null)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ClassDbManager context) =>
               {
                   var data = context.GetClassCnt(from_root_id, portal_id, true, true);

                   if (include_product_type_id != null)
                       data = data.Where(c =>
                           (from e2 in context.EntityItems
                            join p2 in context.ProductEntities on e2.entityID equals p2.productID
                            where e2.parentEntityID == c.classID && include_product_type_id == p2.productTypeID
                            select e2).Any());


                   return data.Count();
               });
        }

        public MainEntity.Models.Class.ClassEntity[] GetClasses(long from_root_id, long portal_id, int start_row_index, int max_rows_count, int? include_product_type_id = null)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ClassDbManager context) =>
               {
                   var data = context.GetClasses(from_root_id, portal_id, true, true);
                   if (include_product_type_id != null)
                       data = data.Where(c =>
                           (from e2 in context.EntityItems
                            join p2 in context.ProductEntities on e2.entityID equals p2.productID
                            where e2.parentEntityID == c.classID && include_product_type_id == p2.productTypeID
                            select e2).Any());

                   var rval = data.Skip(start_row_index).Take(max_rows_count).ToArray();

                   LoadClassData(context, rval);

                   return rval;
               });
        }

        public MainEntity.Models.Class.ClassEntity[] GetFreeClasses(long from_root_id, long portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ClassDbManager context) =>
               {
                   var data = context.GetClasses(from_root_id, portal_id, true, true);
                   var rval = data.Where(d => d.IsFree).ToArray();

                   LoadClassData(context, rval);

                   return rval;
               });
        }

        public MainEntity.Models.Class.ClassEntity GetClass(long from_root_id, long portal_id, long class_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (ClassDbManager context) =>
               {
                   var rval = context.GetClass(from_root_id, portal_id, class_id, true, true);
                   var classes = rval.ToArray();

                   LoadClassData(context, classes);

                   return classes.SingleOrDefault();
               });
        }


        public MainEntity.Models.Class.ProductEntity[] GetProductsList(long[] ids)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ClassDbManager context) =>
              {
                  var r = context.GetProductsList(ids).ToArray();
                  return r;
              });
        }

        public MainEntity.Models.Class.ClassEntity[] GetFreeOfferClasses(long from_root_id, long portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (ClassDbManager context) =>
                {
                    var rval = context.GetFreeOfferClasses(from_root_id, from_root_id, portal_id).ToArray();
                    LoadClassData(context, rval);
                    return rval;
                });
        }

        public int GetFreeOfferClassesCnt(long[] entity_ids, long from_root_id, long portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ClassDbManager context) =>
              {
                  return context.GetFreeOfferClassesCnt(entity_ids, from_root_id, from_root_id, portal_id);
              });
        }


        public int GetFullFreeClassesCnt(long from_root_id, long portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ClassDbManager context) =>
              {
                  return context.GetFullFreeClassesCnt(from_root_id, portal_id);
              });
        }

        public MainEntity.Models.Class.ClassEntity[] GetFullFreeClasses(long from_root_id, long portal_id, int start_row_index, int max_rows_count)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (ClassDbManager context) =>
                {
                    var rval = context.GetFullFreeClasses(from_root_id, portal_id).Skip(start_row_index).Take(max_rows_count).ToArray();
                    LoadClassData(context, rval);
                    return rval;
                });
        }



        public EntityItem[] GetProductsForClasses(long[] classes_ids, int[] product_types_ids)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
              (ClassDbManager context) =>
              {
                  return context.GetProductsForClasses(classes_ids, product_types_ids).ToArray();
              });
        }

        public EntityItem[] GetFilesForClasses(long[] classes_ids)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
             (ClassDbManager context) =>
             {
                 return context.GetFilesForClasses(classes_ids);
             });
        }

        #endregion


        #region Update

        public int IncreaseStatDownloadCnt(long file_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (ClassDbManager context) => context.IncreaseStatCnt(context.SelectClass(file_id).Select(c => c.classID).Single(), 1, 0));
        }

        public int IncreaseStatListenCnt(long file_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (ClassDbManager context) => context.IncreaseStatCnt(context.SelectClass(file_id).Select(c => c.classID).Single(), 0, 1));
        }

        #endregion


        private static void LoadClassData(ClassDbManager context, ClassEntity[] classes)
        {
            var classIDs = classes.Select(c => c.classID).ToArray();
            var products = context.GetProductsForClasses(classIDs, new int[] { (int)ProductTypeEnum.Tape, (int)ProductTypeEnum.Disk, (int)ProductTypeEnum.File, (int)ProductTypeEnum.Package }).ToArray();
            var tags = context.GetTagsForClasses(classIDs, new short[] { (short)TagTypeEnum.ClassLevel }).ToArray();

            foreach (var c in classes)
            {
                
                c.Type = products.Where(p => p.parentEntityID == c.classID && p.ProductEntity.productTypeID == (int)ProductTypeEnum.Tape).FirstOrDefault();
                c.Disc = products.Where(p => p.parentEntityID == c.classID && p.ProductEntity.productTypeID == (int)ProductTypeEnum.Disk).FirstOrDefault();
                c.File = products.Where(p => p.parentEntityID == c.classID && (p.ProductEntity.productTypeID == (int)ProductTypeEnum.File) || (p.ProductEntity.productTypeID == (int)ProductTypeEnum.Package)).FirstOrDefault();
                c.ClassLevel = tags.Where(t => t.entityID == c.classID && t.Tag.tagTypeID == (short)TagTypeEnum.ClassLevel).FirstOrDefault();
            }

            Dictionary<long, string> smallPosters = context.GetSmallPostersForClasses(classes.Select(c => c.classID).ToArray());

            foreach (var c in classes)
                if (smallPosters.ContainsKey(c.classID))
                    c.SmallPosterUrl = smallPosters[c.classID];
        }

        private static Expression<Func<ClassEntity, bool>> GetSearchClassPredicate(ClassDbManager context, int category_id, string[] search_words, string search_code, string word_in_title, long[] class_ids)
        {
            var pr = PredicateBuilder.False<ClassEntity>();
            var e = "[^a-zA-z']";

            foreach (var word in search_words)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    string temp = word;
                    pr = pr.Or(c =>
                        c.Title.StartsWith(temp + e) || c.description.StartsWith(temp + e) ||
                        c.Title.Contains(e + temp + e) || c.description.Contains(e + temp + e) ||
                        c.Title.EndsWith(e + temp) || c.description.EndsWith(e + temp) || c.CatalogItemExtend.code.Contains(temp));
                }
            }

            if (!string.IsNullOrEmpty(word_in_title))
                pr = pr.Or(c => c.Title.StartsWith(word_in_title + e) || c.Title.Contains(e + word_in_title + e) || c.Title.EndsWith(e + word_in_title) || false);

            if (!string.IsNullOrEmpty(search_code))
                pr = pr.Or(c => c.CatalogItemExtend.code.Contains(search_code) || false);

            if (class_ids != null && class_ids.Length > 0)
                pr = pr.Or(c => class_ids.Contains(c.classID) || false);

            if (category_id > 0)
                pr = pr.Or(c => context.TagXrefEntities.Where(t => t.entityID == c.classID && t.tagID == category_id).Count() != 0);
            return pr;
        }

    }
}