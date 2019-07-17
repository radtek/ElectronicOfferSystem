
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server Compact Edition
-- --------------------------------------------------
-- Date Created: 07/15/2019 23:21:45
-- Generated from EDMX file: D:\vs-workspace\ElectronicOfferSystem\ElectronicOfferSystem\ElectronicOfferSystemDB.edmx
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    ALTER TABLE [NaturalBuildingSet] DROP CONSTRAINT [FK_ProjectNaturalBuilding];
GO
    ALTER TABLE [LogicalBuildingSet] DROP CONSTRAINT [FK_ProjectLogicalBuilding];
GO
    ALTER TABLE [LogicalBuildingSet] DROP CONSTRAINT [FK_NaturalBuildingLogicalBuilding];
GO
    ALTER TABLE [FloorSet] DROP CONSTRAINT [FK_ProjectFloor];
GO
    ALTER TABLE [FloorSet] DROP CONSTRAINT [FK_NaturalBuildingFloor];
GO
    ALTER TABLE [HouseholdSet] DROP CONSTRAINT [FK_ProjectHousehold];
GO
    ALTER TABLE [HouseholdSet] DROP CONSTRAINT [FK_NaturalBuildingHousehold];
GO
    ALTER TABLE [ObligeeSet] DROP CONSTRAINT [FK_ProjectObligee];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- NOTE: if the table does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    DROP TABLE [ProjectSet];
GO
    DROP TABLE [NaturalBuildingSet];
GO
    DROP TABLE [LogicalBuildingSet];
GO
    DROP TABLE [FloorSet];
GO
    DROP TABLE [ObligeeSet];
GO
    DROP TABLE [HouseholdSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProjectSet'
CREATE TABLE [ProjectSet] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectName] nvarchar(60)  NOT NULL,
    [DeveloperName] nvarchar(60)  NOT NULL,
    [Type] int  NOT NULL,
    [State] int  NOT NULL,
    [UptateTime] datetime  NOT NULL,
    [Remark] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'NaturalBuildingSet'
CREATE TABLE [NaturalBuildingSet] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectId] uniqueidentifier  NOT NULL,
    [BSM] nvarchar(4000)  NOT NULL,
    [BDCDYH] nvarchar(4000)  NOT NULL,
    [ZDDM] nvarchar(4000)  NOT NULL,
    [ZRZH] nvarchar(4000)  NOT NULL,
    [XMMC] nvarchar(4000)  NOT NULL,
    [JZWMC] nvarchar(4000)  NOT NULL,
    [JGRQ] datetime  NOT NULL,
    [JZWGD] float  NOT NULL,
    [ZZDMJ] float  NOT NULL,
    [ZYDMJ] float  NOT NULL,
    [YCJZMJ] float  NOT NULL,
    [SCJZMJ] float  NOT NULL,
    [ZCS] int  NOT NULL,
    [DSCS] int  NOT NULL,
    [DXCS] int  NOT NULL,
    [DXSD] float  NOT NULL,
    [GHYT] nvarchar(4000)  NOT NULL,
    [FWJG] int  NOT NULL,
    [ZTS] int  NOT NULL,
    [JZWJBYT] nvarchar(4000)  NOT NULL,
    [ZT] int  NOT NULL,
    [BZ] nvarchar(100)  NOT NULL,
    [UpdateTime] datetime  NOT NULL
);
GO

-- Creating table 'LogicalBuildingSet'
CREATE TABLE [LogicalBuildingSet] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [NaturalBuildingID] uniqueidentifier  NOT NULL,
    [LJZH] nvarchar(4000)  NOT NULL,
    [ZRZH] nvarchar(4000)  NOT NULL,
    [YSDM] nvarchar(4000)  NOT NULL,
    [MPH] nvarchar(4000)  NOT NULL,
    [YCJZMJ] float  NOT NULL,
    [YCDXMJ] float  NOT NULL,
    [YCQTMJ] float  NOT NULL,
    [SCJZMJ] float  NOT NULL,
    [SCDXMJ] float  NOT NULL,
    [SCQTMJ] float  NOT NULL,
    [JGRQ] datetime  NOT NULL,
    [FWJG1] nvarchar(4000)  NOT NULL,
    [FWJG2] nvarchar(4000)  NOT NULL,
    [FWJG3] nvarchar(4000)  NOT NULL,
    [JZWZT] nvarchar(4000)  NOT NULL,
    [ZCS] int  NOT NULL,
    [DSCS] int  NOT NULL,
    [DXCS] int  NOT NULL,
    [BZ] nvarchar(4000)  NOT NULL,
    [UpdateTime] datetime  NOT NULL
);
GO

-- Creating table 'FloorSet'
CREATE TABLE [FloorSet] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [NaturalBuildingID] uniqueidentifier  NOT NULL,
    [CH] nvarchar(4000)  NOT NULL,
    [ZRZH] nvarchar(4000)  NOT NULL,
    [YSDM] nvarchar(4000)  NOT NULL,
    [SJC] nvarchar(4000)  NOT NULL,
    [MYC] nvarchar(4000)  NOT NULL,
    [CJZMJ] float  NOT NULL,
    [CTNJZMJ] float  NOT NULL,
    [CYTMJ] float  NOT NULL,
    [CGYJZMJ] float  NOT NULL,
    [CFTJZMJ] float  NOT NULL,
    [CBQMJ] int  NOT NULL,
    [CG] float  NOT NULL,
    [SPTYMJ] float  NOT NULL,
    [UpdateTime] datetime  NOT NULL
);
GO

-- Creating table 'ObligeeSet'
CREATE TABLE [ObligeeSet] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [HBSM] nvarchar(4000)  NOT NULL,
    [QLRMC] nvarchar(4000)  NOT NULL,
    [BDCQZH] nvarchar(4000)  NOT NULL,
    [ZJZL] int  NOT NULL,
    [ZJH] nvarchar(4000)  NOT NULL,
    [GJ] int  NOT NULL,
    [XB] int  NOT NULL,
    [QLRLX] nvarchar(4000)  NOT NULL,
    [DH] nvarchar(4000)  NOT NULL,
    [YB] nvarchar(4000)  NOT NULL,
    [DZ] nvarchar(4000)  NOT NULL,
    [QLLX] int  NOT NULL,
    [GYFS] int  NOT NULL,
    [QLMJ] float  NOT NULL,
    [QLBL] nvarchar(4000)  NOT NULL,
    [FRXM] nvarchar(4000)  NOT NULL,
    [FRZJLX] int  NOT NULL,
    [FRZJH] nvarchar(4000)  NOT NULL,
    [FRDH] nvarchar(4000)  NOT NULL,
    [DLRXM] nvarchar(4000)  NOT NULL,
    [DLRZJLX] int  NOT NULL,
    [DLRZJH] nvarchar(4000)  NOT NULL,
    [DLRDH] nvarchar(4000)  NOT NULL,
    [GZDW] nvarchar(4000)  NOT NULL,
    [DLJGMC] nvarchar(4000)  NOT NULL,
    [DZYJ] nvarchar(4000)  NOT NULL,
    [BZ] nvarchar(4000)  NOT NULL,
    [UpdateTime] datetime  NOT NULL
);
GO

-- Creating table 'HouseholdSet'
CREATE TABLE [HouseholdSet] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [NaturalBuildingID] uniqueidentifier  NOT NULL,
    [HBSM] nvarchar(4000)  NOT NULL,
    [YXTBS] nvarchar(4000)  NOT NULL,
    [BDCDYH] nvarchar(4000)  NOT NULL,
    [FWBM] nvarchar(4000)  NOT NULL,
    [YSDM] nvarchar(4000)  NOT NULL,
    [ZRZH] nvarchar(4000)  NOT NULL,
    [LJZH] nvarchar(4000)  NOT NULL,
    [DYH] nvarchar(4000)  NOT NULL,
    [ZCS] int  NOT NULL,
    [CH] nvarchar(4000)  NOT NULL,
    [FH] nvarchar(4000)  NOT NULL,
    [ZL] nvarchar(4000)  NOT NULL,
    [MJDW] int  NOT NULL,
    [SZC] nvarchar(4000)  NOT NULL,
    [QSC] nvarchar(4000)  NOT NULL,
    [ZZC] nvarchar(4000)  NOT NULL,
    [HH] nvarchar(4000)  NOT NULL,
    [SHBW] nvarchar(4000)  NOT NULL,
    [HX] int  NOT NULL,
    [HXJG] int  NOT NULL,
    [GHYT] nvarchar(4000)  NOT NULL,
    [FWYT1] int  NOT NULL,
    [FWYT2] int  NOT NULL,
    [FWYT3] int  NOT NULL,
    [YCJZMJ] float  NOT NULL,
    [YCTNJZMJ] float  NOT NULL,
    [YCFTJZMJ] float  NOT NULL,
    [YCDXBFJZMJ] float  NOT NULL,
    [YCQTJZMJ] float  NOT NULL,
    [YCFTXS] nvarchar(4000)  NOT NULL,
    [SCJZMJ] float  NOT NULL,
    [SCTNJZMJ] float  NOT NULL,
    [SCFTJZMJ] float  NOT NULL,
    [SCDXBFJZMJ] float  NOT NULL,
    [SCQTJZMJ] float  NOT NULL,
    [SCFTXS] nvarchar(4000)  NOT NULL,
    [GYTDMJ] float  NOT NULL,
    [FTTDMJ] float  NOT NULL,
    [DYTDMJ] float  NOT NULL,
    [FWLX] int  NOT NULL,
    [FWJG] int  NOT NULL,
    [FWXZ] int  NOT NULL,
    [FWCB] int  NOT NULL,
    [FDCJYJG] float  NOT NULL,
    [JGSJ] datetime  NOT NULL,
    [CQLY] nvarchar(4000)  NOT NULL,
    [QTGSD] nvarchar(4000)  NOT NULL,
    [QTGSN] nvarchar(4000)  NOT NULL,
    [QTGSX] nvarchar(4000)  NOT NULL,
    [QTGSB] nvarchar(4000)  NOT NULL,
    [TDSYQR] nvarchar(4000)  NOT NULL,
    [FCFHT] nvarchar(4000)  NOT NULL,
    [ZT] int  NOT NULL,
    [UpdateTime] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'ProjectSet'
ALTER TABLE [ProjectSet]
ADD CONSTRAINT [PK_ProjectSet]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'NaturalBuildingSet'
ALTER TABLE [NaturalBuildingSet]
ADD CONSTRAINT [PK_NaturalBuildingSet]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'LogicalBuildingSet'
ALTER TABLE [LogicalBuildingSet]
ADD CONSTRAINT [PK_LogicalBuildingSet]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'FloorSet'
ALTER TABLE [FloorSet]
ADD CONSTRAINT [PK_FloorSet]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'ObligeeSet'
ALTER TABLE [ObligeeSet]
ADD CONSTRAINT [PK_ObligeeSet]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'HouseholdSet'
ALTER TABLE [HouseholdSet]
ADD CONSTRAINT [PK_HouseholdSet]
    PRIMARY KEY ([ID] );
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProjectId] in table 'NaturalBuildingSet'
ALTER TABLE [NaturalBuildingSet]
ADD CONSTRAINT [FK_ProjectNaturalBuilding]
    FOREIGN KEY ([ProjectId])
    REFERENCES [ProjectSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectNaturalBuilding'
CREATE INDEX [IX_FK_ProjectNaturalBuilding]
ON [NaturalBuildingSet]
    ([ProjectId]);
GO

-- Creating foreign key on [ProjectID] in table 'LogicalBuildingSet'
ALTER TABLE [LogicalBuildingSet]
ADD CONSTRAINT [FK_ProjectLogicalBuilding]
    FOREIGN KEY ([ProjectID])
    REFERENCES [ProjectSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectLogicalBuilding'
CREATE INDEX [IX_FK_ProjectLogicalBuilding]
ON [LogicalBuildingSet]
    ([ProjectID]);
GO

-- Creating foreign key on [NaturalBuildingID] in table 'LogicalBuildingSet'
ALTER TABLE [LogicalBuildingSet]
ADD CONSTRAINT [FK_NaturalBuildingLogicalBuilding]
    FOREIGN KEY ([NaturalBuildingID])
    REFERENCES [NaturalBuildingSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NaturalBuildingLogicalBuilding'
CREATE INDEX [IX_FK_NaturalBuildingLogicalBuilding]
ON [LogicalBuildingSet]
    ([NaturalBuildingID]);
GO

-- Creating foreign key on [ProjectID] in table 'FloorSet'
ALTER TABLE [FloorSet]
ADD CONSTRAINT [FK_ProjectFloor]
    FOREIGN KEY ([ProjectID])
    REFERENCES [ProjectSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectFloor'
CREATE INDEX [IX_FK_ProjectFloor]
ON [FloorSet]
    ([ProjectID]);
GO

-- Creating foreign key on [NaturalBuildingID] in table 'FloorSet'
ALTER TABLE [FloorSet]
ADD CONSTRAINT [FK_NaturalBuildingFloor]
    FOREIGN KEY ([NaturalBuildingID])
    REFERENCES [NaturalBuildingSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NaturalBuildingFloor'
CREATE INDEX [IX_FK_NaturalBuildingFloor]
ON [FloorSet]
    ([NaturalBuildingID]);
GO

-- Creating foreign key on [ProjectID] in table 'HouseholdSet'
ALTER TABLE [HouseholdSet]
ADD CONSTRAINT [FK_ProjectHousehold]
    FOREIGN KEY ([ProjectID])
    REFERENCES [ProjectSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectHousehold'
CREATE INDEX [IX_FK_ProjectHousehold]
ON [HouseholdSet]
    ([ProjectID]);
GO

-- Creating foreign key on [NaturalBuildingID] in table 'HouseholdSet'
ALTER TABLE [HouseholdSet]
ADD CONSTRAINT [FK_NaturalBuildingHousehold]
    FOREIGN KEY ([NaturalBuildingID])
    REFERENCES [NaturalBuildingSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NaturalBuildingHousehold'
CREATE INDEX [IX_FK_NaturalBuildingHousehold]
ON [HouseholdSet]
    ([NaturalBuildingID]);
GO

-- Creating foreign key on [ProjectID] in table 'ObligeeSet'
ALTER TABLE [ObligeeSet]
ADD CONSTRAINT [FK_ProjectObligee]
    FOREIGN KEY ([ProjectID])
    REFERENCES [ProjectSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectObligee'
CREATE INDEX [IX_FK_ProjectObligee]
ON [ObligeeSet]
    ([ProjectID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------