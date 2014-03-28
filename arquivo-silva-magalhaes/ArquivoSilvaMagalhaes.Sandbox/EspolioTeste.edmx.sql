
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/28/2014 12:21:06
-- Generated from EDMX file: D:\GitHub\arquivo-silva-magalhaes\arquivo-silva-magalhaes\ArquivoSilvaMagalhaes.Sandbox\EspolioTeste.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CollectionCollectionText]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CollectionTextSet] DROP CONSTRAINT [FK_CollectionCollectionText];
GO
IF OBJECT_ID(N'[dbo].[FK_CollectionDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentSet] DROP CONSTRAINT [FK_CollectionDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentDocumentText]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentTextSet] DROP CONSTRAINT [FK_DocumentDocumentText];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthorAuthorText]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuthorTextSet] DROP CONSTRAINT [FK_AuthorAuthorText];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthorDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentSet] DROP CONSTRAINT [FK_AuthorDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_CollectionAuthor_Collection]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CollectionAuthor] DROP CONSTRAINT [FK_CollectionAuthor_Collection];
GO
IF OBJECT_ID(N'[dbo].[FK_CollectionAuthor_Author]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CollectionAuthor] DROP CONSTRAINT [FK_CollectionAuthor_Author];
GO
IF OBJECT_ID(N'[dbo].[FK_KeywordKeywordText]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[KeywordTextSet] DROP CONSTRAINT [FK_KeywordKeywordText];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentKeyword_Document]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentKeyword] DROP CONSTRAINT [FK_DocumentKeyword_Document];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentKeyword_Keyword]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentKeyword] DROP CONSTRAINT [FK_DocumentKeyword_Keyword];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecimenSpecimenText]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecimenTextSet] DROP CONSTRAINT [FK_SpecimenSpecimenText];
GO
IF OBJECT_ID(N'[dbo].[FK_FormatSpecimen]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecimenSet] DROP CONSTRAINT [FK_FormatSpecimen];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentSpecimen]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecimenSet] DROP CONSTRAINT [FK_DocumentSpecimen];
GO
IF OBJECT_ID(N'[dbo].[FK_ProcessProcessText]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProcessTextSet] DROP CONSTRAINT [FK_ProcessProcessText];
GO
IF OBJECT_ID(N'[dbo].[FK_ProcessSpecimen]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecimenSet] DROP CONSTRAINT [FK_ProcessSpecimen];
GO
IF OBJECT_ID(N'[dbo].[FK_ClassificationClassificationText]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClassificationTextSet] DROP CONSTRAINT [FK_ClassificationClassificationText];
GO
IF OBJECT_ID(N'[dbo].[FK_ClassificationSpecimen_Classification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClassificationSpecimen] DROP CONSTRAINT [FK_ClassificationSpecimen_Classification];
GO
IF OBJECT_ID(N'[dbo].[FK_ClassificationSpecimen_Specimen]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClassificationSpecimen] DROP CONSTRAINT [FK_ClassificationSpecimen_Specimen];
GO
IF OBJECT_ID(N'[dbo].[FK_DigitalPhotographShowcasePhoto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShowcasePhotoSet] DROP CONSTRAINT [FK_DigitalPhotographShowcasePhoto];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecimenDigitalPhotograph]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DigitalPhotographSet] DROP CONSTRAINT [FK_SpecimenDigitalPhotograph];
GO
IF OBJECT_ID(N'[dbo].[FK_ShowcasePhotoShowcasePhotoText]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShowcasePhotoTextSet] DROP CONSTRAINT [FK_ShowcasePhotoShowcasePhotoText];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CollectionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CollectionSet];
GO
IF OBJECT_ID(N'[dbo].[CollectionTextSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CollectionTextSet];
GO
IF OBJECT_ID(N'[dbo].[DocumentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentSet];
GO
IF OBJECT_ID(N'[dbo].[DocumentTextSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentTextSet];
GO
IF OBJECT_ID(N'[dbo].[AuthorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthorSet];
GO
IF OBJECT_ID(N'[dbo].[AuthorTextSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthorTextSet];
GO
IF OBJECT_ID(N'[dbo].[KeywordSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KeywordSet];
GO
IF OBJECT_ID(N'[dbo].[KeywordTextSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KeywordTextSet];
GO
IF OBJECT_ID(N'[dbo].[SpecimenSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpecimenSet];
GO
IF OBJECT_ID(N'[dbo].[SpecimenTextSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpecimenTextSet];
GO
IF OBJECT_ID(N'[dbo].[FormatSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FormatSet];
GO
IF OBJECT_ID(N'[dbo].[ProcessSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProcessSet];
GO
IF OBJECT_ID(N'[dbo].[ProcessTextSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProcessTextSet];
GO
IF OBJECT_ID(N'[dbo].[ClassificationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClassificationSet];
GO
IF OBJECT_ID(N'[dbo].[ClassificationTextSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClassificationTextSet];
GO
IF OBJECT_ID(N'[dbo].[DigitalPhotographSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DigitalPhotographSet];
GO
IF OBJECT_ID(N'[dbo].[ShowcasePhotoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShowcasePhotoSet];
GO
IF OBJECT_ID(N'[dbo].[ShowcasePhotoTextSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShowcasePhotoTextSet];
GO
IF OBJECT_ID(N'[dbo].[CollectionAuthor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CollectionAuthor];
GO
IF OBJECT_ID(N'[dbo].[DocumentKeyword]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentKeyword];
GO
IF OBJECT_ID(N'[dbo].[ClassificationSpecimen]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClassificationSpecimen];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CollectionSet'
CREATE TABLE [dbo].[CollectionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] tinyint  NOT NULL,
    [ProductionDate] datetime  NOT NULL,
    [LogoLocation] nvarchar(max)  NOT NULL,
    [HasAttachments] bit  NOT NULL,
    [OrganizationSystem] nvarchar(max)  NOT NULL,
    [Notes] nvarchar(max)  NOT NULL,
    [IsVisible] bit  NOT NULL,
    [CatalogCode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CollectionTextSet'
CREATE TABLE [dbo].[CollectionTextSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LanguageCode] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Provenience] nvarchar(max)  NOT NULL,
    [AdministrativeAndBiographicStory] nvarchar(max)  NOT NULL,
    [Dimension] nvarchar(max)  NOT NULL,
    [FieldAndContents] nvarchar(max)  NOT NULL,
    [CopyrightInfo] nvarchar(max)  NOT NULL,
    [CollectionId] int  NOT NULL
);
GO

-- Creating table 'DocumentSet'
CREATE TABLE [dbo].[DocumentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ResponsibleName] nvarchar(max)  NOT NULL,
    [DocumentDate] nvarchar(max)  NOT NULL,
    [CatalogationDate] datetime  NOT NULL,
    [Notes] nvarchar(max)  NOT NULL,
    [CollectionId] int  NOT NULL,
    [AuthorId] int  NOT NULL,
    [CatalogCode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DocumentTextSet'
CREATE TABLE [dbo].[DocumentTextSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LanguageCode] nvarchar(max)  NOT NULL,
    [DocumentLocation] nvarchar(max)  NOT NULL,
    [FieldAndContents] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [DocumentId] int  NOT NULL
);
GO

-- Creating table 'AuthorSet'
CREATE TABLE [dbo].[AuthorSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [BirthDate] nvarchar(max)  NOT NULL,
    [DeathDate] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AuthorTextSet'
CREATE TABLE [dbo].[AuthorTextSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LanguageCode] nvarchar(max)  NOT NULL,
    [Nationality] nvarchar(max)  NOT NULL,
    [Biography] nvarchar(max)  NOT NULL,
    [Curriculum] nvarchar(max)  NOT NULL,
    [AuthorId] int  NOT NULL
);
GO

-- Creating table 'KeywordSet'
CREATE TABLE [dbo].[KeywordSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'KeywordTextSet'
CREATE TABLE [dbo].[KeywordTextSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LanguageCode] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [KeywordId] int  NOT NULL
);
GO

-- Creating table 'SpecimenSet'
CREATE TABLE [dbo].[SpecimenSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CatalogCode] nvarchar(max)  NOT NULL,
    [AuthorCatalogationCode] nvarchar(max)  NOT NULL,
    [HasMarksOrStamps] bit  NOT NULL,
    [Indexation] nvarchar(max)  NOT NULL,
    [Notes] nvarchar(max)  NOT NULL,
    [FormatId] int  NOT NULL,
    [DocumentId] int  NOT NULL,
    [Process_Id] int  NOT NULL
);
GO

-- Creating table 'SpecimenTextSet'
CREATE TABLE [dbo].[SpecimenTextSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LanguageCode] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Topic] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [SimpleStateDescription] nvarchar(max)  NOT NULL,
    [DetailedStateDescription] nvarchar(max)  NOT NULL,
    [InterventionDescription] nvarchar(max)  NOT NULL,
    [Publication] nvarchar(max)  NOT NULL,
    [SpecimenId] int  NOT NULL
);
GO

-- Creating table 'FormatSet'
CREATE TABLE [dbo].[FormatSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FormatDescription] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProcessSet'
CREATE TABLE [dbo].[ProcessSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'ProcessTextSet'
CREATE TABLE [dbo].[ProcessTextSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LanguageCode] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [ProcessId] int  NOT NULL
);
GO

-- Creating table 'ClassificationSet'
CREATE TABLE [dbo].[ClassificationSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'ClassificationTextSet'
CREATE TABLE [dbo].[ClassificationTextSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LanguageCode] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [ClassificationId] int  NOT NULL
);
GO

-- Creating table 'DigitalPhotographSet'
CREATE TABLE [dbo].[DigitalPhotographSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ScanDate] nvarchar(max)  NOT NULL,
    [StoreLocation] nvarchar(max)  NOT NULL,
    [Process] nvarchar(max)  NOT NULL,
    [CopyrightInfo] nvarchar(max)  NOT NULL,
    [IsVisible] nvarchar(max)  NOT NULL,
    [SpecimenId] int  NOT NULL
);
GO

-- Creating table 'ShowcasePhotoSet'
CREATE TABLE [dbo].[ShowcasePhotoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CommenterName] nvarchar(max)  NOT NULL,
    [CommenterEmail] nvarchar(max)  NOT NULL,
    [IsEmailVisible] nvarchar(max)  NOT NULL,
    [VisibleSince] nvarchar(max)  NOT NULL,
    [DigitalPhotographId] int  NOT NULL
);
GO

-- Creating table 'ShowcasePhotoTextSet'
CREATE TABLE [dbo].[ShowcasePhotoTextSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LanguageCode] nvarchar(max)  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [ShowcasePhotoId] int  NOT NULL
);
GO

-- Creating table 'CollectionAuthor'
CREATE TABLE [dbo].[CollectionAuthor] (
    [Collections_Id] int  NOT NULL,
    [Authors_Id] int  NOT NULL
);
GO

-- Creating table 'DocumentKeyword'
CREATE TABLE [dbo].[DocumentKeyword] (
    [Document_Id] int  NOT NULL,
    [Keywords_Id] int  NOT NULL
);
GO

-- Creating table 'ClassificationSpecimen'
CREATE TABLE [dbo].[ClassificationSpecimen] (
    [Classification_Id] int  NOT NULL,
    [Specimens_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CollectionSet'
ALTER TABLE [dbo].[CollectionSet]
ADD CONSTRAINT [PK_CollectionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [LanguageCode] in table 'CollectionTextSet'
ALTER TABLE [dbo].[CollectionTextSet]
ADD CONSTRAINT [PK_CollectionTextSet]
    PRIMARY KEY CLUSTERED ([Id], [LanguageCode] ASC);
GO

-- Creating primary key on [Id] in table 'DocumentSet'
ALTER TABLE [dbo].[DocumentSet]
ADD CONSTRAINT [PK_DocumentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [LanguageCode] in table 'DocumentTextSet'
ALTER TABLE [dbo].[DocumentTextSet]
ADD CONSTRAINT [PK_DocumentTextSet]
    PRIMARY KEY CLUSTERED ([Id], [LanguageCode] ASC);
GO

-- Creating primary key on [Id] in table 'AuthorSet'
ALTER TABLE [dbo].[AuthorSet]
ADD CONSTRAINT [PK_AuthorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [LanguageCode] in table 'AuthorTextSet'
ALTER TABLE [dbo].[AuthorTextSet]
ADD CONSTRAINT [PK_AuthorTextSet]
    PRIMARY KEY CLUSTERED ([Id], [LanguageCode] ASC);
GO

-- Creating primary key on [Id] in table 'KeywordSet'
ALTER TABLE [dbo].[KeywordSet]
ADD CONSTRAINT [PK_KeywordSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [LanguageCode] in table 'KeywordTextSet'
ALTER TABLE [dbo].[KeywordTextSet]
ADD CONSTRAINT [PK_KeywordTextSet]
    PRIMARY KEY CLUSTERED ([Id], [LanguageCode] ASC);
GO

-- Creating primary key on [Id] in table 'SpecimenSet'
ALTER TABLE [dbo].[SpecimenSet]
ADD CONSTRAINT [PK_SpecimenSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [LanguageCode] in table 'SpecimenTextSet'
ALTER TABLE [dbo].[SpecimenTextSet]
ADD CONSTRAINT [PK_SpecimenTextSet]
    PRIMARY KEY CLUSTERED ([Id], [LanguageCode] ASC);
GO

-- Creating primary key on [Id] in table 'FormatSet'
ALTER TABLE [dbo].[FormatSet]
ADD CONSTRAINT [PK_FormatSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProcessSet'
ALTER TABLE [dbo].[ProcessSet]
ADD CONSTRAINT [PK_ProcessSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [LanguageCode] in table 'ProcessTextSet'
ALTER TABLE [dbo].[ProcessTextSet]
ADD CONSTRAINT [PK_ProcessTextSet]
    PRIMARY KEY CLUSTERED ([Id], [LanguageCode] ASC);
GO

-- Creating primary key on [Id] in table 'ClassificationSet'
ALTER TABLE [dbo].[ClassificationSet]
ADD CONSTRAINT [PK_ClassificationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [LanguageCode] in table 'ClassificationTextSet'
ALTER TABLE [dbo].[ClassificationTextSet]
ADD CONSTRAINT [PK_ClassificationTextSet]
    PRIMARY KEY CLUSTERED ([Id], [LanguageCode] ASC);
GO

-- Creating primary key on [Id] in table 'DigitalPhotographSet'
ALTER TABLE [dbo].[DigitalPhotographSet]
ADD CONSTRAINT [PK_DigitalPhotographSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ShowcasePhotoSet'
ALTER TABLE [dbo].[ShowcasePhotoSet]
ADD CONSTRAINT [PK_ShowcasePhotoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [LanguageCode] in table 'ShowcasePhotoTextSet'
ALTER TABLE [dbo].[ShowcasePhotoTextSet]
ADD CONSTRAINT [PK_ShowcasePhotoTextSet]
    PRIMARY KEY CLUSTERED ([Id], [LanguageCode] ASC);
GO

-- Creating primary key on [Collections_Id], [Authors_Id] in table 'CollectionAuthor'
ALTER TABLE [dbo].[CollectionAuthor]
ADD CONSTRAINT [PK_CollectionAuthor]
    PRIMARY KEY CLUSTERED ([Collections_Id], [Authors_Id] ASC);
GO

-- Creating primary key on [Document_Id], [Keywords_Id] in table 'DocumentKeyword'
ALTER TABLE [dbo].[DocumentKeyword]
ADD CONSTRAINT [PK_DocumentKeyword]
    PRIMARY KEY CLUSTERED ([Document_Id], [Keywords_Id] ASC);
GO

-- Creating primary key on [Classification_Id], [Specimens_Id] in table 'ClassificationSpecimen'
ALTER TABLE [dbo].[ClassificationSpecimen]
ADD CONSTRAINT [PK_ClassificationSpecimen]
    PRIMARY KEY CLUSTERED ([Classification_Id], [Specimens_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CollectionId] in table 'CollectionTextSet'
ALTER TABLE [dbo].[CollectionTextSet]
ADD CONSTRAINT [FK_CollectionCollectionText]
    FOREIGN KEY ([CollectionId])
    REFERENCES [dbo].[CollectionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CollectionCollectionText'
CREATE INDEX [IX_FK_CollectionCollectionText]
ON [dbo].[CollectionTextSet]
    ([CollectionId]);
GO

-- Creating foreign key on [CollectionId] in table 'DocumentSet'
ALTER TABLE [dbo].[DocumentSet]
ADD CONSTRAINT [FK_CollectionDocument]
    FOREIGN KEY ([CollectionId])
    REFERENCES [dbo].[CollectionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CollectionDocument'
CREATE INDEX [IX_FK_CollectionDocument]
ON [dbo].[DocumentSet]
    ([CollectionId]);
GO

-- Creating foreign key on [DocumentId] in table 'DocumentTextSet'
ALTER TABLE [dbo].[DocumentTextSet]
ADD CONSTRAINT [FK_DocumentDocumentText]
    FOREIGN KEY ([DocumentId])
    REFERENCES [dbo].[DocumentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentDocumentText'
CREATE INDEX [IX_FK_DocumentDocumentText]
ON [dbo].[DocumentTextSet]
    ([DocumentId]);
GO

-- Creating foreign key on [AuthorId] in table 'AuthorTextSet'
ALTER TABLE [dbo].[AuthorTextSet]
ADD CONSTRAINT [FK_AuthorAuthorText]
    FOREIGN KEY ([AuthorId])
    REFERENCES [dbo].[AuthorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthorAuthorText'
CREATE INDEX [IX_FK_AuthorAuthorText]
ON [dbo].[AuthorTextSet]
    ([AuthorId]);
GO

-- Creating foreign key on [AuthorId] in table 'DocumentSet'
ALTER TABLE [dbo].[DocumentSet]
ADD CONSTRAINT [FK_AuthorDocument]
    FOREIGN KEY ([AuthorId])
    REFERENCES [dbo].[AuthorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthorDocument'
CREATE INDEX [IX_FK_AuthorDocument]
ON [dbo].[DocumentSet]
    ([AuthorId]);
GO

-- Creating foreign key on [Collections_Id] in table 'CollectionAuthor'
ALTER TABLE [dbo].[CollectionAuthor]
ADD CONSTRAINT [FK_CollectionAuthor_Collection]
    FOREIGN KEY ([Collections_Id])
    REFERENCES [dbo].[CollectionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Authors_Id] in table 'CollectionAuthor'
ALTER TABLE [dbo].[CollectionAuthor]
ADD CONSTRAINT [FK_CollectionAuthor_Author]
    FOREIGN KEY ([Authors_Id])
    REFERENCES [dbo].[AuthorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CollectionAuthor_Author'
CREATE INDEX [IX_FK_CollectionAuthor_Author]
ON [dbo].[CollectionAuthor]
    ([Authors_Id]);
GO

-- Creating foreign key on [KeywordId] in table 'KeywordTextSet'
ALTER TABLE [dbo].[KeywordTextSet]
ADD CONSTRAINT [FK_KeywordKeywordText]
    FOREIGN KEY ([KeywordId])
    REFERENCES [dbo].[KeywordSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_KeywordKeywordText'
CREATE INDEX [IX_FK_KeywordKeywordText]
ON [dbo].[KeywordTextSet]
    ([KeywordId]);
GO

-- Creating foreign key on [Document_Id] in table 'DocumentKeyword'
ALTER TABLE [dbo].[DocumentKeyword]
ADD CONSTRAINT [FK_DocumentKeyword_Document]
    FOREIGN KEY ([Document_Id])
    REFERENCES [dbo].[DocumentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Keywords_Id] in table 'DocumentKeyword'
ALTER TABLE [dbo].[DocumentKeyword]
ADD CONSTRAINT [FK_DocumentKeyword_Keyword]
    FOREIGN KEY ([Keywords_Id])
    REFERENCES [dbo].[KeywordSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentKeyword_Keyword'
CREATE INDEX [IX_FK_DocumentKeyword_Keyword]
ON [dbo].[DocumentKeyword]
    ([Keywords_Id]);
GO

-- Creating foreign key on [SpecimenId] in table 'SpecimenTextSet'
ALTER TABLE [dbo].[SpecimenTextSet]
ADD CONSTRAINT [FK_SpecimenSpecimenText]
    FOREIGN KEY ([SpecimenId])
    REFERENCES [dbo].[SpecimenSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SpecimenSpecimenText'
CREATE INDEX [IX_FK_SpecimenSpecimenText]
ON [dbo].[SpecimenTextSet]
    ([SpecimenId]);
GO

-- Creating foreign key on [FormatId] in table 'SpecimenSet'
ALTER TABLE [dbo].[SpecimenSet]
ADD CONSTRAINT [FK_FormatSpecimen]
    FOREIGN KEY ([FormatId])
    REFERENCES [dbo].[FormatSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FormatSpecimen'
CREATE INDEX [IX_FK_FormatSpecimen]
ON [dbo].[SpecimenSet]
    ([FormatId]);
GO

-- Creating foreign key on [DocumentId] in table 'SpecimenSet'
ALTER TABLE [dbo].[SpecimenSet]
ADD CONSTRAINT [FK_DocumentSpecimen]
    FOREIGN KEY ([DocumentId])
    REFERENCES [dbo].[DocumentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentSpecimen'
CREATE INDEX [IX_FK_DocumentSpecimen]
ON [dbo].[SpecimenSet]
    ([DocumentId]);
GO

-- Creating foreign key on [ProcessId] in table 'ProcessTextSet'
ALTER TABLE [dbo].[ProcessTextSet]
ADD CONSTRAINT [FK_ProcessProcessText]
    FOREIGN KEY ([ProcessId])
    REFERENCES [dbo].[ProcessSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcessProcessText'
CREATE INDEX [IX_FK_ProcessProcessText]
ON [dbo].[ProcessTextSet]
    ([ProcessId]);
GO

-- Creating foreign key on [Process_Id] in table 'SpecimenSet'
ALTER TABLE [dbo].[SpecimenSet]
ADD CONSTRAINT [FK_ProcessSpecimen]
    FOREIGN KEY ([Process_Id])
    REFERENCES [dbo].[ProcessSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcessSpecimen'
CREATE INDEX [IX_FK_ProcessSpecimen]
ON [dbo].[SpecimenSet]
    ([Process_Id]);
GO

-- Creating foreign key on [ClassificationId] in table 'ClassificationTextSet'
ALTER TABLE [dbo].[ClassificationTextSet]
ADD CONSTRAINT [FK_ClassificationClassificationText]
    FOREIGN KEY ([ClassificationId])
    REFERENCES [dbo].[ClassificationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClassificationClassificationText'
CREATE INDEX [IX_FK_ClassificationClassificationText]
ON [dbo].[ClassificationTextSet]
    ([ClassificationId]);
GO

-- Creating foreign key on [Classification_Id] in table 'ClassificationSpecimen'
ALTER TABLE [dbo].[ClassificationSpecimen]
ADD CONSTRAINT [FK_ClassificationSpecimen_Classification]
    FOREIGN KEY ([Classification_Id])
    REFERENCES [dbo].[ClassificationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Specimens_Id] in table 'ClassificationSpecimen'
ALTER TABLE [dbo].[ClassificationSpecimen]
ADD CONSTRAINT [FK_ClassificationSpecimen_Specimen]
    FOREIGN KEY ([Specimens_Id])
    REFERENCES [dbo].[SpecimenSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClassificationSpecimen_Specimen'
CREATE INDEX [IX_FK_ClassificationSpecimen_Specimen]
ON [dbo].[ClassificationSpecimen]
    ([Specimens_Id]);
GO

-- Creating foreign key on [DigitalPhotographId] in table 'ShowcasePhotoSet'
ALTER TABLE [dbo].[ShowcasePhotoSet]
ADD CONSTRAINT [FK_DigitalPhotographShowcasePhoto]
    FOREIGN KEY ([DigitalPhotographId])
    REFERENCES [dbo].[DigitalPhotographSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DigitalPhotographShowcasePhoto'
CREATE INDEX [IX_FK_DigitalPhotographShowcasePhoto]
ON [dbo].[ShowcasePhotoSet]
    ([DigitalPhotographId]);
GO

-- Creating foreign key on [SpecimenId] in table 'DigitalPhotographSet'
ALTER TABLE [dbo].[DigitalPhotographSet]
ADD CONSTRAINT [FK_SpecimenDigitalPhotograph]
    FOREIGN KEY ([SpecimenId])
    REFERENCES [dbo].[SpecimenSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SpecimenDigitalPhotograph'
CREATE INDEX [IX_FK_SpecimenDigitalPhotograph]
ON [dbo].[DigitalPhotographSet]
    ([SpecimenId]);
GO

-- Creating foreign key on [ShowcasePhotoId] in table 'ShowcasePhotoTextSet'
ALTER TABLE [dbo].[ShowcasePhotoTextSet]
ADD CONSTRAINT [FK_ShowcasePhotoShowcasePhotoText]
    FOREIGN KEY ([ShowcasePhotoId])
    REFERENCES [dbo].[ShowcasePhotoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ShowcasePhotoShowcasePhotoText'
CREATE INDEX [IX_FK_ShowcasePhotoShowcasePhotoText]
ON [dbo].[ShowcasePhotoTextSet]
    ([ShowcasePhotoId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------