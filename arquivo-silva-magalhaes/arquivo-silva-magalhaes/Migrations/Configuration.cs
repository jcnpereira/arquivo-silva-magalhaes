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
            SeedKeywords(context);
            SeedClassifications(context);
            SeedFormats(context);
            SeedSpecimens(context);
        }

        private void SeedAuthors(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.AuthorSet.AddOrUpdate(x => x.Id,
                new Author { Id = 1, FirstName = "Autor 1 Primeiro", LastName = "Autor 1 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 2, FirstName = "Autor 2 Primeiro", LastName = "Autor 2 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 3, FirstName = "Autor 3 Primeiro", LastName = "Autor 3 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 4, FirstName = "Autor 4 Primeiro", LastName = "Autor 4 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 5, FirstName = "Autor 5 Primeiro", LastName = "Autor 5 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 6, FirstName = "Autor 6 Primeiro", LastName = "Autor 6 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 7, FirstName = "Autor 7 Primeiro", LastName = "Autor 7 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 8, FirstName = "Autor 8 Primeiro", LastName = "Autor 8 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 9, FirstName = "Autor 9 Primeiro", LastName = "Autor 9 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 10, FirstName = "Autor 10 Primeiro", LastName = "Autor 10 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 11, FirstName = "Autor 11 Primeiro", LastName = "Autor 11 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 12, FirstName = "Autor 12 Primeiro", LastName = "Autor 12 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 13, FirstName = "Autor 13 Primeiro", LastName = "Autor 13 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 14, FirstName = "Autor 14 Primeiro", LastName = "Autor 14 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 15, FirstName = "Autor 15 Primeiro", LastName = "Autor 15 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 16, FirstName = "Autor 16 Primeiro", LastName = "Autor 16 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 17, FirstName = "Autor 17 Primeiro", LastName = "Autor 17 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 18, FirstName = "Autor 18 Primeiro", LastName = "Autor 18 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 19, FirstName = "Autor 19 Primeiro", LastName = "Autor 19 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 20, FirstName = "Autor 20 Primeiro", LastName = "Autor 20 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 21, FirstName = "Autor 21 Primeiro", LastName = "Autor 21 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 22, FirstName = "Autor 22 Primeiro", LastName = "Autor 22 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 23, FirstName = "Autor 23 Primeiro", LastName = "Autor 23 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 24, FirstName = "Autor 24 Primeiro", LastName = "Autor 24 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 25, FirstName = "Autor 25 Primeiro", LastName = "Autor 25 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 26, FirstName = "Autor 26 Primeiro", LastName = "Autor 26 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 27, FirstName = "Autor 27 Primeiro", LastName = "Autor 27 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 28, FirstName = "Autor 28 Primeiro", LastName = "Autor 28 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 29, FirstName = "Autor 29 Primeiro", LastName = "Autor 29 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now },
                new Author { Id = 30, FirstName = "Autor 30 Primeiro", LastName = "Autor 30 Último", BirthDate = DateTime.Now, DeathDate = DateTime.Now }
            );

            db.AuthorTextSet.AddOrUpdate(x => new { x.AuthorId, x.LanguageCode },
                new AuthorText { AuthorId = 1, LanguageCode = "en", Biography = "Author 1 Biography in English", Curriculum = "Author 1 Curriculum in English.", Nationality = "Author 1 Nationality in English." },
                new AuthorText { AuthorId = 2, LanguageCode = "en", Biography = "Author 2 Biography in English", Curriculum = "Author 2 Curriculum in English.", Nationality = "Author 2 Nationality in English." },
                new AuthorText { AuthorId = 3, LanguageCode = "en", Biography = "Author 3 Biography in English", Curriculum = "Author 3 Curriculum in English.", Nationality = "Author 3 Nationality in English." },
                new AuthorText { AuthorId = 4, LanguageCode = "en", Biography = "Author 4 Biography in English", Curriculum = "Author 4 Curriculum in English.", Nationality = "Author 4 Nationality in English." },
                new AuthorText { AuthorId = 5, LanguageCode = "en", Biography = "Author 5 Biography in English", Curriculum = "Author 5 Curriculum in English.", Nationality = "Author 5 Nationality in English." },
                new AuthorText { AuthorId = 6, LanguageCode = "en", Biography = "Author 6 Biography in English", Curriculum = "Author 6 Curriculum in English.", Nationality = "Author 6 Nationality in English." },
                new AuthorText { AuthorId = 7, LanguageCode = "en", Biography = "Author 7 Biography in English", Curriculum = "Author 7 Curriculum in English.", Nationality = "Author 7 Nationality in English." },
                new AuthorText { AuthorId = 8, LanguageCode = "en", Biography = "Author 8 Biography in English", Curriculum = "Author 8 Curriculum in English.", Nationality = "Author 8 Nationality in English." },
                new AuthorText { AuthorId = 9, LanguageCode = "en", Biography = "Author 9 Biography in English", Curriculum = "Author 9 Curriculum in English.", Nationality = "Author 9 Nationality in English." },
                new AuthorText { AuthorId = 10, LanguageCode = "en", Biography = "Author 10 Biography in English", Curriculum = "Author 10 Curriculum in English.", Nationality = "Author 10 Nationality in English." },
                new AuthorText { AuthorId = 11, LanguageCode = "en", Biography = "Author 11 Biography in English", Curriculum = "Author 11 Curriculum in English.", Nationality = "Author 11 Nationality in English." },
                new AuthorText { AuthorId = 12, LanguageCode = "en", Biography = "Author 12 Biography in English", Curriculum = "Author 12 Curriculum in English.", Nationality = "Author 12 Nationality in English." },
                new AuthorText { AuthorId = 13, LanguageCode = "en", Biography = "Author 13 Biography in English", Curriculum = "Author 13 Curriculum in English.", Nationality = "Author 13 Nationality in English." },
                new AuthorText { AuthorId = 14, LanguageCode = "en", Biography = "Author 14 Biography in English", Curriculum = "Author 14 Curriculum in English.", Nationality = "Author 14 Nationality in English." },
                new AuthorText { AuthorId = 15, LanguageCode = "en", Biography = "Author 15 Biography in English", Curriculum = "Author 15 Curriculum in English.", Nationality = "Author 15 Nationality in English." },
                new AuthorText { AuthorId = 16, LanguageCode = "en", Biography = "Author 16 Biography in English", Curriculum = "Author 16 Curriculum in English.", Nationality = "Author 16 Nationality in English." },
                new AuthorText { AuthorId = 17, LanguageCode = "en", Biography = "Author 17 Biography in English", Curriculum = "Author 17 Curriculum in English.", Nationality = "Author 17 Nationality in English." },
                new AuthorText { AuthorId = 18, LanguageCode = "en", Biography = "Author 18 Biography in English", Curriculum = "Author 18 Curriculum in English.", Nationality = "Author 18 Nationality in English." },
                new AuthorText { AuthorId = 19, LanguageCode = "en", Biography = "Author 19 Biography in English", Curriculum = "Author 19 Curriculum in English.", Nationality = "Author 19 Nationality in English." },
                new AuthorText { AuthorId = 20, LanguageCode = "en", Biography = "Author 20 Biography in English", Curriculum = "Author 20 Curriculum in English.", Nationality = "Author 20 Nationality in English." },
                new AuthorText { AuthorId = 21, LanguageCode = "en", Biography = "Author 21 Biography in English", Curriculum = "Author 21 Curriculum in English.", Nationality = "Author 21 Nationality in English." },
                new AuthorText { AuthorId = 22, LanguageCode = "en", Biography = "Author 22 Biography in English", Curriculum = "Author 22 Curriculum in English.", Nationality = "Author 22 Nationality in English." },
                new AuthorText { AuthorId = 23, LanguageCode = "en", Biography = "Author 23 Biography in English", Curriculum = "Author 23 Curriculum in English.", Nationality = "Author 23 Nationality in English." },
                new AuthorText { AuthorId = 24, LanguageCode = "en", Biography = "Author 24 Biography in English", Curriculum = "Author 24 Curriculum in English.", Nationality = "Author 24 Nationality in English." },
                new AuthorText { AuthorId = 25, LanguageCode = "en", Biography = "Author 25 Biography in English", Curriculum = "Author 25 Curriculum in English.", Nationality = "Author 25 Nationality in English." },
                new AuthorText { AuthorId = 26, LanguageCode = "en", Biography = "Author 26 Biography in English", Curriculum = "Author 26 Curriculum in English.", Nationality = "Author 26 Nationality in English." },
                new AuthorText { AuthorId = 27, LanguageCode = "en", Biography = "Author 27 Biography in English", Curriculum = "Author 27 Curriculum in English.", Nationality = "Author 27 Nationality in English." },
                new AuthorText { AuthorId = 28, LanguageCode = "en", Biography = "Author 28 Biography in English", Curriculum = "Author 28 Curriculum in English.", Nationality = "Author 28 Nationality in English." },
                new AuthorText { AuthorId = 29, LanguageCode = "en", Biography = "Author 29 Biography in English", Curriculum = "Author 29 Curriculum in English.", Nationality = "Author 29 Nationality in English." },
                new AuthorText { AuthorId = 30, LanguageCode = "en", Biography = "Author 30 Biography in English", Curriculum = "Author 30 Curriculum in English.", Nationality = "Author 30 Nationality in English." },
                new AuthorText { AuthorId = 1, LanguageCode = "pt", Biography = "Biografia Autor 1 em Português", Curriculum = "Currículo Autor 1 em Português.", Nationality = "Nacionalidade Author 1 em Português." },
                new AuthorText { AuthorId = 2, LanguageCode = "pt", Biography = "Biografia Autor 2 em Português", Curriculum = "Currículo Autor 2 em Português.", Nationality = "Nacionalidade Author 2 em Português." },
                new AuthorText { AuthorId = 3, LanguageCode = "pt", Biography = "Biografia Autor 3 em Português", Curriculum = "Currículo Autor 3 em Português.", Nationality = "Nacionalidade Author 3 em Português." },
                new AuthorText { AuthorId = 4, LanguageCode = "pt", Biography = "Biografia Autor 4 em Português", Curriculum = "Currículo Autor 4 em Português.", Nationality = "Nacionalidade Author 4 em Português." },
                new AuthorText { AuthorId = 5, LanguageCode = "pt", Biography = "Biografia Autor 5 em Português", Curriculum = "Currículo Autor 5 em Português.", Nationality = "Nacionalidade Author 5 em Português." },
                new AuthorText { AuthorId = 6, LanguageCode = "pt", Biography = "Biografia Autor 6 em Português", Curriculum = "Currículo Autor 6 em Português.", Nationality = "Nacionalidade Author 6 em Português." },
                new AuthorText { AuthorId = 7, LanguageCode = "pt", Biography = "Biografia Autor 7 em Português", Curriculum = "Currículo Autor 7 em Português.", Nationality = "Nacionalidade Author 7 em Português." },
                new AuthorText { AuthorId = 8, LanguageCode = "pt", Biography = "Biografia Autor 8 em Português", Curriculum = "Currículo Autor 8 em Português.", Nationality = "Nacionalidade Author 8 em Português." },
                new AuthorText { AuthorId = 9, LanguageCode = "pt", Biography = "Biografia Autor 9 em Português", Curriculum = "Currículo Autor 9 em Português.", Nationality = "Nacionalidade Author 9 em Português." },
                new AuthorText { AuthorId = 10, LanguageCode = "pt", Biography = "Biografia Autor 10 em Português", Curriculum = "Currículo Autor 10 em Português.", Nationality = "Nacionalidade Author 10 em Português." },
                new AuthorText { AuthorId = 11, LanguageCode = "pt", Biography = "Biografia Autor 11 em Português", Curriculum = "Currículo Autor 11 em Português.", Nationality = "Nacionalidade Author 11 em Português." },
                new AuthorText { AuthorId = 12, LanguageCode = "pt", Biography = "Biografia Autor 12 em Português", Curriculum = "Currículo Autor 12 em Português.", Nationality = "Nacionalidade Author 12 em Português." },
                new AuthorText { AuthorId = 13, LanguageCode = "pt", Biography = "Biografia Autor 13 em Português", Curriculum = "Currículo Autor 13 em Português.", Nationality = "Nacionalidade Author 13 em Português." },
                new AuthorText { AuthorId = 14, LanguageCode = "pt", Biography = "Biografia Autor 14 em Português", Curriculum = "Currículo Autor 14 em Português.", Nationality = "Nacionalidade Author 14 em Português." },
                new AuthorText { AuthorId = 15, LanguageCode = "pt", Biography = "Biografia Autor 15 em Português", Curriculum = "Currículo Autor 15 em Português.", Nationality = "Nacionalidade Author 15 em Português." },
                new AuthorText { AuthorId = 16, LanguageCode = "pt", Biography = "Biografia Autor 16 em Português", Curriculum = "Currículo Autor 16 em Português.", Nationality = "Nacionalidade Author 16 em Português." },
                new AuthorText { AuthorId = 17, LanguageCode = "pt", Biography = "Biografia Autor 17 em Português", Curriculum = "Currículo Autor 17 em Português.", Nationality = "Nacionalidade Author 17 em Português." },
                new AuthorText { AuthorId = 18, LanguageCode = "pt", Biography = "Biografia Autor 18 em Português", Curriculum = "Currículo Autor 18 em Português.", Nationality = "Nacionalidade Author 18 em Português." },
                new AuthorText { AuthorId = 19, LanguageCode = "pt", Biography = "Biografia Autor 19 em Português", Curriculum = "Currículo Autor 19 em Português.", Nationality = "Nacionalidade Author 19 em Português." },
                new AuthorText { AuthorId = 20, LanguageCode = "pt", Biography = "Biografia Autor 20 em Português", Curriculum = "Currículo Autor 20 em Português.", Nationality = "Nacionalidade Author 20 em Português." },
                new AuthorText { AuthorId = 21, LanguageCode = "pt", Biography = "Biografia Autor 21 em Português", Curriculum = "Currículo Autor 21 em Português.", Nationality = "Nacionalidade Author 21 em Português." },
                new AuthorText { AuthorId = 22, LanguageCode = "pt", Biography = "Biografia Autor 22 em Português", Curriculum = "Currículo Autor 22 em Português.", Nationality = "Nacionalidade Author 22 em Português." },
                new AuthorText { AuthorId = 23, LanguageCode = "pt", Biography = "Biografia Autor 23 em Português", Curriculum = "Currículo Autor 23 em Português.", Nationality = "Nacionalidade Author 23 em Português." },
                new AuthorText { AuthorId = 24, LanguageCode = "pt", Biography = "Biografia Autor 24 em Português", Curriculum = "Currículo Autor 24 em Português.", Nationality = "Nacionalidade Author 24 em Português." },
                new AuthorText { AuthorId = 25, LanguageCode = "pt", Biography = "Biografia Autor 25 em Português", Curriculum = "Currículo Autor 25 em Português.", Nationality = "Nacionalidade Author 25 em Português." },
                new AuthorText { AuthorId = 26, LanguageCode = "pt", Biography = "Biografia Autor 26 em Português", Curriculum = "Currículo Autor 26 em Português.", Nationality = "Nacionalidade Author 26 em Português." },
                new AuthorText { AuthorId = 27, LanguageCode = "pt", Biography = "Biografia Autor 27 em Português", Curriculum = "Currículo Autor 27 em Português.", Nationality = "Nacionalidade Author 27 em Português." },
                new AuthorText { AuthorId = 28, LanguageCode = "pt", Biography = "Biografia Autor 28 em Português", Curriculum = "Currículo Autor 28 em Português.", Nationality = "Nacionalidade Author 28 em Português." },
                new AuthorText { AuthorId = 29, LanguageCode = "pt", Biography = "Biografia Autor 29 em Português", Curriculum = "Currículo Autor 29 em Português.", Nationality = "Nacionalidade Author 29 em Português." },
                new AuthorText { AuthorId = 30, LanguageCode = "pt", Biography = "Biografia Autor 30 em Português", Curriculum = "Currículo Autor 30 em Português.", Nationality = "Nacionalidade Author 30 em Português." });
        }

        private void SeedCollections(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.CollectionSet.AddOrUpdate(x => x.Id,
                new Collection { Id = 1, CatalogCode = "COD-1", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 1", OrganizationSystem = "Sistema de ordenação da Coleção 1", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 2, CatalogCode = "COD-2", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 2", OrganizationSystem = "Sistema de ordenação da Coleção 2", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 3, CatalogCode = "COD-3", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 3", OrganizationSystem = "Sistema de ordenação da Coleção 3", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 4, CatalogCode = "COD-4", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 4", OrganizationSystem = "Sistema de ordenação da Coleção 4", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 5, CatalogCode = "COD-5", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 5", OrganizationSystem = "Sistema de ordenação da Coleção 5", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 6, CatalogCode = "COD-6", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 6", OrganizationSystem = "Sistema de ordenação da Coleção 6", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 7, CatalogCode = "COD-7", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 7", OrganizationSystem = "Sistema de ordenação da Coleção 7", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 8, CatalogCode = "COD-8", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 8", OrganizationSystem = "Sistema de ordenação da Coleção 8", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 9, CatalogCode = "COD-9", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 9", OrganizationSystem = "Sistema de ordenação da Coleção 9", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 10, CatalogCode = "COD-10", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 10", OrganizationSystem = "Sistema de ordenação da Coleção 10", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 11, CatalogCode = "COD-11", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 11", OrganizationSystem = "Sistema de ordenação da Coleção 11", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 12, CatalogCode = "COD-12", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 12", OrganizationSystem = "Sistema de ordenação da Coleção 12", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 13, CatalogCode = "COD-13", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 13", OrganizationSystem = "Sistema de ordenação da Coleção 13", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 14, CatalogCode = "COD-14", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 14", OrganizationSystem = "Sistema de ordenação da Coleção 14", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 15, CatalogCode = "COD-15", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 15", OrganizationSystem = "Sistema de ordenação da Coleção 15", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 16, CatalogCode = "COD-16", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 16", OrganizationSystem = "Sistema de ordenação da Coleção 16", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 17, CatalogCode = "COD-17", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 17", OrganizationSystem = "Sistema de ordenação da Coleção 17", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 18, CatalogCode = "COD-18", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 18", OrganizationSystem = "Sistema de ordenação da Coleção 18", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 19, CatalogCode = "COD-19", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 19", OrganizationSystem = "Sistema de ordenação da Coleção 19", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 20, CatalogCode = "COD-20", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 20", OrganizationSystem = "Sistema de ordenação da Coleção 20", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 21, CatalogCode = "COD-21", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 21", OrganizationSystem = "Sistema de ordenação da Coleção 21", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 22, CatalogCode = "COD-22", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 22", OrganizationSystem = "Sistema de ordenação da Coleção 22", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 23, CatalogCode = "COD-23", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 23", OrganizationSystem = "Sistema de ordenação da Coleção 23", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 24, CatalogCode = "COD-24", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 24", OrganizationSystem = "Sistema de ordenação da Coleção 24", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 25, CatalogCode = "COD-25", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 25", OrganizationSystem = "Sistema de ordenação da Coleção 25", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 26, CatalogCode = "COD-26", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 26", OrganizationSystem = "Sistema de ordenação da Coleção 26", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 27, CatalogCode = "COD-27", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 27", OrganizationSystem = "Sistema de ordenação da Coleção 27", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 28, CatalogCode = "COD-28", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 28", OrganizationSystem = "Sistema de ordenação da Coleção 28", ProductionDate = DateTime.Now, Type = CollectionType.Fond },
                new Collection { Id = 29, CatalogCode = "COD-29", HasAttachments = false, IsVisible = true, LogoLocation = null, Notes = "Notas da Coleção 29", OrganizationSystem = "Sistema de ordenação da Coleção 29", ProductionDate = DateTime.Now, Type = CollectionType.Collection },
                new Collection { Id = 30, CatalogCode = "COD-30", HasAttachments = true, IsVisible = false, LogoLocation = null, Notes = "Notas da Coleção 30", OrganizationSystem = "Sistema de ordenação da Coleção 30", ProductionDate = DateTime.Now, Type = CollectionType.Fond }
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
                    CatalogCode = "DOC-1",
                    DocumentDate = DateTime.Now,
                    Notes = "Notas 1",
                    ResponsibleName = "Responsável Documento 1"
                }
            );

            db.DocumentTextSet.AddOrUpdate(
                dt => new { dt.DocumentId, dt.LanguageCode },
                new DocumentText
                {
                    DocumentId = 1,
                    LanguageCode = "pt",
                    Description = "Descrição PT",
                    DocumentLocation = "Localização PT",
                    FieldAndContents = "Âmbito e Conteúdo PT"
                }
            );
        }

        private void SeedProcesses(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.ProcessSet.AddOrUpdate(
                p => p.Id,
                new Process { Id = 1 },
                new Process { Id = 2 }
            );

            db.ProcessTextSet.AddOrUpdate(
                pt => new { pt.ProcessId, pt.LanguageCode },
                new ProcessText
                {
                    ProcessId = 1,
                    LanguageCode = "pt",
                    Value = "Processo 1 PT"
                },
                new ProcessText
                {
                    ProcessId = 2,
                    LanguageCode = "pt",
                    Value = "Processo 2 PT"
                }
            );
        }

        private void SeedKeywords(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.KeywordSet.AddOrUpdate(
                k => k.Id,
                new Keyword { Id = 1 },
                new Keyword { Id = 2 }
            );

            db.KeywordTextSet.AddOrUpdate(
                kt => new { kt.KeywordId, kt.LanguageCode },
                new KeywordText
                {
                    KeywordId = 1,
                    LanguageCode = "pt",
                    Value = "Palavra-chave 1 PT"
                },
                new KeywordText
                {
                    KeywordId = 2,
                    LanguageCode = "pt",
                    Value = "Palavra-chave 2 PT"
                }
            );
        }

        private void SeedClassifications(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.ClassificationSet.AddOrUpdate(x => x.Id,
                new Classification { Id = 1 },
                new Classification { Id = 2 },
                new Classification { Id = 3 },
                new Classification { Id = 4 },
                new Classification { Id = 5 },
                new Classification { Id = 6 },
                new Classification { Id = 7 },
                new Classification { Id = 8 },
                new Classification { Id = 9 },
                new Classification { Id = 10 },
                new Classification { Id = 11 },
                new Classification { Id = 12 },
                new Classification { Id = 13 },
                new Classification { Id = 14 },
                new Classification { Id = 15 },
                new Classification { Id = 16 },
                new Classification { Id = 17 },
                new Classification { Id = 18 },
                new Classification { Id = 19 },
                new Classification { Id = 20 },
                new Classification { Id = 21 },
                new Classification { Id = 22 },
                new Classification { Id = 23 },
                new Classification { Id = 24 },
                new Classification { Id = 25 },
                new Classification { Id = 26 },
                new Classification { Id = 27 },
                new Classification { Id = 28 },
                new Classification { Id = 29 },
                new Classification { Id = 30 });

            db.ClassificationTextSet.AddOrUpdate(x => new { x.ClassificationId, x.LanguageCode },
                new ClassificationText { ClassificationId = 1, LanguageCode = "en", Value = "Classification 1 in English" },
                new ClassificationText { ClassificationId = 2, LanguageCode = "en", Value = "Classification 2 in English" },
                new ClassificationText { ClassificationId = 3, LanguageCode = "en", Value = "Classification 3 in English" },
                new ClassificationText { ClassificationId = 4, LanguageCode = "en", Value = "Classification 4 in English" },
                new ClassificationText { ClassificationId = 5, LanguageCode = "en", Value = "Classification 5 in English" },
                new ClassificationText { ClassificationId = 6, LanguageCode = "en", Value = "Classification 6 in English" },
                new ClassificationText { ClassificationId = 7, LanguageCode = "en", Value = "Classification 7 in English" },
                new ClassificationText { ClassificationId = 8, LanguageCode = "en", Value = "Classification 8 in English" },
                new ClassificationText { ClassificationId = 9, LanguageCode = "en", Value = "Classification 9 in English" },
                new ClassificationText { ClassificationId = 10, LanguageCode = "en", Value = "Classification 10 in English" },
                new ClassificationText { ClassificationId = 11, LanguageCode = "en", Value = "Classification 11 in English" },
                new ClassificationText { ClassificationId = 12, LanguageCode = "en", Value = "Classification 12 in English" },
                new ClassificationText { ClassificationId = 13, LanguageCode = "en", Value = "Classification 13 in English" },
                new ClassificationText { ClassificationId = 14, LanguageCode = "en", Value = "Classification 14 in English" },
                new ClassificationText { ClassificationId = 15, LanguageCode = "en", Value = "Classification 15 in English" },
                new ClassificationText { ClassificationId = 16, LanguageCode = "en", Value = "Classification 16 in English" },
                new ClassificationText { ClassificationId = 17, LanguageCode = "en", Value = "Classification 17 in English" },
                new ClassificationText { ClassificationId = 18, LanguageCode = "en", Value = "Classification 18 in English" },
                new ClassificationText { ClassificationId = 19, LanguageCode = "en", Value = "Classification 19 in English" },
                new ClassificationText { ClassificationId = 20, LanguageCode = "en", Value = "Classification 20 in English" },
                new ClassificationText { ClassificationId = 21, LanguageCode = "en", Value = "Classification 21 in English" },
                new ClassificationText { ClassificationId = 22, LanguageCode = "en", Value = "Classification 22 in English" },
                new ClassificationText { ClassificationId = 23, LanguageCode = "en", Value = "Classification 23 in English" },
                new ClassificationText { ClassificationId = 24, LanguageCode = "en", Value = "Classification 24 in English" },
                new ClassificationText { ClassificationId = 25, LanguageCode = "en", Value = "Classification 25 in English" },
                new ClassificationText { ClassificationId = 26, LanguageCode = "en", Value = "Classification 26 in English" },
                new ClassificationText { ClassificationId = 27, LanguageCode = "en", Value = "Classification 27 in English" },
                new ClassificationText { ClassificationId = 28, LanguageCode = "en", Value = "Classification 28 in English" },
                new ClassificationText { ClassificationId = 29, LanguageCode = "en", Value = "Classification 29 in English" },
                new ClassificationText { ClassificationId = 30, LanguageCode = "en", Value = "Classification 30 in English" },
                new ClassificationText { ClassificationId = 1, LanguageCode = "pt", Value = "Classificação 1 em Português" },
                new ClassificationText { ClassificationId = 2, LanguageCode = "pt", Value = "Classificação 2 em Português" },
                new ClassificationText { ClassificationId = 3, LanguageCode = "pt", Value = "Classificação 3 em Português" },
                new ClassificationText { ClassificationId = 4, LanguageCode = "pt", Value = "Classificação 4 em Português" },
                new ClassificationText { ClassificationId = 5, LanguageCode = "pt", Value = "Classificação 5 em Português" },
                new ClassificationText { ClassificationId = 6, LanguageCode = "pt", Value = "Classificação 6 em Português" },
                new ClassificationText { ClassificationId = 7, LanguageCode = "pt", Value = "Classificação 7 em Português" },
                new ClassificationText { ClassificationId = 8, LanguageCode = "pt", Value = "Classificação 8 em Português" },
                new ClassificationText { ClassificationId = 9, LanguageCode = "pt", Value = "Classificação 9 em Português" },
                new ClassificationText { ClassificationId = 10, LanguageCode = "pt", Value = "Classificação 10 em Português" },
                new ClassificationText { ClassificationId = 11, LanguageCode = "pt", Value = "Classificação 11 em Português" },
                new ClassificationText { ClassificationId = 12, LanguageCode = "pt", Value = "Classificação 12 em Português" },
                new ClassificationText { ClassificationId = 13, LanguageCode = "pt", Value = "Classificação 13 em Português" },
                new ClassificationText { ClassificationId = 14, LanguageCode = "pt", Value = "Classificação 14 em Português" },
                new ClassificationText { ClassificationId = 15, LanguageCode = "pt", Value = "Classificação 15 em Português" },
                new ClassificationText { ClassificationId = 16, LanguageCode = "pt", Value = "Classificação 16 em Português" },
                new ClassificationText { ClassificationId = 17, LanguageCode = "pt", Value = "Classificação 17 em Português" },
                new ClassificationText { ClassificationId = 18, LanguageCode = "pt", Value = "Classificação 18 em Português" },
                new ClassificationText { ClassificationId = 19, LanguageCode = "pt", Value = "Classificação 19 em Português" },
                new ClassificationText { ClassificationId = 20, LanguageCode = "pt", Value = "Classificação 20 em Português" },
                new ClassificationText { ClassificationId = 21, LanguageCode = "pt", Value = "Classificação 21 em Português" },
                new ClassificationText { ClassificationId = 22, LanguageCode = "pt", Value = "Classificação 22 em Português" },
                new ClassificationText { ClassificationId = 23, LanguageCode = "pt", Value = "Classificação 23 em Português" },
                new ClassificationText { ClassificationId = 24, LanguageCode = "pt", Value = "Classificação 24 em Português" },
                new ClassificationText { ClassificationId = 25, LanguageCode = "pt", Value = "Classificação 25 em Português" },
                new ClassificationText { ClassificationId = 26, LanguageCode = "pt", Value = "Classificação 26 em Português" },
                new ClassificationText { ClassificationId = 27, LanguageCode = "pt", Value = "Classificação 27 em Português" },
                new ClassificationText { ClassificationId = 28, LanguageCode = "pt", Value = "Classificação 28 em Português" },
                new ClassificationText { ClassificationId = 29, LanguageCode = "pt", Value = "Classificação 29 em Português" },
                new ClassificationText { ClassificationId = 30, LanguageCode = "pt", Value = "Classificação 30 em Português" });
        }

        private void SeedFormats(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.FormatSet.AddOrUpdate(
                f => f.Id,
                new Format { Id = 1, FormatDescription = "8x11" },
                new Format { Id = 2, FormatDescription = "35mm" }
            );
        }

        private void SeedSpecimens(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            db.SpecimenSet.AddOrUpdate(
                s => s.Id,
                new Specimen
                {
                    Id = 1,
                    AuthorCatalogationCode = "COD-AUT 1",
                    CatalogCode = "COD-CAT 1",
                    DocumentId = 1,
                    FormatId = 1,
                    HasMarksOrStamps = false,
                    Indexation = "INDEX 1",
                    Notes = "Notas 1",
                    ProcessId = 1
                }
            );

            db.SpecimenTextSet.AddOrUpdate(
                st => new { st.SpecimenId, st.LanguageCode },
                new SpecimenText
                {
                    SpecimenId = 1,
                    LanguageCode = "pt",
                    Description = "Descrição 1",
                    DetailedStateDescription = "Descrição Detalhada 1",
                    SimpleStateDescription = "Descrição Simples 1",
                    InterventionDescription = "Intervenção 1",
                    Publication = "Publicação 1",
                    Title = "Título PT",
                    Topic = "Tópico PT"
                }
            );
        }
    }
}
