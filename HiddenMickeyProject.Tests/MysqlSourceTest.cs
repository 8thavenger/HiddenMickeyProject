using HiddenMickeyProject.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HiddenMickeyProject.Tests
{
    
    
    /// <summary>
    ///This is a test class for MysqlSourceTest and is intended
    ///to contain all MysqlSourceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MysqlSourceTest
    {
        private const string CONNECTION_STRING = "server=519c8110-1102-4817-a396-a3af017a9c00.mysql.sequelizer.com;database=db519c811011024817a396a3af017a9c00;uid=zhnubynhfntiilzz;pwd=mZ5o3gZCaNyzMFmWedTitPwiMxTfh5nhnau5kiApUWJo4G4S6HSHpgSDBUHCuUvd";

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
        ///A test for GetAreaById
        ///</summary>
        [TestMethod()]
        public void GetAreaByIdTest()
        {
            MysqlSource target = new MysqlSource(CONNECTION_STRING);  
            int areaId = 6;  
            Area actual;
            actual = target.GetAreaById(areaId);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetAreasByRegionId
        ///</summary>
        [TestMethod()]
        public void GetAreasByRegionIdTest()
        {
            MysqlSource target = new MysqlSource(CONNECTION_STRING);  
            int id = 3;  
            IEnumerable<Area> actual;
            actual = target.GetAreasByRegionId(id);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count() > 0);
        }

        /// <summary>
        ///A test for GetEntriesByLocationId
        ///</summary>
        [TestMethod()]
        public void GetEntriesByLocationIdTest()
        {
            MysqlSource target = new MysqlSource(CONNECTION_STRING);  
            int locationId = 2;  
            IEnumerable<Entry> actual;
            actual = target.GetEntriesByLocationId(locationId);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count() > 0);
        }

        /// <summary>
        ///A test for GetEntryById
        ///</summary>
        [TestMethod()]
        public void GetEntryByIdTest()
        {
            MysqlSource target = new MysqlSource(CONNECTION_STRING); 
            int entryId = 1; 
            Entry actual;
            actual = target.GetEntryById(entryId);
            Assert.IsNotNull(actual);
            Assert.IsFalse(String.IsNullOrEmpty(actual.Clue), "Clue has no value");
            Assert.IsFalse(String.IsNullOrEmpty(actual.Hint), "Hint has no value");
        }

        /// <summary>
        ///A test for GetLocationById
        ///</summary>
        [TestMethod()]
        public void GetLocationByIdTest()
        {
            MysqlSource target = new MysqlSource(CONNECTION_STRING);  
            int locationId = 1; 
            Location actual;
            actual = target.GetLocationById(locationId);
            Assert.IsNotNull(actual);
            Assert.IsFalse(String.IsNullOrEmpty(actual.LocationName), "Location Name is Empty");
            Assert.AreNotEqual(actual.AreaId, 0, "Area Id is not assigned");
            Assert.AreEqual(actual.LocationId, locationId, "Location Id is incorrect");
        }

        /// <summary>
        ///A test for GetLocationsByAreaId
        ///</summary>
        [TestMethod()]
        public void GetLocationsByAreaIdTest()
        {
            MysqlSource target = new MysqlSource(CONNECTION_STRING);  
            int areaId = 5;  
            IEnumerable<Location> actual;
            actual = target.GetLocationsByAreaId(areaId);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count() > 0);
        }

        /// <summary>
        ///A test for GetRegionById
        ///</summary>
        [TestMethod()]
        public void GetRegionByIdTest()
        {
            MysqlSource target = new MysqlSource(CONNECTION_STRING);  
            int id = 2;  
            Region actual;
            actual = target.GetRegionById(id);
            Assert.IsFalse(String.IsNullOrEmpty(actual.RegionName), "Location Name is Empty");
            Assert.AreEqual(actual.RegionId, id, "Region Id is not assigned");
        }

        /// <summary>
        ///A test for Regions
        ///</summary>
        [TestMethod()]
        public void RegionsTest()
        {
            MysqlSource target = new MysqlSource(CONNECTION_STRING);  
            IEnumerable<Region> actual;
            actual = target.Regions();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count() > 0);
        }
    }
}
