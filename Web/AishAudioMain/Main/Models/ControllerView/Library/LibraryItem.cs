using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models.ControllerView.Library
{
    public class LibraryItem
    {
        public DateTime ShoppingDate { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string SpeakerName { get; set; }
    }
}