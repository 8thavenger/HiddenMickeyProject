using HiddenMickeyProject.Areas.Editor.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using HiddenMickeyProject.Data;
using HiddenMickeyProject.Models;
using System.Web.Mvc;
using Rhino.Mocks;

namespace HiddenMickeyProject.Tests
{
    
    
    /// <summary>
    ///This is a test class for LocationControllerTest and is intended
    ///to contain all LocationControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LocationControllerTest
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

        private INavigationRepository GenerateRepository()
        {
            INavigationRepository repository = MockRepository.GenerateStrictMock<INavigationRepository>();
            return repository;
        }

        private Navigator GenerateNavigator()
        {
            Navigator navigator = new Navigator();
            navigator.RegionId = 1;
            navigator.AreaId = 2;
            navigator.LocationId = 3;
            navigator.RegionName = "Test Region";
            navigator.AreaName = "Test Area";
            navigator.LocationName = "Test Location";
            for (int i=1;i<=5;i++)
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
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void GivenLocationViewModelCreateNewLocation()
        {
            INavigationRepository repository = GenerateRepository();
            LocationController target = new LocationController(repository);
            LocationViewModel location = Utilities.ObjectFactory.CreateLocation(GenerateNavigator());
            repository.Expect(r=>r.SaveLocation(location)).Return(true);
            ActionResult actual;
            actual = target.Create(location);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(RedirectToRouteResult));
            RedirectToRouteResult result = actual as RedirectToRouteResult;
            Assert.AreEqual("area_default", result.RouteName);
            Assert.AreEqual("Details", result.RouteValues["Action"]);
            Assert.AreEqual(location.RegionName, result.RouteValues["RegionName"]);
            Assert.AreEqual(location.AreaName, result.RouteValues["AreaName"]);
            repository.VerifyAllExpectations();
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void GivenNavigatorShowCreateLocationView()
        {
            INavigationRepository repository = GenerateRepository();
            LocationController target = new LocationController(repository);  
            Navigator navigator = GenerateNavigator();
            ActionResult actual;
            actual = target.Create(navigator);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
            ViewResult result = actual as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
            TestViewResult(navigator, result);
            LocationViewModel model = result.Model as LocationViewModel;
            Assert.AreEqual(model.LocationId, 0);
            Assert.IsTrue(String.IsNullOrEmpty(model.LocationName));
        }

        private static void TestViewResult(Navigator navigator, ViewResult result)
        {
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(LocationViewModel));
            LocationViewModel model = result.Model as LocationViewModel;
            Assert.AreEqual(model.AreaId, navigator.AreaId);
            Assert.AreEqual(model.AreaName, navigator.AreaName);
            Assert.AreEqual(model.RegionName, navigator.RegionName);
            Assert.AreEqual(model.Entries.Count, 5);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void GivenNavigatorShowDeleteView()
        {
            INavigationRepository repository = GenerateRepository();
            LocationController target = new LocationController(repository);
            Navigator navigator = GenerateNavigator();
            ActionResult actual;
            actual = target.Delete(navigator);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
            ViewResult result = actual as ViewResult;
            Assert.AreEqual("Delete", result.ViewName);
            TestViewResult(navigator, result);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void GivenLocationViewModelDeleteLocation()
        {
            INavigationRepository repository = GenerateRepository();
            LocationController target = new LocationController(repository);
            LocationViewModel location = Utilities.ObjectFactory.CreateLocation(GenerateNavigator());
            repository.Expect(r => r.DeleteLocation(location)).Return(true);
            ActionResult actual;
            actual = target.Delete(location);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(RedirectToRouteResult));
            RedirectToRouteResult result = actual as RedirectToRouteResult;
            Assert.AreEqual("area_default", result.RouteName);
            Assert.AreEqual("Details", result.RouteValues["Action"]);
            Assert.AreEqual(location.RegionName, result.RouteValues["RegionName"]);
            Assert.AreEqual(location.AreaName, result.RouteValues["AreaName"]);
            repository.VerifyAllExpectations();
        }

        /// <summary>
        ///A test for Details
        ///</summary>
        [TestMethod()]
        public void GivenLocationViewModelShowDetailView()
        {
            INavigationRepository repository = GenerateRepository();
            LocationController target = new LocationController(repository);
            Navigator navigator = GenerateNavigator();
            ActionResult actual;
            actual = target.Details(navigator);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
            ViewResult result = actual as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
            LocationViewModel model = result.Model as LocationViewModel;
            Assert.AreEqual(model.LocationId, navigator.LocationId);
            Assert.AreEqual(model.LocationName, navigator.LocationName);
            TestViewResult(navigator, result);
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod()]
        public void GivenLocationViewModelUpdateLocation()
        {
            INavigationRepository repository = GenerateRepository();
            LocationController target = new LocationController(repository);
            LocationViewModel location = Utilities.ObjectFactory.CreateLocation(GenerateNavigator());
            repository.Expect(r => r.SaveLocation(location)).Return(true);
            ActionResult actual;
            actual = target.Edit(location);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(RedirectToRouteResult));
            RedirectToRouteResult result = actual as RedirectToRouteResult;
            Assert.AreEqual("area_default", result.RouteName);
            Assert.AreEqual("Details", result.RouteValues["Action"]);
            Assert.AreEqual(location.RegionName, result.RouteValues["RegionName"]);
            Assert.AreEqual(location.AreaName, result.RouteValues["AreaName"]);
            repository.VerifyAllExpectations();
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod()]
        public void GivenNavigatorShowEditView()
        {
            INavigationRepository repository = GenerateRepository();
            LocationController target = new LocationController(repository);
            Navigator navigator = GenerateNavigator();
            ActionResult actual;
            actual = target.Edit(navigator);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
            ViewResult result = actual as ViewResult;
            Assert.AreEqual("Edit", result.ViewName);
            LocationViewModel model = result.Model as LocationViewModel;
            Assert.AreEqual(model.LocationId, navigator.LocationId);
            Assert.AreEqual(model.LocationName, navigator.LocationName);
            TestViewResult(navigator, result);
        }
    }
}
