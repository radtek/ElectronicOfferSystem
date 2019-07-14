
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server Compact Edition
-- --------------------------------------------------
-- Date Created: 07/14/2019 13:16:38
-- Generated from EDMX file: D:\vs-workspace\ElectronicOfferSystem\ElectronicOfferSystem\ElectronicOfferSystem\ElectronicOfferSystemDB.edmx
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- NOTE: if the table does not exist, an ignorable error will be reported.
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProjectSet'
CREATE TABLE [ProjectSet] (
    [Id] uniqueidentifier  NOT NULL,
    [ProjectName] nvarchar(4000)  NOT NULL,
    [DeveloperName] nvarchar(4000)  NOT NULL,
    [Type] int  NOT NULL,
    [State] int  NOT NULL,
    [UptateTime] datetime  NOT NULL,
    [Remark] nvarchar(4000)  NOT NULL
);
GO

-- Creating table 'NaturalBuildingSet'
CREATE TABLE [NaturalBuildingSet] (
    [Id] uniqueidentifier  NOT NULL,
    [BSM] nvarchar(4000)  NOT NULL,
    [BDCDYH] nvarchar(4000)  NOT NULL,
    [ProjectId] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ProjectSet'
ALTER TABLE [ProjectSet]
ADD CONSTRAINT [PK_ProjectSet]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'NaturalBuildingSet'
ALTER TABLE [NaturalBuildingSet]
ADD CONSTRAINT [PK_NaturalBuildingSet]
    PRIMARY KEY ([Id] );
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProjectId] in table 'NaturalBuildingSet'
ALTER TABLE [NaturalBuildingSet]
ADD CONSTRAINT [FK_ProjectNaturalBuilding]
    FOREIGN KEY ([ProjectId])
    REFERENCES [ProjectSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectNaturalBuilding'
CREATE INDEX [IX_FK_ProjectNaturalBuilding]
ON [NaturalBuildingSet]
    ([ProjectId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------