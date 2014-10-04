using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiddenMickeyProject.Data;
using HiddenMickeyProject.Areas.Editor;
using HiddenMickeyProject.Areas.Editor.Models;

namespace HiddenMickeyProject.Areas.Editor.Controllers
{
    public class RegionController : Controller
    {
        IRegionRepository repository = null;

        public RegionController(IRegionRepository repository)
        {
            this.repository = repository;
        }
        //
        // GET: /Editor/Region/

        public ActionResult Index()
        {
            IEnumerable<Region> model = this.repository.Regions();
            return View("Index",model);
        }

        //
        // GET: /Editor/Region/Details/5

        public ActionResult Details(int id)
        {
            Region  region = this.repository.GetRegionById(id);
            RegionModel model = new RegionModel();
            model.RegionId = region.RegionId;
            model.RegionName = region.RegionName;
            model.Areas.AddRange(this.repository.GetAreasByRegionId(id));            
            return View("Details",model);
        }

        //
        // GET: /Editor/Region/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Editor/Region/Create

        [HttpPost]
        public ActionResult Create(Region region)
        {
            try
            {
                this.repository.SaveRegon(region);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Editor/Region/Edit/5

        public ActionResult Edit(int id)
        {
            Region model = this.repository.GetRegionById(id);
            return View("Edit",model);
        }

        //
        // POST: /Editor/Region/Edit/5

        [HttpPost]
        public ActionResult Edit(Region region)
        {
            try
            {
                this.repository.SaveRegon(region);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(region);
            }
        }

        //
        // GET: /Editor/Region/Delete/5

        public ActionResult Delete(int id)
        {
            Region model = this.repository.GetRegionById(id);
            return View("Delete",model);
        }

        //
        // POST: /Editor/Region/Delete/5

        [HttpPost]
        public ActionResult Delete(Region region)
        {
            try
            {
                this.repository.DeleteRegion(region);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
