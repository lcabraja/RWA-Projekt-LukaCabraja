using AdventureWorksOBPRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Site.Controllers
{
    public class KategorijaController : Controller
    {
        private AdventureWorksOBPRepo.Repo repo = Models.RepoSingleton.GetInstance();

        // GET: Kategorija
        public ActionResult Index()
        {
            return View(repo.GetMultipleKategorija().Values);
        }

        // GET: Kategorija/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue || id.Value <= 0)
                return RedirectToAction("Index");
            return View(repo.GetKategorija(id.Value));
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
                var naziv = collection.Get("Naziv").Trim();
                if (naziv == string.Empty)
                    throw new Exception("Must have a title");
                var kategorija = new Kategorija { Naziv = naziv };

                repo.CreateKategorija(kategorija);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Kategorija/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue || id.Value <= 0)
                return RedirectToAction("Index");
            return View(repo.GetKategorija(id.Value));
        }

        // POST: Kategorija/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var naziv = collection.Get("Naziv").Trim();
                if (naziv == string.Empty)
                    throw new Exception("Must have a title");
                var kategorija = new Kategorija { IDKategorija = id, Naziv = naziv };

                repo.UpdateKategorija(kategorija);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Kategorija/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            return View(repo.GetKategorija(id.Value));
        }

        // POST: Kategorija/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            try
            {
                repo.DeleteKategorija(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
