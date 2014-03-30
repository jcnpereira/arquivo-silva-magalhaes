namespace ArquivoSilvaMagalhaes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArchiveDataContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 30),
                        BirthDate = c.DateTime(nullable: false),
                        DeathDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthorTexts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LanguageCode = c.String(),
                        Nationality = c.String(nullable: false, maxLength: 20),
                        Biography = c.String(nullable: false),
                        Curriculum = c.String(nullable: false),
                        Author_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Byte(nullable: false),
                        ProductionDate = c.DateTime(nullable: false),
                        LogoLocation = c.String(),
                        HasAttachments = c.Boolean(nullable: false),
                        OrganizationSystem = c.String(),
                        Notes = c.String(),
                        IsVisible = c.Boolean(nullable: false),
                        CatalogCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CollectionTexts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LanguageCode = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Description = c.String(),
                        Provenience = c.String(),
                        AdministrativeAndBiographicStory = c.String(),
                        Dimension = c.String(),
                        FieldAndContents = c.String(),
                        CopyrightInfo = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.LanguageCode })
                .ForeignKey("dbo.Collections", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResponsibleName = c.String(nullable: false),
                        DocumentDate = c.DateTime(nullable: false),
                        CatalogationDate = c.DateTime(nullable: false),
                        Notes = c.String(),
                        CatalogCode = c.String(),
                        Author_Id = c.Int(nullable: false),
                        Collection_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .ForeignKey("dbo.Collections", t => t.Collection_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Collection_Id);
            
            CreateTable(
                "dbo.DocumentTexts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LanguageCode = c.String(nullable: false, maxLength: 128),
                        DocumentLocation = c.String(),
                        FieldAndContents = c.String(),
                        Description = c.String(),
                        DocumentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.LanguageCode })
                .ForeignKey("dbo.Documents", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.DocumentId);
            
            CreateTable(
                "dbo.Keywords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KeywordTexts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LanguageCode = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.LanguageCode })
                .ForeignKey("dbo.Keywords", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Specimen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CatalogCode = c.String(),
                        AuthorCatalogationCode = c.String(),
                        HasMarksOrStamps = c.Boolean(nullable: false),
                        Indexation = c.String(),
                        Notes = c.String(),
                        FormatId = c.Int(nullable: false),
                        DocumentId = c.Int(nullable: false),
                        Process_Id = c.Int(),
                        Keyword_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.DocumentId, cascadeDelete: true)
                .ForeignKey("dbo.Formats", t => t.FormatId, cascadeDelete: true)
                .ForeignKey("dbo.Processes", t => t.Process_Id)
                .ForeignKey("dbo.Keywords", t => t.Keyword_Id)
                .Index(t => t.FormatId)
                .Index(t => t.DocumentId)
                .Index(t => t.Process_Id)
                .Index(t => t.Keyword_Id);
            
            CreateTable(
                "dbo.Classifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClassificationTexts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LanguageCode = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.LanguageCode })
                .ForeignKey("dbo.Classifications", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.DigitalPhotographs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScanDate = c.String(),
                        StoreLocation = c.String(),
                        Process = c.String(),
                        CopyrightInfo = c.String(),
                        IsVisible = c.String(),
                        SpecimenId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specimen", t => t.SpecimenId, cascadeDelete: true)
                .Index(t => t.SpecimenId);
            
            CreateTable(
                "dbo.ShowcasePhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommenterName = c.String(),
                        CommenterEmail = c.String(),
                        IsEmailVisible = c.String(),
                        VisibleSince = c.String(),
                        DigitalPhotographId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DigitalPhotographs", t => t.DigitalPhotographId, cascadeDelete: true)
                .Index(t => t.DigitalPhotographId);
            
            CreateTable(
                "dbo.ShowcasePhotoTexts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LanguageCode = c.String(nullable: false, maxLength: 128),
                        Comment = c.String(),
                        ShowcasePhotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.LanguageCode })
                .ForeignKey("dbo.ShowcasePhotoes", t => t.ShowcasePhotoId, cascadeDelete: true)
                .Index(t => t.ShowcasePhotoId);
            
            CreateTable(
                "dbo.Formats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormatDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Processes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProcessTexts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LanguageCode = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                        ProcessId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.LanguageCode })
                .ForeignKey("dbo.Processes", t => t.ProcessId, cascadeDelete: true)
                .Index(t => t.ProcessId);
            
            CreateTable(
                "dbo.SpecimenTexts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LanguageCode = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Topic = c.String(),
                        Description = c.String(),
                        SimpleStateDescription = c.String(),
                        DetailedStateDescription = c.String(),
                        InterventionDescription = c.String(),
                        Publication = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.LanguageCode })
                .ForeignKey("dbo.Specimen", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.BannerPhotographs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UriPath = c.String(),
                        PublicationDate = c.DateTime(nullable: false),
                        RemovalDate = c.DateTime(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BannerPhotographTexts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LanguageCode = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.LanguageCode })
                .ForeignKey("dbo.BannerPhotographs", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Collaborators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EmailAddress = c.String(),
                        Task = c.String(),
                        ContactVisible = c.Boolean(nullable: false),
                        Contact = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Place = c.String(nullable: false, maxLength: 50),
                        Coordinates = c.String(),
                        VisitorInformation = c.String(),
                        StartMoment = c.DateTime(nullable: false),
                        EndMoment = c.DateTime(nullable: false),
                        PublishDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        HideAfterExpiry = c.Boolean(nullable: false),
                        EventType = c.Byte(nullable: false),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.DocumentAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MimeFormat = c.String(),
                        UriPath = c.String(),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublishDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        HideAfterExpiry = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(nullable: false),
                        NewsItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NewsItems", t => t.NewsItem_Id)
                .Index(t => t.NewsItem_Id);
            
            CreateTable(
                "dbo.ReferencedLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Link = c.String(),
                        Description = c.String(),
                        DateOfCreation = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        IsUsefulLink = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsTexts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LanguageCode = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Subtitle = c.String(),
                        Heading = c.String(),
                        TextContent = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.LanguageCode })
                .ForeignKey("dbo.NewsItems", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.DocumentAttachmentTexts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LanguageCode = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.LanguageCode })
                .ForeignKey("dbo.DocumentAttachments", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.EventTexts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LanguageCode = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Heading = c.String(),
                        SpotLight = c.String(),
                        TextContent = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.LanguageCode })
                .ForeignKey("dbo.Events", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Partnerships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Logo = c.String(),
                        SiteLink = c.String(),
                        EmailAddress = c.String(),
                        Contact = c.String(),
                        PartnershipType = c.Byte(nullable: false),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.SpotlightVideos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UriPath = c.String(),
                        PublicationDate = c.DateTime(nullable: false),
                        RemotionDate = c.DateTime(nullable: false),
                        IsPermanent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TechnicalDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CollectionAuthors",
                c => new
                    {
                        Collection_Id = c.Int(nullable: false),
                        Author_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Collection_Id, t.Author_Id })
                .ForeignKey("dbo.Collections", t => t.Collection_Id, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .Index(t => t.Collection_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.KeywordDocuments",
                c => new
                    {
                        Keyword_Id = c.Int(nullable: false),
                        Document_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Keyword_Id, t.Document_Id })
                .ForeignKey("dbo.Keywords", t => t.Keyword_Id, cascadeDelete: true)
                .ForeignKey("dbo.Documents", t => t.Document_Id, cascadeDelete: true)
                .Index(t => t.Keyword_Id)
                .Index(t => t.Document_Id);
            
            CreateTable(
                "dbo.ClassificationSpecimen",
                c => new
                    {
                        Classification_Id = c.Int(nullable: false),
                        Specimen_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Classification_Id, t.Specimen_Id })
                .ForeignKey("dbo.Classifications", t => t.Classification_Id, cascadeDelete: true)
                .ForeignKey("dbo.Specimen", t => t.Specimen_Id, cascadeDelete: true)
                .Index(t => t.Classification_Id)
                .Index(t => t.Specimen_Id);
            
            CreateTable(
                "dbo.DocumentAttachmentEvents",
                c => new
                    {
                        DocumentAttachment_Id = c.Int(nullable: false),
                        Event_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DocumentAttachment_Id, t.Event_Id })
                .ForeignKey("dbo.DocumentAttachments", t => t.DocumentAttachment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .Index(t => t.DocumentAttachment_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.ReferencedLinkEvents",
                c => new
                    {
                        ReferencedLink_Id = c.Int(nullable: false),
                        Event_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReferencedLink_Id, t.Event_Id })
                .ForeignKey("dbo.ReferencedLinks", t => t.ReferencedLink_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .Index(t => t.ReferencedLink_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.ReferencedLinkNewsItems",
                c => new
                    {
                        ReferencedLink_Id = c.Int(nullable: false),
                        NewsItem_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReferencedLink_Id, t.NewsItem_Id })
                .ForeignKey("dbo.ReferencedLinks", t => t.ReferencedLink_Id, cascadeDelete: true)
                .ForeignKey("dbo.NewsItems", t => t.NewsItem_Id, cascadeDelete: true)
                .Index(t => t.ReferencedLink_Id)
                .Index(t => t.NewsItem_Id);
            
            CreateTable(
                "dbo.NewsItemDocumentAttachments",
                c => new
                    {
                        NewsItem_Id = c.Int(nullable: false),
                        DocumentAttachment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NewsItem_Id, t.DocumentAttachment_Id })
                .ForeignKey("dbo.NewsItems", t => t.NewsItem_Id, cascadeDelete: true)
                .ForeignKey("dbo.DocumentAttachments", t => t.DocumentAttachment_Id, cascadeDelete: true)
                .Index(t => t.NewsItem_Id)
                .Index(t => t.DocumentAttachment_Id);
            
            CreateTable(
                "dbo.EventCollaborators",
                c => new
                    {
                        Event_Id = c.Int(nullable: false),
                        Collaborator_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_Id, t.Collaborator_Id })
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .ForeignKey("dbo.Collaborators", t => t.Collaborator_Id, cascadeDelete: true)
                .Index(t => t.Event_Id)
                .Index(t => t.Collaborator_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Partnerships", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.EventTexts", "Id", "dbo.Events");
            DropForeignKey("dbo.EventCollaborators", "Collaborator_Id", "dbo.Collaborators");
            DropForeignKey("dbo.EventCollaborators", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.DocumentAttachmentTexts", "Id", "dbo.DocumentAttachments");
            DropForeignKey("dbo.NewsTexts", "Id", "dbo.NewsItems");
            DropForeignKey("dbo.NewsItems", "NewsItem_Id", "dbo.NewsItems");
            DropForeignKey("dbo.NewsItemDocumentAttachments", "DocumentAttachment_Id", "dbo.DocumentAttachments");
            DropForeignKey("dbo.NewsItemDocumentAttachments", "NewsItem_Id", "dbo.NewsItems");
            DropForeignKey("dbo.ReferencedLinkNewsItems", "NewsItem_Id", "dbo.NewsItems");
            DropForeignKey("dbo.ReferencedLinkNewsItems", "ReferencedLink_Id", "dbo.ReferencedLinks");
            DropForeignKey("dbo.ReferencedLinkEvents", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.ReferencedLinkEvents", "ReferencedLink_Id", "dbo.ReferencedLinks");
            DropForeignKey("dbo.DocumentAttachmentEvents", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.DocumentAttachmentEvents", "DocumentAttachment_Id", "dbo.DocumentAttachments");
            DropForeignKey("dbo.BannerPhotographTexts", "Id", "dbo.BannerPhotographs");
            DropForeignKey("dbo.Specimen", "Keyword_Id", "dbo.Keywords");
            DropForeignKey("dbo.SpecimenTexts", "Id", "dbo.Specimen");
            DropForeignKey("dbo.Specimen", "Process_Id", "dbo.Processes");
            DropForeignKey("dbo.ProcessTexts", "ProcessId", "dbo.Processes");
            DropForeignKey("dbo.Specimen", "FormatId", "dbo.Formats");
            DropForeignKey("dbo.Specimen", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.DigitalPhotographs", "SpecimenId", "dbo.Specimen");
            DropForeignKey("dbo.ShowcasePhotoTexts", "ShowcasePhotoId", "dbo.ShowcasePhotoes");
            DropForeignKey("dbo.ShowcasePhotoes", "DigitalPhotographId", "dbo.DigitalPhotographs");
            DropForeignKey("dbo.ClassificationSpecimen", "Specimen_Id", "dbo.Specimen");
            DropForeignKey("dbo.ClassificationSpecimen", "Classification_Id", "dbo.Classifications");
            DropForeignKey("dbo.ClassificationTexts", "Id", "dbo.Classifications");
            DropForeignKey("dbo.KeywordTexts", "Id", "dbo.Keywords");
            DropForeignKey("dbo.KeywordDocuments", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.KeywordDocuments", "Keyword_Id", "dbo.Keywords");
            DropForeignKey("dbo.DocumentTexts", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.Documents", "Collection_Id", "dbo.Collections");
            DropForeignKey("dbo.Documents", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.CollectionTexts", "Id", "dbo.Collections");
            DropForeignKey("dbo.CollectionAuthors", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.CollectionAuthors", "Collection_Id", "dbo.Collections");
            DropForeignKey("dbo.AuthorTexts", "Author_Id", "dbo.Authors");
            DropIndex("dbo.EventCollaborators", new[] { "Collaborator_Id" });
            DropIndex("dbo.EventCollaborators", new[] { "Event_Id" });
            DropIndex("dbo.NewsItemDocumentAttachments", new[] { "DocumentAttachment_Id" });
            DropIndex("dbo.NewsItemDocumentAttachments", new[] { "NewsItem_Id" });
            DropIndex("dbo.ReferencedLinkNewsItems", new[] { "NewsItem_Id" });
            DropIndex("dbo.ReferencedLinkNewsItems", new[] { "ReferencedLink_Id" });
            DropIndex("dbo.ReferencedLinkEvents", new[] { "Event_Id" });
            DropIndex("dbo.ReferencedLinkEvents", new[] { "ReferencedLink_Id" });
            DropIndex("dbo.DocumentAttachmentEvents", new[] { "Event_Id" });
            DropIndex("dbo.DocumentAttachmentEvents", new[] { "DocumentAttachment_Id" });
            DropIndex("dbo.ClassificationSpecimen", new[] { "Specimen_Id" });
            DropIndex("dbo.ClassificationSpecimen", new[] { "Classification_Id" });
            DropIndex("dbo.KeywordDocuments", new[] { "Document_Id" });
            DropIndex("dbo.KeywordDocuments", new[] { "Keyword_Id" });
            DropIndex("dbo.CollectionAuthors", new[] { "Author_Id" });
            DropIndex("dbo.CollectionAuthors", new[] { "Collection_Id" });
            DropIndex("dbo.Partnerships", new[] { "Event_Id" });
            DropIndex("dbo.EventTexts", new[] { "Id" });
            DropIndex("dbo.DocumentAttachmentTexts", new[] { "Id" });
            DropIndex("dbo.NewsTexts", new[] { "Id" });
            DropIndex("dbo.NewsItems", new[] { "NewsItem_Id" });
            DropIndex("dbo.Events", new[] { "Event_Id" });
            DropIndex("dbo.BannerPhotographTexts", new[] { "Id" });
            DropIndex("dbo.SpecimenTexts", new[] { "Id" });
            DropIndex("dbo.ProcessTexts", new[] { "ProcessId" });
            DropIndex("dbo.ShowcasePhotoTexts", new[] { "ShowcasePhotoId" });
            DropIndex("dbo.ShowcasePhotoes", new[] { "DigitalPhotographId" });
            DropIndex("dbo.DigitalPhotographs", new[] { "SpecimenId" });
            DropIndex("dbo.ClassificationTexts", new[] { "Id" });
            DropIndex("dbo.Specimen", new[] { "Keyword_Id" });
            DropIndex("dbo.Specimen", new[] { "Process_Id" });
            DropIndex("dbo.Specimen", new[] { "DocumentId" });
            DropIndex("dbo.Specimen", new[] { "FormatId" });
            DropIndex("dbo.KeywordTexts", new[] { "Id" });
            DropIndex("dbo.DocumentTexts", new[] { "DocumentId" });
            DropIndex("dbo.Documents", new[] { "Collection_Id" });
            DropIndex("dbo.Documents", new[] { "Author_Id" });
            DropIndex("dbo.CollectionTexts", new[] { "Id" });
            DropIndex("dbo.AuthorTexts", new[] { "Author_Id" });
            DropTable("dbo.EventCollaborators");
            DropTable("dbo.NewsItemDocumentAttachments");
            DropTable("dbo.ReferencedLinkNewsItems");
            DropTable("dbo.ReferencedLinkEvents");
            DropTable("dbo.DocumentAttachmentEvents");
            DropTable("dbo.ClassificationSpecimen");
            DropTable("dbo.KeywordDocuments");
            DropTable("dbo.CollectionAuthors");
            DropTable("dbo.TechnicalDocuments");
            DropTable("dbo.SpotlightVideos");
            DropTable("dbo.Partnerships");
            DropTable("dbo.EventTexts");
            DropTable("dbo.DocumentAttachmentTexts");
            DropTable("dbo.NewsTexts");
            DropTable("dbo.ReferencedLinks");
            DropTable("dbo.NewsItems");
            DropTable("dbo.DocumentAttachments");
            DropTable("dbo.Events");
            DropTable("dbo.Collaborators");
            DropTable("dbo.BannerPhotographTexts");
            DropTable("dbo.BannerPhotographs");
            DropTable("dbo.SpecimenTexts");
            DropTable("dbo.ProcessTexts");
            DropTable("dbo.Processes");
            DropTable("dbo.Formats");
            DropTable("dbo.ShowcasePhotoTexts");
            DropTable("dbo.ShowcasePhotoes");
            DropTable("dbo.DigitalPhotographs");
            DropTable("dbo.ClassificationTexts");
            DropTable("dbo.Classifications");
            DropTable("dbo.Specimen");
            DropTable("dbo.KeywordTexts");
            DropTable("dbo.Keywords");
            DropTable("dbo.DocumentTexts");
            DropTable("dbo.Documents");
            DropTable("dbo.CollectionTexts");
            DropTable("dbo.Collections");
            DropTable("dbo.AuthorTexts");
            DropTable("dbo.Authors");
        }
    }
}
