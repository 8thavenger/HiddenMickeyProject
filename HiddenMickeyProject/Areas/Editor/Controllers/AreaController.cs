using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiddenMickeyProject.Data;
using HiddenMickeyProject.Models;

namespace HiddenMickeyProject.Areas.Editor.Controllers
{
    public class AreaController : Controller
    {
        INavigationRepository repository = null;

        public AreaController(INavigationRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ViewResult Details(Navigator navigator)
        {
            return View("Details",navigator);
        }

        [HttpGet]
        public ViewResult Edit(Navigator navigator)
        {
            return View("Edit",navigator);
        }

        [HttpPost]
        public RedirectToRouteResult Edit(Area area, string regionName)
        {
            this.repository.SaveArea(area);
            return RedirectToRoute("region_default", new { RegionName = regionName, Action="Details"});
        }

        [HttpGet]
        public ActionResult AddLocation(Navigator navigator)
        {
            return View("AddLocation",navigator);
        }

        [HttpPost]
        public RedirectToRouteResult AddLocation(Location location, string regionName, string areaName)
        {
            this.repository.SaveLocation(location);
            return RedirectToRoute("area_default", new { RegionName = regionName, AreaName= areaName });
        }

        [HttpGet]
        public ActionResult Delete(Navigator navigator)
        {
            return View("Delete",navigator);
        }

        [HttpPost]
        public RedirectToRouteResult Delete(Area area, string regionName)
        {
            this.repository.DeleteArea(area);
            return RedirectToRoute("region_default", new { RegionName = regionName, Action="Details"});
        }

    }
}
