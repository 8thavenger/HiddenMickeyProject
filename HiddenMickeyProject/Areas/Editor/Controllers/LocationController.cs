using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiddenMickeyProject.Areas.Editor.Controllers
{
    public class LocationController : Controller
    {
        //
        // GET: /Editor/Location/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Editor/Location/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Editor/Location/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Editor/Location/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Editor/Location/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Editor/Location/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Editor/Location/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Editor/Location/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
