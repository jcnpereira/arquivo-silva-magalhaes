using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Globalization;
using System.Diagnostics;

namespace ArquivoSilvaMagalhaes.Tests
{
    [TestClass]
    public class TranslationTests
    {
        [TestMethod]
        public void TestLanguageName()
        {
            //var c = Thread.CurrentThread.CurrentCulture;

            //Assert.AreEqual(CultureInfo.GetCultureInfo("pt-PT").DisplayName, "Português (Portugal)");

            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            //Assert.AreEqual(CultureInfo.GetCultureInfo("pt-PT").NativeName, "Portuguese (Portugal)");

            //Thread.CurrentThread.CurrentCulture = c;
        }
    }
}
