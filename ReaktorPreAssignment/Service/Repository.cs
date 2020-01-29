using ReaktorPreAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReaktorPreAssignment.Service
{
    public static class Repository
    {
        public static Dictionary<string, PackageItem> Packages
        {
            get
            {
                if ((HttpContext.Current.Session["Packages"] == null))
                    HttpContext.Current.Session.Add("Packages", null);
                return HttpContext.Current.Session["Packages"] as Dictionary<string, PackageItem>;
            }
            set { HttpContext.Current.Session["Packages"] = value; }
        }
    }
}