using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            IList<AdventureWorksOBPRepo.Kupac> kupci;
            var order = DetermineOrder(Request.QueryString.Get("orderby"));
            int grad;
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

            if (!int.TryParse(Request.QueryString.Get("grad"), out grad) || grad <= 0)
            {
                kupci = Models.RepoSingleton.GetInstance().GetMultipleKupac((uint)take + 1, (uint)skip, order).Values;
            }
            else
            {
                kupci = Models.RepoSingleton.GetInstance().GetMultipleKupac(grad, (uint)take + 1, (uint)skip, order).Values;
            }

            Session["kupci-search"] = new { @grad = grad, @page = page, @take = take };

            ViewBag.remaining = kupci.Count;
            if (kupci.Count > take)
                return View(kupci.Take(kupci.Count - 1));
            else
                return View(kupci.Take(take));
        }

        private AdventureWorksOBPRepo.Repo.KupacOrderBy DetermineOrder(string queryStringRequest)
        {
            switch (queryStringRequest)
            {
                case "0":
                    return AdventureWorksOBPRepo.Repo.KupacOrderBy.IDKupacAsc;
                case "1":
                    return AdventureWorksOBPRepo.Repo.KupacOrderBy.IDKupacDesc;
                case "2":
                    return AdventureWorksOBPRepo.Repo.KupacOrderBy.ImeAsc;
                case "3":
                    return AdventureWorksOBPRepo.Repo.KupacOrderBy.ImeDesc;
                case "4":
                    return AdventureWorksOBPRepo.Repo.KupacOrderBy.PrezimeAsc;
                case "5":
                    return AdventureWorksOBPRepo.Repo.KupacOrderBy.PrezimeDesc;
                default:
                    return AdventureWorksOBPRepo.Repo.KupacOrderBy.IDKupacAsc;
            }
        }

        // GET: Kupac/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            return View(Models.RepoSingleton.GetInstance().GetKupac(id.Value));
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            Session["update-kupac"] = Models.RepoSingleton.GetInstance().GetKupac(id.Value);
            return new RedirectResult("/Kupac/Update.aspx");
        }

        public ActionResult RacunDetails(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            return View(Models.RepoSingleton.GetInstance().GetRacun(id.Value));
        }
        public ActionResult StavkaDetails(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            return View(Models.RepoSingleton.GetInstance().GetStavka(id.Value));
        }

        #region json
        public JsonResult Grad(int id)
        {
            return Json(Models.RepoSingleton.GetInstance().GetGrad(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Gradovi()
        {
            return Json(Models.RepoSingleton.GetInstance().GetMultipleGrad().Values, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GradDrzava(int id)
        {
            return Json(Models.RepoSingleton.GetInstance().GetMultipleGrad(id).Values, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Drzave()
        {
            return Json(Models.RepoSingleton.GetInstance().GetMultipleDrzava().Values, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Drzava(int id)
        {
            return Json(Models.RepoSingleton.GetInstance().GetDrzava(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
