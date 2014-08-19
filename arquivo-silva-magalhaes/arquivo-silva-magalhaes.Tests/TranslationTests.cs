//using ArquivoSilvaMagalhaes.Models;
//using ArquivoSilvaMagalhaes.Common;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Diagnostics;
//using System.Globalization;
//using System.Threading;

//namespace ArquivoSilvaMagalhaes.Tests
//{
//    class MockTranslateableEntity : TranslateableEntity<MockEntityTranslation>
//    {

//        public int PropA { get; set; }

//        public string I18nPropA
//        {
//            get
//            {
//                return GetTranslatedValueOrDefault("I18nPropA");
//            }

//            set
//            {
//                SetTranslatedValue("I18nPropA", value);
//            }
//        }

//        public int MyProperty { get; set; }
//    }

//    class MockEntityTranslation : EntityTranslation
//    {
//        public string I18nPropA { get; set; }
//    }

//    [TestClass]
//    public class TranslationTests
//    {
//        [TestMethod]
//        public void TestLanguageName()
//        {
//            Assert.AreEqual(Thread.CurrentThread.CurrentCulture.Name, "pt-PT");
//        }
//    }

//    [TestClass]
//    public class EntityTranslationTests
//    {
//        [TestMethod]
//        public void TestGetValue()
//        {
//            var entity = new MockTranslateableEntity();

//            entity.Translations.Add(new MockEntityTranslation 
//            { 
//                LanguageCode = LanguageDefinitions.DefaultLanguage,
//                I18nPropA = "Português"
//            });

//            entity.Translations.Add(new MockEntityTranslation
//            {
//                LanguageCode = "en",
//                I18nPropA = "English"
//            });

//            Trace.WriteLine(entity.Translations.GetType().GetGenericArguments()[0].Name);

//            Trace.WriteLine("For language code 'pt', value should be 'Português'.");
//            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt");
//            Assert.AreEqual("Português", entity.I18nPropA);

//            Trace.WriteLine("For language code 'en', value should be 'English'.");
//            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
//            Assert.AreEqual("English", entity.I18nPropA);

//            Trace.WriteLine("For language code 'fr', value should be 'Português', as there's no 'fr' in the translations.");
//            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr");
//            Assert.AreEqual("Português", entity.I18nPropA);

//            Trace.WriteLine("Adding 'fr' to the translations.");
//            entity.I18nPropA = "Français";

//            Trace.WriteLine("After adding 'fr' to the translations, value shoud be 'Français'.");
//            Assert.AreEqual("Français", entity.I18nPropA);

//            Trace.WriteLine("Entity translation count should be 3 (pt, en, fr).");
//            Assert.AreEqual(3, entity.Translations.Count);

//            Trace.WriteLine("After changing locale to 'pt', value should be 'Português'.");
//            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt");
//            Assert.AreEqual("Português", entity.I18nPropA);
//        }
//    }
//}
