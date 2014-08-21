using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using System.Threading;
using System.Globalization;

namespace ArquivoSilvaMagalhaes.Tests.Feature
{
    [TestClass]
    public class TranslationViewModelTests
    {
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
