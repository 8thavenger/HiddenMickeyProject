using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiddenMickeyProject.Data;

namespace HiddenMickeyProject.Controllers
{
    public class HomeController : Controller
    {
        Data.INavigationRepository repository = null;

        public HomeController(INavigationRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            return View("Index",repository.Regions());
        }

        public ActionResult ScavengerHunt(Models.Navigator navigator)
        {
            return View("ScavengerHunt",navigator);
        }

    }
}
