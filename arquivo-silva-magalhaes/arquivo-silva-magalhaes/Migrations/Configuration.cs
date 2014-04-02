namespace ArquivoSilvaMagalhaes.Migrations
{
    using ArquivoSilvaMagalhaes.Models.ArchiveModels;
    using ArquivoSilvaMagalhaes.Models.SiteModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ArquivoSilvaMagalhaes.Models.ArchiveDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ArquivoSilvaMagalhaes.Models.ArchiveDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            SeedAuthors(context);
            SeedCollections(context);

            SeedCollaborators(context);
            
        }




        protected void SeedAuthors(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            var authors = new List<Author>
            {
                new Author { Id = 1, FirstName = "António", LastName = "da Silva Magalhães", BirthDate = new DateTime(1834, 6, 19), DeathDate = new DateTime(1897, 3, 3) },
                new Author { Id = 2, FirstName = "António", LastName = "Carrapato", BirthDate = new DateTime(1966, 1, 1), DeathDate = DateTime.Now },
                new Author { Id = 3, FirstName = "Luís Filipe", LastName = "de Aboim Pereira", BirthDate = new DateTime(1800, 1, 1), DeathDate = DateTime.Now }
            };

            var authorTexts = new List<AuthorText>
            {
                new AuthorText { LanguageCode = "pt", Nationality = "pt", Author = authors[0], Biography = "Biografia pt.", Curriculum = "Curriculum pt." }
            };

            authors.ForEach(author => db.AuthorSet.AddOrUpdate(a => new { a.LastName, a.FirstName }, author));
            authorTexts.ForEach(text => db.AuthorTextSet.AddOrUpdate(text));

            db.SaveChanges();


        }

        protected void SeedDocuments(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            // TODO
        }

        void SeedCollections(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.CollectionSet.AddOrUpdate(
                c => c.OrganizationCode,
                new Collection
                {
                    Id = 1,
                    Authors = new List<Author> { db.AuthorSet.Find(1) },
                    OrganizationCode = "PT/AFSM/COLA",
                    HasAttachments = false,
                    IsVisible = true,
                    Notes = "Notas da coleção A.",
                    CatalogCode = "COLA",
                    Type = CollectionType.Collection,
                    ProductionDate = DateTime.Now
                },
                new Collection
                {
                    Id = 2,
                    Authors = new List<Author> { db.AuthorSet.Find(2) },
                    OrganizationCode = "PT/AFSM/COLB",
                    HasAttachments = true,
                    IsVisible = true,
                    Notes = "Notas da coleção B.",
                    CatalogCode = "COLB",
                    Type = CollectionType.Collection,
                    ProductionDate = DateTime.Now
                },
                new Collection
                {
                    Id = 3,
                    Authors = new List<Author> { db.AuthorSet.Find(1) },
                    OrganizationCode = "PT/AFSM/FUNA",
                    HasAttachments = false,
                    IsVisible = true,
                    Notes = "Notas do fundo A.",
                    CatalogCode = "FUNA",
                    Type = CollectionType.Fond,
                    ProductionDate = DateTime.Now
                },
                new Collection
                {
                    Id = 4,
                    Authors = new List<Author> { db.AuthorSet.Find(2) },
                    OrganizationCode = "PT/AFSM/FUNB",
                    HasAttachments = false,
                    IsVisible = true,
                    Notes = "Notas do fundo B.",
                    CatalogCode = "FUNB",
                    Type = CollectionType.Fond,
                    ProductionDate = DateTime.Now
                }
            );

            db.CollectionTextSet.AddOrUpdate(
                new CollectionText
                {
                    Collection = db.CollectionSet.Find(1),
                    LanguageCode = "pt",
                    Title = "Coleção A",
                    Description = "Descrição da Coleção A.",
                    Provenience = "Proveniência da Coleção A.",
                    AdministrativeAndBiographicStory = "História Administrativa e Biográfica da Coleção A.",
                    Dimension = "Dimensão da Coleção A.",
                    FieldAndContents = "Âmbito e conteúdo da Coleção A.",
                    CopyrightInfo = "Condições de Reprodução da Coleção A."
                },
                new CollectionText
                {
                    Collection = db.CollectionSet.Find(2),
                    LanguageCode = "pt",
                    Title = "Coleção B",
                    Description = "Descrição da Coleção B.",
                    Provenience = "Proveniência da Coleção B.",
                    AdministrativeAndBiographicStory = "História Administrativa e Biográfica da Coleção B.",
                    Dimension = "Dimensão da Coleção B.",
                    FieldAndContents = "Âmbito e conteúdo da Coleção B.",
                    CopyrightInfo = "Condições de Reprodução da Coleção B."
                },
                new CollectionText
                {
                    Collection = db.CollectionSet.Find(3),
                    LanguageCode = "pt",
                    Title = "Fundo A",
                    Description = "Descrição do Fundo A.",
                    Provenience = "Proveniência do Fundo A.",
                    AdministrativeAndBiographicStory = "História Administrativa e Biográfica do Fundo A.",
                    Dimension = "Dimensão do Fundo A.",
                    FieldAndContents = "Âmbito e conteúdo do Fundo A.",
                    CopyrightInfo = "Condições de Reprodução do Fundo A."
                },
                new CollectionText
                {
                    Collection = db.CollectionSet.Find(4),
                    LanguageCode = "pt",
                    Title = "Fundo B",
                    Description = "Descrição do Fundo B.",
                    Provenience = "Proveniência do Fundo B.",
                    AdministrativeAndBiographicStory = "História Administrativa e Biográfica do Fundo B.",
                    Dimension = "Dimensão do Fundo B.",
                    FieldAndContents = "Âmbito e conteúdo do Fundo B.",
                    CopyrightInfo = "Condições de Reprodução do Fundo B."
                }
            );

            db.SaveChanges();
        }



        protected void SeedCollaborators(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            var collaborators = new List<Collaborator>{
                    new Collaborator{ Id=1, Name="Abc",  Contact="999999999", EmailAddress="abc@mail.com", Task="xyz", ContactVisible=true},
                    new Collaborator{ Id=2, Name="Def", Contact="911111111", EmailAddress="def@mail.com", Task="ghj", ContactVisible=true }
                    
            };
            db.SaveChanges();
        }
    }
}
