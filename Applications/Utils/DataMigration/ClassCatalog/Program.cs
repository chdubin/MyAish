using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using MainBL;
using MainCommon;
using System.Net;

namespace ClassCatalog
{
	class Program
	{
		private const string CONNECTION_NAME = "ApplicationServices";

		static void Main(string[] args)
		{
			var dataPath = args[0];
			var dataStream = File.OpenText(dataPath);
			var portal = new PortalService(CONNECTION_NAME).GetPortal(Properties.Settings.Default.PortalID, false, true);

			var data = ParseData(dataStream);
			var speakerData = data.Select(d => d.Speaker).Distinct();
			var codeCnt = data.Select(d => d.Code).Distinct().Count();

			if (codeCnt != data.Count()) throw new Exception("Internal error in Program.Main: code is not unique");

			var service = new CatalogService(CONNECTION_NAME);
			for(int i=0;i<data.Count;i++)
			{
				var item = data[i];
				Console.WriteLine(string.Format("Migrate {2}/{3}: # {0}, {1}", item.Code, item.Title,i+1,data.Count));
				InsertOrSkipClass(portal, service, item);
			}

		}

		private static void InsertOrSkipClass(MainEntity.Models.Portal.PortalEntity portal, CatalogService service, ClassCatalogItem item)
		{
			var items = service.GetCatalogItems(Properties.Settings.Default.RootEntityID, item.Code, false, true);

			if (items.Count() == 0)
			{
				var speakerID = InsertOrSkipSpeaker(item.Speaker);
				Stream thumbnail = null;
				long thumbnailLength = 0;
				var xportals = new MainEntity.Models.Catalog.CatalogItemXrefPortal[]{new MainEntity.Models.Catalog.CatalogItemXrefPortal() 
				{
					isFree = item.IsFree,
					isFreeOffer = false,
					isVisible = true,
					portalID = portal.portalID
				}};
				var products = new List<MainEntity.Models.Catalog.ProductEntity>();

				if (!string.IsNullOrEmpty(item.ThumbnailUrl))
				{
					using (var client = new WebClient())
						try
						{
							thumbnail = client.OpenRead(item.ThumbnailUrl);
							thumbnailLength = long.Parse(client.ResponseHeaders[HttpResponseHeader.ContentLength]);
						}
						catch
						{ }
				}

				if (item.TapePrice1 != null)
					products.Add(new MainEntity.Models.Catalog.ProductEntity() { price1 = item.TapePrice1, price2 = null, productTypeID = (short)ProductTypeEnum.Tape });
				if (item.CdPrice1 != null)
					products.Add(new MainEntity.Models.Catalog.ProductEntity() { price1 = item.CdPrice1, price2 = null, productTypeID = (short)ProductTypeEnum.Disk });
				if (item.FilePrice1 != null || item.FilePrice2 != null)
					products.Add(new MainEntity.Models.Catalog.ProductEntity()
					{
						price1 = item.FilePrice1,
						price2 = item.FilePrice2,
						productTypeID = (short)ProductTypeEnum.File,
						File = new MainEntity.Models.Catalog.FileEntity() { filePath = item.Code.Replace(' ', '_'), fileTypeID = (int)FileTypeIDEnum.S3File }
					});

				var tagID = service.InsertOrSkipTag(item.Level, TagTypeEnum.ClassLevel);


                //service.InsertClass(item.Title, item.Description,
                //    Properties.Settings.Default.CreatorID, true, Properties.Settings.Default.RootEntityID,
                //    speakerID, tagID, xportals, products.ToArray(),
                //    thumbnail, thumbnailLength, Properties.Settings.Default.UploadThumbnailPath, Path.GetExtension(item.ThumbnailUrl),
                //    null, TimeSpan.Zero, item.Code);
			}
			else
			{
				var entity = items[0];
				InsertOrSkipTag(service, entity.entityID, item.Level, TagTypeEnum.ClassLevel);
				InsertOrUpdateShippingInfo(service, entity.entityID, item.ShippingLocation);
			}
		}

		private static void InsertOrUpdateShippingInfo(CatalogService service, long entity_id, string shipping_name)
		{
			var shippingLocationID = service.InsertOrSkipShippingLocation(shipping_name);
			var products = service.GetClassProducts(entity_id);
			foreach (var product in products)
			{
				if (product.ProductEntity.productTypeID == (short)ProductTypeEnum.Tape ||
					product.ProductEntity.productTypeID == (short)ProductTypeEnum.Disk)
				{
					service.DeleteProductShippingLocation(product.entityID);
					if (!string.IsNullOrEmpty(shipping_name) && shipping_name!="N/A" && 
						(product.ProductEntity.price1 != null || product.ProductEntity.price2 != null))
						service.InsertProductShippingLocation(product.entityID, shippingLocationID);
				}
			}
		}

		private static void InsertOrSkipTag(CatalogService service, long entity_id, string tag_name, TagTypeEnum tag_type)
		{
			var tagID = service.InsertOrSkipTag(tag_name, tag_type);
			service.DeleteTagXrefEntity(entity_id, tag_type);
			service.InsertTagXrefEntity(tagID, entity_id);
		}

		private static List<ClassCatalogItem> ParseData(StreamReader dataStream)
		{
			var rval = new List<ClassCatalogItem>();

			do
			{
				string dataLine = dataStream.ReadLine();
				//new	level	thubnail	isfree	title	code	speaker	description	file_price1	file_price2	tape_price1	cd_price1
				var data = dataLine.Split('\t');
				var catalogItem = new ClassCatalogItem()
				{
					New = data[0],
					Level = data[1],
					ThumbnailUrl = data[2],
					IsFree = bool.Parse(data[3]),
					Title = data[4],
					Code = data[5].Trim('#', ' '),
					Speaker = data[6],
					Description = data[7],
					FilePrice1 = ParseDecimal(data[8]),
					FilePrice2 = ParseDecimal(data[9]),
					TapePrice1 = ParseDecimal(data[10]),
					CdPrice1 = ParseDecimal(data[11]),
					ShippingLocation = data[12]
				};

				rval.Add(catalogItem);
			}
			while (!dataStream.EndOfStream);

			return rval;
		}

		private static long InsertOrSkipSpeaker(string speaker)
		{
			long rval = 0;
			var service = new SpeakerService(CONNECTION_NAME);

			var sp = service.GetSpeaker(speaker);
			if (sp == null)
				rval = service.InsertSpeaker(Properties.Settings.Default.CreatorID, Properties.Settings.Default.RootEntityID, speaker, null, null);
			else
				rval = sp.entityID;

			return rval;
		}


		private static decimal? ParseDecimal(string val)
		{
			decimal? rv = null;
			decimal value;
			if (decimal.TryParse(val.Trim('$', ' '), System.Globalization.NumberStyles.Currency, CultureInfo.InvariantCulture, out value))
				rv = value;
			return rv;
		}
	}
}
