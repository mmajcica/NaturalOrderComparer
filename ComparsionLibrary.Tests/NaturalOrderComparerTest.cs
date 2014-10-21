using NaturalSortOrder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace ComparsionLibrary.Tests
{
    /// <summary>
    ///This is a test class for NaturalOrderComparerTest and is intended
    ///to contain all NaturalOrderComparerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NaturalOrderComparerTest
    {
        #region TestContext
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion

        [TestMethod]
        [Description(@"Testing to check if the decimals in the strings are actually treated as decimals
                       and if they are sorted as they should as they were actually decimals and not strings.")]
        public void NocDecimalTest()
        {
            string[] unsorted = new string[20]
                {
                    "v1.0",
                    "v1.9",
                    "v1.91",
                    "v1.99",
                    "v1.901",
                    "v1.1",
                    "v1.301",
                    "v1.8",
                    "v1.6",
                    "v1.6",
                    "v1.2",
                    "v1.22",
                    "v1.31",
                    "v1.11",
                    "v1.3",
                    "v1.6001",
                    "v1.23",
                    "v1.24",
                    "v1.14",
                    "v1.111"
                };

            string[] expected = new string[20]
                {
                    "v1.0",
                    "v1.1",
                    "v1.11",
                    "v1.111",
                    "v1.14",
                    "v1.2",
                    "v1.22",
                    "v1.23",
                    "v1.24",
                    "v1.3",
                    "v1.301",
                    "v1.31",
                    "v1.6",
                    "v1.6",
                    "v1.6001",
                    "v1.8",
                    "v1.9",
                    "v1.901",
                    "v1.91",
                    "v1.99"
                };

            Array.Sort(unsorted, new NaturalOrderComparer("."));

            CollectionAssert.AreEquivalent(expected, unsorted);
        }

        [TestMethod()]
        public void ScDecimalTest()
        {
            string[] unsorted = new string[20]
                {
                    "v1.0",
                    "v1.9",
                    "v1.91",
                    "v1.99",
                    "v1.901",
                    "v1.1",
                    "v1.301",
                    "v1.8",
                    "v1.6",
                    "v1.6",
                    "v1.2",
                    "v1.22",
                    "v1.31",
                    "v1.11",
                    "v1.3",
                    "v1.6001",
                    "v1.23",
                    "v1.24",
                    "v1.14",
                    "v1.111"
                };

            string[] expected = new string[20]
                {
                    "v1.0",
                    "v1.1",
                    "v1.11",
                    "v1.111",
                    "v1.14",
                    "v1.2",
                    "v1.22",
                    "v1.23",
                    "v1.24",
                    "v1.3",
                    "v1.301",
                    "v1.31",
                    "v1.6",
                    "v1.6",
                    "v1.6001",
                    "v1.8",
                    "v1.9",
                    "v1.901",
                    "v1.91",
                    "v1.99"
                };

            Array.Sort(unsorted, StringComparer.InvariantCulture);

            CollectionAssert.AreEquivalent(expected, unsorted);
        }

        [TestMethod()]
        public void NocIgnoreDecimals()
        {
            string[] unsorted = new string[20]
                {
                    "v1.0",
                    "v1.9",
                    "v1.91",
                    "v1.99",
                    "v1.901",
                    "v1.1",
                    "v1.301",
                    "v1.8",
                    "v1.6",
                    "v1.6",
                    "v1.2",
                    "v1.22",
                    "v1.31",
                    "v1.11",
                    "v1.3",
                    "v1.6001",
                    "v1.23",
                    "v1.24",
                    "v1.14",
                    "v1.111"
                };

            string[] expected = new string[20]
                {
                    "v1.0",
                    "v1.1",
                    "v1.2",
                    "v1.3",
                    "v1.6",
                    "v1.6",
                    "v1.8",
                    "v1.9",
                    "v1.11",
                    "v1.14",
                    "v1.22",
                    "v1.23",
                    "v1.24",
                    "v1.31",
                    "v1.91",
                    "v1.99",
                    "v1.111",
                    "v1.301",
                    "v1.901",
                    "v1.6001"
                };

            Array.Sort(unsorted, new NaturalOrderComparer(true));

            CollectionAssert.AreEquivalent(expected, unsorted);
        }

        /// <summary>
        /// We will check if the decimal separator is treated
        /// properly in different situations.
        /// </summary>
        [TestMethod()]
        public void NocCheckTreatmentOfDecimalSeparator()
        {
            string[] unsorted = new string[20]
                {
                    ".0",
                    ".11",
                    ".91",
                    ".2",
                    "1.901",
                    ".1",
                    "1,301",
                    "1.8",
                    "2.6.32",
                    "2.6.31",
                    "1.6.33",
                    "1.6.303",
                    "1.06.303",
                    "1.601.54",
                    "1.3",
                    "1.301",
                    "1.23",
                    "1.24",
                    "1.14",
                    "1.111"
                };

            string[] expected = new string[20]
                {
                    ".0",
                    ".1",
                    ".2",
                    ".11",
                    ".91",
                    "1,301",
                    "1.06.303",
                    "1.111",
                    "1.14",
                    "1.23",
                    "1.24",
                    "1.3",
                    "1.301",
                    "1.6.33",
                    "1.6.303",
                    "1.601.54",
                    "1.8",
                    "1.901",
                    "2.6.31",
                    "2.6.32"                    
                };

            Array.Sort(unsorted, new NaturalOrderComparer("."));

            CollectionAssert.AreEquivalent(expected, unsorted);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void NocCheckOrdering()
        {
            string[] unsorted = new string[10]
                {
                    "My File.txt",
                    "My File (4).txt",
                    "My File (41).txt",
                    "My File (552).txt",
                    "My File (31).txt",
                    "My File (52).txt",
                    "My File (10).txt",
                    "My File (200).txt",
                    "My File (3000).txt",
                    "My File (3).txt"
                };

            string[] expected = new string[10]
                {
                    "My File (3).txt",
                    "My File (4).txt",
                    "My File (10).txt",
                    "My File (31).txt",
                    "My File (41).txt",
                    "My File (52).txt",
                    "My File (200).txt",
                    "My File (552).txt",
                    "My File (3000).txt",
                    "My File.txt"
                };

            Array.Sort(unsorted, new NaturalOrderComparer(true));

            CollectionAssert.AreEquivalent(expected, unsorted);
        }
    }
}
