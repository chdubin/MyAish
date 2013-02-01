using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Main.Common
{
    /// <summary>
    /// Provides a flexible <see cref="WebFormViewEngine" /> for adding theming capabilities to your views.
    /// You have the option of using a theme to override only what you need, whether that's CSS, Javascript,
    /// MasterPages, or specific views. This way both look and feel as well as site structure may be
    /// changed on demand.
    /// </summary>
    public class ThemeViewEngine : WebFormViewEngine
    {
        // format is ":ViewCacheEntry:{cacheType}:{prefix}:{name}:{controllerName}:{areaName}:{themeName}"
        private const string CacheKeyFormat = ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}:{5}";
        private const string CacheKeyPrefixMaster = "Master";
        private const string CacheKeyPrefixPartial = "Partial";
        private const string CacheKeyPrefixView = "View";
        
        private static readonly string[] _emptyLocations = new string[0];

        public string Theme { get; private set; }

        // {0}name:{1}controllerName:{2}areaName:{3}themeName
        public ThemeViewEngine()
        {
            MasterLocationFormats = new[] {
                                              // Theme-specific locations (opt-in)
                                              "~/Content/{3}/{0}.master",
                                              "~/Content/{3}/Views/{1}/{0}.master",
                                              "~/Content/{3}/Views/Shared/{0}.master",
                                              // Default locations
                                              "~/Views/{1}/{0}.master",
                                              "~/Views/Shared/{0}.master"
                                          };

            AreaMasterLocationFormats = new[] {
                                                  // Theme-specific locations (opt-in)
                                                  "~/Areas/{2}/Content/{3}/{0}.master",
                                                  "~/Areas/{2}/Content/{3}/Views/{1}/{0}.master",
                                                  "~/Areas/{2}/Content/{3}/Views/Shared/{0}.master",
                                                  "~/Content/{3}/Areas/{2}/{0}.master}",
                                                  "~/Content/{3}/Areas/{2}/Views/{1}/{0}.master}",
                                                  "~/Content/{3}/Areas/{2}/Views/Shared/{0}.master}",
                                                  // Default locations
                                                  "~/Areas/{2}/Views/{1}/{0}.master",
                                                  "~/Areas/{2}/Views/Shared/{0}.master",
                                                  "~/Views/Areas/{2}/{1}/{0}.master",
                                                  "~/Views/Areas/{2}/Shared/{0}.master"
                                              };

            ViewLocationFormats = new[] {
                                            // Theme-specific locations (opt-in)
                                            "~/Content/{3}/Views/{1}/{0}.aspx",
                                            "~/Content/{3}/Views/{1}/{0}.ascx",
                                            "~/Content/{3}/Views/Shared/{0}.aspx",
                                            "~/Content/{3}/Views/Shared/{0}.ascx",
                                            // Default locations
                                            "~/Views/{1}/{0}.aspx",
                                            "~/Views/{1}/{0}.ascx",
                                            "~/Views/Shared/{0}.aspx",
                                            "~/Views/Shared/{0}.ascx"
                                        };

            AreaViewLocationFormats = new[] {
                                                // Theme-specific locations (opt-in)
                                                "~/Areas/{2}/Content/{3}/Views/{1}/{0}.aspx",
                                                "~/Areas/{2}/Content/{3}/Views/{1}/{0}.ascx",
                                                "~/Areas/{2}/Content/{3}/Views/Shared/{0}.aspx",
                                                "~/Areas/{2}/Content/{3}/Views/Shared/{0}.ascx",
                                                "~/Content/{3}/Views/Areas/{2}/{1}/{0}.aspx",
                                                "~/Content/{3}/Views/Areas/{2}/{1}/{0}.ascx",
                                                "~/Content/{3}/Views/Areas/{2}/Shared/{0}.aspx",
                                                "~/Content/{3}/Views/Areas/{2}/Shared/{0}.ascx",
                                                // Default locations
                                                "~/Areas/{2}/Views/{1}/{0}.aspx",
                                                "~/Areas/{2}/Views/{1}/{0}.ascx",
                                                "~/Areas/{2}/Views/Shared/{0}.aspx",
                                                "~/Areas/{2}/Views/Shared/{0}.ascx",
                                                "~/Views/Areas/{2}/{1}/{0}.aspx",
                                                "~/Views/Areas/{2}/{1}/{0}.ascx",
                                                "~/Views/Areas/{2}/Shared/{0}.aspx",
                                                "~/Views/Areas/{2}/Shared/{0}.ascx"
                                            };

            PartialViewLocationFormats = ViewLocationFormats;
            AreaPartialViewLocationFormats = AreaViewLocationFormats;
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (String.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException("Argument cannot be null or empty", "viewName");
            }

            SetTheme(controllerContext);

            string[] viewLocationsSearched;
            string[] masterLocationsSearched;

            var controllerName = controllerContext.RouteData.GetRequiredString("controller");

            var viewPath = GetPath(controllerContext, ViewLocationFormats, AreaViewLocationFormats,
                                   "ViewLocationFormats", viewName, controllerName, CacheKeyPrefixView, useCache,
                                   out viewLocationsSearched);

            var masterPath = GetPath(controllerContext, MasterLocationFormats, AreaMasterLocationFormats,
                                     "MasterLocationFormats", masterName, controllerName, CacheKeyPrefixMaster,
                                     useCache, out masterLocationsSearched);

            if (String.IsNullOrEmpty(viewPath) ||
                (String.IsNullOrEmpty(masterPath) && !String.IsNullOrEmpty(masterName)))
            {
                return new ViewEngineResult(viewLocationsSearched.Union(masterLocationsSearched));
            }

            return new ViewEngineResult(CreateView(controllerContext, viewPath, masterPath), this);
        }

        private void SetTheme(ControllerContext controllerContext)
        {
            var context = controllerContext.RequestContext.HttpContext;
            var request = context.Request;
            var session = context.Session;
            //var queryString = request.QueryString;

            //if (queryString.AllKeys.Contains("theme"))
            //{
            //    var theme = queryString["theme"];
            //    session.Add(MainCommon.SessionEnum.Theme.ToString(), theme);
            //}

            if (context.Items[MainCommon.ContextEnum.CurrentTheme.ToString()] != null)
                Theme = context.Items[MainCommon.ContextEnum.CurrentTheme.ToString()].ToString();
            //else if (session[MainCommon.SessionEnum.Theme.ToString()] != null)
            //    Theme = session[MainCommon.SessionEnum.Theme.ToString()].ToString();

        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (String.IsNullOrEmpty(partialViewName))
            {
                throw new ArgumentException("Argument cannot be null or empty", "partialViewName");
            }

            SetTheme(controllerContext);

            string[] searched;
            var controllerName = controllerContext.RouteData.GetRequiredString("controller");
            var partialPath = GetPath(controllerContext, PartialViewLocationFormats, AreaPartialViewLocationFormats, "PartialViewLocationFormats", partialViewName, controllerName, CacheKeyPrefixPartial, useCache, out searched);

            return String.IsNullOrEmpty(partialPath)
                       ? new ViewEngineResult(searched)
                       : new ViewEngineResult(CreatePartialView(controllerContext, partialPath), this);
        }

        private string GetPath(ControllerContext controllerContext, IEnumerable<string> locations, string[] areaLocations, string locationsPropertyName, string name, string controllerName, string cacheKeyPrefix, bool useCache, out string[] searchedLocations)
        {
            searchedLocations = _emptyLocations;

            if (String.IsNullOrEmpty(name))
            {
                return String.Empty;
            }

            var areaName = controllerContext.RouteData.GetAreaName();
            var usingAreas = !String.IsNullOrEmpty(areaName);
            var viewLocations = GetViewLocations(locations, (usingAreas) ? areaLocations : null);
            if (viewLocations.Count == 0)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentUICulture,
                                                                  "{0} cannot be null or empty.", locationsPropertyName));
            }

            var nameRepresentsPath = IsSpecificPath(name);
            var cacheKey = CreateCacheKey(cacheKeyPrefix, name, (nameRepresentsPath) ? String.Empty : controllerName, areaName);
            if (useCache)
            {
                return ViewLocationCache.GetViewLocation(controllerContext.HttpContext, cacheKey);
            }

            return (nameRepresentsPath) ? 
                GetPathFromSpecificName(controllerContext, name, cacheKey, ref searchedLocations) :
                GetPathFromGeneralName(controllerContext, viewLocations, name, controllerName, areaName, cacheKey, ref searchedLocations);
        }

        private string CreateCacheKey(string prefix, string name, string controllerName, string areaName)
        {
            return String.Format(CultureInfo.InvariantCulture, CacheKeyFormat,
                                 GetType().AssemblyQualifiedName, prefix, name, controllerName, areaName, Theme);
        }

        private string GetPathFromGeneralName(ControllerContext controllerContext, IList<ViewLocation> locations, string name, string controllerName, string areaName, string cacheKey, ref string[] searchedLocations)
        {
            var result = String.Empty;
            searchedLocations = new string[locations.Count];

            for (var i = 0; i < locations.Count; i++)
            {
                var location = locations[i];
                var virtualPath = location.Format(name, controllerName, areaName, Theme);

                if (FileExists(controllerContext, virtualPath))
                {
                    searchedLocations = _emptyLocations;
                    result = virtualPath;
                    ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, result);
                    break;
                }

                searchedLocations[i] = virtualPath;
            }

            return result;
        }

        private string GetPathFromSpecificName(ControllerContext controllerContext, string name, string cacheKey, ref string[] searchedLocations)
        {
            var result = name;

            if (!FileExists(controllerContext, name))
            {
                result = String.Empty;
                searchedLocations = new[] { name };
            }

            ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, result);
            return result;
        }

        private static List<ViewLocation> GetViewLocations(IEnumerable<string> viewLocationFormats, 
                                                           IEnumerable<string> areaViewLocationFormats)
        {
            var allLocations = new List<ViewLocation>();

            if (areaViewLocationFormats != null)
            {
                foreach (var areaViewLocationFormat in areaViewLocationFormats)
                {
                    allLocations.Add(new AreaAwareViewLocation(areaViewLocationFormat));
                }
            }

            if (viewLocationFormats != null)
            {
                foreach (var viewLocationFormat in viewLocationFormats)
                {
                    allLocations.Add(new ViewLocation(viewLocationFormat));
                }
            }

            return allLocations;
        }

        private static bool IsSpecificPath(string name)
        {
            var c = name[0];
            return (c == '~' || c == '/');
        }

        private class ViewLocation
        {
            protected readonly string _virtualPathFormatString;

            public ViewLocation(string virtualPathFormatString)
            {
                _virtualPathFormatString = virtualPathFormatString;
            }

            public virtual string Format(string viewName, string controllerName, string areaName, string themeName)
            {
                var result = String.Format(CultureInfo.InvariantCulture, _virtualPathFormatString, viewName, controllerName, areaName, themeName);
                return result;
            }
        }

        private class AreaAwareViewLocation : ViewLocation
        {
            public AreaAwareViewLocation(string virtualPathFormatString)
                : base(virtualPathFormatString)
            {

            }

            public override string Format(string viewName, string controllerName, string areaName, string themeName)
            {
                var result = String.Format(CultureInfo.InvariantCulture, _virtualPathFormatString, viewName, controllerName, areaName, themeName);
                return result;
            }
        }
    }
}