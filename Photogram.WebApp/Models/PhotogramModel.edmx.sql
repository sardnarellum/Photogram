
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/28/2015 17:09:31
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

IF OBJECT_ID(N'[dbo].[FK_ProjectProjectTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translation_ProjectTitle] DROP CONSTRAINT [FK_ProjectProjectTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectProjectDescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translation_ProjectDescription] DROP CONSTRAINT [FK_ProjectProjectDescription];
GO
IF OBJECT_ID(N'[dbo].[FK_MediaMediaTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translation_MediaTitle] DROP CONSTRAINT [FK_MediaMediaTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_MediaMediaDescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translation_MediaDescription] DROP CONSTRAINT [FK_MediaMediaDescription];
GO
IF OBJECT_ID(N'[dbo].[FK_SetupSetupMainTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translation_SetupMainTitle] DROP CONSTRAINT [FK_SetupSetupMainTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_SetupSetupFooter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translation_SetupFooter] DROP CONSTRAINT [FK_SetupSetupFooter];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageTranslation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translation] DROP CONSTRAINT [FK_LanguageTranslation];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectIncludeProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectInclude] DROP CONSTRAINT [FK_ProjectIncludeProject];
GO
IF OBJECT_ID(N'[dbo].[FK_MediaProjectInclude]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectInclude] DROP CONSTRAINT [FK_MediaProjectInclude];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectTitle_inherits_Translation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translation_ProjectTitle] DROP CONSTRAINT [FK_ProjectTitle_inherits_Translation];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectDescription_inherits_Translation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translation_ProjectDescription] DROP CONSTRAINT [FK_ProjectDescription_inherits_Translation];
GO
IF OBJECT_ID(N'[dbo].[FK_MediaTitle_inherits_Translation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translation_MediaTitle] DROP CONSTRAINT [FK_MediaTitle_inherits_Translation];
GO
IF OBJECT_ID(N'[dbo].[FK_MediaDescription_inherits_Translation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translation_MediaDescription] DROP CONSTRAINT [FK_MediaDescription_inherits_Translation];
GO
IF OBJECT_ID(N'[dbo].[FK_SetupMainTitle_inherits_Translation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translation_SetupMainTitle] DROP CONSTRAINT [FK_SetupMainTitle_inherits_Translation];
GO
IF OBJECT_ID(N'[dbo].[FK_SetupFooter_inherits_Translation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translation_SetupFooter] DROP CONSTRAINT [FK_SetupFooter_inherits_Translation];
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
IF OBJECT_ID(N'[dbo].[Translation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Translation];
GO
IF OBJECT_ID(N'[dbo].[ProjectInclude]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectInclude];
GO
IF OBJECT_ID(N'[dbo].[Translation_ProjectTitle]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Translation_ProjectTitle];
GO
IF OBJECT_ID(N'[dbo].[Translation_ProjectDescription]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Translation_ProjectDescription];
GO
IF OBJECT_ID(N'[dbo].[Translation_MediaTitle]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Translation_MediaTitle];
GO
IF OBJECT_ID(N'[dbo].[Translation_MediaDescription]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Translation_MediaDescription];
GO
IF OBJECT_ID(N'[dbo].[Translation_SetupMainTitle]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Translation_SetupMainTitle];
GO
IF OBJECT_ID(N'[dbo].[Translation_SetupFooter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Translation_SetupFooter];
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
    [Type] int  NOT NULL
);
GO

-- Creating table 'Language'
CREATE TABLE [dbo].[Language] (
    [LCID] int  NOT NULL
);
GO

-- Creating table 'Translation'
CREATE TABLE [dbo].[Translation] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Language_LCID] int  NOT NULL
);
GO

-- Creating table 'ProjectInclude'
CREATE TABLE [dbo].[ProjectInclude] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Position] int  NOT NULL,
    [Cover] bit  NOT NULL,
    [Project_Id] int  NOT NULL,
    [Media_Id] int  NOT NULL
);
GO

-- Creating table 'Translation_ProjectTitle'
CREATE TABLE [dbo].[Translation_ProjectTitle] (
    [Id] int  NOT NULL,
    [Project_Id] int  NOT NULL
);
GO

-- Creating table 'Translation_ProjectDescription'
CREATE TABLE [dbo].[Translation_ProjectDescription] (
    [Id] int  NOT NULL,
    [Project_Id] int  NOT NULL
);
GO

-- Creating table 'Translation_MediaTitle'
CREATE TABLE [dbo].[Translation_MediaTitle] (
    [Id] int  NOT NULL,
    [Media_Id] int  NOT NULL
);
GO

-- Creating table 'Translation_MediaDescription'
CREATE TABLE [dbo].[Translation_MediaDescription] (
    [Id] int  NOT NULL,
    [Media_Id] int  NOT NULL
);
GO

-- Creating table 'Translation_SetupMainTitle'
CREATE TABLE [dbo].[Translation_SetupMainTitle] (
    [Id] int  NOT NULL,
    [Setup_Id] int  NOT NULL
);
GO

-- Creating table 'Translation_SetupFooter'
CREATE TABLE [dbo].[Translation_SetupFooter] (
    [Id] int  NOT NULL,
    [Setup_Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'Translation'
ALTER TABLE [dbo].[Translation]
ADD CONSTRAINT [PK_Translation]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProjectInclude'
ALTER TABLE [dbo].[ProjectInclude]
ADD CONSTRAINT [PK_ProjectInclude]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Translation_ProjectTitle'
ALTER TABLE [dbo].[Translation_ProjectTitle]
ADD CONSTRAINT [PK_Translation_ProjectTitle]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Translation_ProjectDescription'
ALTER TABLE [dbo].[Translation_ProjectDescription]
ADD CONSTRAINT [PK_Translation_ProjectDescription]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Translation_MediaTitle'
ALTER TABLE [dbo].[Translation_MediaTitle]
ADD CONSTRAINT [PK_Translation_MediaTitle]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Translation_MediaDescription'
ALTER TABLE [dbo].[Translation_MediaDescription]
ADD CONSTRAINT [PK_Translation_MediaDescription]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Translation_SetupMainTitle'
ALTER TABLE [dbo].[Translation_SetupMainTitle]
ADD CONSTRAINT [PK_Translation_SetupMainTitle]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Translation_SetupFooter'
ALTER TABLE [dbo].[Translation_SetupFooter]
ADD CONSTRAINT [PK_Translation_SetupFooter]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Project_Id] in table 'Translation_ProjectTitle'
ALTER TABLE [dbo].[Translation_ProjectTitle]
ADD CONSTRAINT [FK_ProjectProjectTitle]
    FOREIGN KEY ([Project_Id])
    REFERENCES [dbo].[Project]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectProjectTitle'
CREATE INDEX [IX_FK_ProjectProjectTitle]
ON [dbo].[Translation_ProjectTitle]
    ([Project_Id]);
GO

-- Creating foreign key on [Project_Id] in table 'Translation_ProjectDescription'
ALTER TABLE [dbo].[Translation_ProjectDescription]
ADD CONSTRAINT [FK_ProjectProjectDescription]
    FOREIGN KEY ([Project_Id])
    REFERENCES [dbo].[Project]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectProjectDescription'
CREATE INDEX [IX_FK_ProjectProjectDescription]
ON [dbo].[Translation_ProjectDescription]
    ([Project_Id]);
GO

-- Creating foreign key on [Media_Id] in table 'Translation_MediaTitle'
ALTER TABLE [dbo].[Translation_MediaTitle]
ADD CONSTRAINT [FK_MediaMediaTitle]
    FOREIGN KEY ([Media_Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaMediaTitle'
CREATE INDEX [IX_FK_MediaMediaTitle]
ON [dbo].[Translation_MediaTitle]
    ([Media_Id]);
GO

-- Creating foreign key on [Media_Id] in table 'Translation_MediaDescription'
ALTER TABLE [dbo].[Translation_MediaDescription]
ADD CONSTRAINT [FK_MediaMediaDescription]
    FOREIGN KEY ([Media_Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaMediaDescription'
CREATE INDEX [IX_FK_MediaMediaDescription]
ON [dbo].[Translation_MediaDescription]
    ([Media_Id]);
GO

-- Creating foreign key on [Setup_Id] in table 'Translation_SetupMainTitle'
ALTER TABLE [dbo].[Translation_SetupMainTitle]
ADD CONSTRAINT [FK_SetupSetupMainTitle]
    FOREIGN KEY ([Setup_Id])
    REFERENCES [dbo].[Setup]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SetupSetupMainTitle'
CREATE INDEX [IX_FK_SetupSetupMainTitle]
ON [dbo].[Translation_SetupMainTitle]
    ([Setup_Id]);
GO

-- Creating foreign key on [Setup_Id] in table 'Translation_SetupFooter'
ALTER TABLE [dbo].[Translation_SetupFooter]
ADD CONSTRAINT [FK_SetupSetupFooter]
    FOREIGN KEY ([Setup_Id])
    REFERENCES [dbo].[Setup]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SetupSetupFooter'
CREATE INDEX [IX_FK_SetupSetupFooter]
ON [dbo].[Translation_SetupFooter]
    ([Setup_Id]);
GO

-- Creating foreign key on [Language_LCID] in table 'Translation'
ALTER TABLE [dbo].[Translation]
ADD CONSTRAINT [FK_LanguageTranslation]
    FOREIGN KEY ([Language_LCID])
    REFERENCES [dbo].[Language]
        ([LCID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageTranslation'
CREATE INDEX [IX_FK_LanguageTranslation]
ON [dbo].[Translation]
    ([Language_LCID]);
GO

-- Creating foreign key on [Project_Id] in table 'ProjectInclude'
ALTER TABLE [dbo].[ProjectInclude]
ADD CONSTRAINT [FK_ProjectIncludeProject]
    FOREIGN KEY ([Project_Id])
    REFERENCES [dbo].[Project]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectIncludeProject'
CREATE INDEX [IX_FK_ProjectIncludeProject]
ON [dbo].[ProjectInclude]
    ([Project_Id]);
GO

-- Creating foreign key on [Media_Id] in table 'ProjectInclude'
ALTER TABLE [dbo].[ProjectInclude]
ADD CONSTRAINT [FK_MediaProjectInclude]
    FOREIGN KEY ([Media_Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaProjectInclude'
CREATE INDEX [IX_FK_MediaProjectInclude]
ON [dbo].[ProjectInclude]
    ([Media_Id]);
GO

-- Creating foreign key on [Id] in table 'Translation_ProjectTitle'
ALTER TABLE [dbo].[Translation_ProjectTitle]
ADD CONSTRAINT [FK_ProjectTitle_inherits_Translation]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Translation]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Translation_ProjectDescription'
ALTER TABLE [dbo].[Translation_ProjectDescription]
ADD CONSTRAINT [FK_ProjectDescription_inherits_Translation]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Translation]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Translation_MediaTitle'
ALTER TABLE [dbo].[Translation_MediaTitle]
ADD CONSTRAINT [FK_MediaTitle_inherits_Translation]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Translation]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Translation_MediaDescription'
ALTER TABLE [dbo].[Translation_MediaDescription]
ADD CONSTRAINT [FK_MediaDescription_inherits_Translation]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Translation]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Translation_SetupMainTitle'
ALTER TABLE [dbo].[Translation_SetupMainTitle]
ADD CONSTRAINT [FK_SetupMainTitle_inherits_Translation]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Translation]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Translation_SetupFooter'
ALTER TABLE [dbo].[Translation_SetupFooter]
ADD CONSTRAINT [FK_SetupFooter_inherits_Translation]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Translation]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------