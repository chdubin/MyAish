namespace MainEntity.Models.Class
{
    public partial class ClassEntity
    {
        public string SpeakerName { get; set; }

        public string SmallPosterUrl { get; set; }

        public EntityItem Type { get; set; }
        public EntityItem Disc { get; set; }
        public EntityItem File { get; set; }
// this was added on feb 2 from version i had download from git hub  
public string FilePath { get; set; }

        public bool IsFree { get; set; }

		public TagXrefEntity ClassLevel { get; set; }


		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }

		public string Title { get; set; }
		public string Code { get; set; }

    }

	public partial class EntityItem
	{
		public string ShippingLocationName { get; set; }
	}

}