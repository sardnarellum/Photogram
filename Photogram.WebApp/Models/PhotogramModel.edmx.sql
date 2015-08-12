
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/12/2015 15:06:59
-- Generated from EDMX file: C:\Programming\Websites\aspHOSTpage\sardnarellum\Photogram\Photogram.WebApp\Models\PhotogramModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [sardnarellum\Photogram];
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
IF OBJECT_ID(N'[dbo].[FK_ProjectTextValueTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TextValue] DROP CONSTRAINT [FK_ProjectTextValueTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectTextValueDescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TextValue] DROP CONSTRAINT [FK_ProjectTextValueDescription];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Media] DROP CONSTRAINT [FK_ProjectMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_TextValueLanguage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TextValue] DROP CONSTRAINT [FK_TextValueLanguage];
GO
IF OBJECT_ID(N'[dbo].[FK_TextValueMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TextValue] DROP CONSTRAINT [FK_TextValueMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_MediaTextValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TextValue] DROP CONSTRAINT [FK_MediaTextValue];
GO
IF OBJECT_ID(N'[dbo].[FK_MediaLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Log] DROP CONSTRAINT [FK_MediaLog];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Log] DROP CONSTRAINT [FK_ProjectLog];
GO
IF OBJECT_ID(N'[dbo].[FK_TextValueLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Log] DROP CONSTRAINT [FK_TextValueLog];
GO
IF OBJECT_ID(N'[dbo].[FK_SetupLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Log] DROP CONSTRAINT [FK_SetupLog];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Log] DROP CONSTRAINT [FK_LanguageLog];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Setup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Setup];
GO
IF OBJECT_ID(N'[dbo].[Project]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Project];
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
IF OBJECT_ID(N'[dbo].[Log]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Log];
GO
IF OBJECT_ID(N'[dbo].[Stat]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stat];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Setup'
CREATE TABLE [dbo].[Setup] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(254)  NOT NULL,
    [Published] bit  NOT NULL
);
GO

-- Creating table 'Project'
CREATE TABLE [dbo].[Project] (
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
    [ProjectTitle_Id] int  NULL,
    [ProjectDescription_Id] int  NULL,
    [Language_LCID] int  NOT NULL,
    [MediaTitle_Id] int  NULL,
    [MediaDescription_Id] int  NULL
);
GO

-- Creating table 'Media'
CREATE TABLE [dbo].[Media] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(max)  NOT NULL,
    [Type] int  NOT NULL,
    [PositionInProject] int  NULL,
    [Project_Id] int  NULL
);
GO

-- Creating table 'Language'
CREATE TABLE [dbo].[Language] (
    [LCID] int  NOT NULL
);
GO

-- Creating table 'Log'
CREATE TABLE [dbo].[Log] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Time] datetime  NOT NULL,
    [Type] int  NOT NULL,
    [Media_Id] int  NULL,
    [Project_Id] int  NULL,
    [TextValue_Id] int  NULL,
    [Setup_Id] int  NULL,
    [Language_LCID] int  NULL
);
GO

-- Creating table 'Stat'
CREATE TABLE [dbo].[Stat] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Time] datetime  NOT NULL,
    [Page] nvarchar(max)  NOT NULL
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

-- Creating primary key on [Id] in table 'Project'
ALTER TABLE [dbo].[Project]
ADD CONSTRAINT [PK_Project]
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

-- Creating primary key on [LCID] in table 'Language'
ALTER TABLE [dbo].[Language]
ADD CONSTRAINT [PK_Language]
    PRIMARY KEY CLUSTERED ([LCID] ASC);
GO

-- Creating primary key on [Id] in table 'Log'
ALTER TABLE [dbo].[Log]
ADD CONSTRAINT [PK_Log]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Stat'
ALTER TABLE [dbo].[Stat]
ADD CONSTRAINT [PK_Stat]
    PRIMARY KEY CLUSTERED ([Id] ASC);
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
GO

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
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TextValueSetupFooter'
CREATE INDEX [IX_FK_TextValueSetupFooter]
ON [dbo].[TextValue]
    ([SetupFooter_Id]);
GO

-- Creating foreign key on [ProjectTitle_Id] in table 'TextValue'
ALTER TABLE [dbo].[TextValue]
ADD CONSTRAINT [FK_ProjectTextValueTitle]
    FOREIGN KEY ([ProjectTitle_Id])
    REFERENCES [dbo].[Project]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectTextValueTitle'
CREATE INDEX [IX_FK_ProjectTextValueTitle]
ON [dbo].[TextValue]
    ([ProjectTitle_Id]);
GO

-- Creating foreign key on [ProjectDescription_Id] in table 'TextValue'
ALTER TABLE [dbo].[TextValue]
ADD CONSTRAINT [FK_ProjectTextValueDescription]
    FOREIGN KEY ([ProjectDescription_Id])
    REFERENCES [dbo].[Project]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectTextValueDescription'
CREATE INDEX [IX_FK_ProjectTextValueDescription]
ON [dbo].[TextValue]
    ([ProjectDescription_Id]);
GO

-- Creating foreign key on [Project_Id] in table 'Media'
ALTER TABLE [dbo].[Media]
ADD CONSTRAINT [FK_ProjectMedia]
    FOREIGN KEY ([Project_Id])
    REFERENCES [dbo].[Project]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectMedia'
CREATE INDEX [IX_FK_ProjectMedia]
ON [dbo].[Media]
    ([Project_Id]);
GO

-- Creating foreign key on [Language_LCID] in table 'TextValue'
ALTER TABLE [dbo].[TextValue]
ADD CONSTRAINT [FK_TextValueLanguage]
    FOREIGN KEY ([Language_LCID])
    REFERENCES [dbo].[Language]
        ([LCID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TextValueLanguage'
CREATE INDEX [IX_FK_TextValueLanguage]
ON [dbo].[TextValue]
    ([Language_LCID]);
GO

-- Creating foreign key on [MediaTitle_Id] in table 'TextValue'
ALTER TABLE [dbo].[TextValue]
ADD CONSTRAINT [FK_TextValueMedia]
    FOREIGN KEY ([MediaTitle_Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TextValueMedia'
CREATE INDEX [IX_FK_TextValueMedia]
ON [dbo].[TextValue]
    ([MediaTitle_Id]);
GO

-- Creating foreign key on [MediaDescription_Id] in table 'TextValue'
ALTER TABLE [dbo].[TextValue]
ADD CONSTRAINT [FK_MediaTextValue]
    FOREIGN KEY ([MediaDescription_Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaTextValue'
CREATE INDEX [IX_FK_MediaTextValue]
ON [dbo].[TextValue]
    ([MediaDescription_Id]);
GO

-- Creating foreign key on [Media_Id] in table 'Log'
ALTER TABLE [dbo].[Log]
ADD CONSTRAINT [FK_MediaLog]
    FOREIGN KEY ([Media_Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaLog'
CREATE INDEX [IX_FK_MediaLog]
ON [dbo].[Log]
    ([Media_Id]);
GO

-- Creating foreign key on [Project_Id] in table 'Log'
ALTER TABLE [dbo].[Log]
ADD CONSTRAINT [FK_ProjectLog]
    FOREIGN KEY ([Project_Id])
    REFERENCES [dbo].[Project]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectLog'
CREATE INDEX [IX_FK_ProjectLog]
ON [dbo].[Log]
    ([Project_Id]);
GO

-- Creating foreign key on [TextValue_Id] in table 'Log'
ALTER TABLE [dbo].[Log]
ADD CONSTRAINT [FK_TextValueLog]
    FOREIGN KEY ([TextValue_Id])
    REFERENCES [dbo].[TextValue]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TextValueLog'
CREATE INDEX [IX_FK_TextValueLog]
ON [dbo].[Log]
    ([TextValue_Id]);
GO

-- Creating foreign key on [Setup_Id] in table 'Log'
ALTER TABLE [dbo].[Log]
ADD CONSTRAINT [FK_SetupLog]
    FOREIGN KEY ([Setup_Id])
    REFERENCES [dbo].[Setup]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SetupLog'
CREATE INDEX [IX_FK_SetupLog]
ON [dbo].[Log]
    ([Setup_Id]);
GO

-- Creating foreign key on [Language_LCID] in table 'Log'
ALTER TABLE [dbo].[Log]
ADD CONSTRAINT [FK_LanguageLog]
    FOREIGN KEY ([Language_LCID])
    REFERENCES [dbo].[Language]
        ([LCID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageLog'
CREATE INDEX [IX_FK_LanguageLog]
ON [dbo].[Log]
    ([Language_LCID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------