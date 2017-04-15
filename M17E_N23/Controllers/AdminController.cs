using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M17E_N23.Models;

namespace M17E_N23.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        AdminBD bd = new AdminBD();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GerirComentarios()
        {
         
            return View(bd.listaComentarios());
        }
        
        [HttpPost]
        public ActionResult GerirComentarios(AdminModel dados)
        {
            return View(bd.pesquisa(dados.comentario));
        }

        public ActionResult Apagar(int? id)
        {
       
            if (id==null) return RedirectToAction("GerirComentarios");
            try
            {
                return View(bd.lista((int)id)[0]);
            }
            catch (Exception x)
            {

                return RedirectToAction("GerirComentarios");
            }

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Apagar")]
        public ActionResult ConfirmarApagar(int? id)
        {
            bd.removerComentario((int)id);
            return RedirectToAction("GerirComentarios");
        }

        public ActionResult Aprovar(int? id)
        {
            if (id == null) return RedirectToAction("GerirComentarios");
            bd.aprovarComentario((int)id);
            return RedirectToAction("GerirComentarios");

        }

        public ActionResult Desaprovar(int? id)
        {
            if (id == null) return RedirectToAction("GerirComentarios");
            bd.desaprovarComentario((int)id);
            return RedirectToAction("GerirComentarios");

        }


    }
}