using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M17E_N23.Models;

namespace M17E_N23.Controllers
{
   
    public class AlbunsController : Controller
    {
        AlbunsBD bd = new AlbunsBD();
        // GET: Albuns

        [Authorize]
        public ActionResult Adicionar()
        {
               ViewBag.listaEscritores = bd.listaEscritores();
            
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(AlbunsModel dados, HttpPostedFileBase capa)
        {
            if (ModelState.IsValid)
            {
                if (capa == null)
                {
                    ModelState.AddModelError("", "Indique uma capa para o album");
                    return View(dados);
                }
                int id = bd.adicionarAlbuns(dados);

                string caminho = Server.MapPath("~/Imagens/") + id + ".jpg";
                capa.SaveAs(caminho);

                return RedirectToAction("index");
            }else
            {
                var erros = ModelState.Values.SelectMany(v => v.Errors);
            }

            ViewBag.listaEscritores = bd.listaEscritores();
            return View(dados);
        }


        public ActionResult Index()
        {
            return View(bd.lista());
        }
        [HttpPost]
        public ActionResult Index(AlbunsModel dados)
        {
                    return View(bd.pesquisa(dados.nome));
        }

        public ActionResult Reviews(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(404);
            return View(bd.lista((int)id)[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reviews(AlbunsModel dados)
        {
            
               int id = bd.adicionarComentario(dados);
                
                            
            
            return View(bd.lista(id)[0]);
        }


        [Authorize]
        public ActionResult Editar(int? id)
        {
            if (id == null) return RedirectToAction("index");
            return View(bd.lista((int)id)[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(AlbunsModel dados, HttpPostedFileBase capa)
        {
            if (ModelState.IsValid)
            {

                if (capa != null)
                {
                    
                    string caminho = Server.MapPath("~/Imagens/") + dados.nr + ".jpg";
                    capa.SaveAs(caminho);
                }
             

               

             
                bd.atualizarAlbum(dados);
                return RedirectToAction("index");
            }
            return View(dados);
        }

        public ActionResult Apagar(int? id)
        {
            if (id == null) return RedirectToAction("index");
            return View(bd.lista((int)id)[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Apagar")]
        public ActionResult ConfirmarApagar(int? id)
        {
            bd.apagarAlbum((int)id);
            return RedirectToAction("index");
        }

      

        public JsonResult ComentariosJson(string id)
        {
            if (String.IsNullOrEmpty(id))
                return Json(null, JsonRequestBehavior.AllowGet);
            return Json(bd.comentariosAlbum(int.Parse(id)), JsonRequestBehavior.AllowGet);
        }
    }
}