
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server Compact Edition
-- --------------------------------------------------
-- Date Created: 07/19/2019 22:03:12
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
    ALTER TABLE [FloorSet] DROP CONSTRAINT [FK_ProjectFloor];
GO
    ALTER TABLE [HouseholdSet] DROP CONSTRAINT [FK_ProjectHousehold];
GO
    ALTER TABLE [ObligeeSet] DROP CONSTRAINT [FK_ProjectObligee];
GO
    ALTER TABLE [LogicalBuildingSet] DROP CONSTRAINT [FK_NaturalBuildingLogicalBuilding];
GO
    ALTER TABLE [FloorSet] DROP CONSTRAINT [FK_NaturalBuildingFloor];
GO
    ALTER TABLE [HouseholdSet] DROP CONSTRAINT [FK_NaturalBuildingHousehold];
GO
    ALTER TABLE [ObligeeSet] DROP CONSTRAINT [FK_HouseholdObligee];
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
    DROP TABLE [BDCS_CONST];
GO
    DROP TABLE [BDCS_CONSTCLS];
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
    [Remark] nvarchar(400)  NULL
);
GO

-- Creating table 'NaturalBuildingSet'
CREATE TABLE [NaturalBuildingSet] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [BSM] nvarchar(60)  NOT NULL,
    [YSDM] nvarchar(60)  NOT NULL,
    [BDCDYH] nvarchar(60)  NOT NULL,
    [ZDDM] nvarchar(60)  NOT NULL,
    [ZRZH] nvarchar(60)  NOT NULL,
    [XMMC] nvarchar(60)  NULL,
    [JZWMC] nvarchar(60)  NULL,
    [JGRQ] datetime  NULL,
    [JZWGD] float  NULL,
    [ZZDMJ] float  NOT NULL,
    [ZYDMJ] float  NOT NULL,
    [YCJZMJ] float  NULL,
    [SCJZMJ] float  NULL,
    [ZCS] int  NOT NULL,
    [DSCS] int  NOT NULL,
    [DXCS] int  NOT NULL,
    [DXSD] float  NULL,
    [GHYT] nvarchar(60)  NULL,
    [FWJG] int  NOT NULL,
    [ZTS] int  NOT NULL,
    [JZWJBYT] nvarchar(60)  NULL,
    [ZT] int  NOT NULL,
    [BZ] nvarchar(200)  NULL,
    [UpdateTime] datetime  NOT NULL,
    [Project_ID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'LogicalBuildingSet'
CREATE TABLE [LogicalBuildingSet] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [ZRZH] nvarchar(4000)  NOT NULL,
    [LJZH] nvarchar(60)  NOT NULL,
    [YSDM] nvarchar(60)  NOT NULL,
    [MPH] nvarchar(60)  NULL,
    [YCJZMJ] float  NULL,
    [YCDXMJ] float  NULL,
    [YCQTMJ] float  NULL,
    [SCJZMJ] float  NULL,
    [SCDXMJ] float  NULL,
    [SCQTMJ] float  NULL,
    [JGRQ] datetime  NULL,
    [FWJG1] nvarchar(60)  NULL,
    [FWJG2] nvarchar(60)  NULL,
    [FWJG3] nvarchar(60)  NULL,
    [JZWZT] nvarchar(60)  NULL,
    [ZCS] int  NULL,
    [DSCS] int  NULL,
    [DXCS] int  NULL,
    [BZ] nvarchar(200)  NULL,
    [UpdateTime] datetime  NOT NULL,
    [Project_ID] uniqueidentifier  NOT NULL,
    [NaturalBuilding_ID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'FloorSet'
CREATE TABLE [FloorSet] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [CH] nvarchar(60)  NOT NULL,
    [ZRZH] nvarchar(60)  NOT NULL,
    [YSDM] nvarchar(60)  NOT NULL,
    [SJC] nvarchar(60)  NULL,
    [MYC] nvarchar(60)  NULL,
    [CJZMJ] float  NULL,
    [CTNJZMJ] float  NULL,
    [CYTMJ] float  NULL,
    [CGYJZMJ] float  NULL,
    [CFTJZMJ] float  NULL,
    [CBQMJ] int  NULL,
    [CG] float  NULL,
    [SPTYMJ] float  NULL,
    [UpdateTime] datetime  NOT NULL,
    [Project_ID] uniqueidentifier  NOT NULL,
    [NaturalBuilding_ID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'ObligeeSet'
CREATE TABLE [ObligeeSet] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [HBSM] nvarchar(60)  NOT NULL,
    [QLRMC] nvarchar(60)  NOT NULL,
    [BDCQZH] nvarchar(60)  NOT NULL,
    [ZJZL] int  NOT NULL,
    [ZJH] nvarchar(60)  NOT NULL,
    [GJ] int  NOT NULL,
    [XB] int  NULL,
    [QLRLX] int  NOT NULL,
    [DH] nvarchar(4000)  NULL,
    [YB] nvarchar(4000)  NULL,
    [DZ] nvarchar(4000)  NULL,
    [QLLX] int  NOT NULL,
    [GYFS] int  NOT NULL,
    [QLMJ] float  NULL,
    [QLBL] nvarchar(4000)  NULL,
    [FRXM] nvarchar(4000)  NULL,
    [FRZJLX] int  NULL,
    [FRZJH] nvarchar(4000)  NULL,
    [FRDH] nvarchar(4000)  NULL,
    [DLRXM] nvarchar(4000)  NULL,
    [DLRZJLX] int  NULL,
    [DLRZJH] nvarchar(4000)  NULL,
    [DLRDH] nvarchar(4000)  NULL,
    [GZDW] nvarchar(4000)  NULL,
    [DLJGMC] nvarchar(4000)  NULL,
    [DZYJ] nvarchar(4000)  NULL,
    [BZ] nvarchar(4000)  NULL,
    [UpdateTime] datetime  NOT NULL,
    [Project_ID] uniqueidentifier  NOT NULL,
    [Household_ID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'HouseholdSet'
CREATE TABLE [HouseholdSet] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [HBSM] nvarchar(60)  NOT NULL,
    [YXTBS] nvarchar(60)  NULL,
    [BDCDYH] nvarchar(60)  NOT NULL,
    [FWBM] nvarchar(60)  NOT NULL,
    [YSDM] nvarchar(4000)  NOT NULL,
    [ZRZH] nvarchar(60)  NOT NULL,
    [LJZH] nvarchar(60)  NULL,
    [DYH] nvarchar(60)  NULL,
    [ZCS] int  NULL,
    [CH] nvarchar(60)  NULL,
    [FH] nvarchar(60)  NULL,
    [ZL] nvarchar(100)  NOT NULL,
    [MJDW] int  NOT NULL,
    [SZC] nvarchar(60)  NOT NULL,
    [QSC] nvarchar(60)  NOT NULL,
    [ZZC] nvarchar(60)  NOT NULL,
    [HH] nvarchar(4000)  NULL,
    [SHBW] nvarchar(60)  NOT NULL,
    [HX] int  NULL,
    [HXJG] int  NULL,
    [GHYT] nvarchar(60)  NULL,
    [FWYT1] int  NULL,
    [FWYT2] int  NULL,
    [FWYT3] int  NULL,
    [YCJZMJ] float  NULL,
    [YCTNJZMJ] float  NULL,
    [YCFTJZMJ] float  NULL,
    [YCDXBFJZMJ] float  NULL,
    [YCQTJZMJ] float  NULL,
    [YCFTXS] nvarchar(60)  NULL,
    [SCJZMJ] float  NULL,
    [SCTNJZMJ] float  NULL,
    [SCFTJZMJ] float  NULL,
    [SCDXBFJZMJ] float  NULL,
    [SCQTJZMJ] float  NULL,
    [SCFTXS] nvarchar(4000)  NULL,
    [GYTDMJ] float  NULL,
    [FTTDMJ] float  NULL,
    [DYTDMJ] float  NULL,
    [FWLX] int  NULL,
    [FWJG] int  NULL,
    [FWXZ] int  NULL,
    [FWCB] int  NULL,
    [FDCJYJG] float  NULL,
    [JGSJ] datetime  NULL,
    [CQLY] nvarchar(4000)  NULL,
    [QTGSD] nvarchar(4000)  NULL,
    [QTGSN] nvarchar(4000)  NULL,
    [QTGSX] nvarchar(4000)  NULL,
    [QTGSB] nvarchar(4000)  NULL,
    [TDSYQR] nvarchar(4000)  NULL,
    [FCFHT] nvarchar(4000)  NULL,
    [ZT] int  NULL,
    [UpdateTime] datetime  NOT NULL,
    [Project_ID] uniqueidentifier  NOT NULL,
    [NaturalBuilding_ID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'BDCS_CONST'
CREATE TABLE [BDCS_CONST] (
    [MBBSM] int IDENTITY(1,1) NOT NULL,
    [CONSTSLSID] int  NULL,
    [CONSTVALUE] nvarchar(100)  NULL,
    [CONSTTRANS] nvarchar(100)  NULL,
    [PARENTNODE] int  NULL,
    [CONSTORDER] int  NULL,
    [BZ] nvarchar(100)  NULL,
    [CREATETIME] datetime  NULL,
    [MODIFYTIME] datetime  NULL,
    [REPORTVALUE] nvarchar(50)  NULL,
    [GJCONSTTRANS] nvarchar(50)  NULL,
    [SFSY] nvarchar(50)  NULL,
    [GJVALUE] nvarchar(50)  NULL
);
GO

-- Creating table 'BDCS_CONSTCLS'
CREATE TABLE [BDCS_CONSTCLS] (
    [MBBSM] int IDENTITY(1,1) NOT NULL,
    [CONSTSLSID] int  NULL,
    [CONSTCLSNAME] nvarchar(100)  NULL,
    [CONSTCLSTYPE] nvarchar(100)  NULL,
    [BZ] nvarchar(200)  NULL,
    [CREATETIME] datetime  NULL,
    [MODIFYTIME] datetime  NULL
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

-- Creating primary key on [MBBSM] in table 'BDCS_CONST'
ALTER TABLE [BDCS_CONST]
ADD CONSTRAINT [PK_BDCS_CONST]
    PRIMARY KEY ([MBBSM] );
GO

-- Creating primary key on [MBBSM] in table 'BDCS_CONSTCLS'
ALTER TABLE [BDCS_CONSTCLS]
ADD CONSTRAINT [PK_BDCS_CONSTCLS]
    PRIMARY KEY ([MBBSM] );
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Project_ID] in table 'NaturalBuildingSet'
ALTER TABLE [NaturalBuildingSet]
ADD CONSTRAINT [FK_ProjectNaturalBuilding]
    FOREIGN KEY ([Project_ID])
    REFERENCES [ProjectSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectNaturalBuilding'
CREATE INDEX [IX_FK_ProjectNaturalBuilding]
ON [NaturalBuildingSet]
    ([Project_ID]);
GO

-- Creating foreign key on [Project_ID] in table 'LogicalBuildingSet'
ALTER TABLE [LogicalBuildingSet]
ADD CONSTRAINT [FK_ProjectLogicalBuilding]
    FOREIGN KEY ([Project_ID])
    REFERENCES [ProjectSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectLogicalBuilding'
CREATE INDEX [IX_FK_ProjectLogicalBuilding]
ON [LogicalBuildingSet]
    ([Project_ID]);
GO

-- Creating foreign key on [Project_ID] in table 'FloorSet'
ALTER TABLE [FloorSet]
ADD CONSTRAINT [FK_ProjectFloor]
    FOREIGN KEY ([Project_ID])
    REFERENCES [ProjectSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectFloor'
CREATE INDEX [IX_FK_ProjectFloor]
ON [FloorSet]
    ([Project_ID]);
GO

-- Creating foreign key on [Project_ID] in table 'HouseholdSet'
ALTER TABLE [HouseholdSet]
ADD CONSTRAINT [FK_ProjectHousehold]
    FOREIGN KEY ([Project_ID])
    REFERENCES [ProjectSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectHousehold'
CREATE INDEX [IX_FK_ProjectHousehold]
ON [HouseholdSet]
    ([Project_ID]);
GO

-- Creating foreign key on [Project_ID] in table 'ObligeeSet'
ALTER TABLE [ObligeeSet]
ADD CONSTRAINT [FK_ProjectObligee]
    FOREIGN KEY ([Project_ID])
    REFERENCES [ProjectSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectObligee'
CREATE INDEX [IX_FK_ProjectObligee]
ON [ObligeeSet]
    ([Project_ID]);
GO

-- Creating foreign key on [NaturalBuilding_ID] in table 'LogicalBuildingSet'
ALTER TABLE [LogicalBuildingSet]
ADD CONSTRAINT [FK_NaturalBuildingLogicalBuilding]
    FOREIGN KEY ([NaturalBuilding_ID])
    REFERENCES [NaturalBuildingSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NaturalBuildingLogicalBuilding'
CREATE INDEX [IX_FK_NaturalBuildingLogicalBuilding]
ON [LogicalBuildingSet]
    ([NaturalBuilding_ID]);
GO

-- Creating foreign key on [NaturalBuilding_ID] in table 'FloorSet'
ALTER TABLE [FloorSet]
ADD CONSTRAINT [FK_NaturalBuildingFloor]
    FOREIGN KEY ([NaturalBuilding_ID])
    REFERENCES [NaturalBuildingSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NaturalBuildingFloor'
CREATE INDEX [IX_FK_NaturalBuildingFloor]
ON [FloorSet]
    ([NaturalBuilding_ID]);
GO

-- Creating foreign key on [NaturalBuilding_ID] in table 'HouseholdSet'
ALTER TABLE [HouseholdSet]
ADD CONSTRAINT [FK_NaturalBuildingHousehold]
    FOREIGN KEY ([NaturalBuilding_ID])
    REFERENCES [NaturalBuildingSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NaturalBuildingHousehold'
CREATE INDEX [IX_FK_NaturalBuildingHousehold]
ON [HouseholdSet]
    ([NaturalBuilding_ID]);
GO

-- Creating foreign key on [Household_ID] in table 'ObligeeSet'
ALTER TABLE [ObligeeSet]
ADD CONSTRAINT [FK_HouseholdObligee]
    FOREIGN KEY ([Household_ID])
    REFERENCES [HouseholdSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HouseholdObligee'
CREATE INDEX [IX_FK_HouseholdObligee]
ON [ObligeeSet]
    ([Household_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------