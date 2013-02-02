using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Entity;
using MainCommon;
using BLToolkit.Data.Linq;

namespace MainEntity
{
    public partial class EntityItemDbManager
    {
        #region Get

        public KeyValuePair<long,string> GetHierarchiAndRootID(long from_entity_id)
        {
            return this.BaseEntityItems.
                Where(e => e.entityID == from_entity_id).
                Select(e =>new KeyValuePair<long,string>(e.rootEntityID,e.hierarchiID)).
                Single();
        }

        #endregion


        #region Update

        public int UpdateEntityItem(long entity_id, string title, bool active)
        {
            return this.BaseEntityItems.Where(e => e.entityID == entity_id).
                Update(e => new EntityItem()
                {
                    title = title,
                    active = active
                });
        }

        #endregion


        #region Insert

        public long InsertEnityItem(string title, int sort_order, bool active,
            EntityItemTypeEnum type, Guid creator_id, string hierarchi_id, long root_entity_id, long parent_entity_id, DateTime create_date)
        {
            return Convert.ToInt64(this.BaseEntityItems.InsertWithIdentity(() => new EntityItem
            {
                rootEntityID = root_entity_id,
                parentEntityID = parent_entity_id,
                typeID = (int)type,
                creatorID = creator_id,
                hierarchiID = hierarchi_id,
                title = title,
                sortOrder = sort_order,
                createDate = create_date,
                deleted = false,
                active = active
            }));
        }

        public void InsertFileEnity(long file_id, string file_path, FileTypeIDEnum type)
        {
            this.BaseFileEntities.InsertWithIdentity(() => new FileEntity
            {
                fileID = file_id,
                filePath = file_path,
                fileTypeID = (short)type
            });
        }


        #endregion
    }
}
