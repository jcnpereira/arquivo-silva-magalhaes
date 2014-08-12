using ArquivoSilvaMagalhaes.Utilitites;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArquivoSilvaMagalhaes.Tests
{
    [TestClass]
    public class HtmlEncodeTest
    {
        [TestMethod]
        public void TestEncoding()
        {
            var attr = new HtmlEncodeAttribute
            {
                ForbiddenTags = "script",
                ThrowOnForbidden = false
            };

            var htmlStr = "<p>Olá, mundo!!<script>alert('boo');</script></p>";
            bool x = attr.IsValid(htmlStr);

            Assert.AreEqual("<p>Olá, mundo!!</p>", htmlStr);

        }
    }
}
