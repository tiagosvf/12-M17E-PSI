using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace M17E_N23.Controllers
{
    public class LogOutController : Controller
    {     // GET: LogOut
        public ActionResult Index()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("LoggedOut");
        }
        public ActionResult LoggedOut()
        {
            return View();
        }
    }
}