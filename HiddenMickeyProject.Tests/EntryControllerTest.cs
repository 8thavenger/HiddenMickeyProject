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
    ///This is a test class for EntryControllerTest and is intended
    ///to contain all EntryControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EntryControllerTest
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
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void GivenEntryViewModelCreateNewEntry()
        {
            INavigationRepository repository = GenerateRepository();
            EntryController target = new EntryController(repository);
            EntryViewModel model = Utilities.ObjectFactory.CreateEntry(GenerateNavigator());
            repository.Expect(r => r.SaveEntry(model)).Return(true);
            ActionResult actual;
            actual = target.Create(model);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(RedirectToRouteResult));
            RedirectToRouteResult result = actual as RedirectToRouteResult;
            Assert.AreEqual("location_default", result.RouteName);
            Assert.AreEqual("Details", result.RouteValues["Action"]);
            Assert.AreEqual(model.RegionName, result.RouteValues["RegionName"]);
            Assert.AreEqual(model.AreaName, result.RouteValues["AreaName"]);
            Assert.AreEqual(model.LocationName, result.RouteValues["LocationName"]);
            repository.VerifyAllExpectations();
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void GivenNavigatorShowCreateEntryView()
        {
            INavigationRepository repository = GenerateRepository();
            EntryController target = new EntryController(repository);
            Navigator navigator = GenerateNavigator();             
            ActionResult actual;
            actual = target.Create(navigator);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
            ViewResult result = actual as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
            EntryViewModel model = result.Model as EntryViewModel;
            Assert.AreEqual(model.EntryId, 0);
            Assert.IsTrue(String.IsNullOrEmpty(model.Clue));
            Assert.IsTrue(String.IsNullOrEmpty(model.Hint));
        }

        /// <summary>
        ///A test for Delete
        ///</summary>=
        [TestMethod()]
        public void GivenNavigatorShowDeleteEntryView()
        {
            INavigationRepository repository = GenerateRepository();
            EntryController target = new EntryController(repository);
            Navigator navigator = GenerateNavigator();             
            ActionResult actual;
            actual = target.Delete(navigator);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
            ViewResult result = actual as ViewResult;
            Assert.AreEqual("Delete", result.ViewName);
            EntryViewModel model = result.Model as EntryViewModel;
            Assert.AreEqual(model.EntryId, navigator.EntryId);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void GivenEntryViewModelDeleteEntry()
        {
            INavigationRepository repository = GenerateRepository();
            EntryController target = new EntryController(repository); 
            EntryViewModel model = Utilities.ObjectFactory.CreateEntry(GenerateNavigator());
            repository.Expect(r => r.DeleteEntry(model)).Return(true); 
            ActionResult actual;
            actual = target.Delete(model);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(RedirectToRouteResult));
            RedirectToRouteResult result = actual as RedirectToRouteResult;
            Assert.AreEqual("location_default", result.RouteName);
            Assert.AreEqual("Details", result.RouteValues["Action"]);
            Assert.AreEqual(model.RegionName, result.RouteValues["RegionName"]);
            Assert.AreEqual(model.AreaName, result.RouteValues["AreaName"]);
            Assert.AreEqual(model.LocationName, result.RouteValues["LocationName"]);
            repository.VerifyAllExpectations();
        }

        /// <summary>
        ///A test for Details
        ///</summary>
        [TestMethod()]
        public void GivenNavigatorShowEntryDetailsView()
        {
            INavigationRepository repository = GenerateRepository();
            EntryController target = new EntryController(repository);
            Navigator navigator = GenerateNavigator();             
            ActionResult actual;
            actual = target.Details(navigator);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
            ViewResult result = actual as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
            EntryViewModel model = result.Model as EntryViewModel;
            Assert.AreEqual(model.EntryId, navigator.EntryId);
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod()]
        public void GivenEntryViewModelUpdateEntry()
        {
            INavigationRepository repository = GenerateRepository();
            EntryController target = new EntryController(repository);
            EntryViewModel model = Utilities.ObjectFactory.CreateEntry(GenerateNavigator());
            repository.Expect(r => r.SaveEntry(model)).Return(true); 
            ActionResult actual;
            actual = target.Edit(model);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(RedirectToRouteResult));
            RedirectToRouteResult result = actual as RedirectToRouteResult;
            Assert.AreEqual("location_default", result.RouteName);
            Assert.AreEqual("Details", result.RouteValues["Action"]);
            Assert.AreEqual(model.RegionName, result.RouteValues["RegionName"]);
            Assert.AreEqual(model.AreaName, result.RouteValues["AreaName"]);
            Assert.AreEqual(model.LocationName, result.RouteValues["LocationName"]);
            repository.VerifyAllExpectations();
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod()]
        public void GivenNavigatorShowEditEntryView()
        {
            INavigationRepository repository = GenerateRepository();
            EntryController target = new EntryController(repository); 
            Navigator navigator = GenerateNavigator();             
            ActionResult actual;
            actual = target.Edit(navigator); 
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
            ViewResult result = actual as ViewResult;
            Assert.AreEqual("Edit", result.ViewName);
            EntryViewModel model = result.Model as EntryViewModel;
            Assert.AreEqual(model.EntryId, 0);
            Assert.IsTrue(String.IsNullOrEmpty(model.Clue));
            Assert.IsTrue(String.IsNullOrEmpty(model.Hint));
        }
    }
}
