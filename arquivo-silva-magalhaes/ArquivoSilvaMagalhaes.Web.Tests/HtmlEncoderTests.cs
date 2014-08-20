using ArquivoSilvaMagalhaes.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArquivoSilvaMagalhaes.Tests
{
    [TestClass]
    public class HtmlEncoderTests
    {
        [TestMethod]
        public void HtmlEncoder_RemovesScriptTags()
        {
            var htmlStr = "<p>Olá, mundo!!<script>alert('boo');</script></p>";
            var expectedString = "<p>Olá, mundo!!</p>";

            var outputString = HtmlEncoder.Encode(htmlStr, "script");

            Assert.AreEqual(expectedString, outputString);
        }

        [TestMethod]
        public void HtmlEncoder_RemovesUppercaseScriptTags()
        {
            var htmlStr = "<p>Olá, mundo!!<SCRIPT>alert('boo');</SCRIPT></p>";
            var expectedString = "<p>Olá, mundo!!</p>";

            var outputString = HtmlEncoder.Encode(htmlStr, "script");

            Assert.AreEqual(expectedString, outputString);
        }

        [TestMethod]
        public void HtmlEncoder_RemovesWeirdScriptTags()
        {
            var htmlStr = "<p>Olá, mundo!!<sCrIpT>alert('boo');</sCrIpT></p>";
            var expectedString = "<p>Olá, mundo!!</p>";

            var outputString = HtmlEncoder.Encode(htmlStr, "script");

            Assert.AreEqual(expectedString, outputString);
        }

        [TestMethod]
        public void HtmlEncoder_RemovesRootScriptTag()
        {
            var htmlStr = "<script>alert('boo');</script>";
            var expectedString = "";

            var outputString = HtmlEncoder.Encode(htmlStr, "script");

            Assert.AreEqual(expectedString, outputString);
        }
    }
}
