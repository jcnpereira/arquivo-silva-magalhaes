namespace ArquivoSilvaMagalhaes.Migrations
{
    using ArquivoSilvaMagalhaes.Models;
    using ArquivoSilvaMagalhaes.Models.ArchiveModels;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class ArchiveDataContext_Configuration : DbMigrationsConfiguration<ArquivoSilvaMagalhaes.Models.ArchiveDataContext>
    {
        public ArchiveDataContext_Configuration()
        {
            AutomaticMigrationsEnabled = true;

            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ArquivoSilvaMagalhaes.Models.ArchiveDataContext context)
        {
                
        }


        private void SeedAuthors(ArchiveDataContext db)
        {
            for (int i = 1; i <= 100; i++)
            {
                var a = new Author
                {
                    FirstName = "Primeiro " + i,
                    LastName = "Último " + i,
                    BirthDate = DateTime.Now
                };

                if (i % 3 == 0)
                {
                    a.DeathDate = DateTime.Now;
                }

                db.Authors.Add(a);
            }
        }
    }
}
