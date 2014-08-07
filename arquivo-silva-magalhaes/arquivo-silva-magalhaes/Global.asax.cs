using ArquivoSilvaMagalhaes.App_Start;
using ArquivoSilvaMagalhaes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ArquivoSilvaMagalhaes
{
    public class MvcApplication : System.Web.HttpApplication
    {
    
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // BundleTable.EnableOptimizations = true;

            // Map AutoMapper mappings.
            // MapperConfig.RegisterMappings();

            MembershipConfig.SeedMembership();
        }

        //protected void Application_AcquireRequestState()
        //{
        //    var routes = RouteTable.Routes;

        //    var httpContext = Request.RequestContext.HttpContext;
        //    if (httpContext == null) return;

        //    var routeData = routes.GetRouteData(httpContext);

        //    var language = routeData.Values["lang"] as string;
        //    var cultureInfo = new CultureInfo(language);

        //    System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
        //    System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        //}

        

    }
}
