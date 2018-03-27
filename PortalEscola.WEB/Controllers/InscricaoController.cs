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

namespace PortalEscola.WEB.Controllers
{
    public class InscricaoController : Controller
    {
        private PortalEscolaContexto db = new PortalEscolaContexto();

        // GET: Inscricao
        public ActionResult Index()
        {
            var inscricoes = db.Inscricoes.Include(i => i.Curso).Include(i => i.Estudante);
            return View(inscricoes.ToList());
        }

        // GET: Inscricao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscricao inscricao = db.Inscricoes.Find(id);
            if (inscricao == null)
            {
                return HttpNotFound();
            }
            return View(inscricao);
        }

        // GET: Inscricao/Create
        public ActionResult Create()
        {
            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "NomeCurso");
            ViewBag.EstudanteId = new SelectList(db.Estudantes, "EstudanteId", "Nome");
            return View();
        }

        // POST: Inscricao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InscricaoId,CursoId,EstudanteId,Grau")] Inscricao inscricao)
        {
            if (ModelState.IsValid)
            {
                db.Inscricoes.Add(inscricao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "NomeCurso", inscricao.CursoId);
            ViewBag.EstudanteId = new SelectList(db.Estudantes, "EstudanteId", "Nome", inscricao.EstudanteId);
            return View(inscricao);
        }

        // GET: Inscricao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscricao inscricao = db.Inscricoes.Find(id);
            if (inscricao == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "NomeCurso", inscricao.CursoId);
            ViewBag.EstudanteId = new SelectList(db.Estudantes, "EstudanteId", "Nome", inscricao.EstudanteId);
            return View(inscricao);
        }

        // POST: Inscricao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InscricaoId,CursoId,EstudanteId,Grau")] Inscricao inscricao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inscricao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "NomeCurso", inscricao.CursoId);
            ViewBag.EstudanteId = new SelectList(db.Estudantes, "EstudanteId", "Nome", inscricao.EstudanteId);
            return View(inscricao);
        }

        // GET: Inscricao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscricao inscricao = db.Inscricoes.Find(id);
            if (inscricao == null)
            {
                return HttpNotFound();
            }
            return View(inscricao);
        }

        // POST: Inscricao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inscricao inscricao = db.Inscricoes.Find(id);
            db.Inscricoes.Remove(inscricao);
            db.SaveChanges();
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
