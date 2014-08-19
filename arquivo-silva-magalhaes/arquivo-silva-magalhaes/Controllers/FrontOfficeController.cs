using ArquivoSilvaMagalhaes.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class FrontOfficeController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            /*
             * Set the thread language.
             * 
             * The priority is as follows:
             * 1. The "lang" route parameter, if it exists. 
             * 
             * 2. A "lang" cookie, which was previously set, in case the "lang" parameter does not exist.
             *    
             * 3. If the cookie does not exist, the browser's most-prioritised
             *    language, sent by the "Accept-Language" header.
             */

            var languageCode = LanguageDefinitions.DefaultLanguage;

            /*var langRegex = new Regex(@"^/[a-z]{2}(-[a-z])?");
            bool pathHasLang = langRegex.IsMatch(Request.Path.ToLower());

            // We should always redirect if the path doesn't have
            // a language set in it.
            bool shouldRedirect = pathHasLang == false;

            // If the "lang" route parameter doesn't exist, results will not
            // be as expected.
            if (pathHasLang)
            // if (RouteData.Values["lang"] as string != "none-specified")
            {
                languageCode = RouteData.Values["lang"] as string;
            }
            // No route parameter. Check cookie.
            else */if (Request.Cookies["lang"] != null && !String.IsNullOrEmpty(Request.Cookies["lang"].Value))
            {
                languageCode = Request.Cookies["lang"].Value;
            }
            // None of the above use the most-prioritised language.
            else
            {
                // We split becaus of the ;q=x.y part in some of the languages.
                // we're only interested in the code, not the priority, as the list
                // is already sorted.
                languageCode = Request.UserLanguages[0].Split(';')[0];
            }

            // Try to set the language. If it fails, set to default.
            if (Thread.CurrentThread.CurrentUICulture.Name != languageCode)
            {
                // Always redirect if the language changed.
                //shouldRedirect = true;
                try
                {
                    var culture = CultureInfo.GetCultureInfo(languageCode);

                    Thread.CurrentThread.CurrentUICulture = culture;
                    Thread.CurrentThread.CurrentCulture = culture;
                }
                catch (Exception)
                {
                    var culture = CultureInfo.GetCultureInfo(LanguageDefinitions.DefaultLanguage);

                    Thread.CurrentThread.CurrentUICulture = culture;
                    Thread.CurrentThread.CurrentCulture = culture;
                }
            }

            //if (shouldRedirect && !pathHasLang)
            //{
            //    filterContext.Result = new RedirectResult(Request.Path.ToLower().Replace("backoffice", "backoffice/" + languageCode));
            //    //Response.RedirectPermanent(Request.Path.Replace("BackOffice", "BackOffice/" + languageCode), true);
            //}

            base.OnActionExecuting(filterContext);
        }
    }
}