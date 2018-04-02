using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PortalEscola.WEB.Models
{
    public class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CursoId { get; set; }


        [Display(Name = "Nome do Curso")]
        public string NomeCurso { get; set; }
        public int Creditos { get; set; }

        public virtual ICollection<Inscricao> Inscricoes { get; set; }
        /*
                public Curso(int cId, string nome, int creditos)
                {
                    CursoId = cId;
                    NomeCurso = nome;
                    Creditos = creditos;
                }
                */
    }
}