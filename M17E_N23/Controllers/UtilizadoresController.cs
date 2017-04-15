using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M17E_N23.Models;

namespace M17E_N23.Controllers
{
    public class UtilizadoresController : Controller
    {
        UtilizadoresBD bd = new UtilizadoresBD();
        // GET: Utilizadores
        public ActionResult Index()
        {
            if (Session["perfil"] == null || !Session["perfil"].Equals(1))
            {
                RedirectToAction("Index", "Home");

            }
            return View();
        }

        [Authorize]
        public ActionResult Gerir()
        {
            if (Session["perfil"] == null || !Session["perfil"].Equals(1))
            {
                RedirectToAction("Index", "Home");

            }
            return View(bd.lista());
        }
        [Authorize]
        [HttpPost]
        public ActionResult Gerir(AlbunsModel dados)
        {
            return View(bd.pesquisa(dados.nome));
        }

        [Authorize]
        public ActionResult Adicionar()
        {
            if (Session["perfil"] == null || !Session["perfil"].Equals(1))
            {
                RedirectToAction("Index", "Home");

            }
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(UtilizadoresModel dados)
        {
            if (ModelState.IsValid)
            {
              
               bd.adicionarUtilizadores(dados);

                 return RedirectToAction("Gerir");
            }
            return View(dados);
        }

        [Authorize]
        public ActionResult Apagar(string id)
        {
            if (Session["perfil"] == null || !Session["perfil"].Equals(1))
            {
                RedirectToAction("Index", "Home");

            }
            if (String.IsNullOrEmpty(id)) return RedirectToAction("Gerir");
            try
            {
                return View(bd.lista(id)[0]);
            }
            catch (Exception)
            {

                return RedirectToAction("Gerir");
            }
            
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Apagar")]
        public ActionResult ConfirmarApagar(string id)
        {
            bd.removerUtilizador(id);
            return RedirectToAction("Gerir");
        }

        [Authorize]
        public ActionResult Editar(string id)
        {
            if (Session["perfil"] == null || !Session["perfil"].Equals(1))
            {
                RedirectToAction("Index", "Home");

            }
            if (String.IsNullOrEmpty(id)) return RedirectToAction("Gerir");
            return View(bd.lista(id)[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(UtilizadoresModel dados)
        {
            if (ModelState.IsValid)
            {

            




                bd.atualizarUtilizador(dados);
                return RedirectToAction("Gerir");
            }
            return View(dados);
        }


    }
}