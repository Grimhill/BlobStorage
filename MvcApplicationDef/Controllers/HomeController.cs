using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplicationDef.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Please login or register to use storage";

            return View();
        }

        public ActionResult AfterReg()
        {
            ViewBag.Message = "Now you can start use storage";

            return View();
        }
    }
}
