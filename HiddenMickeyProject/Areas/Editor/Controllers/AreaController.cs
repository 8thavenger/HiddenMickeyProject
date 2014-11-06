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
        public ViewResult Create(Navigator navigator)
        {
            AreaViewModel model = Utilities.ObjectFactory.CreateArea(navigator);
            model.AreaName = String.Empty;
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(AreaViewModel area)
        {
            if (this.repository.SaveArea(area))
            {
                return RedirectToRoute("region_default", new { RegionName = area.RegionName, action = "Details" });
            }
            else
            {
                return View("Create", area);
            }
        }


        [HttpGet]
        public ViewResult Details(Navigator navigator)
        {
            AreaViewModel model = Utilities.ObjectFactory.CreateArea(navigator);
            return View("Details",model);
        }

        [HttpGet]
        public ViewResult Edit(Navigator navigator)
        {
            AreaViewModel model = Utilities.ObjectFactory.CreateArea(navigator);
            return View("Edit",model);
        }

        [HttpPost]
        public ActionResult Edit(AreaViewModel area)
        {
            if (this.repository.SaveArea(area))
            {
                return RedirectToRoute("region_default", new { RegionName = area.RegionName, Action = "Details" });
            }
            else
            {
                return View("Edit", area);
            }
        }

        [HttpGet]
        public ActionResult Delete(Navigator navigator)
        {
            AreaViewModel model = Utilities.ObjectFactory.CreateArea(navigator);
            return View("Delete",model);
        }

        [HttpPost]
        public ActionResult Delete(AreaViewModel model)
        {
            if (this.repository.DeleteArea(model))
            {
                return RedirectToRoute("region_default", new { RegionName = model.RegionName, Action = "Details" });
            }
            else
            {
                return View("Delete", model);
            }
        }

    }
}
