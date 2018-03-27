using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PortalEscola.WEB.Models
{
    public enum GRAU
    {
        A, B, C, D, F
    }


    public class Inscricao
    {

        public int InscricaoId { get; set; }
        public int CursoId { get; set; }
        public int EstudanteId { get; set; }
        public GRAU? Grau { get; set; }

        [ForeignKey("CursoId")]
        [Display(Name ="Curso")]
        public virtual Curso Curso { get; set; }

        [ForeignKey("EstudanteId")]
        [Display(Name ="Estudante")]
        public virtual Estudante Estudante { get; set; }
    }
}