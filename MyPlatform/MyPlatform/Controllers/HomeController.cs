using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPlatform.Controllers
{
    public class HomeController : Controller
    {
        MyPlatform.BLL.Sys_Tables bll = new BLL.Sys_Tables();
        public ActionResult Index()
        {
            bll.Test();
            return View();
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