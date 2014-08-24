using ArquivoSilvaMagalhaes.Web.Libs.RouteHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Web.Libs.Extensions
{
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// Maps a route that is not localized, but needs to be.
        /// </summary>
        /// <returns></returns>
        public static Route MapRouteToLocalizeRedirect(this RouteCollection routes, string name, string url, object defaults)
        {
            var redirectRoute = new Route(url, new RouteValueDictionary(defaults), new LocalizationRedirectRouteHandler());
            routes.Add(name, redirectRoute);

            return redirectRoute;
        }

        /// <summary>
        /// Maps a localized route with the specified
        /// name, url, defaults and constraints.
        /// </summary>
        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return routes.MapLocalizedRoute(name, url, defaults, new { });
        }

        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            var route = routes.MapRoute(name, url, defaults, constraints);

            route.RouteHandler = new LocalizedRouteHandler();

            return route;
        }

        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {

            var route = routes.MapRoute(name, url, defaults, constraints, namespaces);

            route.RouteHandler = new LocalizedRouteHandler();

            return route;
        }
    }
}
