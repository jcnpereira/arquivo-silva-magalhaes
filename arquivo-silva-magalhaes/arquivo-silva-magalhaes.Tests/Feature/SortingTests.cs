using ArquivoSilvaMagalhaes.Utilitites;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace ArquivoSilvaMagalhaes.Tests.Feature
{
    /// <summary>
    /// 
    /// The entity codes used to catalog entities
    /// in the archive contain both numbers and chars.
    /// 
    /// A regular .NET string sorter cannot operate on such
    /// conditions, because:
    ///  a) Being strings, they can contain chars, which means
    ///     int.Parse() cannot be used;
    ///  b) Even if the strings only contained digits, "2" > "10",
    ///     because of the way that chars are compared.
    ///     
    /// The sorter must be able to sort digit strings that contain
    /// chars in them in such a way that:
    ///  a) "2" < "10",
    ///  b) "2" < "2a" < "3"
    /// 
    /// </summary>
    [TestClass]
    public class SortingTests
    {
        private string[] StringArrayFromObjects(params object[] items)
        {
            return items.Select(i => i.ToString()).ToArray();
        }

        [TestMethod]
        public void AlphaNumericSorter_SortsNumbersOnly()
        {
            var comparator = new AlphaNumericComparator();

            var items = 
                StringArrayFromObjects(1, 2, 6, 4, 7, 10, 5, 3, 9);

            var expected = 
                StringArrayFromObjects(1, 2, 3, 4, 5, 6, 7, 9, 10);

            Trace.WriteLine(String.Format("Items: {0}", String.Join(", ", items)));
            Trace.WriteLine(String.Format("Expected: {0}", String.Join(", ", expected)));

            Array.Sort(items, comparator);

            Assert.IsTrue(expected.SequenceEqual(items));
        }

        [TestMethod]
        public void AlphaNumericSorter_SortsNumbersWithChars()
        {
            var comp = new AlphaNumericComparator();

            Assert.IsTrue(comp.Compare("2", "10") < 0);
            Assert.IsTrue(comp.Compare("2a", "2") > 0);
            Assert.IsTrue(comp.Compare("3", "2a") > 0);
            Assert.IsTrue(comp.Compare("003", "3") == 0);
            Assert.IsTrue(comp.Compare("0000", "00000000000000000001") < 0);
        }

        [TestMethod]
        public void CodeComparer_SortsCodes()
        {
            var comp = new CodeComparer();

            var unsortedCodes = StringArrayFromObjects("ASM-1-01A", "ASM-001-1", "ASM-003-30");
            var sortedCodes = StringArrayFromObjects("ASM-001-1", "ASM-1-01A", "ASM-003-30");

            Array.Sort(unsortedCodes, comp);

            Assert.IsTrue(unsortedCodes.SequenceEqual(sortedCodes));
            
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void CodeComparer_ThrowsOnIncompatibleCodeTypes()
        {
            var comp = new CodeComparer();

            var invalidCodeList = StringArrayFromObjects("ASM-1", "ASM-001-1", "ASM-003-30");

            Array.Sort(invalidCodeList, comp);
        }
    }
}
