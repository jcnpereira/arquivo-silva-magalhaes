using System.Web.Mvc;
using System.Web.Routing;
using ArquivoSilvaMagalhaes.Web.Libs.Extensions;

namespace ArquivoSilvaMagalhaes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{Id}",
            //    //url: "{controller}/{action}/{Id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new string[] { "ArquivoSilvaMagalhaes.Controllers" }//,
            //    //constraints: new { lang = "[a-zA-Z]{2}(-[a-zA-Z])?"}
            //);

            routes.MapLocalizedRoute("Default",
                url: "{culture}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { culture = "[a-zA-Z]{2}(-[a-zA-Z]{2})?" },
                namespaces: new string[] { "ArquivoSilvaMagalhaes.Controllers" });

            routes.MapRouteToLocalizeRedirect("RedirectToLocalize",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
