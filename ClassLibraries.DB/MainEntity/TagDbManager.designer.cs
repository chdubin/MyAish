using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data.Linq;
using MainEntity.Models.Tag;



namespace MainEntity
{
    public partial class TagDbManager : EntityItemDbManager
    {
        public TagDbManager(string configuartion_name)
            : base(configuartion_name)
        {
        }

        public Table<Tag> TagItems { get { return this.GetTable<Tag>(); } }

    }
}
