using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Site.Controllers
{
    public class KategorijaController : Controller
    {
        // GET: Kategorija
        public ActionResult Index()
        {
            return View(Models.RepoSingleton.GetInstance().GetMultipleKategorija().Values);
        }

        // GET: Kategorija/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Kategorija/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kategorija/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert  logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Kategorija/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Kategorija/Edit/5
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

        // GET: Kategorija/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kategorija/Delete/5
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
