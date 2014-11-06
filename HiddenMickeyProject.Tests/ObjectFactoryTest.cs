using HiddenMickeyProject.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using HiddenMickeyProject.Models;
using HiddenMickeyProject.Data;

namespace HiddenMickeyProject.Tests
{
    
    
    /// <summary>
    ///This is a test class for ObjectFactoryTest and is intended
    ///to contain all ObjectFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ObjectFactoryTest
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



        private Navigator GenerateNavigator()
        {
            Navigator navigator = new Navigator();
            navigator.RegionId = 1;
            navigator.AreaId = 2;
            navigator.LocationId = 3;
            navigator.RegionName = "Test Region";
            navigator.AreaName = "Test Area";
            navigator.LocationName = "Test Location";
            navigator.EntryId = 2;
            for (int i = 1; i <= 5; i++)
            {
                Entry entity = new Entry();
                entity.EntryId = i;
                entity.LocationId = navigator.LocationId;
                entity.Clue = "Test Clue #" + i.ToString();
                entity.Hint = "Test Hint #" + i.ToString();
                navigator.Entries.Add(entity);
            }
            return navigator;
        }

        /// <summary>
        ///A test for CreateEntry
        ///</summary>
        [TestMethod()]
        public void CreateEntryTest()
        {
            Navigator navigator = GenerateNavigator();
            EntryViewModel actual;
            Entry entry = navigator.Entry;
            Assert.IsNotNull(entry);
            actual = ObjectFactory.CreateEntry(navigator);
            Assert.IsNotNull(actual);
            Assert.AreEqual(entry.Clue, actual.Clue);
            Assert.AreEqual(entry.EntryId, actual.EntryId);
            Assert.AreEqual(entry.Hint, actual.Hint);
            Assert.AreEqual(navigator.LocationId, actual.LocationId);
            Assert.AreEqual(navigator.AreaName, actual.AreaName);
            Assert.AreEqual(navigator.LocationName, actual.LocationName);
            Assert.AreEqual(navigator.RegionName, actual.RegionName);


        }
    }
}
