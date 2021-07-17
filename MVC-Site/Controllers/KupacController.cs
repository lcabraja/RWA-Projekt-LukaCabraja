using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Site.Controllers
{
    public class KupacController : Controller
    {
        // GET: Kupac
        public ActionResult Index()
        {
            return View();
        }

        // GET: Kupac/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Kupac/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kupac/Create
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

        // GET: Kupac/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Kupac/Edit/5
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

        // GET: Kupac/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kupac/Delete/5
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
