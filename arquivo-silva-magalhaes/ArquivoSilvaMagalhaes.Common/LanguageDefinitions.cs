using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

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
        ///     Gets the translated language name for the specified language code.
        /// </summary>
        /// <param name="languageCode">
        ///     The language code to obtain the language name from.
        /// </param>
        /// <returns>
        ///     The language name.
        /// </returns>
        public static string GetLanguage(string languageCode)
        {
            return LanguageNames.ResourceManager.GetString(languageCode);
        }

        /// <summary>
        ///     Returns whether the two specified codes are equivalent or not. Two codes are
        ///     equivalent if they are exactly equal or if the first component of each code ("pt"
        ///     for "pt-PT") are equal.
        /// </summary>
        /// <param name="x">
        ///     The first code to test.
        /// </param>
        /// <param name="y">
        ///     The second code to test.
        /// </param>
        /// <returns>
        ///     True if the codes are equal, false otherwise.
        /// </returns>
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

        /// <summary>
        ///     Returns the closest-supported language code for the specified list of language
        ///     codes. If none of the supplied language codes is supported or the list is empty, the
        ///     default language is returned.
        /// </summary>
        /// <param name="languages">
        ///     The languages to test.
        /// </param>
        /// <returns>
        ///     One of the language codes, or the default if none are supported.
        /// </returns>
        public static string GetClosestLanguageCode(params string[] languages)
        {
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

            // Get the language code from the culture code, i.e., en-US -> en, and try again.
            var language = cultureCode.Split('-')[0];

            if (Languages.Contains(language.ToLower()))
            {
                return language;
            }

            return null;
        }

        /// <summary>
        ///     Generates a select list with all the languages that are not specified. If all
        ///     languages are taken, InvalidOperationException is thrown.
        /// </summary>
        /// <param name="languages">
        ///     The languages which are to be removed, as they are already translated.
        /// </param>
        /// <returns>
        ///     A list of languages which have yet to have translations.
        /// </returns>
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