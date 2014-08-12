using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice
{
    public class BackOfficeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BackOffice";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    "BackOffice_default",
            //    "BackOffice/{lang}/{controller}/{action}/{Id}",
            //    defaults: new { /*lang = "pt-PT",*/ controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    constraints: new { lang = "[a-zA-Z]{2}-[a-zA-Z]" }
            //);

            context.MapRoute(
                "BackOffice_default",
                "BackOffice/{controller}/{action}/{Id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}