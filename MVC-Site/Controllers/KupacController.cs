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

            kupci = Models.RepoSingleton.GetInstance().GetMultipleKupac((uint)take + 1, (uint)skip, order).Values;
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
        public ActionResult Details(int id)
        {
            return View(Models.RepoSingleton.GetInstance().GetKupac(id));
        }

        public ActionResult Edit(int id)
        {
            Session["update-kupac"] = Models.RepoSingleton.GetInstance().GetKupac(id);
            return new RedirectResult("/Kupac/Update.aspx");
        }

        public JsonResult Grad()
        {
            return Json(Models.RepoSingleton.GetInstance().GetMultipleGrad().Values, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GradDrzava(int idDrzava)
        {
            return Json(Models.RepoSingleton.GetInstance().GetMultipleGrad(idDrzava).Values, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Drzava()
        {
            return Json(Models.RepoSingleton.GetInstance().GetMultipleDrzava().Values, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Drzava(int idDrzava)
        {
            return Json(Models.RepoSingleton.GetInstance().GetDrzava(idDrzava), JsonRequestBehavior.AllowGet);
        }

        
    }
}
