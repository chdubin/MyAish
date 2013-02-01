using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Areas.Admin.Models.ControllerView
{
    public class FixAmazonFile
    {
        public Main.Common.AmazonS3.S3File[] Files { get; private set; }

        public long ClassID { get; private set; }
        public string Title { get; private set; }
        public long FileID { get; private set; }
        public bool Alternate { get; private set; }
        public string FilePath { get; set; }

        public FixAmazonFile(long class_id, string title, long file_id, bool alternate, string file_path, Main.Common.AmazonS3.S3File[] files)
        {
            ClassID = class_id;
            Title = title;
            FileID = file_id;
            Alternate = alternate;
            FilePath = file_path;

            Files = files;
        }

    }
}