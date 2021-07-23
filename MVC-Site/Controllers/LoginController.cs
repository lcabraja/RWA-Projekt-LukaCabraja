using AdventureWorksOBPRepo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Site.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            var cookie = Request.Cookies.Get("Login");
            if (cookie != null && cookie.Expires > DateTime.Now)
            {
                Session["Login"] = new { @ID = cookie["ID"], @Username = cookie["Username"] };
                return RedirectToAction("Index", "Kupac");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {

            var username = collection.Get("username");
            var password = collection.Get("password");
            var presentedlogindata = new LoginData { Username = username, Password = password };

            var dblogindata = Models.RepoSingleton.GetInstance().GetLoginData(presentedlogindata);

            if (dblogindata.IDLoginData > 0 && BCrypt.Net.BCrypt.Verify(password, dblogindata.Password))
            {
                HttpCookie cookie = new HttpCookie("Login");
                cookie["ID"] = dblogindata.IDLoginData.ToString();
                cookie["Username"] = username;
                cookie.Expires = DateTime.Now.AddMinutes(10);
                Response.SetCookie(cookie);

                Session["Login"] = new { @ID = dblogindata.IDLoginData, @Username = dblogindata.Username };
                return RedirectToAction("Index", "Kupac");
            }
            else
            {
                return View();
            }
        }
    }
}