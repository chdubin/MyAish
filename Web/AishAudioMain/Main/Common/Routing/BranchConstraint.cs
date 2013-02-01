using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using MainEntity.Models.Portal;

namespace Main.Common.Routing
{
    public class PortalConstraint : IRouteConstraint
    {
        private string[][] _aliases;

        public PortalConstraint(PortalAlias[] aliases)
        {
            _aliases = aliases.OrderByDescending(a => a.alias.Length)
                .Select(a => a.alias.Split(new[] { '/' }, 2))
                .Where(a => a.Length > 1).ToArray();
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var val = (string)values[parameterName];
            var exist = _aliases.Where(a => string.Compare(a[1], val, true) == 0 && string.Compare(httpContext.Request.Url.Host, a[0], true) == 0).Any();

            return exist;
        }
    }
}