namespace ArquivoSilvaMagalhaes.Migrations
{
    using ArquivoSilvaMagalhaes.Models;
    using ArquivoSilvaMagalhaes.Models.ArchiveModels;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ArchiveDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ArquivoSilvaMagalhaes.Models.ArchiveModels.ArchiveDataContext";
        }

        protected override void Seed(ArchiveDataContext context)
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
            SeedDocuments(context);
        }

        /// <summary>
        /// Adds collections to the database.
        /// </summary>
        /// <param name="context"></param>
        private void SeedCollections(ArchiveDataContext context)
        {
            context.Collections.AddOrUpdate(
                new Collection
                {
                    Id = 1,
                    Provenience = "Proveniência desta coleção.",
                    Type = "Coleção",
                    Dimension = 50,
                    HistoricalDetails = "Detalhes Históricos.",
                    ProductionDate = new DateTime(2000, 12, 31), 
                    
                }
            );
        }

        /// <summary>
        /// Adds documents to the database.
        /// </summary>
        /// <param name="context"></param>
        private void SeedDocuments(ArchiveDataContext context)
        {
            context.Documents.AddOrUpdate(
                new Document
                {
                    Id = 1,
                    Title = "Documento 1",
                    Notes = "Notas do documento 1.",
                    Author = context.Authors.Find(1),
                    Collection = context.Collections.Find(1),
                    CatalogDate = DateTime.Now,
                    DocumentDate = new DateTime(2000, 12, 31)
                }
            );
        }

        /// <summary>
        /// Adds authors to the database.
        /// </summary>
        /// <param name="context"></param>
        private void SeedAuthors(ArchiveDataContext context)
        {
            context.Authors.AddOrUpdate(
                new Author 
                { 
                    Id = 1,
                    FirstName = "António da",
                    LastName = "Silva Magalhães", 
                    Biography = "Biografia do Silva Magalhães.",
                    Curriculum = "Curriculum do Silva Magalhães.",
                    Nationality = "Português",
                    BirthDate = new DateTime(1834, 6, 19),
                    DeathDate = new DateTime(1897, 3, 3)
                }
            );
        }
    }
}
