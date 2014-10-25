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
        public ActionResult Index()
        {
            return View("Index",new List<Region>());
        }

        public ActionResult ScavengerHunt(Models.Navigator navigator)
        {
            return View("ScavengerHunt",navigator);
        }

    }
}
