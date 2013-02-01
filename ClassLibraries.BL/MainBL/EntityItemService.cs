using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPartnerKinoBL.Extension;
using BLToolkit.Data.Linq;
using MainCommon;
using MainEntity;
using MainEntity.Models.Entity;

namespace MainBL
{
    public class EntityItemService : BaseBO
    {
        public EntityItemService(string connection_name)
            : base(connection_name)
        {
        }


        #region Insert

        public static long InsertEnityItem(string title, int sort_order, bool active,
            EntityItemTypeEnum type, Guid creator_id, long root_entity_id, long parent_entity_id, DateTime create_date,
            EntityItemDbManager context)
        {
            var parent = context.GetHierarchiAndRootID(parent_entity_id);
            if (root_entity_id != parent.Key && root_entity_id != parent_entity_id)
                throw new ArgumentException("Internal error in: EntityItemService.InsertEnityItem, root_entity_id must equals rootID from parent or parent_entity_id");
            var hierarchi = parent.Value + parent_entity_id + ".";
            return context.InsertEnityItem(title, sort_order, active, type, creator_id, hierarchi, root_entity_id, parent_entity_id, create_date);
        }

        public static void InsertFileEnity(long file_id, string file_path, FileTypeIDEnum type, EntityItemDbManager context)
        {
            context.InsertFileEnity(file_id, file_path, type);
        }

        #endregion


        #region Delete

        public static int DeleteEnityItem(long entity_id, EntityItemDbManager context)
        {
            return context.BaseEntityItems.Where(e => e.entityID == entity_id).Update(e => new EntityItem() { deleted = true });
        }

        #endregion
    }
}
