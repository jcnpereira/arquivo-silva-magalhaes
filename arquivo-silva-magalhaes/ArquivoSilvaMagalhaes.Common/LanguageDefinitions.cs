using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;
using System;

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

        public static string GetLanguage(string languageCode)
        {
            return LanguageNames.ResourceManager.GetString(languageCode);
        }

        public static bool AreCodesEquivalent(string x, string y)
        {
            x = x.ToLower();
            y = y.ToLower();

            if (x == y)
            {
                return true;
            }

            if (x.Split('-')[0] == y.Split('-')[0])
            {
                return true;
            }

            return false;
        }

        public static string GetClosestLanguageCode(params string[] languages)
        {
            languages = languages ?? new string[] { };

            foreach (var lang in languages.Where(l => !string.IsNullOrEmpty(l)))
            {
                var resultingLanguage = GetClosestLanguageCode(lang);

                if (resultingLanguage != null)
                {
                    return resultingLanguage;
                }
            }

            return DefaultLanguage;
        }

        private static string GetClosestLanguageCode(string cultureCode)
        {
            // Ensure that the culture code is valid.
            try
            {
                CultureInfo.GetCultureInfo(cultureCode);
            }
            catch
            {
                return null;
            }

            // If the language code is in the list, return it.
            if (Languages.Contains(cultureCode.ToLower()))
            {
                return cultureCode;
            }

            // Get the language code from the culture code,
            // i.e., en-US -> en, and try again.
            var language = cultureCode.Split('-')[0];

            if (Languages.Contains(language.ToLower()))
            {
                return language;
            }

            return null;
        }

        public static IEnumerable<SelectListItem> GenerateAvailableLanguageDDL(IEnumerable<string> languages)
        {
            if (languages.Count() == Languages.Count)
            {
                throw new InvalidOperationException("No more languages available.");
            }

            return Languages
                .Where(l => !languages.Contains(l))
                .Select(l => new SelectListItem
                {
                    Value = l,
                    Text = GetLanguage(l)
                })
                .ToList();
        }
    }
}