using System.Linq;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Utilitites
{
    // This logic was taken from http://stackoverflow.com/questions/9604049/asp-net-mvc-3-validation-exclude-some-field-validation-in-tryupdatemodel

    /// <summary>
    /// Validates only the attributes which were bound to
    /// a controller action.
    /// </summary>
    public class ValidateOnlyIncomingValuesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;
            var valueProvider = filterContext.Controller.ValueProvider;

            var keysWithNoIncomingValue = modelState.Keys.Where(x => !valueProvider.ContainsPrefix(x));

            foreach (var key in keysWithNoIncomingValue)
            {
                modelState[key].Errors.Clear();
            }
        }
    }
}