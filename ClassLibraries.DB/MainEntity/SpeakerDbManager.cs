using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data.Linq;
using MainEntity.Models.Speaker;
using MainCommon;

namespace MainEntity
{
    public partial class SpeakerDbManager
    {
        #region Select

        /*public int GetSpeakerClassesCnt(long from_root_id, long parent_id, long speaker_id, bool? with_only_active,
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

                           ClassEntity = new ClassEntity()
                           {
                               classID = e.ClassEntity.classID,
                               description = e.ClassEntity.description,
                               duration = e.ClassEntity.duration,
                           },

                           SpeakerEntity = new SpeakerEntity()
                           {
                               speakerID = e.ClassEntity.speakerID,
                               description = e.SpeakerEntity.description,
                               contactInfo = e.SpeakerEntity.contactInfo                               
                           }

                       };


            if (with_only_active != null && with_only_active.Value)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }
        */
        
        public IQueryable<EntityItem> GetSpeaker(string name) 
        {
            var rval = from e in this.EntityItems
                       where e.title == name
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

                                      SpeakerEntity = new SpeakerEntity()
                                      {
                                          speakerID = e.SpeakerEntity.speakerID,
                                          contactInfo = e.SpeakerEntity.contactInfo,
                                          description = e.SpeakerEntity.description
                                      }
                                  };
            return rval;
        }

        public IQueryable<EntityItem> GetSpeaker(long speaker_id)
        {
            var rval = from e in this.EntityItems
                       where e.entityID == speaker_id
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

                           SpeakerEntity = new SpeakerEntity()
                           {
                               speakerID = e.SpeakerEntity.speakerID,
                               contactInfo = e.SpeakerEntity.contactInfo,
                               description = e.SpeakerEntity.description
                           }
                       };
            return rval;
        }


        public int GetSpeakersCnt(long from_root_id, long parent_id, bool? with_only_active, bool with_only_nondeleted, long from_portal_id)
        {
            var rval = from e in this.EntityItems
                       where e.rootEntityID == from_root_id && e.parentEntityID == parent_id && e.typeID == (int)EntityItemTypeEnum.SpeakerItem
                       && (from c in this.ClassEntities
                           join ce in this.EntityItems on c.classID equals ce.entityID
                           join x in this.CatalogItemXrefPortals on ce.entityID equals x.catalogItemID
                           where c.speakerID == e.entityID && ce.active && !ce.deleted && x.portalID == from_portal_id
                           select c).Any()
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

        public int GetSpeakersCnt1(long from_root_id, long parent_id, bool? with_only_active, bool with_only_nondeleted, long from_portal_id)
        {
            var rval = from e in this.EntityItems
                       where e.rootEntityID == from_root_id && e.parentEntityID == parent_id && e.typeID == (int)EntityItemTypeEnum.SpeakerItem && (e.deleted == false) && (e.active == true)
                      
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
        public IQueryable<EntityItem> GetSpeakers(long from_root_id, long parent_id, bool? with_only_active, bool with_only_nondeleted, long from_portal_id)
        {
            var rval = from e in this.EntityItems
                       where e.rootEntityID == from_root_id && e.parentEntityID == parent_id && e.typeID == (int)EntityItemTypeEnum.SpeakerItem
                       && (from c in this.ClassEntities
                           join ce in this.EntityItems on c.classID equals ce.entityID
                           join x in this.CatalogItemXrefPortals on ce.entityID equals x.catalogItemID
                           where c.speakerID == e.entityID && ce.active && !ce.deleted && x.portalID == from_portal_id
                           select c).Any()
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

                           SpeakerEntity = new SpeakerEntity()
                           {
                               speakerID = e.SpeakerEntity.speakerID,
                               contactInfo = e.SpeakerEntity.contactInfo,
                               description = e.SpeakerEntity.description
                           }
                       };

            if (with_only_active != null && with_only_active.Value)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }

        public IQueryable<EntityItem> GetSpeakers1(long from_root_id, long parent_id, bool? with_only_active, bool with_only_nondeleted, long from_portal_id)
        {
            var rval = from e in this.EntityItems
                       where e.rootEntityID == from_root_id && e.parentEntityID == parent_id && e.typeID == (int)EntityItemTypeEnum.SpeakerItem && (e.deleted == false) && (e.active == true) 
                    
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

                           SpeakerEntity = new SpeakerEntity()
                           {
                               speakerID = e.SpeakerEntity.speakerID,
                               contactInfo = e.SpeakerEntity.contactInfo,
                               description = e.SpeakerEntity.description
                           }
                       };

            if (with_only_active != null && with_only_active.Value)
                rval = rval.Where(e => e.active);
            if (with_only_nondeleted)
                rval = rval.Where(e => !e.deleted);

            return rval;
        }

        public IQueryable<string> GetSpeakerPhoto(long speaker_id)
        {
            var rval = from e in this.EntityItems
                       where e.parentEntityID == speaker_id && e.typeID == (int)EntityItemTypeEnum.FileItem
                       select e.FileEntity.filePath;

            return rval;
        }

        #endregion

        #region Insert

        public int InsetSpeaker(long entity_id, string description, string contact_info)
        {
            return this.SpeakerEntities.Insert(() => new SpeakerEntity { speakerID = entity_id, description = description, contactInfo = contact_info });
        }

        #endregion

        #region Update

        public int UpdateSpeaker(long speaker_id, string contact_info, string descr)
        {
            return this.SpeakerEntities.Where(s => s.speakerID == speaker_id).
                Update(s => new SpeakerEntity()
                {
                    contactInfo = contact_info,
                    description = descr                    
                });
        }

        #endregion
    }
}
