using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M17E_N23.Models;

namespace M17E_N23.Controllers
{
    public class HomeController : Controller
    {
        AlbunsBD bd = new AlbunsBD();
        public ActionResult Index()
        {
            return View(bd.topEscritores());
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