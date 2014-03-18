namespace ArquivoSilvaMagalhaes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Nationality = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        DeathDate = c.DateTime(),
                        Biography = c.String(),
                        Curriculum = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Provenience = c.String(),
                        Dimension = c.Short(nullable: false),
                        HistoricalDetails = c.String(),
                        Type = c.String(),
                        ProductionDate = c.DateTime(nullable: false),
                        Author_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DocumentDate = c.DateTime(nullable: false),
                        CatalogDate = c.DateTime(nullable: false),
                        Notes = c.String(),
                        CollectionId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Collections", t => t.CollectionId, cascadeDelete: true)
                .Index(t => t.CollectionId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.PhotographicSpecimen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorCode = c.String(),
                        Notes = c.String(),
                        InterventionDescription = c.String(),
                        Topic = c.String(),
                        SpecimenDate = c.DateTime(nullable: false),
                        Document_Id = c.Int(),
                        PhotographicFormat_Id = c.Int(),
                        PhotographicProcess_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.Document_Id)
                .ForeignKey("dbo.PhotographicFormats", t => t.PhotographicFormat_Id)
                .ForeignKey("dbo.PhotographicProcesses", t => t.PhotographicProcess_Id)
                .Index(t => t.Document_Id)
                .Index(t => t.PhotographicFormat_Id)
                .Index(t => t.PhotographicProcess_Id);
            
            CreateTable(
                "dbo.DigitalPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotographicSpecimen_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhotographicSpecimen", t => t.PhotographicSpecimen_Id)
                .Index(t => t.PhotographicSpecimen_Id);
            
            CreateTable(
                "dbo.KeyWords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Word = c.String(),
                        PhotographicSpecimen_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhotographicSpecimen", t => t.PhotographicSpecimen_Id)
                .Index(t => t.PhotographicSpecimen_Id);
            
            CreateTable(
                "dbo.PhotographicFormats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormatDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhotographicProcesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotographicSpecimen", "PhotographicProcess_Id", "dbo.PhotographicProcesses");
            DropForeignKey("dbo.PhotographicSpecimen", "PhotographicFormat_Id", "dbo.PhotographicFormats");
            DropForeignKey("dbo.KeyWords", "PhotographicSpecimen_Id", "dbo.PhotographicSpecimen");
            DropForeignKey("dbo.PhotographicSpecimen", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.DigitalPhotoes", "PhotographicSpecimen_Id", "dbo.PhotographicSpecimen");
            DropForeignKey("dbo.Documents", "CollectionId", "dbo.Collections");
            DropForeignKey("dbo.Documents", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Collections", "Author_Id", "dbo.Authors");
            DropIndex("dbo.KeyWords", new[] { "PhotographicSpecimen_Id" });
            DropIndex("dbo.DigitalPhotoes", new[] { "PhotographicSpecimen_Id" });
            DropIndex("dbo.PhotographicSpecimen", new[] { "PhotographicProcess_Id" });
            DropIndex("dbo.PhotographicSpecimen", new[] { "PhotographicFormat_Id" });
            DropIndex("dbo.PhotographicSpecimen", new[] { "Document_Id" });
            DropIndex("dbo.Documents", new[] { "AuthorId" });
            DropIndex("dbo.Documents", new[] { "CollectionId" });
            DropIndex("dbo.Collections", new[] { "Author_Id" });
            DropTable("dbo.PhotographicProcesses");
            DropTable("dbo.PhotographicFormats");
            DropTable("dbo.KeyWords");
            DropTable("dbo.DigitalPhotoes");
            DropTable("dbo.PhotographicSpecimen");
            DropTable("dbo.Documents");
            DropTable("dbo.Collections");
            DropTable("dbo.Authors");
        }
    }
}
