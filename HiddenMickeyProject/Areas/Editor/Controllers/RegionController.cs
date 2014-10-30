using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiddenMickeyProject.Data;
using HiddenMickeyProject.Areas.Editor;
using HiddenMickeyProject.Models;

namespace HiddenMickeyProject.Areas.Editor.Controllers
{
    public class RegionController : Controller
    {
        INavigationRepository repository = null;

        public RegionController(INavigationRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            IEnumerable<Region> model = this.repository.Regions();
            return View("Index",model);
        }

        public ViewResult Details(Navigator navigator)
        {
            Models.RegionViewModel model = Utilities.ObjectFactory.CreateRegion(navigator);
            return View("Details", model);
        }

        [HttpGet]
        public ViewResult Edit(Navigator navigator)
        {
            Models.RegionViewModel model = Utilities.ObjectFactory.CreateRegion(navigator);
            return View("Edit", model);
        }

        [HttpPost]
        public RedirectToRouteResult Edit(Region region)
        {
            if(this.repository.SaveRegon(region))
                return RedirectToAction("Index");
            else{
                Navigator navigator = Utilities.ObjectFactory.GetNavigator(region.RegionName, string.Empty, string.Empty);
                return RedirectToAction("Edit", new { navigator = navigator });
            }
        }

        [HttpGet]
        public ViewResult Create()
        {
            RegionViewModel model = new RegionViewModel();
            return View("Create", model);
        }

        [HttpPost]
        public RedirectToRouteResult Create(Region region)
        {
            if (this.repository.SaveRegon(region))
                return RedirectToAction("Index");
            else
            {
                Navigator navigator = Utilities.ObjectFactory.GetNavigator(region.RegionName, string.Empty, string.Empty);
                return RedirectToAction("Create", new { navigator = navigator });
            }
        }

        [HttpGet]
        public ViewResult Delete(Navigator navigator)
        {
            Models.RegionViewModel model = Utilities.ObjectFactory.CreateRegion(navigator);
            return View("Delete", model);
        }

        [HttpPost]
        public RedirectToRouteResult Delete(Region region)
        {
            if (this.repository.DeleteRegion(region))
                return RedirectToAction("Index");
            else
            {
                Navigator navigator = Utilities.ObjectFactory.GetNavigator(region.RegionName, string.Empty, string.Empty);
                return RedirectToAction("Delete", new { navigator = navigator });
            }
        }
    }
}
