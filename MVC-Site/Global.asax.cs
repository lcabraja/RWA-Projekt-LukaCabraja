using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC_Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_BeginRequest(Object source, EventArgs e)
        {
            if (Context.Request.Path.Contains("Login"))
                return;

            var cookie = Context.Request.Cookies.Get("Login");
            if (cookie == null)
            {
                Response.Redirect("/Login");
            }
        }
        void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            var code = (error is HttpException) ? (error as HttpException).GetHttpCode() : 500;

            Response.Clear();
            Server.ClearError();

            string path = Request.Path;
            Context.RewritePath(string.Format("~/Default.aspx", code), false);
            IHttpHandler httpHandler = new MvcHttpHandler();
            httpHandler.ProcessRequest(Context);
            Context.RewritePath(path, false);
        }
    }
}
