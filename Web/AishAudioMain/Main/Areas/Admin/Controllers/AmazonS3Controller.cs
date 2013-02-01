using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Common.AmazonS3;
using System.IO;
using System.Web.Script.Serialization;

namespace Main.Areas.Admin.Controllers
{
    public class AmazonS3Controller : Controller
    {
		[ChildActionOnly]
		public ActionResult Files(string path, string func)
		{
			return FileList(path, func);
		}
		
        [HttpPost]
        public ActionResult FileList(string path, string func, int start=0, int count=50, string contains_with = null)
        {
            ViewData["Path"] = path;
            ViewData["Function"] = func;
            ViewData["ContainsWith"] = contains_with;
			ViewData["Start"] = start;
			ViewData["Count"] = count;
            path = path ?? string.Empty;

            S3File[] model = null;
            var hierarchy = MvcApplication.S3Amazon.Hierarchy;
            if (hierarchy == null)
                model = new S3File[] { };
            else
            {
                var data = hierarchy.Files.Where(f => f.Path == path);
                if (!string.IsNullOrEmpty(contains_with))
                    data = data.Where(f => f.Name.ToLower().Contains(contains_with.ToLower()));
                model = data.ToArray();
            }


			return PartialView("FileList", model);
        }

        [HttpPost]
        public ActionResult SelectFile()
        {
            return PartialView(Main.MvcApplication.S3Amazon.Hierarchy);
        }

        public ActionResult UploadFile(string path, string func)
        {
            if (string.IsNullOrEmpty(path) || path.Equals("root", StringComparison.InvariantCultureIgnoreCase)) path = "/";

            ViewData["Path"] = path;
            ViewData["FunctionName"] = func;

            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(FormCollection param)
        {
            string path = param["PathFile"].Trim().Replace('\\','/');
            string func = param["FunctionName"];
            var file = Request.Files["UploadFile"];

            string tempUplaod = HttpContext.Server.MapPath(Properties.Settings.Default.UploadTemporaryPath);
            string fileName = Path.GetFileName(file.FileName);
            string tempFileName = Path.Combine(tempUplaod, Guid.NewGuid().ToString("n"));
            string tempMp3FileName=tempFileName + ".mp3";

            if (path == "/") path = string.Empty;
            try
            {
                if (MvcApplication.S3Amazon.Hierarchy.Files.Where(f => (f.Path + f.Name).Equals(path + fileName)).Count() == 0)
                {
                    file.SaveAs(tempMp3FileName);

                    var mp3File = new TagLib.Mpeg.AudioFile(tempMp3FileName);
                    var uploadData = new S3File(path, fileName, file.ContentLength) { Duration = mp3File.Properties.Duration.TotalMilliseconds };
                    JavaScriptSerializer se = new JavaScriptSerializer();
                    var meta = se.Serialize(uploadData);
                    System.IO.File.WriteAllText(tempFileName + ".meta", meta, System.Text.Encoding.UTF8);
                    MvcApplication.S3Amazon.Hierarchy.AddFile(uploadData);

                    ViewData["UploadComplete"] = true;
                    ViewData["Path"] = path + fileName;
                }
                else throw new Exception("File exists");
            }
            catch
            {
                if (System.IO.File.Exists(tempMp3FileName)) System.IO.File.Delete(tempMp3FileName);
                if (System.IO.File.Exists(tempFileName + ".meta")) System.IO.File.Delete(tempFileName + ".meta");
                ViewData["Path"] = path;
            }
            ViewData["FunctionName"] = func;

            return View();
        }

    }
}
