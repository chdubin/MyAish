using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace MainCommon.Models
{
    public class ImageInfo
    {
        public bool AsUrl { get; private set; }

        public Uri Url { get; private set; }

        public Stream Stream { get; private set; }

        public int ContentLenght { get; private set; }

        public string ImageExt { get; private set; }

        public string ImagePath { get; private set; }

        public ImageInfo(Stream image, int content_lenght, string ext, string image_path)
        {
            AsUrl = false;

            Stream = image;
            ContentLenght = content_lenght;
            ImageExt = ext;
            ImagePath = image_path;
        }

        public ImageInfo(Uri url)
        {
            AsUrl = true;
            Url = url;
        }

        public string GetImageUrl(long file_id)
        {
            string rval;
            if (AsUrl) rval = Url.ToString();
            else rval = file_id + ImageExt;

            return rval;
        }

        public void Save(string path)
        {
            if (!AsUrl)
            {
                path = Path.Combine(ImagePath, path);
                var imagePath = HttpContext.Current.Server.MapPath(path);

                if (File.Exists(imagePath)) File.Delete(imagePath);

                using (var file = File.Open(imagePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var buff = new byte[65536];
                    int len = 0;
                    for (long size = 0; size < ContentLenght; size += len)
                    {
                        len = Stream.Read(buff, 0, buff.Length);
                        file.Write(buff, 0, len);
                    }
                }
            }
        }

    }
}
