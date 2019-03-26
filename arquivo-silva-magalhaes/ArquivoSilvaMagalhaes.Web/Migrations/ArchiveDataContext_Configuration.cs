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

            // SeedEvents(db);
            // SeedNews(db);
        }

        //private void SeedAuthors(ArchiveDataContext db)
        //{
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        var a = new Author
        //        {
        //            Id = i,
        //            FirstName = "Primeiro " + i,
        //            LastName = "Último " + i,
        //            BirthDate = new DateTime(1900 + i, (i % 12) + 1, (i % 28) + 1)
        //        };

        //        a.Translations.Add(new AuthorTranslation
        //        {
        //            AuthorId = a.Id,
        //            Biography = "Biografia " + i,
        //            Curriculum = "Curriculo " + i,
        //            Nationality = "Nacionalidade " + i,
        //            LanguageCode = "pt"
        //        });

        //        if (i % 3 == 0)
        //        {
        //            a.DeathDate = a.BirthDate.AddYears((i * 90) % 60);

        //            a.Translations.Add(new AuthorTranslation
        //            {
        //                AuthorId = a.Id,
        //                Biography = "Biography " + i,
        //                Curriculum = "Curriculum " + i,
        //                Nationality = "Nationality " + i,
        //                LanguageCode = "en"
        //            });
        //        }

        //        db.Authors.AddOrUpdate(author => new { author.Id }, a);
        //    }
        //    db.SaveChanges();
        //}

        //private void SeedCollections(ArchiveDataContext db)
        //{
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        var c = new Collection
        //        {
        //            Id = i,
        //            CatalogCode = "COL" + i,
        //            AttachmentsDescriptions = "Anexos " + i,
        //            IsVisible = i % 3 == 0,
        //            OrganizationSystem = "Org " + i,
        //            ProductionPeriod = (1900 + i).ToString(),
        //            Type = CollectionType.Collection,
        //            Notes = "Notas " + i
        //        };

        //        c.Authors = db.Authors
        //            .Where(a => a.Id == (i % 2) + 1)
        //            .ToList();

        //        c.Translations.Add(new CollectionTranslation
        //        {
        //            CollectionId = c.Id,
        //            LanguageCode = "pt",
        //            AdministrativeAndBiographicStory = "História " + i,
        //            CopyrightInfo = "Copyright " + i,
        //            FieldAndContents = "Âmbito " + i,
        //            Title = "Coleção " + i,
        //            Provenience = "Proveniência " + i,
        //            Dimension = "Dimensão " + i,
        //            Description = "Descrição " + i
        //        });

        //        if (i % 3 == 0)
        //        {
        //            c.Translations.Add(new CollectionTranslation
        //            {
        //                CollectionId = c.Id,
        //                LanguageCode = "en",
        //                AdministrativeAndBiographicStory = "History " + i,
        //                CopyrightInfo = "Copyright " + i,
        //                FieldAndContents = "Field " + i,
        //                Title = "Collection " + i,
        //                Provenience = "Provenience " + i,
        //                Dimension = "Dimension " + i,
        //                Description = "Description " + i
        //            });
        //        }

        //        db.Collections.AddOrUpdate(collection => new { collection.CatalogCode }, c);
        //    }
        //    db.SaveChanges();
        //}


        //private void SeedDocuments(ArchiveDataContext db)
        //{
        //    for (int i = 1; i <= 100; i++)
        //    {
        //        var document = new Document
        //        {
        //            Id = i,
        //            AuthorId = (i % 10) + 1,
        //            CollectionId = (i % 10) + 1,
        //            DocumentDate = (1940 + (i % 50)) + "-" + ((i % 12) + 1) + "-" + ((i % 20) + 1),
        //            CatalogationDate = new DateTime(1996 + (i % 15), (i % 12) + 1, (i % 20) + 1),
        //            Notes = "Notas " + i,
        //            ResponsibleName = "Responsável " + i,
        //            Title = "Título " + i,
        //            CatalogCode = "COL" + ((i % 10) + 1) + "-" + i
        //        };

        //        document.Translations.Add(new DocumentTranslation
        //        {
        //            DocumentId = document.Id,
        //            LanguageCode = "pt",
        //            Description = "Descrição " + i,
        //            DocumentLocation = "Localização " + i,
        //            FieldAndContents = "Âmbito " + i
        //        });

        //        if (i % 3 == 0)
        //        {
        //            document.Translations.Add(new DocumentTranslation
        //            {
        //                DocumentId = document.Id,
        //                LanguageCode = "en",
        //                Description = "Description " + i,
        //                DocumentLocation = "Place " + i,
        //                FieldAndContents = "Field " + i
        //            });
        //        }

        //        db.Documents.AddOrUpdate(d => new { d.CatalogCode }, document);
        //    }
        //    db.SaveChanges();
        //}



        //private void SeedClassifications(ArchiveDataContext db)
        //{
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        var classification = new Classification
        //        {
        //            Id = i
        //        };

        //        var tr = new ClassificationTranslation
        //        {
        //            ClassificationId = classification.Id,
        //            LanguageCode = "pt",
        //            Value = "Classificação " + i
        //        };

        //        classification.Translations.Add(tr);

        //        if (i % 3 == 0)
        //        {
        //            classification.Translations.Add(new ClassificationTranslation
        //            {
        //                ClassificationId = classification.Id,
        //                LanguageCode = "en",
        //                Value = "Classification " + i
        //            });
        //        }

        //        if (!db.ClassificationTranslations.Any(kt => kt.Value == tr.Value && kt.LanguageCode == tr.LanguageCode))
        //        {
        //            db.Classifications.Add(classification);
        //        }
        //    }
        //    db.SaveChanges();
        //}



        //private void SeedKeywords(ArchiveDataContext db)
        //{
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        var kw = new Keyword
        //        {
        //            Id = i
        //        };

        //        var tr = new KeywordTranslation
        //        {
        //            KeywordId = kw.Id,
        //            LanguageCode = "pt",
        //            Value = "Indexação " + i
        //        };

        //        kw.Translations.Add(tr);

        //        if (i % 3 == 0)
        //        {
        //            kw.Translations.Add(new KeywordTranslation
        //            {
        //                KeywordId = kw.Id,
        //                LanguageCode = "en",
        //                Value = "Keyword " + i
        //            });
        //        }

        //        if (!db.KeywordTranslations.Any(kt => kt.Value == tr.Value && kt.LanguageCode == tr.LanguageCode))
        //        {
        //            db.Keywords.Add(kw);
        //        }
        //    }
        //    db.SaveChanges();
        //}



        //private void SeedImages(ArchiveDataContext db)
        //{
        //    var imgList = new List<Image>();

        //    for (int i = 1; i <= 1000; i++)
        //    {
        //        var image = new Image
        //        {
        //            Id = i,
        //            DocumentId = (i % 100) + 1,
        //            ClassificationId = (i % 10) + 1,
        //            ImageCode = "COL" + ((i % 10) + 1) + "-" + (i % 100) + 1 + "-" + i,
        //            IsVisible = i % 3 == 0,
        //            ProductionDate = (1940 + (i % 50)) + "-" + ((i % 12) + 1) + "-" + ((i % 20) + 1)
        //        };

        //        image.Translations.Add(new ImageTranslation
        //        {
        //            ImageId = image.Id,
        //            LanguageCode = "pt",
        //            Description = "Descrição " + i,
        //            Location = "Localização " + i,
        //            Publication = "Publicação " + i,
        //            Subject = "Assunto " + i,
        //            Title = "Título " + i
        //        });

        //        if (i % 3 == 0)
        //        {
        //            image.Translations.Add(new ImageTranslation
        //            {
        //                ImageId = image.Id,
        //                LanguageCode = "en",
        //                Description = "Description " + i,
        //                Location = "Place " + i,
        //                Publication = "Published at " + i,
        //                Subject = "Subject " + i,
        //                Title = "Title " + i
        //            });
        //        }

        //        image.Keywords = db.Keywords
        //             .Where(k => k.Id == (i % 4) + 1)
        //             .ToList();

        //        imgList.Add(image);
        //    }
        //    db.Images.AddOrUpdate(img => new { img.ImageCode }, imgList.ToArray());
        //    db.SaveChanges();
        //}

        //private void SeedNews(ArchiveDataContext db)
        //{
        //    db.NewsItems.RemoveRange(db.NewsItems.ToList());

        //    db.SaveChanges();

        //    var newsList = new List<NewsItem>();

        //    for (int i = 1; i <= 20; i++)
        //    {
        //        var newsItem = new NewsItem
        //        {
        //            Id = i,
        //            CreationDate = DateTime.Now,
        //            PublishDate = new DateTime(2014, 10, 17),
        //            ExpiryDate = DateTime.Now.AddDays(i),
        //            HideAfterExpiry = i % 3 == 0
        //        };

        //        newsItem.Translations.Add(new NewsItemTranslation
        //        {
        //            NewsItem = newsItem,
        //            Heading = "Destaque " + i,
        //            LanguageCode = "pt",
        //            TextContent = "Texto " + i,
        //            Title = "Notícia " + i
        //        });

        //        if (i % 3 == 0)
        //        {
        //            newsItem.Translations.Add(new NewsItemTranslation
        //            {
        //                NewsItem = newsItem,
        //                Heading = "Heading " + i,
        //                LanguageCode = "en",
        //                TextContent = "Text " + i,
        //                Title = "News " + i
        //            });
        //        }

        //        newsList.Add(newsItem);
        //    }
        //    db.NewsItems.AddOrUpdate(ni => new { ni.PublishDate }, newsList.ToArray());
        //    db.SaveChanges();
        //}

        //private void SeedEvents(ArchiveDataContext db)
        //{
        //    db.Events.RemoveRange(db.Events.ToList());

        //    db.SaveChanges();

        //    var eventList = new List<Event>();

        //    for (int i = 1; i <= 20; i++)
        //    {
        //        var evt = new Event
        //        {
        //            Id = i,
        //            StartMoment = DateTime.Now,
        //            EndMoment = DateTime.Now.AddDays(i),

        //            PublishDate = DateTime.Now,
        //            ExpiryDate = DateTime.Now.AddDays(i),
        //            HideAfterExpiry = i % 3 == 0,
        //            EventType = EventType.Expo,
        //            Latitude = "1.0",
        //            Longitude = "1.0",
        //            Place = "Local " + i
        //        };

        //        evt.Translations.Add(new EventTranslation
        //        {
        //            Event = evt,
        //            Heading = "Destaque " + i,
        //            LanguageCode = "pt",
        //            TextContent = "Texto " + i,
        //            Title = "Evento " + i
        //        });

        //        if (i % 3 == 0)
        //        {
        //            evt.Translations.Add(new EventTranslation
        //            {
        //                Event = evt,
        //                Heading = "Heading " + i,
        //                LanguageCode = "en",
        //                TextContent = "Text " + i,
        //                Title = "Event " + i
        //            });
        //        }

        //        eventList.Add(evt);
        //    }
        //    db.Events.AddOrUpdate(e => new { e.Place }, eventList.ToArray());
        //    db.SaveChanges();
        //}
    }
}
