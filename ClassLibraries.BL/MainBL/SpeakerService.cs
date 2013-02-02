using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPartnerKinoBL.Extension;
using MainEntity.Interfaces;
using MainEntity;
using MainCommon;
using MainEntity.Models.Speaker;
using System.Web;

namespace MainBL
{
    public class SpeakerService : BaseBO, ISpeakerService
    {
        public SpeakerService(string connection_name)
            : base(connection_name) { }

        #region Select


        /*public int GetSpeakerClassesCnt(long from_root_id, long speaker_id, bool? with_only_active, bool with_only_nondeleted, long portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (SpeakerDbManager context) =>
                {
                    var rval = context.GetSpeakerClassesCnt(from_root_id, from_root_id, speaker_id, with_only_active, 
                        with_only_nondeleted, portal_id);
                    return rval;                    
                });
        }


        public EntityItem[] GetSpeakerClasses(long from_root_id, long speaker_id, int start_row_index, int max_rows_count, bool? with_only_active,
            bool with_only_nondeleted, long portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (SpeakerDbManager context) =>
                {
                    return context.GetSpeakerClasses(from_root_id, from_root_id, speaker_id, with_only_active, 
                        with_only_nondeleted, portal_id).Skip(start_row_index).Take(max_rows_count).ToArray();
                });
        }
        */

        public string GetSpeakerPhoto(long speaker_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (SpeakerDbManager context) =>
                {
                    return context.GetSpeakerPhoto(speaker_id).SingleOrDefault();
                });
        }

        public EntityItem GetSpeaker(string name) 
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (SpeakerDbManager context) =>
               {
                   return context.GetSpeaker(name).SingleOrDefault();
               });
        }

        public EntityItem GetSpeaker(long speaker_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
               (SpeakerDbManager context) =>
               {
                   return context.GetSpeaker(speaker_id).SingleOrDefault();
               });
        }

        public int GetSpeakersCnt(long from_root_id, bool? with_only_active, bool with_only_nondeleted, long portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (SpeakerDbManager context) =>
                {
                    var rval = context.GetSpeakersCnt(from_root_id, from_root_id, with_only_active, with_only_nondeleted, portal_id);
                    return rval;
                });
        }

        public int GetSpeakersCnt1(long from_root_id, bool? with_only_active, bool with_only_nondeleted, long portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (SpeakerDbManager context) =>
                {
                    var rval = context.GetSpeakersCnt1(from_root_id, from_root_id, with_only_active, with_only_nondeleted, portal_id);
                    return rval;
                });
        }

        public MainEntity.Models.Speaker.EntityItem[] GetSpeakers(long from_root_id, bool? with_only_active, bool with_only_nondeleted, int start_row_index, int max_rows_count, SortParametersEnum sort, bool descending, long portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (SpeakerDbManager context) =>
                {
                    var rvalQuery = context.GetSpeakers(from_root_id, from_root_id, with_only_active, with_only_nondeleted, portal_id);

                    switch (sort) 
                    {
                        case SortParametersEnum.Title: rvalQuery = descending ? rvalQuery.OrderByDescending(e => e.title) : rvalQuery.OrderBy(e => e.title); break;
                        case SortParametersEnum.Date: rvalQuery = descending ? rvalQuery.OrderByDescending(e => e.createDate) : rvalQuery.OrderBy(e => e.createDate); break;
                        case SortParametersEnum.Active: rvalQuery = descending ? rvalQuery.OrderByDescending(e => e.active) : rvalQuery.OrderBy(e => e.active); break;

                        default: rvalQuery = descending ? rvalQuery.OrderByDescending(e => e.entityID) : rvalQuery.OrderBy(e => e.entityID); break;
                    }

                    var rval = rvalQuery.Skip(start_row_index).Take(max_rows_count).ToArray();
                    var ids = rval.Select(e => e.entityID).ToArray();

                    return rval;
                });
        }

        public MainEntity.Models.Speaker.EntityItem[] GetSpeakers1(long from_root_id, bool? with_only_active, bool with_only_nondeleted, int start_row_index, int max_rows_count, SortParametersEnum sort, bool descending, long portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (SpeakerDbManager context) =>
                {
                    var rvalQuery = context.GetSpeakers1(from_root_id, from_root_id, with_only_active, with_only_nondeleted, portal_id);

                    switch (sort)
                    {
                        case SortParametersEnum.Title: rvalQuery = descending ? rvalQuery.OrderByDescending(e => e.title) : rvalQuery.OrderBy(e => e.title); break;
                        case SortParametersEnum.Date: rvalQuery = descending ? rvalQuery.OrderByDescending(e => e.createDate) : rvalQuery.OrderBy(e => e.createDate); break;
                        case SortParametersEnum.Active: rvalQuery = descending ? rvalQuery.OrderByDescending(e => e.active) : rvalQuery.OrderBy(e => e.active); break;

                        default: rvalQuery = descending ? rvalQuery.OrderByDescending(e => e.entityID) : rvalQuery.OrderBy(e => e.entityID); break;
                    }

                    var rval = rvalQuery.Skip(start_row_index).Take(max_rows_count).ToArray();
                    var ids = rval.Select(e => e.entityID).ToArray();

                    return rval;
                });
        }

        #endregion

        #region Insert

        public long InsertSpeaker(Guid creator_id, long root_entity_id, string speaker_name, string speaker_description,
            string speaker_contact_info)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (SpeakerDbManager context) =>
               {
                   var entityID = EntityItemService.InsertEnityItem(speaker_name, 0, true, EntityItemTypeEnum.SpeakerItem,
                           creator_id, root_entity_id, root_entity_id, DateTime.Now.ToUniversalTime(), context);

                   context.InsetSpeaker(entityID, speaker_description, speaker_contact_info);

                   return entityID;
               });
        }

        public long InsertSpeaker(Guid creator_id, long root_entity_id, string name, HttpPostedFileBase photo, string photo_path, string description,
            string contact_info, bool active)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
               (SpeakerDbManager context) =>
               {
                   var entityID = EntityItemService.InsertEnityItem(name, 0, active, EntityItemTypeEnum.SpeakerItem,
                           creator_id, root_entity_id, root_entity_id, DateTime.Now.ToUniversalTime(), context);

                   context.InsetSpeaker(entityID, description, contact_info);


                   // insert picture
                   if (photo != null)
                   {
                       var fileName = "[" + EntityItemTypeEnum.FileItem.ToString() + "/" + (FileTypeIDEnum.SmallPoster).ToString() + "] " + name;
                       var photoEntityID = EntityItemService.InsertEnityItem(fileName, 0, active, EntityItemTypeEnum.FileItem,
                               creator_id, root_entity_id, entityID, DateTime.Now.ToUniversalTime(), context);

                       string filePath = MainCommon.MyUtils.ReplaceFileName(photo_path, photoEntityID.ToString());
                       photo.SaveAs(filePath);
                       EntityItemService.InsertFileEnity(photoEntityID, filePath, FileTypeIDEnum.SmallPoster, context);
                   }

                   return entityID;
               });
        }

        #endregion

        #region Update

        public void Update(Guid creator_id, long root_entity_id, long speaker_id, string name, HttpPostedFileBase photo, string photo_path, string contact_info, string descr, bool active)
        {
            this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (SpeakerDbManager context) =>
                {
                    context.UpdateEntityItem(speaker_id, name, active);
                    context.UpdateSpeaker(speaker_id, contact_info, descr);


                    // insert picture
                    if (photo != null)
                    {
                        string photoPath = context.GetSpeakerPhoto(speaker_id).SingleOrDefault();                    
                        if (string.IsNullOrEmpty(photoPath))
                        {
                            var fileName = "[" + EntityItemTypeEnum.FileItem.ToString() + "/" + (FileTypeIDEnum.SmallPoster).ToString() + "] " + name;
                            var photoEntityID = EntityItemService.InsertEnityItem(fileName, 0, active, EntityItemTypeEnum.FileItem,
                                    creator_id, root_entity_id, speaker_id, DateTime.Now.ToUniversalTime(), context);

                            string filePath = MainCommon.MyUtils.ReplaceFileName(photo_path, photoEntityID.ToString());
                            photo.SaveAs(filePath);
                            EntityItemService.InsertFileEnity(photoEntityID, filePath, FileTypeIDEnum.SmallPoster, context);
                        }
                        else
                        {
                            photo.SaveAs(photoPath);
                        }
                    }
                });
        }

        #endregion

        #region Delete

        public int DeleteSpeaker(long speaker_id)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (PortalDbManager context) => EntityItemService.DeleteEnityItem(speaker_id, context));
        }

        #endregion
    }
}
