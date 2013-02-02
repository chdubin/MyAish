using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Utilities;

namespace Main.Controllers
{
    [MyRequireHttpsAttribute]
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Domain()
        {
            return View();
        }

    }
}
