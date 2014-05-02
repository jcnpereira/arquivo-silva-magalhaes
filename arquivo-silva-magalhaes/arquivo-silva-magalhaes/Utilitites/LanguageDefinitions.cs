using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace ArquivoSilvaMagalhaes.Utilitites
{
    public class LanguageDefinitions
    {
        public const string DefaultLanguage = "pt";

        public static List<string> Languages = new List<string>
        {
            "pt", 
            "en"
        };

        //public static string GetLanguageNameForCurrentLanguage(string languageCode)
        //{
        //    return new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name).Na
        //}

        public static string GetLanguageName(string languageCode)
        {
            return CultureInfo.GetCultureInfo(languageCode).NativeName;
        }
    }
}