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
            FileService service = new FileService();
            service.ReadFile();
            return View();
        }

        private void ReadFile()
        {
            throw new NotImplementedException();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}