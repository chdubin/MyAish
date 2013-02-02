using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using MainEntity.Models.Speaker;

namespace MainEntity
{
    public partial class SpeakerDbManager : EntityItemDbManager
    {
        public SpeakerDbManager(string configuartion_name)
            : base(configuartion_name)
        {
        }

        public Table<EntityItem> EntityItems { get { return this.GetTable<EntityItem>(); } }

        public Table<SpeakerEntity> SpeakerEntities { get { return this.GetTable<SpeakerEntity>(); } }

        public Table<ClassEntity> ClassEntities { get { return this.GetTable<ClassEntity>(); } }

        public Table<CatalogItemXrefPortal> CatalogItemXrefPortals { get { return this.GetTable<CatalogItemXrefPortal>(); } }


    }
}
