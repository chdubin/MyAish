using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainCommon;
using MainEntity.Models.Tag;

namespace MainEntity
{
    public partial class TagDbManager
    {
        #region Select

        public IQueryable<Tag> GetCategories()
        {
            var rval = (from t in this.TagItems
                        where t.tagTypeID == (short)TagTypeEnum.Category
                        select t);


            return rval;
        }

        public IQueryable<Tag> GetLevels()
        {
            var rval = (from t in this.TagItems
                        where t.tagTypeID == (short)TagTypeEnum.ClassLevel
                        select t);

            return rval;
        }

        public string GetTagName(int tag_id)
        {
            var rval = (from t in this.TagItems
                        where t.tagID == tag_id
                        select t.name);

            return rval.SingleOrDefault();
        }

        #endregion
    }
}
