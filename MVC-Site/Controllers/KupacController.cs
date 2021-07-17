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
            var order = DetermineOrder(Request.QueryString.Get("order"));
            int page;
            int skip;
            int take;
            if (int.TryParse(Request.QueryString.Get("page"), out page) &&
                int.TryParse(Request.QueryString.Get("perpage"), out take) &&
                page >= 0 && take >= 0)
            {
                skip = take * page;
                return View(Models.RepoSingleton.GetInstance().GetMultipleKupac((uint)take, (uint)skip, order).Values);
            }
            return View(Models.RepoSingleton.GetInstance().GetMultipleKupac(30, 0, order).Values);
        }

        private AdventureWorksOBPRepo.Repo.KupacOrderBy DetermineOrder(string queryStringRequest)
        {
            switch (queryStringRequest)
            {
                case "IDKupacAsc":
                    return AdventureWorksOBPRepo.Repo.KupacOrderBy.IDKupacAsc;
                case "IDKupacDesc":
                    return AdventureWorksOBPRepo.Repo.KupacOrderBy.IDKupacDesc;
                case "ImeAsc":
                    return AdventureWorksOBPRepo.Repo.KupacOrderBy.ImeAsc;
                case "ImeDesc":
                    return AdventureWorksOBPRepo.Repo.KupacOrderBy.ImeDesc;
                case "PrezimeAsc":
                    return AdventureWorksOBPRepo.Repo.KupacOrderBy.PrezimeAsc;
                case "PrezimeDesc":
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
    }
}
