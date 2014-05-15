namespace ArquivoSilvaMagalhaes.Migrations
{
    using ArquivoSilvaMagalhaes.Models.ArchiveModels;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ArquivoSilvaMagalhaes.Models.ArchiveDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ArquivoSilvaMagalhaes.Models.ArchiveDataContext context)
        {
            SeedAuthors(context);
            SeedCollections(context);
            SeedDocuments(context);
            SeedProcesses(context);
            SeedFormats(context);
            SeedSpecimens(context);
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
        }

        private void SeedAuthors(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.AuthorSet.AddOrUpdate(
                a => a.Id,
                new Author { Id = 1, FirstName = "Autor 1 Primeiro", LastName = "Autor 1 Último", BirthDate = new DateTime(2000, 1, 1), DeathDate = new DateTime(2000, 1, 2) },
                new Author { Id = 2, FirstName = "Autor 2 Primeiro", LastName = "Autor 2 Último", BirthDate = new DateTime(2000, 2, 1), DeathDate = new DateTime(2000, 2, 2) }
            );

            db.AuthorTextSet.AddOrUpdate(
                t => new { t.AuthorId, t.LanguageCode },
                new AuthorText { AuthorId = 1, LanguageCode = "pt", Biography = "Biografia", Curriculum = "Curriculo", Nationality = "Português" },
                new AuthorText { AuthorId = 2, LanguageCode = "pt", Biography = "Biografia", Curriculum = "Curriculo", Nationality = "Português" }
            );

        }

        private void SeedCollections(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.CollectionSet.AddOrUpdate(
                c => c.Id,
                new Collection
                {
                    Id = 1,
                    CatalogCode = "a",
                    HasAttachments = false,
                    IsVisible = false,
                    LogoLocation = "a",
                    Notes = "a",
                    OrganizationCode = "a",
                    ProductionDate = DateTime.Now,
                    Type = CollectionType.Collection
                }
            );
        }

        private void SeedDocuments(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.DocumentSet.AddOrUpdate(
                d => d.Id,
                new Document
                {
                    Id = 1,
                    AuthorId = 1,
                    CollectionId = 1,
                    CatalogationDate = DateTime.Now,
                    CatalogCode = "a",
                    DocumentDate = DateTime.Now,
                    Notes = "a",
                    ResponsibleName = "a"
                }
            );
        }

        private void SeedProcesses(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.ProcessSet.AddOrUpdate(
                p => p.Id,
                new Process { Id = 1 }
            );
        }

        private void SeedFormats(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.FormatSet.AddOrUpdate(
                f => f.Id,
                new Format { Id = 1, FormatDescription = "8x11" }
            );
        }

        private void SeedSpecimens(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.SpecimenSet.AddOrUpdate(
                s => s.Id,
                new Specimen
                {
                    Id = 1,
                    AuthorCatalogationCode = "a",
                    CatalogCode = "a",
                    DocumentId = 1,
                    FormatId = 1,
                    HasMarksOrStamps = false,
                    Indexation = "a",
                    Notes = "a",
                    ProcessId = 1
                }
            );
        }
    }
}
