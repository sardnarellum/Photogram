
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/17/2015 18:43:05
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

IF OBJECT_ID(N'[dbo].[FK_ProjectMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Media] DROP CONSTRAINT [FK_ProjectMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageProjectTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectTitle] DROP CONSTRAINT [FK_LanguageProjectTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectProjectTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectTitle] DROP CONSTRAINT [FK_ProjectProjectTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageProjectDescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectDescription] DROP CONSTRAINT [FK_LanguageProjectDescription];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectProjectDescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectDescription] DROP CONSTRAINT [FK_ProjectProjectDescription];
GO
IF OBJECT_ID(N'[dbo].[FK_MediaMediaTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MediaTitle] DROP CONSTRAINT [FK_MediaMediaTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageMediaTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MediaTitle] DROP CONSTRAINT [FK_LanguageMediaTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_MediaMediaDescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MediaDescription] DROP CONSTRAINT [FK_MediaMediaDescription];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageMediaDescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MediaDescription] DROP CONSTRAINT [FK_LanguageMediaDescription];
GO
IF OBJECT_ID(N'[dbo].[FK_SetupSetupMainTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SetupMainTitle] DROP CONSTRAINT [FK_SetupSetupMainTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageSetupMainTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SetupMainTitle] DROP CONSTRAINT [FK_LanguageSetupMainTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_SetupSetupFooter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SetupFooter] DROP CONSTRAINT [FK_SetupSetupFooter];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageSetupFooter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SetupFooter] DROP CONSTRAINT [FK_LanguageSetupFooter];
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
IF OBJECT_ID(N'[dbo].[Media]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Media];
GO
IF OBJECT_ID(N'[dbo].[Language]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Language];
GO
IF OBJECT_ID(N'[dbo].[ProjectTitle]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectTitle];
GO
IF OBJECT_ID(N'[dbo].[ProjectDescription]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectDescription];
GO
IF OBJECT_ID(N'[dbo].[MediaTitle]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaTitle];
GO
IF OBJECT_ID(N'[dbo].[MediaDescription]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaDescription];
GO
IF OBJECT_ID(N'[dbo].[SetupMainTitle]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SetupMainTitle];
GO
IF OBJECT_ID(N'[dbo].[SetupFooter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SetupFooter];
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

-- Creating table 'ProjectTitle'
CREATE TABLE [dbo].[ProjectTitle] (
    [ProjectId] int  NOT NULL,
    [LCID] int  NOT NULL,
    [Text] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ProjectDescription'
CREATE TABLE [dbo].[ProjectDescription] (
    [ProjectId] int  NOT NULL,
    [LCID] int  NOT NULL,
    [Text] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MediaTitle'
CREATE TABLE [dbo].[MediaTitle] (
    [MediaId] int  NOT NULL,
    [LCID] int  NOT NULL,
    [Text] nvarchar(80)  NOT NULL
);
GO

-- Creating table 'MediaDescription'
CREATE TABLE [dbo].[MediaDescription] (
    [MediaId] int  NOT NULL,
    [LCID] int  NOT NULL,
    [Text] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SetupMainTitle'
CREATE TABLE [dbo].[SetupMainTitle] (
    [SetupId] int  NOT NULL,
    [LCID] int  NOT NULL,
    [Text] nvarchar(80)  NOT NULL
);
GO

-- Creating table 'SetupFooter'
CREATE TABLE [dbo].[SetupFooter] (
    [SetupId] int  NOT NULL,
    [LCID] int  NOT NULL,
    [Text] nvarchar(max)  NOT NULL
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

-- Creating primary key on [ProjectId], [LCID] in table 'ProjectTitle'
ALTER TABLE [dbo].[ProjectTitle]
ADD CONSTRAINT [PK_ProjectTitle]
    PRIMARY KEY CLUSTERED ([ProjectId], [LCID] ASC);
GO

-- Creating primary key on [ProjectId], [LCID] in table 'ProjectDescription'
ALTER TABLE [dbo].[ProjectDescription]
ADD CONSTRAINT [PK_ProjectDescription]
    PRIMARY KEY CLUSTERED ([ProjectId], [LCID] ASC);
GO

-- Creating primary key on [MediaId], [LCID] in table 'MediaTitle'
ALTER TABLE [dbo].[MediaTitle]
ADD CONSTRAINT [PK_MediaTitle]
    PRIMARY KEY CLUSTERED ([MediaId], [LCID] ASC);
GO

-- Creating primary key on [MediaId], [LCID] in table 'MediaDescription'
ALTER TABLE [dbo].[MediaDescription]
ADD CONSTRAINT [PK_MediaDescription]
    PRIMARY KEY CLUSTERED ([MediaId], [LCID] ASC);
GO

-- Creating primary key on [SetupId], [LCID] in table 'SetupMainTitle'
ALTER TABLE [dbo].[SetupMainTitle]
ADD CONSTRAINT [PK_SetupMainTitle]
    PRIMARY KEY CLUSTERED ([SetupId], [LCID] ASC);
GO

-- Creating primary key on [SetupId], [LCID] in table 'SetupFooter'
ALTER TABLE [dbo].[SetupFooter]
ADD CONSTRAINT [PK_SetupFooter]
    PRIMARY KEY CLUSTERED ([SetupId], [LCID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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

-- Creating foreign key on [LCID] in table 'ProjectTitle'
ALTER TABLE [dbo].[ProjectTitle]
ADD CONSTRAINT [FK_LanguageProjectTitle]
    FOREIGN KEY ([LCID])
    REFERENCES [dbo].[Language]
        ([LCID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageProjectTitle'
CREATE INDEX [IX_FK_LanguageProjectTitle]
ON [dbo].[ProjectTitle]
    ([LCID]);
GO

-- Creating foreign key on [ProjectId] in table 'ProjectTitle'
ALTER TABLE [dbo].[ProjectTitle]
ADD CONSTRAINT [FK_ProjectProjectTitle]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[Project]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [LCID] in table 'ProjectDescription'
ALTER TABLE [dbo].[ProjectDescription]
ADD CONSTRAINT [FK_LanguageProjectDescription]
    FOREIGN KEY ([LCID])
    REFERENCES [dbo].[Language]
        ([LCID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageProjectDescription'
CREATE INDEX [IX_FK_LanguageProjectDescription]
ON [dbo].[ProjectDescription]
    ([LCID]);
GO

-- Creating foreign key on [ProjectId] in table 'ProjectDescription'
ALTER TABLE [dbo].[ProjectDescription]
ADD CONSTRAINT [FK_ProjectProjectDescription]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[Project]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [MediaId] in table 'MediaTitle'
ALTER TABLE [dbo].[MediaTitle]
ADD CONSTRAINT [FK_MediaMediaTitle]
    FOREIGN KEY ([MediaId])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [LCID] in table 'MediaTitle'
ALTER TABLE [dbo].[MediaTitle]
ADD CONSTRAINT [FK_LanguageMediaTitle]
    FOREIGN KEY ([LCID])
    REFERENCES [dbo].[Language]
        ([LCID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageMediaTitle'
CREATE INDEX [IX_FK_LanguageMediaTitle]
ON [dbo].[MediaTitle]
    ([LCID]);
GO

-- Creating foreign key on [MediaId] in table 'MediaDescription'
ALTER TABLE [dbo].[MediaDescription]
ADD CONSTRAINT [FK_MediaMediaDescription]
    FOREIGN KEY ([MediaId])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [LCID] in table 'MediaDescription'
ALTER TABLE [dbo].[MediaDescription]
ADD CONSTRAINT [FK_LanguageMediaDescription]
    FOREIGN KEY ([LCID])
    REFERENCES [dbo].[Language]
        ([LCID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageMediaDescription'
CREATE INDEX [IX_FK_LanguageMediaDescription]
ON [dbo].[MediaDescription]
    ([LCID]);
GO

-- Creating foreign key on [SetupId] in table 'SetupMainTitle'
ALTER TABLE [dbo].[SetupMainTitle]
ADD CONSTRAINT [FK_SetupSetupMainTitle]
    FOREIGN KEY ([SetupId])
    REFERENCES [dbo].[Setup]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [LCID] in table 'SetupMainTitle'
ALTER TABLE [dbo].[SetupMainTitle]
ADD CONSTRAINT [FK_LanguageSetupMainTitle]
    FOREIGN KEY ([LCID])
    REFERENCES [dbo].[Language]
        ([LCID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageSetupMainTitle'
CREATE INDEX [IX_FK_LanguageSetupMainTitle]
ON [dbo].[SetupMainTitle]
    ([LCID]);
GO

-- Creating foreign key on [SetupId] in table 'SetupFooter'
ALTER TABLE [dbo].[SetupFooter]
ADD CONSTRAINT [FK_SetupSetupFooter]
    FOREIGN KEY ([SetupId])
    REFERENCES [dbo].[Setup]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [LCID] in table 'SetupFooter'
ALTER TABLE [dbo].[SetupFooter]
ADD CONSTRAINT [FK_LanguageSetupFooter]
    FOREIGN KEY ([LCID])
    REFERENCES [dbo].[Language]
        ([LCID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageSetupFooter'
CREATE INDEX [IX_FK_LanguageSetupFooter]
ON [dbo].[SetupFooter]
    ([LCID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------