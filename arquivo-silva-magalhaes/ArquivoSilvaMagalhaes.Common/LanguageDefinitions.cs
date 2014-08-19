using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace ArquivoSilvaMagalhaes.Common
{



    public class LanguageDefinitions
    {
        public const string DefaultLanguage = "pt";

        public static List<string> Languages = new List<string>
        {
            "pt", 
            "en"
        };

        /// <summary>
        /// Sets the current thread's culture for localization purposes.
        /// The order for setting the thread's language is as follows:
        /// 1. The value of the "lang" cookie.
        /// 2. 
        /// parameter on 
        /// </summary>
        /// <param name="routeData"></param>
        public static void SetThreadLanguage(RouteData routeData, HttpCookie langCookie = null)
        {
            var lang = routeData.Values["lang"] as string ?? "pt";

            if (Thread.CurrentThread.CurrentUICulture.Name != lang)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            }
        }

        public static string GetLanguage(string languageCode)
        {
            return LanguageNames.ResourceManager.GetString(languageCode);
        }

        //public static string GetLanguageNameForCurrentLanguage(string languageCode)
        //{
        //    return new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name).Na
        //}

        public static string GetLanguageName(string languageCode)
        {
            return CultureInfo.GetCultureInfo(languageCode).NativeName;
        }

        public static IEnumerable<SelectListItem> GenerateAvailableLanguageDDL(params string[] languages)
        {
            return Languages.Where(l => !languages.Contains(l)).Select(l => new SelectListItem
                {
                    Value = l,
                    Text = GetLanguage(l)
                })
                .ToList();
        }
    }
}