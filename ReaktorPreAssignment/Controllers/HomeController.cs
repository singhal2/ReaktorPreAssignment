using ReaktorPreAssignment.Models;
using ReaktorPreAssignment.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ReaktorPreAssignment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Load packages if repo is null
            if (Repository.Packages == null)
            {
                Parser service = new Parser();
                var packages = service.GetData();
                Repository.Packages = packages;
            }

            return View(Repository.Packages);
        }

        public ActionResult ShowInfo(string packageKey)
        {
            //Find selected package
            PackageItem selectedPackage = Repository.Packages[packageKey];
            return View("Info", selectedPackage);
        }
    }
}