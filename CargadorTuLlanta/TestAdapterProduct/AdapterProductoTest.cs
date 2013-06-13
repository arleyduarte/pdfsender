using CargadorTuLlanta.com.amdp.tullanta;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestAdapterProduct
{
    
    
    /// <summary>
    ///This is a test class for AdapterProductoTest and is intended
    ///to contain all AdapterProductoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AdapterProductoTest
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
        ///A test for getProducto
        ///</summary>
        [TestMethod()]
        public void getProductoTest()
        {
            string line = "12547          195       60   13   1001  YOKOHAMA            070   A-DRIVE             SI300-A-A             H 80 90"; // TODO: Initialize to an appropriate value
            Producto expected = new Producto(); // TODO: Initialize to an appropriate value
            expected.Cod = "12547";
            expected.Ancho = "195";
            expected.Alto = "60";
            expected.Perfil = "13";
            expected.CodMarca = "1001";
            expected.NomMarca = "YOKOHAMA";
            expected.CodDiseno = "070";
            expected.NomDiseno = "A-DRIVE";
            expected.Promocion = "SI";
            expected.UTQG = "300-A-A";
            expected.Velocidad = "H";
            expected.Carga = "80";
            expected.Existencia = "90";
            
            Producto actual;
            AdapterProducto adapter = new AdapterProducto();
            actual = (Producto)adapter.getEntity(line);

            Assert.AreEqual(expected.Cod, actual.Cod);
            Assert.AreEqual(expected.Existencia, actual.Existencia);
            Assert.AreEqual(expected.Velocidad, actual.Velocidad);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
