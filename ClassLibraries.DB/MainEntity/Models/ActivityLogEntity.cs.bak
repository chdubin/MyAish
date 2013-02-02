using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainCommon;
using System.ComponentModel.DataAnnotations;

namespace MainEntity.Models.Activity
{
    public partial class ActivityLog
    {
        public string Title { get; set; }

        public string Speaker { get; set; }

        public decimal? Units { get; set; }

        public long ClassID { get; set; }
    }

    public class LastActivityLog
    {
        public Guid UserId { get; set; }

        public string Title { get; set; }

        public DateTime? Date { get; set; }
    }
}
