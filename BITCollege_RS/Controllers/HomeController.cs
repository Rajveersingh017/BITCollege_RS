using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace BITCollege_RS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          
            ViewBag.Message = "BIT College";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "BIT College Data Maintenance System.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}