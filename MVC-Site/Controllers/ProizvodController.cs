using AdventureWorksOBPRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Site.Controllers
{
    public class ProizvodController : Controller
    {
        private AdventureWorksOBPRepo.Repo repo = Models.RepoSingleton.GetInstance();
        // GET: Proizvod
        public ActionResult Index()
        {
            IList<AdventureWorksOBPRepo.Proizvod> proizvodi;
            int page;
            int skip;
            int take;

            if (!int.TryParse(Request.QueryString.Get("take"), out take))
            {
                take = 15;
            }
            if (!int.TryParse(Request.QueryString.Get("page"), out page))
            {
                page = 0;
            }
            skip = take * page;

            proizvodi = Models.RepoSingleton.GetInstance().GetMultipleProizvod((uint)take + 1, (uint)skip).Values;

            ViewBag.remaining = proizvodi.Count;
            if (proizvodi.Count > take)
                return View(proizvodi.Take(proizvodi.Count - 1));
            else
                return View(proizvodi.Take(take));
        }

        // GET: Proizvod/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue || id.Value <= 0)
                return RedirectToAction("Index");
            return View(repo.GetProizvod(id.Value));
        }

        public JsonResult Potkategorije(int? id)
        {
            return id.HasValue ? Json(repo.GetMultiplePotkategorija(id.Value).Values, JsonRequestBehavior.AllowGet) : Json(repo.GetMultiplePotkategorija().Values, JsonRequestBehavior.AllowGet);
        }
        // GET: Proizvod/Create
        public ActionResult Create()
        {
            ViewBag.Boje = Enum.GetValues(typeof(Boja));
            ViewBag.Kategorije = repo.GetMultipleKategorija();
            return View();
        }

        // POST: Proizvod/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var naziv = collection.Get("Naziv").Trim();
                var brojProizvoda = collection.Get("BrojProizvoda").Trim();
                var boja = (Boja)int.Parse(collection.Get("Boja"));
                var idpotkategorija = int.Parse(collection.Get("Boja"));
                var potkategorija = repo.GetPotkategorija(idpotkategorija);
                int minimalnaKolicinaNaSkladistu = int.Parse(collection.Get("MinimalnaKolicinaNaSkladistu")); ;
                decimal cijenaBezPDV = decimal.Parse(collection.Get("CijenaBezPDV"));

                if (naziv == string.Empty || brojProizvoda == string.Empty)
                    throw new Exception("form invalid");

                Proizvod proizvod = new Proizvod
                {
                    Naziv = naziv,
                    Boja = boja,
                    BrojProizvoda = brojProizvoda,
                    Potkategorija = potkategorija,
                    MinimalnaKolicinaNaSkladistu = minimalnaKolicinaNaSkladistu,
                    CijenaBezPDV = cijenaBezPDV
                };

                repo.CreateProizvod(proizvod);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private static List<T> GetEnumList<T>()
        {
            var enumList = Enum.GetValues(typeof(T))
                .Cast<T>().ToList();
            return enumList;
        }

        // GET: Proizvod/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue || id.Value <= 0)
                return RedirectToAction("Index");
            var proizvod = repo.GetProizvod(id.Value);

            ViewBag.SelectedBoja = (int)proizvod.Boja;
            ViewBag.SelectedKategorija = proizvod.Potkategorija?.Kategorija.IDKategorija ?? 0;
            ViewBag.SelectedPotkategorija = proizvod.Potkategorija?.IDPotkategorija ?? 0;

            ViewBag.Boje = GetEnumList<Boja>();
            ViewBag.Kategorije = repo.GetMultipleKategorija().Values;
            ViewBag.Potkategorije = repo.GetMultiplePotkategorija(proizvod.Potkategorija?.Kategorija.IDKategorija ?? 0).Values;

            return View(proizvod);
        }

        // POST: Proizvod/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var naziv = collection.Get("Naziv").Trim();
                var brojProizvoda = collection.Get("BrojProizvoda").Trim();
                var boja = (Boja)int.Parse(collection.Get("Boja"));
                var idpotkategorija = int.Parse(collection.Get("Potkategorija"));
                var potkategorija = idpotkategorija == 0 ? null : repo.GetPotkategorija(idpotkategorija);
                int minimalnaKolicinaNaSkladistu = int.Parse(collection.Get("MinimalnaKolicinaNaSkladistu")); ;
                decimal cijenaBezPDV = decimal.Parse(collection.Get("CijenaBezPDV"));

                if (naziv == string.Empty || brojProizvoda == string.Empty)
                    throw new Exception("form invalid");

                Proizvod proizvod = new Proizvod
                {
                    IDProizvod = id,
                    Naziv = naziv,
                    Boja = boja,
                    BrojProizvoda = brojProizvoda,
                    Potkategorija = potkategorija,
                    MinimalnaKolicinaNaSkladistu = minimalnaKolicinaNaSkladistu,
                    CijenaBezPDV = cijenaBezPDV
                };

                repo.UpdateProizvod(proizvod);

                return RedirectToAction("Index");
            }
            catch
            {
                var proizvod = repo.GetProizvod(id);

                ViewBag.SelectedBoja = (int)proizvod.Boja;
                ViewBag.SelectedKategorija = proizvod.Potkategorija?.Kategorija.IDKategorija ?? 0;
                ViewBag.SelectedPotkategorija = proizvod.Potkategorija?.IDPotkategorija ?? 0;

                ViewBag.Boje = GetEnumList<Boja>();
                ViewBag.Kategorije = repo.GetMultipleKategorija().Values;
                ViewBag.Potkategorije = repo.GetMultiplePotkategorija(proizvod.Potkategorija?.Kategorija.IDKategorija ?? 0).Values;

                return View(repo.GetProizvod(id));
            }
        }

        // GET: Proizvod/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue || id.Value <= 0)
                return RedirectToAction("Index");
            return View(repo.GetProizvod(id.Value));
        }

        // POST: Proizvod/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            try
            {
                repo.DeleteProizvod(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(repo.GetProizvod(id));
            }
        }

        public ActionResult KategorijaDetails(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            return RedirectToAction("Details", "Kategorija", new { id = id });
        }

        public ActionResult PotkategorijaDetails(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            return RedirectToAction("Details", "Potkategorija", new { id = id });
        }
    }
}
