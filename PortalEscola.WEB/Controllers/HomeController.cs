using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalEscola.WEB.Contexto;
using PortalEscola.WEB.ViewModels;

namespace PortalEscola.WEB.Controllers
{
    public class HomeController : Controller
    {
        private PortalEscolaContexto Db = new PortalEscolaContexto();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<GrupoDataInscricao> data = from estudante in Db.Estudantes
                                                  group estudante by estudante.DataInscricao into dataGrupo
                                                  select new GrupoDataInscricao()
                                                  {
                                                      DataInscricao = dataGrupo.Key,
                                                      QuantidadeEstudantes = dataGrupo.Count()

                                                  };

            return View(data.ToList());
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        protected override void Dispose(bool disposing)
        {
            Db.Dispose();
            base.Dispose(disposing);
        }
    }
}