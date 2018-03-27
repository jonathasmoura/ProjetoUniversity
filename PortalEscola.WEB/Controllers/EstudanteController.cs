using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PortalEscola.WEB.Contexto;
using PortalEscola.WEB.Models;
using PagedList;

namespace PortalEscola.WEB.Controllers
{
    public class EstudanteController : Controller
    {
        private PortalEscolaContexto db = new PortalEscolaContexto();

        // GET: Estudante
        public ViewResult Index(string sorteioOrdem, string pesquisarPalavra, string filtroAtual, int? page)
        {
            ViewBag.SorteioAtual = sorteioOrdem;
            ViewBag.ParamSorteioNome = string.IsNullOrEmpty(sorteioOrdem) ? "name_desc" : "";
            ViewBag.ParamSorteioData = sorteioOrdem == "Data" ? "data_desc" : "Data";

            if (pesquisarPalavra != null)
            {
                page = 1;
            }
            else
            {
                pesquisarPalavra = filtroAtual;
            }
            ViewBag.FiltroAtual = pesquisarPalavra;

            var estudantes = from es in db.Estudantes
                             select es;

            if (!string.IsNullOrEmpty(pesquisarPalavra))
            {
                estudantes = estudantes.Where(es => es.Nome.ToUpper().Contains(pesquisarPalavra.ToUpper()) || es.Sobrenome.ToUpper().Contains(pesquisarPalavra.ToUpper()));
            }

            switch (sorteioOrdem)
            {
                case "name_desc":
                    estudantes = estudantes.OrderByDescending(es => es.Nome);
                    break;

                case "Data":
                    estudantes = estudantes.OrderBy(es => es.DataInscricao);
                    break;

                case "data_desc":
                    estudantes = estudantes.OrderByDescending(es => es.DataInscricao);
                    break;

                default:
                    estudantes = estudantes.OrderBy(es => es.Nome);
                    break;
            }

            int totalPagina = 3;
            int numeroPaginas = (page ?? 1);
            return View(estudantes.ToPagedList(numeroPaginas, totalPagina));
        }

        // GET: Estudante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudante estudante = db.Estudantes.Find(id);
            if (estudante == null)
            {
                return HttpNotFound();
            }
            return View(estudante);
        }

        // GET: Estudante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estudante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstudanteId,Nome,Sobrenome,DataInscricao")] Estudante estudante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Estudantes.Add(estudante);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Não é possível salvar as alterações.\nSe persistir os problemas,contate o administrador!");
            }
            return View(estudante);

        }

        // GET: Estudante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudante estudante = db.Estudantes.Find(id);
            if (estudante == null)
            {
                return HttpNotFound();
            }
            return View(estudante);
        }

        // POST: Estudante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var atualizarEstudante = db.Estudantes.Find(id);
            if (TryUpdateModel(atualizarEstudante, "",
                new string[] { "Nome", "Sobrenome", "DataInscricao" }))
            {
                try
                {
                    db.Entry(atualizarEstudante).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Não é possível salvar as alterações.\nTente novamente,se persistir os problemas contate o administrador!");
                }
            }

            return View(atualizarEstudante);
        }

        // GET: Estudante/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Falha na exclusão!\nTente novamente,se perssistir sintomas contate administrador,";
            }
            Estudante estudante = db.Estudantes.Find(id);
            if (estudante == null)
            {
                return HttpNotFound();
            }
            return View(estudante);
        }

        // POST: Estudante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Estudante estudanteParaDeletar = new Estudante();  //db.Estudantes.Find(id);
                db.Entry(estudanteParaDeletar).State = EntityState.Deleted;                                                    //Estudantes.Remove(estudante);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
