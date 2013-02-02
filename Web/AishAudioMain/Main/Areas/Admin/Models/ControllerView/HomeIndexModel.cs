using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Main.Common;

namespace Main.Areas.Admin.Models.ControllerView
{
    public class HomeIndexModel
    {
        public LazyLoad<MainEntity.Models.Portal.PortalEntity[]> Portals { get; set; }

    }
}