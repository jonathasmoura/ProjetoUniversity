using PortalEscola.WEB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PortalEscola.WEB.Contexto
{
    public class PortalEscolaContexto : DbContext
    {

        public PortalEscolaContexto()
        : base("EscolaContexto")
        {

        }

        public DbSet<Estudante> Estudantes { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        
        //public DbSet<Departamento> Departamentos { get; set; }
        //public DbSet<Professor> Professores { get; set; }
        //public DbSet<Pessoa> Pessoas { get; set; }
        //public DbSet<AssiOficial> Assinaturas { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }


        //public override int SaveChanges()
        //{
        //   foreach(var entrada in ChangeTracker.Entries().Where(entrada => entrada.Entity.GetType().GetProperty("DataInscricao") != null))
        //    {
        //        if (entrada.State == EntityState.Added)
        //        {
        //            entrada.Property("DataInscricao").CurrentValue = DateTime.Now;
        //        }
        //        if(entrada.State == EntityState.Modified)
        //        {
        //            entrada.Property("DataInscricao").IsModified = false;
        //        }
        //    }
        //    return base.SaveChanges();
        //}

    }
}