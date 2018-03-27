namespace PortalEscola.WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Curso",
                c => new
                    {
                        CursoId = c.Int(nullable: false, identity:true),
                        NomeCurso = c.String(),
                        Creditos = c.Int(nullable: false),
                        UniversidadeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CursoId)
                .ForeignKey("dbo.Universidade", t => t.UniversidadeId)
                .Index(t => t.UniversidadeId);
            
            CreateTable(
                "dbo.Inscricao",
                c => new
                    {
                        InscricaoId = c.Int(nullable: false, identity: true),
                        CursoId = c.Int(nullable: false),
                        EstudanteId = c.Int(nullable: false),
                        Grau = c.Int(),
                    })
                .PrimaryKey(t => t.InscricaoId)
                .ForeignKey("dbo.Curso", t => t.CursoId)
                .ForeignKey("dbo.Estudante", t => t.EstudanteId)
                .Index(t => t.CursoId)
                .Index(t => t.EstudanteId);
            
            CreateTable(
                "dbo.Estudante",
                c => new
                    {
                        EstudanteId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Sobrenome = c.String(),
                        DataInscricao = c.DateTime(nullable: false),
                        CursoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EstudanteId)
                .ForeignKey("dbo.Curso", t => t.CursoId)
                .Index(t => t.CursoId);
            
            CreateTable(
                "dbo.Universidade",
                c => new
                    {
                        UniversidadeId = c.Int(nullable: false, identity: true),
                        NomeUniversidade = c.String(),
                        Cidade = c.String(),
                        UF = c.String(),
                    })
                .PrimaryKey(t => t.UniversidadeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Curso", "UniversidadeId", "dbo.Universidade");
            DropForeignKey("dbo.Inscricao", "EstudanteId", "dbo.Estudante");
            DropForeignKey("dbo.Estudante", "CursoId", "dbo.Curso");
            DropForeignKey("dbo.Inscricao", "CursoId", "dbo.Curso");
            DropIndex("dbo.Estudante", new[] { "CursoId" });
            DropIndex("dbo.Inscricao", new[] { "EstudanteId" });
            DropIndex("dbo.Inscricao", new[] { "CursoId" });
            DropIndex("dbo.Curso", new[] { "UniversidadeId" });
            DropTable("dbo.Universidade");
            DropTable("dbo.Estudante");
            DropTable("dbo.Inscricao");
            DropTable("dbo.Curso");
        }
    }
}
