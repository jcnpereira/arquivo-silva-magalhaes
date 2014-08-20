using System.Web.Mvc;
using System.Web.Routing;

namespace ArquivoSilvaMagalhaes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{Id}",
                //url: "{controller}/{action}/{Id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ArquivoSilvaMagalhaes.Controllers" }//,
                //constraints: new { lang = "[a-zA-Z]{2}(-[a-zA-Z])?"}
            );

            //routes.MapRoute(
            //    name: "Localized",
            //    url: "{lang}/{controller}/{action}/{Id}",
            //    defaults: new { lang = UrlParameter.Optional, controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    constraints: new { lang = "^[a-zA-Z]{2}(-[a-zA-Z]+)?$" },
            //    namespaces: new string[] { "ArquivoSilvaMagalhaes.Controllers" }
            //);
        }
    }
}
