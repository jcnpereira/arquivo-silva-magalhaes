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

        //private void SeedSpecimens(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        //{
        //    var specimens = new List<Specimen>
        //    {
        //        new Specimen { Id = 1, 
        //            AuthorCatalogationCode = "whatever", 
        //            CatalogCode = "whatever",
        //            Classification = new Classification { ClassificationTexts = new List<ClassificationText> {new ClassificationText { Value = "whatever", LanguageCode = "pt" } }},
        //            Document  = db.DocumentSet.Find(1),
        //            Format = new Format { FormatDescription = "8x11" },
        //            HasMarksOrStamps = false,
        //            Indexation = "whatever",
        //            Notes = "what?",
        //            Process = new Process { ProcessTexts = new List<ProcessText> {new ProcessText {}}
        //        }
        //    };
        //}


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
                    new Collaborator{ 
                        Id=1, 
                        Name="Abc",  
                        Contact="999999999", 
                        EmailAddress="abc@mail.com", 
                        Task="xyz", 
                        ContactVisible=true},
                    new Collaborator{ 
                        Id=2, 
                        Name="Def", 
                        Contact="911111111", 
                        EmailAddress="def@mail.com", 
                        Task="ghj", 
                        ContactVisible=true }     
            };
            db.SaveChanges();
        }

        protected void Event(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            var events = new List<Event>{
                new Event{ Id=1, 
                    StartMoment=new DateTime(15,01,05), 
                    EndMoment= new DateTime(15,04,05), 
                    HideAfterExpiry=true, 
                    Place ="IPT",
                    EventType=EventType.Expo, 
                    ExpiryDate = new DateTime(15,05,01),
                    VisitorInformation="", 
                    Coordinates="", 
                    Collaborators = new List<Collaborator>{db.CollaboratorSet.Find(1)} }
            };
            db.SaveChanges();
        }

        protected void EventText(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            var eventTexts = new List<EventText>{
                new EventText{ Id=1, 
                    LanguageCode="1", 
                    Title="Título", 
                    Heading="Cabeça", 
                    SpotLight="xpto", 
                    TextContent="Texto...", 
                    Event = db.EventSet.Find(1) },
                new EventText{ Id=2, 
                    LanguageCode="2", 
                    Title="Title", 
                    Heading="Head", 
                    SpotLight="xpto", 
                    TextContent="Text...", 
                    Event = db.EventSet.Find(1) }
            };
            db.SaveChanges();
        }


        protected void SeedNewsItem(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            var news = new List<NewsItem>{
                new NewsItem{ Id=1, 
                    CreationDate=DateTime.Now, 
                    ExpiryDate= new DateTime(15,02,08) , 
                    HideAfterExpiry=true, 
                    LastModificationDate= DateTime.Now,
                    PublishDate = DateTime.Today,
                    Links=null                        
                }
                };
            var newsText = new List<NewsText>{
                new NewsText{ Id=1, 
                    LanguageCode="1", 
                    Title="Título", 
                    Subtitle="Subtítulo", 
                    Heading="Cabeça",
                    NewsItem= db.NewsSet.Find(1),
                    TextContent= "Texto..."},
           
                new NewsText{ Id=2, 
                    LanguageCode="1", 
                    Title="Título", 
                    Subtitle="Subtítulo", 
                    Heading="Cabeça",
                    NewsItem= db.NewsSet.Find(1),
                    TextContent= "Texto..."}
            };
            db.SaveChanges();
        }

        protected void SeedPartnership(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        {
            var partner = new List<Partnership>{
                new Partnership{  Id=1,
                    Name="Aristides",
                    Contact="918888888",
                    EmailAddress="aristides@mail.com",
                    Event= db.EventSet.Find(1),
                    Logo = "Logotides",
                    PartnershipType= Models.SiteModels.PartnershipType.Sponsor,
                    SiteLink = "www.arist.com"}
            };

            db.SaveChanges();
        }



        //protected void SeedReferencedLinks(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        //{
        //    var links = new List<ReferencedLink>{
        //            new ReferencedLink{ Id=1, 
        //                Title="O grande evento", 
        //                Description="Ligação disponibilizada para o grande evento", 
        //                DateOfCreation=new DateTime(13,10,21), 
        //                EventsUsingThis=Event(1), 
        //                IsUsefulLink=true, 
        //                LastModifiedDate=DateTime.Now,  
        //                Link="www.ograndeevento.com"},

        //            new ReferencedLink{ Id=1, 
        //                Title="Nova coleção", 
        //                Description="Ligação disponibilizada para notícia da nova coleção fotográfica", 
        //                DateOfCreation=new DateTime(13,10,21), 
        //                EventsUsingThis=NewsItem(1), 
        //                IsUsefulLink=true, 
        //                LastModifiedDate=DateTime.Now,  
        //                Link="www.fotonews.com/newcollection"}
        //             };
        //   }

        //protected void SeedDocumentAttachment(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        //{
        //    var docs = new List<DocumentAttachment>
        //    {
        //        new DocumentAttachment{ 
        //            Id=1, 
        //            MimeFormat="text/pdf", 
        //            UriPath="h", 
        //            EventsUsingAttachment=EventText(1), 
        //            NewsUsingAttachment=NewsText(1),  
        //            Size=1, 
        //            TextUsingAttachment=DocumentText(1)}
        //    };
        //}

       
            
        // protected void SeedDocumentAttachmentText(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        //{
        //    var doctext = new List<DocumentAttachmentText>
        //     {
        //         new DocumentAttachmentText{ 
        //             Id=1, 
        //             DocumentAttachment=DocumentAttachment(1), 
        //             Title="Revelação", 
        //             Description="Como revelar...", 
        //             LanguageCode="pt"}
        //     };
        //}



        // protected void SeedBannerPhotograph(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        // {
        //     var banners = new BannerPhotograph
        //     {
        //         Id = 1,
        //         BannerTexts = BannerPhotographText(1),
        //         IsVisible = true,
        //         PublicationDate = new DateTime(12, 01, 14),
        //         RemovalDate = new DateTime(15, 01, 14),
        //         UriPath = "asdasd"
        //     };
        // }

         

        // protected void SeedBannerPhotographText(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        // {
        //     var bannerTexts = new BannerPhotographText
        //     {
        //         Id = 1,
        //         LanguageCode = "pt",
        //         Photograph = BannerPhotograph(1),
        //         Title = "PTbanner1"
        //     };
        // }


        //protected void SeedTechnicalDocuments(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        //{
        //    var documents = new List<TechnicalDocument>{
        //        new TechnicalDocument{ 
        //            Id=1,  
        //            DocumentType=DocumentType.PDF, 
        //            Format="texto",  
        //            Language="pt", 
        //            LastModificationDate=new DateTime(13,10,23), 
        //            Title="Processo de revelação",  
        //            UploadedDate=new DateTime(12,02,01),  
        //            UriPath="C:Processos/Revelacao/prorev.pdf"}
        //    };
        //}

        //protected void SeedSpotLightVideo(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        //{
        //    var videos = new List<SpotlightVideo>{
        //        new SpotlightVideo{
        //            Id=1,
        //            IsPermanent=false,
        //            PublicationDate= new DateTime(13,10,23),
        //            RemotionDate = new DateTime(15,10,23),
        //            UriPath = "http://www.youtube.com/videoAFSM"
                    
        //        }

        //    };
        //}

        //protected void SeedArchive( ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        //{
        //    var archives = new List<Archive>{
        //        new Archive{
        //             Id=1,
        //             Contacts=Contact(1),
        //             ArchiveTexts=ArchiveText(1)
        //        }

        //    };
        //}


        //protected void SeedArchiveText (ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        //{
        //    var archivetexts = new List<ArchiveText>{
        //        new ArchiveText{
        //            Id=1,
        //            LanguageCode="pt",
        //            Archive=Archive(1),
        //            ArchiveHistory="blablabla...",
        //            ArchiveMission="blalala..."
        //        }
        //    };
        //}


        //protected void SeedContact(ArquivoSilvaMagalhaes.Models.ArchiveDataContext db)
        //    {
        //        var contacts = new List<Contact>{
        //        new Contact{
        //            Id=1,
        //            Archive=Archive(1),
        //            Name="António",
        //            Address="Rua r",
        //            ContactDetails="Fixo",
        //            Email="antonio@mail.com",
        //            Service="Organizador de eventos"

        //        }
        //    };

        //    }



    }
}