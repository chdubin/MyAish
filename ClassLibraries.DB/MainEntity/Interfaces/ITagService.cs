using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Tag;

namespace MainEntity.Interfaces
{
    public interface ITagService
    {
        Tag[] GetCategories();

        Tag[] GetLevels();

        string GetTagName(int tag_id);
    }
}
