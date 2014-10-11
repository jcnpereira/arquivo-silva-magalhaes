using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using System.Threading;
using System.Globalization;

namespace ArquivoSilvaMagalhaes.Tests.Localization
{
    [TestClass]
    public class TranslationViewModelTests
    {
        /// <summary>
        /// Ensure that, if the translation exists for the current language,
        /// then it's returned.
        /// </summary>
        [TestMethod]
        public void TranslationViewModel_GetsTranslation()
        {
            var c = new Collection();
            c.Notes = "Notas";
            c.Translations.Add(new CollectionTranslation
                {
                    LanguageCode = "pt",
                    Description = "Português"
                });

            c.Translations.Add(new CollectionTranslation
            {
                LanguageCode = "en",
                Description = "Inglês"
            });

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");

            var vm = new TranslatedViewModel<Collection, CollectionTranslation>(c);

            Assert.AreEqual("Inglês", vm.Translation.Description);
        }

        /// <summary>
        /// Ensure that, when the translation
        /// doesn't exist for the desired language,
        /// the default language is returned instead.
        /// </summary>
        [TestMethod]
        public void TranslationViewModel_GetsDefaultTranslation()
        {
            var c = new Collection();
            c.Notes = "Notas";
            c.Translations.Add(new CollectionTranslation
            {
                LanguageCode = "pt",
                Description = "Português"
            });

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");

            var vm = new TranslatedViewModel<Collection, CollectionTranslation>(c);

            Assert.AreEqual("Português", vm.Translation.Description);
        }

    }
}
