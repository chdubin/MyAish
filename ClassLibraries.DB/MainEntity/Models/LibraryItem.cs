using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainEntity.Models.Custom
{
    public class LibraryItem
    {
        public long EntityID { get; set; }
        public DateTime ShoppingDate { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string SpeakerName { get; set; }
    }
}
