using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdventureWorksOBPRepo;

namespace MVC_Site.Controllers
{
    public class PotkategorijaController : Controller
    {
        private AdventureWorksOBPRepo.Repo repo = Models.RepoSingleton.GetInstance();

        // GET: Potkategorija
        public ActionResult Index()
        {
            return View(repo.GetMultiplePotkategorija().Values);
        }

        // GET: Potkategorija/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            return View(repo.GetPotkategorija(id.Value));
        }

        // GET: Potkategorija/Create
        public ActionResult Create()
        {
            ViewBag.Kategorije = repo.GetMultipleKategorija().Values;
            return View();
        }

        // POST: Potkategorija/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var naziv = collection.Get("Naziv");
                var kategorija = repo.GetKategorija(int.Parse(collection.Get("Kategorija")));
                var potkategorija = new Potkategorija { Naziv = naziv, Kategorija = kategorija };

                repo.CreatePotkategorija(potkategorija);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Potkategorija/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue || id.Value <= 0)
                return RedirectToAction("Index");

            var potkategorija = repo.GetPotkategorija(id.Value);

            ViewBag.Selected = potkategorija.Kategorija.IDKategorija;
            ViewBag.Kategorije = repo.GetMultipleKategorija().Values;
            return View(potkategorija);
        }

        // POST: Potkategorija/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            try
            {
                var naziv = collection.Get("Naziv");
                var kategorija = repo.GetKategorija(int.Parse(collection.Get("Kategorija")));
                var potkategorija = new Potkategorija { IDPotkategorija = id, Naziv = naziv, Kategorija = kategorija };

                repo.UpdatePotkategorija(potkategorija);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Kategorije = repo.GetMultipleKategorija().Values;
                return View(repo.GetPotkategorija(id));
            }
        }

        // GET: Potkategorija/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            return View(repo.GetPotkategorija(id.Value));
        }

        // POST: Potkategorija/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            try
            {
                repo.DeletePotkategorija(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
