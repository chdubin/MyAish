using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Main.Areas.Admin.Models.Speaker
{
    public class SpeakerEditModel
    {
        public HttpPostedFileBase Photo { get; set; }

        public string PhotoPath { get; set; }

        public long SpeakerID { get; set; }

        public DateTime CreateDate { get; set; }

        public bool Active { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        public string ContactInfo { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(512)]
        public string Name { get; set; }


        public SpeakerEditModel()
        { 

        }

        public SpeakerEditModel(MainEntity.Models.Speaker.EntityItem entity_item)
        {
            SpeakerID = entity_item.SpeakerEntity.speakerID;
            Description = entity_item.SpeakerEntity.description;
            ContactInfo = entity_item.SpeakerEntity.contactInfo;
            CreateDate = entity_item.createDate;
            Active = entity_item.active;
            Name = entity_item.title;
        }
    }
}