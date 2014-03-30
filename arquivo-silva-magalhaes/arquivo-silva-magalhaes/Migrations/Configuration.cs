namespace ArquivoSilvaMagalhaes.Migrations
{
    using ArquivoSilvaMagalhaes.Models.ArchiveModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ArquivoSilvaMagalhaes.Models.ArchiveDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
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
        }

        protected void SeedAuthors(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            var authors = new List<Author>
            {
                new Author { Id = 1, FirstName = "António", LastName = "da Silva Magalhães", BirthDate = new DateTime(1834, 6, 19), DeathDate = new DateTime(1897, 3, 3) },
                new Author { Id = 2, FirstName = "António", LastName = "Ayres da Silva", BirthDate = new DateTime(1800, 1, 1), DeathDate = DateTime.Now },
                new Author { Id = 3, FirstName = "Luís Filipe", LastName = "de Aboim Pereira", BirthDate = new DateTime(1800, 1, 1), DeathDate = DateTime.Now }
            };

            var authorTexts = new List<AuthorText>
            {
                new AuthorText { LanguageCode = "pt", Nationality = "pt", Author = authors[0], Biography = "Biografia pt.", Curriculum = "Curriculum pt." },
                new AuthorText { LanguageCode = "pt-PT", Nationality = "pt", Author = authors[0], Biography = "Biografia pt-PT.", Curriculum = "Curriculum pt-PT." }
            };

            authors.ForEach(author => db.AuthorSet.AddOrUpdate(a => a.LastName, author));
            authorTexts.ForEach(text => db.AuthorTextSet.AddOrUpdate(text));

            db.SaveChanges();


        }

        protected void SeedDocuments(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            // TODO
        }
    }
}
