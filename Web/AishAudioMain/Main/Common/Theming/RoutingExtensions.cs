using System.Web.Mvc;
using System.Web.Routing;

namespace Main.Common
{
    public static class RoutingExtensions
    {
        public static string GetAreaName(this RouteBase route)
        {
            var routeWithArea = route as IRouteWithArea;
            if (routeWithArea != null)
            {
                return routeWithArea.Area;
            }

            var castRoute = route as Route;
            if (castRoute != null && castRoute.DataTokens != null)
            {
                return castRoute.DataTokens["area"] as string;
            }

            return null;
        }

        public static string GetAreaName(this RouteData routeData)
        {
            object area;
            if (routeData.DataTokens.TryGetValue("area", out area))
            {
                return area as string;
            }

            return GetAreaName(routeData.Route);
        }

        public static string GetControllerName(this RouteData routeData)
        {
            object controller;
            if (routeData.Values.TryGetValue("controller", out controller))
            {
                return controller as string;
            }

            return null;
        }

        public static string GetActionName(this RouteData routeData)
        {
            object action;
            if (routeData.Values.TryGetValue("action", out action))
            {
                return action as string;
            }

            return null;
        }

    }
}