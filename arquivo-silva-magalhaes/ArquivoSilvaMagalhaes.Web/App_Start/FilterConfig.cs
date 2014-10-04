using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            var errAttribute = new HandleErrorAttribute();
            errAttribute.View = "~/Views/Shared/Error.cshtml";

            filters.Add(errAttribute);
        }
    }
}
