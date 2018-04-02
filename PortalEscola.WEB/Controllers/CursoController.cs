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
    public class CursoController : Controller
    {
        private PortalEscolaContexto db = new PortalEscolaContexto();

        // GET: Curso
        public ActionResult Index(string palavraNomeCredit, string pesqPalavra)
        {

            ViewBag.NomeCursoSel = String.IsNullOrEmpty(palavraNomeCredit) ? "nome_curso" : "";
            ViewBag.CreditCursoSel = palavraNomeCredit == "Credit" ? "credit_desc" : "Credit";

            var cursos = from c in db.Cursos
                         select c;

            if (!String.IsNullOrEmpty(pesqPalavra))
            {
                cursos = cursos.Where(c => c.NomeCurso.Contains(pesqPalavra));
            }

            switch (palavraNomeCredit)
            {
                case "nome_curso":
                    cursos = cursos.OrderByDescending(c => c.NomeCurso);
                    break;

                case "Credit":
                    cursos = cursos.OrderBy(c => c.Creditos);
                    break;

                case "credit_desc":
                    cursos = cursos.OrderByDescending(c => c.Creditos);
                    break;

                default:
                    cursos = cursos.OrderBy(c => c.NomeCurso);
                    break;
            }

            return View(cursos.ToList());
        }

        // GET: Curso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // GET: Curso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Curso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CursoId,NomeCurso,Creditos")] Curso curso)
        {
            if (ModelState.IsValid)
            {


                db.Cursos.Add(curso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(curso);
        }

        // GET: Curso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // POST: Curso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CursoId,NomeCurso,Creditos")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(curso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(curso);
        }

        // GET: Curso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Curso curso = db.Cursos.Find(id);
            db.Cursos.Remove(curso);
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
