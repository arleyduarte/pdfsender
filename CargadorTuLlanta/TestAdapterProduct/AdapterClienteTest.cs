using CargadorTuLlanta.com.amdp.tullanta;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestAdapterProduct
{
    
    
    /// <summary>
    ///This is a test class for AdapterClienteTest and is intended
    ///to contain all AdapterClienteTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AdapterClienteTest
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
        ///A test for getCliente
        ///</summary>
        [TestMethod()]
        public void getClienteTest()
        {
            string line = "860353630      5 DISTRILLANTAS LTDA                      10010 AV 15 # 122-35                SANTA BARBARA                 6230976             3172340976          distrillantas@hotmail.com     PEDRO PEREZ                   DISTRILLANTAS                 DIS12345                      35 01  "; // TODO: Initialize to an appropriate value
            Cliente expected = new Cliente(); // TODO: Initialize to an appropriate value
            expected.Cod = "860353630";
            expected.Nombre = "DISTRILLANTAS LTDA";
            expected.Estado = "1";
            expected.PwdWeb = "DIS12345";
            Cliente actual;

            AdapterCliente adapter = new AdapterCliente();
            actual = (Cliente)adapter.getEntity(line);
            Assert.AreEqual(expected.Cod, actual.Cod);
            Assert.AreEqual(expected.Nombre, actual.Nombre);
            Assert.AreEqual(expected.PwdWeb, actual.PwdWeb);
            Assert.AreEqual(expected.Estado, actual.Estado);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
