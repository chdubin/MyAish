using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Main.Common.Routing;
using System.Web.Security;
using System.Web.Profile;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MvcContrib.FluentHtml;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc.Html;

namespace Main.Common
{
    public static class MainExtensions
    {

        public static MvcHtmlString ActionLink2<T>(this AjaxHelper<T> helper, string inner_html, string action_name, object route_values, AjaxOptions ajax_options, object html_attributes)
        {
            var rval = helper.ActionLink("{*}", action_name, route_values, ajax_options, html_attributes);

            rval = MvcHtmlString.Create(rval.ToHtmlString().Replace("{*}", inner_html));

            return rval;
        }

        public static MvcHtmlString ActionLink2<T>(this HtmlHelper<T> helper, string inner_html, string action_name, object route_values, object html_attributes)
        {
            var rval = helper.ActionLink("{*}", action_name, route_values, html_attributes);

            rval = MvcHtmlString.Create(rval.ToHtmlString().Replace("{*}", inner_html));

            return rval;
        }

        public static MvcHtmlString ActionLink2<T>(this HtmlHelper<T> helper, string inner_html, string action_name, string controller_name, object route_values, object html_attributes)
        {
            var rval = helper.ActionLink("{*}", action_name, controller_name, route_values, html_attributes);

            rval = MvcHtmlString.Create(rval.ToHtmlString().Replace("{*}", inner_html));

            return rval;
        }


        public static LazyLoad<T> GetLazyLoad<T>(this Controller controller, Func<T> func)
        {
            return new LazyLoad<T>(func);
        }

        private static object _cacheSyncObj = new object();
        public static T GetValue<T>(this System.Web.Caching.Cache cache, object key, TimeSpan expire, Func<T> get)
        {
            T rval;
            string keyVal = key.ToString();

            lock (_cacheSyncObj)
            {
                rval = (T)cache[keyVal];
                if (rval == null)
                {
                    rval = get();
                    if (rval != null)
                        cache.Add(keyVal, rval, null,
                            DateTime.Now.Add(expire), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }

            return rval;
        }

        public static bool IsAuthorized(this ViewDataDictionary view_data)
        {
            return view_data["IsAuthorized"] != null && (bool)view_data["IsAuthorized"];
        }

        public static bool IsSubscriber(this ViewDataDictionary view_data)
        {
            return view_data["ActiveSubscribe"] != null && (bool)view_data["ActiveSubscribe"];
        }


        #region HttpContext extensions

        public static MainEntity.Models.Portal.PortalEntity GetCurrentPortal(this HttpContextBase context)
        {
            return (MainEntity.Models.Portal.PortalEntity)context.Items[MainCommon.ContextEnum.CurrentPortal.ToString()];
        }

        public static MembershipProvider GetCurrentMembershipProvider(this HttpContextBase context)
        {
            return (MembershipProvider)context.Items[MainCommon.ContextEnum.CurrentMembershipProvider.ToString()];
        }

        public static MainEntity.Models.Portal.PortalEntity GetCurrentPortal(this HttpContext context)
        {
            return (MainEntity.Models.Portal.PortalEntity)context.Items[MainCommon.ContextEnum.CurrentPortal.ToString()];
        }

        public static MembershipProvider GetCurrentMembershipProvider(this HttpContext context)
        {
            return (MembershipProvider)context.Items[MainCommon.ContextEnum.CurrentMembershipProvider.ToString()];
        }


        public static MembershipUser GetMembershipUser(this HttpContextBase context)
        {
            MembershipUser rval = null;
            if (context.IsAuthenticated())
            {
                rval = (MembershipUser)context.Items[MainCommon.ContextEnum.CurrentUserMembership.ToString()];
                if (rval == null)
                {
                    var curProvider = (MembershipProvider)context.Items[MainCommon.ContextEnum.CurrentMembershipProvider.ToString()];
                    rval = curProvider.GetUser(context.User.Identity.Name, false);
                    if (!rval.IsApproved || rval.IsLockedOut) rval = null;
                    else context.Items[MainCommon.ContextEnum.CurrentUserMembership.ToString()] = rval;
                }
            }

            return rval;
        }

        public static bool IsAuthenticated(this HttpContextBase context)
        {
            var identity = context.User.Identity as FormsIdentity;
            var isAuthenticated = identity != null && identity.IsAuthenticated &&
                string.Compare(context.GetCurrentMembershipProvider().ApplicationName, identity.Ticket.UserData, true) == 0;

            return isAuthenticated;

        }

        public static RoleProvider GetCurrentRoleProvider(this HttpContextBase context)
        {
            return (RoleProvider)context.Items[MainCommon.ContextEnum.CurrentRoleProvider.ToString()];
        }

        public static ProfileProvider GetCurrentProfileProvider(this HttpContextBase context)
        {
            return (ProfileProvider)context.Items[MainCommon.ContextEnum.CurrentProfileProvider.ToString()];
        }

        #endregion


        public static PortalDomainRoute MapPortalRoute(this RouteCollection routes, string name, MainEntity.Interfaces.IPortalService portal_service, string url, object defaults, object constraints, string[] namespaces)
        {
            PortalDomainRoute route = new PortalDomainRoute(portal_service, url)
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };

            if ((namespaces != null) && (namespaces.Length > 0))
            {
                route.DataTokens["Namespaces"] = namespaces;
            }

            routes.Add(name, route);

            return route;
        }

        public static PortalDomainRoute MapPortalRoute(this AreaRegistrationContext area_context, string name, MainEntity.Interfaces.IPortalService portal_service, string url, object defaults, object constraints, string[] namespaces)
        {
            if (namespaces == null && area_context.Namespaces != null)
            {
                namespaces = area_context.Namespaces.ToArray();
            }

            PortalDomainRoute route = area_context.Routes.MapPortalRoute(name, portal_service, url, defaults, constraints, namespaces);
            route.DataTokens["area"] = area_context.AreaName;

            bool useNamespaceFallback = (namespaces == null || namespaces.Length == 0);
            route.DataTokens["UseNamespaceFallback"] = useNamespaceFallback;

            return route;
        }

        public static List<byte> AddTextToArray(this List<byte> array, string text)
        {
            var b = System.Text.Encoding.UTF8.GetBytes(text);
            array.Add((byte)b.Length);
            array.AddRange(b);

            return array;
        }

        public static string GetValue(this HttpContextBase context, MainCommon.SessionEnum key, string default_value)
        {
            string rval;
            var sessionValue = context.Request.Cookies[key.ToString()];
            if (sessionValue == null)
            {
                //session[key.ToString()] = default_value;
                rval = default_value;
            }
            else rval = sessionValue.Value;

            return rval;
        }

        public static void SetValue(this HttpContextBase context, MainCommon.SessionEnum key, string val)
        {
            HttpCookie cookie = new HttpCookie(key.ToString(), val);
            cookie.Expires = DateTime.Now.AddYears(100);
            context.Request.Cookies.Add(cookie);
            context.Response.Cookies.Add(cookie);
        }
    }
}