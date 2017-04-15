using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M17E_N23;
using M17E_N23.Models;
using System.Web.Security;

namespace M17E_N23.Controllers
{
    public class LoginController : Controller
    {
        LoginBD bd = new LoginBD();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel dados)
        {
            if (ModelState.IsValid)
            {
                UtilizadoresModel utilizador = bd.validarLogin(dados);
                if (utilizador == null)
                {
                    ModelState.AddModelError("", "Login falhou. Tente novamente.");
                    return View(dados);
                }
                else
                {
                    Session["perfil"] = utilizador.perfil;
                    Session["nome"] = utilizador.nome;
                    FormsAuthentication.SetAuthCookie(utilizador.nome, false);

                    if (Request.QueryString["ReturnUrl"] == null)
                        return RedirectToAction("Index", "Home");
                    else
                        return Redirect(Request.QueryString["ReturnUrl"].ToString());
                }
            }
            return View(dados);
        }
    }
}