using CargadorTuLlanta.com.amdp.tullanta;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestAdapterProduct
{
    
    
    /// <summary>
    ///This is a test class for AdapterPrecioTest and is intended
    ///to contain all AdapterPrecioTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AdapterPrecioTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for getPrecios
        ///</summary>
        [TestMethod()]
        public void getPreciosTest()
        {
            string line = "12547          200000  01 180000  05 180000  11 170000  13 160000  15 150000  20 140000  21 130000  22 120000  23 110000  25 100000  30 100000  32 90000   33 80000   34 80000   35 70000 "; // TODO: Initialize to an appropriate value
            Precios expected = new Precios(); // TODO: Initialize to an appropriate value
            expected.CodProducto = "12547";
            expected.PrecioPublico = "200000";
            Precios actual;
            AdapterPrecio adapter = new AdapterPrecio();
            actual = (Precios)adapter.getEntity(line);

            Assert.AreEqual(expected.CodProducto, actual.CodProducto);
            Assert.AreEqual(expected.PrecioPublico, actual.PrecioPublico);
            Assert.AreEqual("140000", actual.getPrecio("20"));
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getPrecios
        ///</summary>
        [TestMethod()]
        public void getPrecios2Test()
        {

            String line = "16543          260000  01 250000  05 240000  11 230000  13 220000  15 210000  20 190000  21 180000  22 170000  ";
            Precios expected = new Precios(); // TODO: Initialize to an appropriate value
            expected.CodProducto = "16543";
            expected.PrecioPublico = "260000";
            Precios actual;

            AdapterPrecio adapter = new AdapterPrecio();
            actual = (Precios)adapter.getEntity(line);


            Assert.AreEqual(expected.CodProducto, actual.CodProducto);
            Assert.AreEqual(expected.PrecioPublico, actual.PrecioPublico);
            Assert.AreEqual("220000", actual.getPrecio("13"));
            Assert.AreEqual("170000", actual.getPrecio("22"));
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
