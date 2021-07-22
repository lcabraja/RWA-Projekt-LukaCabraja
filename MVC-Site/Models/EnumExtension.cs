using AdventureWorksOBPRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Site
{
    public static class EnumExtension
    {
        public static string ToFullName(this Boja boja)
        {
            switch (boja)
            {
                case Boja.SrebrnaCrna:
                    return "Srebrna/Crna";
                case Boja.Bijela:
                case Boja.Crna:
                case Boja.Crvena:
                case Boja.Plava:
                case Boja.Siva:
                case Boja.Srebrna:
                case Boja.Sarena:
                case Boja.Zuta:
                    return boja.ToString();
                default:
                    return "No Color";
            }
        }
    }
}