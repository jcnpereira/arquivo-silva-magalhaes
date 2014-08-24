using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArquivoSilvaMagalhaes.Common;

namespace ArquivoSilvaMagalhaes.Tests.Localization
{
    [TestClass]
    public class LanguageResolvingTests
    {
        [TestMethod]
        public void LanguageResolver_GetsLanguage_IfExactMatch()
        {
            var cultureCode = "en";

            Assert.AreEqual("en", LanguageDefinitions.GetClosestLanguageCode(cultureCode));
        }

        [TestMethod]
        public void LanguageResolver_GetsClosestLanguage_IfItDoesntExist()
        {
            var cultureCode = "en-US";

            Assert.AreEqual("en", LanguageDefinitions.GetClosestLanguageCode(cultureCode));
        }

        [TestMethod]
        public void LanguageResolver_GetsDefaultLanguage_IfNoneMatch()
        {
            var cultureCode = "de-DE";

            Assert.AreEqual("pt", LanguageDefinitions.GetClosestLanguageCode(cultureCode));
        }

        [TestMethod]
        public void LanguageResolver_GetsFallbackLanguage_IfFallbacksAreAccepted()
        {
            var otherLanguages = new string[] { "de-DE", "de", "fr", "en-US", "en", "pt" };

            Assert.AreEqual("en", LanguageDefinitions.GetClosestLanguageCode(otherLanguages));
        }

        [TestMethod]
        public void LanguageResolver_GetsDefaultLanguage_IfFallbacksAreNotSupported()
        {
            var otherLanguages = new string[] { "de-DE", "de", "fr-FR" };

            Assert.AreEqual("pt", LanguageDefinitions.GetClosestLanguageCode(otherLanguages));
        }

        [TestMethod]
        public void LanguageResolver_GetsDefaultLanguage_IfNoneSupplied()
        {
            Assert.AreEqual("pt", LanguageDefinitions.GetClosestLanguageCode(null));
        }

        [TestMethod]
        public void LanguageResolver_MatchesExactCodes()
        {
            Assert.AreEqual(true, LanguageDefinitions.AreCodesEquivalent("pt", "pt"));
            Assert.AreEqual(true, LanguageDefinitions.AreCodesEquivalent("pt-pt", "pt-PT"));
        }

        [TestMethod]
        public void LanguageResolver_MatchesSimilarCodes()
        {
            Assert.AreEqual(true, LanguageDefinitions.AreCodesEquivalent("en-US", "en-GB"));
            Assert.AreEqual(true, LanguageDefinitions.AreCodesEquivalent("pt", "pt-BR"));
        }

        [TestMethod]
        public void LanguageResolver_DoesntMatchDifferentCodes()
        {
            Assert.AreEqual(false, LanguageDefinitions.AreCodesEquivalent("en", "de-DE"));
            Assert.AreEqual(false, LanguageDefinitions.AreCodesEquivalent("de-DE", "fr-CA"));
        }
    }
}
