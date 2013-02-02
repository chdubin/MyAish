using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using MainEntity.Interfaces;
using Main.Common;
using MainCommon;
using System.Web.Security;
using System.Web.Profile;
using Main.Common;

namespace Main.Common.Routing
{
    public class PortalDomainRoute : Route
    {
        private IPortalService _portalService;

        public PortalDomainRoute(IPortalService portal_service, string url)
            : base(url, new MvcRouteHandler())
        {
            _portalService = portal_service;
        }


        public PortalDomainRoute(IPortalService portal_service, string url, RouteValueDictionary defaults)
            : base(url, defaults, new MvcRouteHandler())
        {
            _portalService = portal_service;
        }

        public PortalDomainRoute(IPortalService portal_service, string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
            _portalService = portal_service;
        }

        public PortalDomainRoute(IPortalService portal_service, string url, object defaults)
            : base(url, new RouteValueDictionary(defaults), new MvcRouteHandler())
        {
            _portalService = portal_service;
        }

        public PortalDomainRoute(IPortalService portal_service, string url, object defaults, IRouteHandler routeHandler)
            : base(url, new RouteValueDictionary(defaults), routeHandler)
        {
            _portalService = portal_service;
        }


        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var rval = base.GetRouteData(httpContext);

            if (rval != null )
            {
                var portal = httpContext.Request.Url.Host + (rval.Values.ContainsKey("portal_part") ? "/" + rval.Values["portal_part"] : string.Empty);
                var aliases = httpContext.Cache.GetValue(CacheEnum.AliasesCache.ToString(), TimeSpan.FromSeconds(GlobalConstant.PORTAL_ALIASES_IN_CACHE_SEC),
                    () => _portalService.GetAliases());
                var alias = aliases.OrderByDescending(a=>a.alias.Length).Where(a => string.Compare(portal,a.alias,true)==0).FirstOrDefault();

                string themeName;
                if (alias == null)
                {
                    rval = new RouteData(this, new MvcRouteHandler());
                    rval.Values.Add("controller", "Error");
                    rval.Values.Add("action", "Domain");
                    rval.DataTokens.Add("area", "");
                    themeName = GlobalConstant.DEFAULT_THEME_NAME;
                }
                else
                    themeName = InitializePortal(httpContext, alias);

                httpContext.Items[MainCommon.ContextEnum.CurrentTheme.ToString()] = themeName;

                #region /key/value logic

                if (rval.Values.ContainsKey("key_values"))
                {
                    if (rval.Values["key_values"] != null)
                    {
                        string[] keyValue = ((string)rval.Values["key_values"]).Split(new char[] { '/' });
                        for (int i = 0; i < keyValue.Length; i += 2)
                            rval.Values.Add(keyValue[i], keyValue[i + 1]);
                    }
                    rval.Values.Remove("key_values");
                }

                #endregion
            }

            return rval;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            var rval = base.GetVirtualPath(requestContext, values);

            return rval;
        }

        private static string InitializePortal(HttpContextBase httpContext, MainEntity.Models.Portal.PortalAlias alias)
        {
            string rval = GlobalConstant.DEFAULT_THEME_NAME;

            if (!string.IsNullOrEmpty(alias.PortalEntity.applicationName))
            {
                MembershipProvider membershipProvider = null;
                RoleProvider roleProvider = null;
                ProfileProvider profileProvider = null;

                membershipProvider = Membership.Providers.Cast<MembershipProvider>().
                    Where(m => m.ApplicationName.Equals(alias.PortalEntity.applicationName, StringComparison.InvariantCultureIgnoreCase)).
                    SingleOrDefault() ?? Membership.Provider;

                roleProvider = Roles.Providers.Cast<RoleProvider>().
                    Where(m => m.ApplicationName.Equals(alias.PortalEntity.applicationName, StringComparison.InvariantCultureIgnoreCase)).
                    SingleOrDefault() ?? Roles.Provider;

                profileProvider = ProfileManager.Providers.Cast<ProfileProvider>().
                    Where(m => m.ApplicationName.Equals(alias.PortalEntity.applicationName, StringComparison.InvariantCultureIgnoreCase)).
                    SingleOrDefault() ?? ProfileManager.Provider;

                httpContext.Items[MainCommon.ContextEnum.CurrentMembershipProvider.ToString()] = membershipProvider;
                httpContext.Items[MainCommon.ContextEnum.CurrentRoleProvider.ToString()] = roleProvider;
                httpContext.Items[MainCommon.ContextEnum.CurrentProfileProvider.ToString()] = profileProvider;

            }

            httpContext.Items[MainCommon.ContextEnum.CurrentPortal.ToString()] = alias.PortalEntity;

            rval = alias.PortalEntity.themeName;

            return rval;
        }

    }
}