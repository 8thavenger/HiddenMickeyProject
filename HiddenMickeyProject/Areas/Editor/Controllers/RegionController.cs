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

    }
}
