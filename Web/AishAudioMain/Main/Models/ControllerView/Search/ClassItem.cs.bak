using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainEntity.Models.Class;
using System.Configuration;
using MainCommon;

namespace Main.Models.ControllerView.Search
{
    public class ClassListItem
    {
        public long ClassID { get; set; }
       
        public string Title { get; set; }
        public string Code { get; set; }
        public string SpeakerName { get; set; }
        public string Description { get; set; }
        public string SmallPosterUrl { get; set; }
        public int? NewOrder { get; set; }

        public bool FileAvailable { get; set; }
        public decimal FilePrice1 { get; set; }
        public decimal FilePrice2 { get; set; }
        public long FileID { get; set; }

        public bool TypeAvailable { get; set; }
        public decimal TypePrice { get; set; }
        public long TypeID { get; set; }
        public long TypeIdX { get; set; }

        public bool DiscAvailable { get; set; }
        public decimal DiscPrice { get; set; }
        public long DiscID { get; set; }

        public bool FileInCart { get; set; }
        public bool TypeInCart { get; set; }
        public bool DiscInCart { get; set; }

        public bool IsFree { get; set; }

		public string Level { get; set; }
		public string ShippingLocation { get; set; }

        public string Length { get; set; }

        public static List<ClassListItem> GetForList(ClassEntity[] classes, long[] class_in_cart_ids) 
        {
			List<ClassListItem> rval = classes.Select(c => new ClassListItem
			{
				ClassID = c.classID,
				Title = c.EntityItem.title,
				SpeakerName = c.SpeakerName,
				Description = c.description,
                SmallPosterUrl = MyUtils.GetImageUrl(ConfigurationManager.AppSettings["UploadClassImgFolderName"], c.SmallPosterUrl),
                NewOrder = c.newOrder,
                TypeIdX = c.EntityItem.typeID,
             	FileAvailable = c.File != null,
                FilePrice1 = c.File != null && c.File.ProductEntity != null ? (c.File.ProductEntity.price1 ?? 0) : 0 ,
				FilePrice2 = c.File != null && c.File.ProductEntity != null ? (c.File.ProductEntity.price2 ?? 0) : 0,
				FileID = c.File != null ? c.File.entityID : 0,

				TypeAvailable = c.Type != null,
				TypePrice = c.Type != null ? (c.Type.ProductEntity.price1 ?? 0) : 0,
				TypeID = c.Type != null ? c.Type.entityID : 0,

				DiscAvailable = c.Disc != null,
				DiscPrice = c.Disc != null ? (c.Disc.ProductEntity.price1 ?? 0) : 0,
				DiscID = c.Disc != null ? c.Disc.entityID : 0,
                FileInCart = c.EntityItem.typeID != 8 ? (c.File != null ? class_in_cart_ids.Contains(c.File.entityID) : false) : class_in_cart_ids.Contains(c.classID),
				TypeInCart = c.Type != null ? class_in_cart_ids.Contains(c.Type.entityID) : false,
				DiscInCart = c.Disc != null ? class_in_cart_ids.Contains(c.Disc.entityID) : false,

				Code = c.CatalogItemExtend != null ? c.CatalogItemExtend.code : string.Empty,

				IsFree = c.IsFree,

                Level = c.ClassLevel != null ? c.ClassLevel.Tag.name : string.Empty,
				ShippingLocation = c.Disc != null ? c.Disc.ShippingLocationName : (c.Type != null ? c.Type.ShippingLocationName : null),

                Length = c.duration.HasValue ? String.Format("{0} min.", Math.Round(c.duration.Value.TotalMinutes)) : string.Empty
			}).ToList();

            return rval;
        }
    }
}