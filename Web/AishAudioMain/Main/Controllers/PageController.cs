using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Main.Common;
using Main.Common.Attributes;
using Main.Utilities;

namespace Main.Controllers
{
    [MyRequireHttpsAttribute]
    [AuthorizedOnlyPortal]
    public class PageController : Controller
    {
        //
        // GET: /Page/
        private string GetStaticContent(string param) 
        {
            long portalID = this.HttpContext.GetCurrentPortal().portalID;

            string filePath = Server.MapPath("~/Page/" + portalID + "/") + Path.GetFileNameWithoutExtension(param) + ".html";
            if (!System.IO.File.Exists(filePath))
                filePath = Server.MapPath("~/Page/") + Path.GetFileNameWithoutExtension(param) + ".html";
            
            string fileContent = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                    fileContent = sr.ReadToEnd();
            }
            catch (FileNotFoundException) 
            {
                return "file not found";
            }

            int start = fileContent.IndexOf("<body>");
            int end = fileContent.LastIndexOf("</body>");
            fileContent = fileContent.Substring(start + 6, end - (start + 6));

            return fileContent;
        }

        public ActionResult Default(string param)
        {
            string content = GetStaticContent(param);

            return View((object)content);
        }

        public ActionResult OneColumn(string param)
        {
            string content = GetStaticContent(param);

            return View((object)content);
        }
    }
}
