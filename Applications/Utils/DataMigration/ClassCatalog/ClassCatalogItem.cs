using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassCatalog
{
	//new	level	thubnail	isfree	title	code	speaker	description	file_price1	file_price2	tape_price1	cd_price1
	class ClassCatalogItem
	{
		public string New { get; set; }

		public string Level { get; set; }

		public string ThumbnailUrl { get; set; }

		public bool IsFree { get; set; }

		public string Title { get; set; }

		public string Code { get; set; }

		public string Speaker { get; set; }

		public string Description { get; set; }

		public decimal? FilePrice1 { get; set; }

		public decimal? FilePrice2 { get; set; }

		public decimal? TapePrice1 { get; set; }

		public decimal? CdPrice1 { get; set; }

		public string ShippingLocation { get; set; }
	}
}
