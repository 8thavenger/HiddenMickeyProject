using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiddenMickeyProject.Models;
using HiddenMickeyProject.Data;

namespace HiddenMickeyProject.Areas.Editor.Controllers
{
    public class LocationController : Controller
    {
        private INavigationRepository repository = null;

        public LocationController(INavigationRepository repository)
        {
            this.repository = repository;
        }

        //
        // GET: /Editor/Location/Details/5
        [HttpGet]
        public ActionResult Details(Navigator navigator)
        {
            LocationViewModel model = Utilities.ObjectFactory.CreateLocation(navigator);
            return View("Details",model);
        }

        //
        // GET: /Editor/Location/Create
        [HttpGet]
        public ActionResult Create(Navigator navigator)
        {
            LocationViewModel model = Utilities.ObjectFactory.CreateLocation(navigator);
            model.LocationId = 0;
            model.LocationName = string.Empty;
            return View("Create", model);
        }

        //
        // POST: /Editor/Location/Create

        [HttpPost]
        public ActionResult Create(LocationViewModel location)
        {
            if (this.repository.SaveLocation(location))
            {
                return RedirectToRoute("area_default", new { RegionName = location.RegionName, AreaName = location.AreaName, action = "Details" });
            }
            else
            {
                return View("Create", location);
            }
        }

        //
        // GET: /Editor/Location/Edit/5
        [HttpGet]
        public ActionResult Edit(Navigator navigator)
        {
            LocationViewModel model = Utilities.ObjectFactory.CreateLocation(navigator);
            return View("Edit", model);
        }

        //
        // POST: /Editor/Location/Edit/5

        [HttpPost]
        public ActionResult Edit(LocationViewModel location)
        {
            if (this.repository.SaveLocation(location))
            {
                return RedirectToRoute("area_default", new { RegionName = location.RegionName, AreaName = location.AreaName, action = "Details" });
            }
            else
            {
                return View("Create", location);
            }
        }

        //
        // GET: /Editor/Location/Delete/5
        [HttpGet]
        public ActionResult Delete(Navigator navigator)
        {
            LocationViewModel model = Utilities.ObjectFactory.CreateLocation(navigator);
            return View("Delete",model);
        }

        //
        // POST: /Editor/Location/Delete/5

        [HttpPost]
        public ActionResult Delete(LocationViewModel location)
        {
            if (this.repository.DeleteLocation(location))
            {
                return RedirectToRoute("area_default", new { RegionName = location.RegionName, AreaName = location.AreaName, action = "Details" });
            }
            else
            {
                return View("Create", location);
            }
        }
    }
}
