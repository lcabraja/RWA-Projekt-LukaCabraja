using AdventureWorksOBPRepo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MVC_Site.Models
{
    public static class RepoSingleton
    {
        private static Repo obj = null;
        private static readonly object padLock = new object();
        private static string cs = ConfigurationManager.ConnectionStrings["cloud"].ConnectionString;

        public static Repo GetInstance()
        {
            lock (padLock)
            {
                if (obj == null)
                {
                    obj = new Repo(cs);
                }
            }
            return obj;
        }
    }
}
