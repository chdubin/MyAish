using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Main.Common
{
    public class MainHelper
    {
        public string[] AllowedApplicationNames { get; private set; }
        public string[] AllowedThemes { get;private set; }

        public MainHelper(string[] application_names, string[] themes)
        {
            AllowedApplicationNames = application_names;
            AllowedThemes = themes;
        }
    }
}