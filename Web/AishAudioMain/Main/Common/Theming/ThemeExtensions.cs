using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Main.Common
{
    public static class ThemeExtensions
    {
        // format is ":ThemeCacheEntry:{areaName}:{themeName}:{resourceType}:{resourceName}"
        private const string CacheKeyFormat = ":ThemeCacheEntry:{0}:{1}:{2}:{3}";
        
        // {0}areaName:{1}themeName:{2}resourceType:{3}resourceName
        private static readonly string[] _contentLocationFormats = new[]
           {
               // Area and type-specific locations
               "~/Areas/{0}/Content/{1}/{3}",
               "~/Areas/{0}/Content/{2}/{3}",
               "~/Areas/{0}/Content/{1}/{2}/{3}",
               "~/Areas/{0}/Content/{1}/{2}/{3}",
               // Area-specific default locations
               "~/Areas/{0}/Content/{3}",
               "~/Areas/{0}/Scripts/{3}",
               // Area default locations
               "~/Areas/Content/{3}",
               "~/Areas/Scripts/{3}",
               // Theme and type-specific locations
               //"~/Content/{1}/{2}/{3}",
               "~/Content/{1}/{3}",
               "~/Content/{2}/{3}",
               "~/Content/{1}/{2}/{3}",
               "~/Scripts/{1}/{3}",
               // Default locations
               "~/Content/{3}",
               "~/Scripts/{3}",
               "~/Content/Default/{2}/{3}",
               "~/Content/Default/{3}",
           };

        public static string JavaScript(this UrlHelper helper, string scriptName)
        {
            return StaticResource(helper, "JavaScript", scriptName);
        }

        public static string Css(this UrlHelper helper, string styleName)
        {
            return StaticResource(helper, "Css", styleName);
        }

        public static string Image(this UrlHelper helper, string imageName)
        {
            return StaticResource(helper, "Images", imageName);
        }

        public static string StaticResource(this UrlHelper helper,
                                            string resourceType, 
                                            string resourceName)
        {
            return helper.StaticResource(resourceType, resourceName, true);
        }

        public static string StaticResource(this UrlHelper helper, 
                                            string resourceType, 
                                            string resourceName,
                                            bool useCache)
        {
            var areaName = helper.RequestContext.RouteData.GetAreaName();
            var themeName = FindThemeName();

            var cacheKey = String.Format(CacheKeyFormat, areaName, themeName, resourceType, resourceName);
            if(useCache)
            {
                var value = HttpRuntime.Cache[cacheKey];
                if (value != null)
                {
                    return value.ToString();
                }
            }

            var searchedLocations = new List<string>();
            foreach (var mask in _contentLocationFormats)
            {
                var relativePath = String.Format(mask, areaName, themeName ?? "", resourceType, resourceName);
                var absolutePath = VirtualPathUtility.ToAbsolute(relativePath);
                var serverPath = helper.RequestContext.HttpContext.Server.MapPath(absolutePath);

                searchedLocations.Add(absolutePath);
                if (!File.Exists(serverPath))
                {
                    continue;
                }
                
                HttpRuntime.Cache.Insert(cacheKey, absolutePath, new CacheDependency(serverPath));
                return absolutePath;
            }

            //throw new FileNotFoundException("Not found resource: " + resourceName);
            return string.Empty;
        }

        private static string FindThemeName()
        {
            string themeName = null;
            foreach(var engine in ViewEngines.Engines)
            {
                var themeViewEngine = engine as ThemeViewEngine;
                if (themeViewEngine == null)
                {
                    continue;
                }

                themeName = themeViewEngine.Theme;
            }
            return themeName;
        }
    }
}