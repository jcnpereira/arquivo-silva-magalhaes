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
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ArquivoSilvaMagalhaes.Models.ArchiveDataContext context)
        {
            SeedAuthors(context);
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
    }
}
