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
        IRegionRepository repository = null;

        public RegionController(IRegionRepository repository)
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
            return View("Details", navigator);
        }

        [HttpGet]
        public ViewResult Edit(Navigator navigator)
        {
            return View("Edit", navigator);
        }

        [HttpPost]
        public RedirectToRouteResult Edit(Region region)
        {
            if(this.repository.SaveRegon(region))
                return RedirectToAction("Index");
            else{
                Navigator navigator = new Navigator();
                navigator.RegionId = region.RegionId;
                navigator.RegionName = region.RegionName;
                return RedirectToAction("Edit", new { navigator = navigator });
            }
        }

        [HttpGet]
        public ViewResult Create()
        {
            Navigator navigator = new Navigator();
            return View("Create", navigator);
        }

        [HttpPost]
        public RedirectToRouteResult Create(Region region)
        {
            if (this.repository.SaveRegon(region))
                return RedirectToAction("Index");
            else
            {
                Navigator navigator = new Navigator();
                navigator.RegionId = region.RegionId;
                navigator.RegionName = region.RegionName;
                return RedirectToAction("Create", new { navigator = navigator });
            }
        }

        [HttpGet]
        public ViewResult CreateArea(string regionName)
        {
            Area model = new Area();
            model.RegionId = this.repository.Regions().DefaultIfEmpty(new Region()).FirstOrDefault(r => r.RegionName == regionName).RegionId;
            return View("CreateArea", model);
        }

        [HttpPost]
        public ActionResult CreateArea(Area area)
        {
            try
            {
                this.repository.SaveArea(area);
                Models.Navigator model = new Navigator();
                model.RegionName = this.repository.GetRegionById(area.RegionId).RegionName;
                return RedirectToAction("Details", model);
            }
            catch
            {
                return View("CreateArea", area);
            }            
        }

        [HttpGet]
        public ViewResult Delete(Navigator navigator)
        {
            return View("Delete", navigator);
        }

        [HttpPost]
        public RedirectToRouteResult Delete(Region region)
        {
            if (this.repository.DeleteRegion(region))
                return RedirectToAction("Index");
            else
            {
                Navigator navigator = new Navigator();
                navigator.RegionId = region.RegionId;
                navigator.RegionName = region.RegionName;
                return RedirectToAction("Delete", new { navigator = navigator });
            }
        }
    }
}
