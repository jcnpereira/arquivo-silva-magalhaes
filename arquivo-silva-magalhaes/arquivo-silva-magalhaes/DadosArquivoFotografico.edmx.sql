
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/18/2014 02:33:22
-- Generated from EDMX file: D:\GitHub\arquivo-silva-magalhaes\arquivo-silva-magalhaes\arquivo-silva-magalhaes\DadosArquivoFotografico.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ArquivoSilvaMagalhaes.Data];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PhotographicSpecimenPhotographicProcess]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhotographicSpecies] DROP CONSTRAINT [FK_PhotographicSpecimenPhotographicProcess];
GO
IF OBJECT_ID(N'[dbo].[FK_PhotographicSpecimenPhotographicFormat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhotographicSpecies] DROP CONSTRAINT [FK_PhotographicSpecimenPhotographicFormat];
GO
IF OBJECT_ID(N'[dbo].[FK_CollectionDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_CollectionDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentPhotographicSpecimen]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhotographicSpecies] DROP CONSTRAINT [FK_DocumentPhotographicSpecimen];
GO
IF OBJECT_ID(N'[dbo].[FK_KeyWordPhotographicSpecimen_KeyWord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[KeyWordPhotographicSpecimen] DROP CONSTRAINT [FK_KeyWordPhotographicSpecimen_KeyWord];
GO
IF OBJECT_ID(N'[dbo].[FK_KeyWordPhotographicSpecimen_PhotographicSpecimen]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[KeyWordPhotographicSpecimen] DROP CONSTRAINT [FK_KeyWordPhotographicSpecimen_PhotographicSpecimen];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthorDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_AuthorDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthorCollection_Author]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuthorCollection] DROP CONSTRAINT [FK_AuthorCollection_Author];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthorCollection_Collection]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuthorCollection] DROP CONSTRAINT [FK_AuthorCollection_Collection];
GO
IF OBJECT_ID(N'[dbo].[FK_DigitalPhotoPhotographicSpecimen]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DigitalPhotos] DROP CONSTRAINT [FK_DigitalPhotoPhotographicSpecimen];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Collections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Collections];
GO
IF OBJECT_ID(N'[dbo].[Authors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Authors];
GO
IF OBJECT_ID(N'[dbo].[Documents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents];
GO
IF OBJECT_ID(N'[dbo].[PhotographicSpecies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhotographicSpecies];
GO
IF OBJECT_ID(N'[dbo].[PhotographicProcesses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhotographicProcesses];
GO
IF OBJECT_ID(N'[dbo].[PhotographicFormats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhotographicFormats];
GO
IF OBJECT_ID(N'[dbo].[KeyWords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KeyWords];
GO
IF OBJECT_ID(N'[dbo].[DigitalPhotos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DigitalPhotos];
GO
IF OBJECT_ID(N'[dbo].[KeyWordPhotographicSpecimen]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KeyWordPhotographicSpecimen];
GO
IF OBJECT_ID(N'[dbo].[AuthorCollection]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthorCollection];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Collections'
CREATE TABLE [dbo].[Collections] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Provenience] nvarchar(max)  NOT NULL,
    [Dimension] smallint  NOT NULL,
    [HistoricalDetails] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [ProductionDate] datetime  NOT NULL
);
GO

-- Creating table 'Authors'
CREATE TABLE [dbo].[Authors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Nationality] nvarchar(max)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [DeathDate] datetime  NULL,
    [Biography] nvarchar(max)  NOT NULL,
    [Curriculum] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Documents'
CREATE TABLE [dbo].[Documents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [DocumentDate] datetime  NOT NULL,
    [CatalogDate] datetime  NOT NULL,
    [Notes] nvarchar(max)  NOT NULL,
    [CollectionId] int  NOT NULL,
    [AuthorId] int  NOT NULL
);
GO

-- Creating table 'PhotographicSpecies'
CREATE TABLE [dbo].[PhotographicSpecies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AuthorCode] nvarchar(max)  NOT NULL,
    [Notes] nvarchar(max)  NOT NULL,
    [InterventionDescription] nvarchar(max)  NOT NULL,
    [Topic] nvarchar(max)  NOT NULL,
    [SpecimenDate] datetime  NOT NULL,
    [PhotographicProcess_Id] int  NOT NULL,
    [PhotographicFormat_Id] int  NOT NULL,
    [Document_Id] int  NOT NULL
);
GO

-- Creating table 'PhotographicProcesses'
CREATE TABLE [dbo].[PhotographicProcesses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProcessName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PhotographicFormats'
CREATE TABLE [dbo].[PhotographicFormats] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FormatDescription] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'KeyWords'
CREATE TABLE [dbo].[KeyWords] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Word] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DigitalPhotos'
CREATE TABLE [dbo].[DigitalPhotos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PhotographicSpecimen_Id] int  NOT NULL
);
GO

-- Creating table 'KeyWordPhotographicSpecimen'
CREATE TABLE [dbo].[KeyWordPhotographicSpecimen] (
    [KeyWords_Id] int  NOT NULL,
    [KeyWordPhotographicSpecimen_KeyWord_Id] int  NOT NULL
);
GO

-- Creating table 'AuthorCollection'
CREATE TABLE [dbo].[AuthorCollection] (
    [AuthorCollection_Collection_Id] int  NOT NULL,
    [Collections_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Collections'
ALTER TABLE [dbo].[Collections]
ADD CONSTRAINT [PK_Collections]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Authors'
ALTER TABLE [dbo].[Authors]
ADD CONSTRAINT [PK_Authors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [PK_Documents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PhotographicSpecies'
ALTER TABLE [dbo].[PhotographicSpecies]
ADD CONSTRAINT [PK_PhotographicSpecies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PhotographicProcesses'
ALTER TABLE [dbo].[PhotographicProcesses]
ADD CONSTRAINT [PK_PhotographicProcesses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PhotographicFormats'
ALTER TABLE [dbo].[PhotographicFormats]
ADD CONSTRAINT [PK_PhotographicFormats]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'KeyWords'
ALTER TABLE [dbo].[KeyWords]
ADD CONSTRAINT [PK_KeyWords]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DigitalPhotos'
ALTER TABLE [dbo].[DigitalPhotos]
ADD CONSTRAINT [PK_DigitalPhotos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [KeyWords_Id], [KeyWordPhotographicSpecimen_KeyWord_Id] in table 'KeyWordPhotographicSpecimen'
ALTER TABLE [dbo].[KeyWordPhotographicSpecimen]
ADD CONSTRAINT [PK_KeyWordPhotographicSpecimen]
    PRIMARY KEY CLUSTERED ([KeyWords_Id], [KeyWordPhotographicSpecimen_KeyWord_Id] ASC);
GO

-- Creating primary key on [AuthorCollection_Collection_Id], [Collections_Id] in table 'AuthorCollection'
ALTER TABLE [dbo].[AuthorCollection]
ADD CONSTRAINT [PK_AuthorCollection]
    PRIMARY KEY CLUSTERED ([AuthorCollection_Collection_Id], [Collections_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PhotographicProcess_Id] in table 'PhotographicSpecies'
ALTER TABLE [dbo].[PhotographicSpecies]
ADD CONSTRAINT [FK_PhotographicSpecimenPhotographicProcess]
    FOREIGN KEY ([PhotographicProcess_Id])
    REFERENCES [dbo].[PhotographicProcesses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PhotographicSpecimenPhotographicProcess'
CREATE INDEX [IX_FK_PhotographicSpecimenPhotographicProcess]
ON [dbo].[PhotographicSpecies]
    ([PhotographicProcess_Id]);
GO

-- Creating foreign key on [PhotographicFormat_Id] in table 'PhotographicSpecies'
ALTER TABLE [dbo].[PhotographicSpecies]
ADD CONSTRAINT [FK_PhotographicSpecimenPhotographicFormat]
    FOREIGN KEY ([PhotographicFormat_Id])
    REFERENCES [dbo].[PhotographicFormats]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PhotographicSpecimenPhotographicFormat'
CREATE INDEX [IX_FK_PhotographicSpecimenPhotographicFormat]
ON [dbo].[PhotographicSpecies]
    ([PhotographicFormat_Id]);
GO

-- Creating foreign key on [CollectionId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_CollectionDocument]
    FOREIGN KEY ([CollectionId])
    REFERENCES [dbo].[Collections]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CollectionDocument'
CREATE INDEX [IX_FK_CollectionDocument]
ON [dbo].[Documents]
    ([CollectionId]);
GO

-- Creating foreign key on [Document_Id] in table 'PhotographicSpecies'
ALTER TABLE [dbo].[PhotographicSpecies]
ADD CONSTRAINT [FK_DocumentPhotographicSpecimen]
    FOREIGN KEY ([Document_Id])
    REFERENCES [dbo].[Documents]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentPhotographicSpecimen'
CREATE INDEX [IX_FK_DocumentPhotographicSpecimen]
ON [dbo].[PhotographicSpecies]
    ([Document_Id]);
GO

-- Creating foreign key on [KeyWords_Id] in table 'KeyWordPhotographicSpecimen'
ALTER TABLE [dbo].[KeyWordPhotographicSpecimen]
ADD CONSTRAINT [FK_KeyWordPhotographicSpecimen_KeyWord]
    FOREIGN KEY ([KeyWords_Id])
    REFERENCES [dbo].[KeyWords]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [KeyWordPhotographicSpecimen_KeyWord_Id] in table 'KeyWordPhotographicSpecimen'
ALTER TABLE [dbo].[KeyWordPhotographicSpecimen]
ADD CONSTRAINT [FK_KeyWordPhotographicSpecimen_PhotographicSpecimen]
    FOREIGN KEY ([KeyWordPhotographicSpecimen_KeyWord_Id])
    REFERENCES [dbo].[PhotographicSpecies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_KeyWordPhotographicSpecimen_PhotographicSpecimen'
CREATE INDEX [IX_FK_KeyWordPhotographicSpecimen_PhotographicSpecimen]
ON [dbo].[KeyWordPhotographicSpecimen]
    ([KeyWordPhotographicSpecimen_KeyWord_Id]);
GO

-- Creating foreign key on [AuthorId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_AuthorDocument]
    FOREIGN KEY ([AuthorId])
    REFERENCES [dbo].[Authors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthorDocument'
CREATE INDEX [IX_FK_AuthorDocument]
ON [dbo].[Documents]
    ([AuthorId]);
GO

-- Creating foreign key on [AuthorCollection_Collection_Id] in table 'AuthorCollection'
ALTER TABLE [dbo].[AuthorCollection]
ADD CONSTRAINT [FK_AuthorCollection_Author]
    FOREIGN KEY ([AuthorCollection_Collection_Id])
    REFERENCES [dbo].[Authors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Collections_Id] in table 'AuthorCollection'
ALTER TABLE [dbo].[AuthorCollection]
ADD CONSTRAINT [FK_AuthorCollection_Collection]
    FOREIGN KEY ([Collections_Id])
    REFERENCES [dbo].[Collections]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthorCollection_Collection'
CREATE INDEX [IX_FK_AuthorCollection_Collection]
ON [dbo].[AuthorCollection]
    ([Collections_Id]);
GO

-- Creating foreign key on [PhotographicSpecimen_Id] in table 'DigitalPhotos'
ALTER TABLE [dbo].[DigitalPhotos]
ADD CONSTRAINT [FK_DigitalPhotoPhotographicSpecimen]
    FOREIGN KEY ([PhotographicSpecimen_Id])
    REFERENCES [dbo].[PhotographicSpecies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DigitalPhotoPhotographicSpecimen'
CREATE INDEX [IX_FK_DigitalPhotoPhotographicSpecimen]
ON [dbo].[DigitalPhotos]
    ([PhotographicSpecimen_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------