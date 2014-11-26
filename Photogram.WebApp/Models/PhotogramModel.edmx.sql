
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/12/2014 18:24:29
-- Generated from EDMX file: C:\Programming\Websites\Photogram\Photogram.WebApp\Models\PhotogramModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Photogram];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TextValueSetupMainTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TextValue] DROP CONSTRAINT [FK_TextValueSetupMainTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_TextValueSetupFooter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TextValue] DROP CONSTRAINT [FK_TextValueSetupFooter];
GO
IF OBJECT_ID(N'[dbo].[FK_GalleryTextValueTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TextValue] DROP CONSTRAINT [FK_GalleryTextValueTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_GalleryTextValueDescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TextValue] DROP CONSTRAINT [FK_GalleryTextValueDescription];
GO
IF OBJECT_ID(N'[dbo].[FK_GalleryMedia_Gallery]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GalleryMedia] DROP CONSTRAINT [FK_GalleryMedia_Gallery];
GO
IF OBJECT_ID(N'[dbo].[FK_GalleryMedia_Media]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GalleryMedia] DROP CONSTRAINT [FK_GalleryMedia_Media];
GO
IF OBJECT_ID(N'[dbo].[FK_TextValueLanguage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TextValue] DROP CONSTRAINT [FK_TextValueLanguage];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Setup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Setup];
GO
IF OBJECT_ID(N'[dbo].[Gallery]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Gallery];
GO
IF OBJECT_ID(N'[dbo].[TextValue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TextValue];
GO
IF OBJECT_ID(N'[dbo].[Media]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Media];
GO
IF OBJECT_ID(N'[dbo].[Language]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Language];
GO
IF OBJECT_ID(N'[dbo].[GalleryMedia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GalleryMedia];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Setup'
CREATE TABLE [dbo].[Setup] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(254)  NOT NULL
);
GO

-- Creating table 'Gallery'
CREATE TABLE [dbo].[Gallery] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Visible] bit  NOT NULL,
    [Type] int  NOT NULL,
    [Year] smallint  NOT NULL,
    [Position] int  NOT NULL
);
GO

-- Creating table 'TextValue'
CREATE TABLE [dbo].[TextValue] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [SetupMainTitle_Id] int  NULL,
    [SetupFooter_Id] int  NULL,
    [GalleryTitle_Id] int  NULL,
    [GalleryDescription_Id] int  NULL,
    [Language_Code] nvarchar(3)  NOT NULL
);
GO

-- Creating table 'Media'
CREATE TABLE [dbo].[Media] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Source] nvarchar(max)  NOT NULL,
    [Visible] bit  NOT NULL,
    [Type] int  NOT NULL
);
GO

-- Creating table 'Language'
CREATE TABLE [dbo].[Language] (
    [Code] nvarchar(3)  NOT NULL,
    [Name] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'GalleryMedia'
CREATE TABLE [dbo].[GalleryMedia] (
    [Gallery_Id] int  NOT NULL,
    [Media_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Setup'
ALTER TABLE [dbo].[Setup]
ADD CONSTRAINT [PK_Setup]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Gallery'
ALTER TABLE [dbo].[Gallery]
ADD CONSTRAINT [PK_Gallery]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TextValue'
ALTER TABLE [dbo].[TextValue]
ADD CONSTRAINT [PK_TextValue]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Media'
ALTER TABLE [dbo].[Media]
ADD CONSTRAINT [PK_Media]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Code] in table 'Language'
ALTER TABLE [dbo].[Language]
ADD CONSTRAINT [PK_Language]
    PRIMARY KEY CLUSTERED ([Code] ASC);
GO

-- Creating primary key on [Gallery_Id], [Media_Id] in table 'GalleryMedia'
ALTER TABLE [dbo].[GalleryMedia]
ADD CONSTRAINT [PK_GalleryMedia]
    PRIMARY KEY CLUSTERED ([Gallery_Id], [Media_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SetupMainTitle_Id] in table 'TextValue'
ALTER TABLE [dbo].[TextValue]
ADD CONSTRAINT [FK_TextValueSetupMainTitle]
    FOREIGN KEY ([SetupMainTitle_Id])
    REFERENCES [dbo].[Setup]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TextValueSetupMainTitle'
CREATE INDEX [IX_FK_TextValueSetupMainTitle]
ON [dbo].[TextValue]
    ([SetupMainTitle_Id]);
GO

-- Creating foreign key on [SetupFooter_Id] in table 'TextValue'
ALTER TABLE [dbo].[TextValue]
ADD CONSTRAINT [FK_TextValueSetupFooter]
    FOREIGN KEY ([SetupFooter_Id])
    REFERENCES [dbo].[Setup]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TextValueSetupFooter'
CREATE INDEX [IX_FK_TextValueSetupFooter]
ON [dbo].[TextValue]
    ([SetupFooter_Id]);
GO

-- Creating foreign key on [GalleryTitle_Id] in table 'TextValue'
ALTER TABLE [dbo].[TextValue]
ADD CONSTRAINT [FK_GalleryTextValueTitle]
    FOREIGN KEY ([GalleryTitle_Id])
    REFERENCES [dbo].[Gallery]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GalleryTextValueTitle'
CREATE INDEX [IX_FK_GalleryTextValueTitle]
ON [dbo].[TextValue]
    ([GalleryTitle_Id]);
GO

-- Creating foreign key on [GalleryDescription_Id] in table 'TextValue'
ALTER TABLE [dbo].[TextValue]
ADD CONSTRAINT [FK_GalleryTextValueDescription]
    FOREIGN KEY ([GalleryDescription_Id])
    REFERENCES [dbo].[Gallery]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GalleryTextValueDescription'
CREATE INDEX [IX_FK_GalleryTextValueDescription]
ON [dbo].[TextValue]
    ([GalleryDescription_Id]);
GO

-- Creating foreign key on [Gallery_Id] in table 'GalleryMedia'
ALTER TABLE [dbo].[GalleryMedia]
ADD CONSTRAINT [FK_GalleryMedia_Gallery]
    FOREIGN KEY ([Gallery_Id])
    REFERENCES [dbo].[Gallery]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Media_Id] in table 'GalleryMedia'
ALTER TABLE [dbo].[GalleryMedia]
ADD CONSTRAINT [FK_GalleryMedia_Media]
    FOREIGN KEY ([Media_Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GalleryMedia_Media'
CREATE INDEX [IX_FK_GalleryMedia_Media]
ON [dbo].[GalleryMedia]
    ([Media_Id]);
GO

-- Creating foreign key on [Language_Code] in table 'TextValue'
ALTER TABLE [dbo].[TextValue]
ADD CONSTRAINT [FK_TextValueLanguage]
    FOREIGN KEY ([Language_Code])
    REFERENCES [dbo].[Language]
        ([Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TextValueLanguage'
CREATE INDEX [IX_FK_TextValueLanguage]
ON [dbo].[TextValue]
    ([Language_Code]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------