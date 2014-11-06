using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiddenMickeyProject.Models;
using HiddenMickeyProject.Data;
using System.Web.Routing;

namespace HiddenMickeyProject.Areas.Editor.Controllers
{
    public class EntryController : Controller
    {

        private INavigationRepository repository = null;

        public EntryController(INavigationRepository repository)
        {
            this.repository = repository;
        }

        //
        // GET: /Editor/Entry/Details/5
        [HttpGet]
        public ActionResult Details(Navigator navigator)
        {
            EntryViewModel model = Utilities.ObjectFactory.CreateEntry(navigator);
            return View("Details",model);
        }

        //
        // GET: /Editor/Entry/Create
        [HttpGet]
        public ActionResult Create(Navigator navigator)
        {
            EntryViewModel model = Utilities.ObjectFactory.CreateEntry(navigator);
            return View("Create",model);
        }

        //
        // POST: /Editor/Entry/Create

        [HttpPost]
        public ActionResult Create(EntryViewModel model)
        {
            if (this.repository.SaveEntry(model))
                return this.Index(model);
            else
                return View("Create", model);
        }

        //
        // GET: /Editor/Entry/Edit/5
        [HttpGet]
        public ActionResult Edit(Navigator navigator)
        {
            EntryViewModel model = Utilities.ObjectFactory.CreateEntry(navigator);
            return View("Edit",model);
        }

        //
        // POST: /Editor/Entry/Edit/5

        [HttpPost]
        public ActionResult Edit(EntryViewModel model)
        {
            if (this.repository.SaveEntry(model))
                return this.Index(model);
            else
                return View("Create", model);
        }

        //
        // GET: /Editor/Entry/Delete/5
        [HttpGet]
        public ActionResult Delete(Navigator navigator)
        {
            EntryViewModel model = Utilities.ObjectFactory.CreateEntry(navigator);
            return View("Delete",model);
        }

        //
        // POST: /Editor/Entry/Delete/5

        [HttpPost]
        public ActionResult Delete(EntryViewModel model)
        {
            if (this.repository.DeleteEntry(model))
                return this.Index(model);
            else
                return View("Create", model);
        }

        private RedirectToRouteResult Index(EntryViewModel model)
        {
            RouteValueDictionary values = new RouteValueDictionary();
            values.Add("RegionName",model.RegionName);
            values.Add("AreaName",model.AreaName);
            values.Add("LocationName",model.LocationName);
            values.Add("Action", "Details");
            return new RedirectToRouteResult("location_default", values);
        }
    }
}
