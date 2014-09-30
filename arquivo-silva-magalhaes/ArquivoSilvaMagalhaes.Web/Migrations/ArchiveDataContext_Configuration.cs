namespace ArquivoSilvaMagalhaes.Migrations
{
    using ArquivoSilvaMagalhaes.Models;
    using ArquivoSilvaMagalhaes.Models.ArchiveModels;
    using ArquivoSilvaMagalhaes.Models.SiteModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ArchiveDataContext_Configuration : DbMigrationsConfiguration<ArchiveDataContext>
    {
        public ArchiveDataContext_Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ArchiveDataContext db)
        {
            db.Configurations.AddOrUpdate(
                c => new { c.Key },
                new AppConfiguration
                {
                    Key = AppConfiguration.VideoUrlKey,
                    Value = "63JNz7UZYwU"
                });

            var archive = new Archive
                {
                    Address = "Morada - MUDAR!!"
                };

            archive.Translations.Add(new ArchiveTranslation
                {
                    Archive = archive,
                    LanguageCode = "pt",
                    ArchiveHistory = "História",
                    ArchiveMission = "Missão"
                });

            db.Archives.AddOrUpdate(
                a => new { a.Address },
                archive);

            //SeedClassifications(db);
            //SeedKeywords(db);

            //SeedAuthors(db);
            //SeedCollections(db);
            //SeedDocuments(db);

            //SeedImages(db);
        }

        private void SeedAuthors(ArchiveDataContext db)
        {
            for (int i = 1; i <= 10; i++)
            {
                var a = new Author
                {
                    Id = i,
                    FirstName = "Primeiro " + i,
                    LastName = "Último " + i,
                    BirthDate = DateTime.Now
                };

                a.Translations.Add(new AuthorTranslation
                {
                    AuthorId = a.Id,
                    Biography = "Biografia " + i,
                    Curriculum = "Curriculo " + i,
                    Nationality = "Nacionalidade " + i,
                    LanguageCode = "pt"
                });

                if (i % 3 == 0)
                {
                    a.DeathDate = DateTime.Now;
                }

                db.Authors.AddOrUpdate(author => new { author.Id }, a);
            }
        }

        private void SeedCollections(ArchiveDataContext db)
        {
            for (int i = 1; i <= 10; i++)
            {
                var c = new Collection
                {
                    Id = i,
                    CatalogCode = "COL" + i,
                    AttachmentsDescriptions = "Anexos " + i,
                    IsVisible = i % 2 == 0,
                    OrganizationSystem = "Org " + i,
                    ProductionPeriod = (1900 + i).ToString(),
                    Type = CollectionType.Collection,
                    Notes = "Notas " + i
                };

                c.Authors = db.Authors.Where(a => a.Id == i)
                    .Take(5)
                    .ToList();

                c.Translations.Add(new CollectionTranslation
                    {
                        CollectionId = c.Id,
                        LanguageCode = "pt",
                        AdministrativeAndBiographicStory = "História " + i,
                        CopyrightInfo = "Copyright " + i,
                        FieldAndContents = "Âmbito " + i,
                        Title = "Coleção " + i,
                        Provenience = "Proveniência" + i,
                        Dimension = "Dimensão " + i,
                        Description = "Descrição " + i
                    });

                db.Collections.AddOrUpdate(collection => new { collection.CatalogCode }, c);
            }
        }


        private void SeedDocuments(ArchiveDataContext db)
        {
            for (int i = 1; i <= 100; i++)
            {
                var document = new Document
                {
                    Id = i,
                    AuthorId = (i % 10) + 1,
                    CollectionId = (i % 10) + 1,
                    DocumentDate = (1940 + (i % 50)) + "-" + ((i % 12) + 1) + "-" + ((i % 20) + 1),
                    CatalogationDate = new DateTime(1996 + (i % 15), (i % 12) + 1, (i % 20) + 1),
                    Notes = "Notas " + i,
                    ResponsibleName = "Responsável " + i,
                    Title = "Título " + i,
                    CatalogCode = "COL" + ((i % 10) + 1) + "-" + i
                };

                document.Translations.Add(new DocumentTranslation
                    {
                        DocumentId = document.Id,
                        LanguageCode = "pt",
                        Description = "Descrição " + i,
                        DocumentLocation = "Localização " + i,
                        FieldAndContents = "Âmbito " + i
                    });

                db.Documents.AddOrUpdate(d => new { d.CatalogCode }, document);
            }
        }

        private void SeedClassifications(ArchiveDataContext db)
        {
            for (int i = 1; i <= 30; i++)
            {
                var classification = new Classification
                {
                    Id = i
                };

                var tr = new ClassificationTranslation
                    {
                        ClassificationId = classification.Id,
                        LanguageCode = "pt",
                        Value = "Classificação " + i
                    };

                classification.Translations.Add(tr);

                db.Classifications.AddOrUpdate(ct => new { ct.Id }, classification);
            }
        }

        private void SeedKeywords(ArchiveDataContext db)
        {
            for (int i = 1; i <= 100; i++)
            {
                var kw = new Keyword
                {
                    Id = i
                };

                var tr = new KeywordTranslation
                {
                    KeywordId = kw.Id,
                    LanguageCode = "pt",
                    Value = "Indexação " + i
                };

                kw.Translations.Add(tr);

                db.Keywords.AddOrUpdate(kt => new { kt.Id }, kw);
            }
        }

        private void SeedImages(ArchiveDataContext db)
        {
            var imgList = new List<Image>();

            for (int i = 1; i <= 1000; i++)
            {
                var image = new Image
                {
                    Id = i,
                    DocumentId = (i % 100) + 1,
                    ClassificationId = (i % 30) + 1,
                    ImageCode = "COL" + ((i % 10) + 1) + "-" + (i % 100) + 1 + "-" + i,
                    IsVisible = i % 3 == 0,
                    ProductionDate = (1940 + (i % 50)) + "-" + ((i % 12) + 1) + "-" + ((i % 20) + 1)
                };

                image.Translations.Add(new ImageTranslation
                    {
                        ImageId = image.Id,
                        LanguageCode = "pt",
                        Description = "Descrição " + i,
                        Location = "Localização " + i,
                        Publication = "Publicação " + i,
                        Subject = "Assunto " + i,
                        Title = "Título " + i
                    });

                image.Keywords = db.Keywords
                    .Where(k => k.Id == (i % 100) + 1)
                    .Take(10)
                    .ToList();

                imgList.Add(image);
            }
            db.Images.AddOrUpdate(img => new { img.ImageCode }, imgList.ToArray());
        }
    }
}
