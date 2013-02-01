using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPartnerKinoBL.Extension;
using MainEntity.Interfaces;
using MainEntity;
using MainEntity.Models.Tag;

namespace MainBL
{
    public class TagService : BaseBO, ITagService
    {
        public TagService(string connection_name)
            : base(connection_name) { }

        #region Select


        public Tag[] GetCategories()
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (TagDbManager context) =>
                {
                    var rval = context.GetCategories().OrderBy(c=>c.name);
                    return rval.ToArray();
                });
        }

        public Tag[] GetLevels()
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (TagDbManager context) =>
                {
                    var rval = context.GetLevels();
                    return rval.ToArray();
                });
        }

        public string GetTagName(int tag_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (TagDbManager context) =>
                {
                    return context.GetTagName(tag_id);
                });
        }

        #endregion
    }
}
