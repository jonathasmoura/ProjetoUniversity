using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PortalEscola.WEB.Models
{
    public class Estudante
    {
        public int EstudanteId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        // public GRAU Grau { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0 : yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de inscrição")]
        public DateTime DataInscricao { get; set; }

        public virtual ICollection<Inscricao> Inscricoes { get; set; }



    }
}