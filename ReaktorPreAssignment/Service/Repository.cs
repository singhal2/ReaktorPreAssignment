using ReaktorPreAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReaktorPreAssignment.Service
{
    public static class Repository
    {
        public static SortedDictionary<string, PackageItem> Packages
        {
            get
            {
                if ((HttpContext.Current.Session["Packages"] == null))
                    HttpContext.Current.Session.Add("Packages", null);
                return HttpContext.Current.Session["Packages"] as SortedDictionary<string, PackageItem>;
            }
            set { HttpContext.Current.Session["Packages"] = value; }
        }
    }
}