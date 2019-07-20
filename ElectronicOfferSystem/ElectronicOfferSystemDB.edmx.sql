
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server Compact Edition
-- --------------------------------------------------
-- Date Created: 07/20/2019 20:07:51
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

-- Creating table 'Project'
CREATE TABLE [Project] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectName] nvarchar(60)  NOT NULL,
    [DeveloperName] nvarchar(60)  NOT NULL,
    [Type] int  NOT NULL,
    [State] int  NOT NULL,
    [UptateTime] datetime  NOT NULL,
    [Remark] nvarchar(400)  NULL
);
GO

-- Creating table 'NaturalBuilding'
CREATE TABLE [NaturalBuilding] (
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
    [FWJG] nvarchar(60)  NOT NULL,
    [ZTS] int  NOT NULL,
    [JZWJBYT] nvarchar(60)  NULL,
    [ZT] nvarchar(60)  NOT NULL,
    [BZ] nvarchar(200)  NULL,
    [UpdateTime] datetime  NOT NULL,
    [Project_ID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'LogicalBuilding'
CREATE TABLE [LogicalBuilding] (
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

-- Creating table 'Floor'
CREATE TABLE [Floor] (
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

-- Creating table 'Obligee'
CREATE TABLE [Obligee] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [HBSM] nvarchar(60)  NOT NULL,
    [QLRMC] nvarchar(60)  NOT NULL,
    [BDCQZH] nvarchar(60)  NOT NULL,
    [ZJZL] nvarchar(60)  NOT NULL,
    [ZJH] nvarchar(60)  NOT NULL,
    [GJ] nvarchar(60)  NOT NULL,
    [XB] nvarchar(60)  NULL,
    [QLRLX] nvarchar(60)  NOT NULL,
    [DH] nvarchar(4000)  NULL,
    [YB] nvarchar(4000)  NULL,
    [DZ] nvarchar(4000)  NULL,
    [QLLX] nvarchar(60)  NOT NULL,
    [GYFS] nvarchar(60)  NOT NULL,
    [QLMJ] float  NULL,
    [QLBL] nvarchar(4000)  NULL,
    [FRXM] nvarchar(4000)  NULL,
    [FRZJLX] nvarchar(60)  NULL,
    [FRZJH] nvarchar(4000)  NULL,
    [FRDH] nvarchar(4000)  NULL,
    [DLRXM] nvarchar(4000)  NULL,
    [DLRZJLX] nvarchar(60)  NULL,
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

-- Creating table 'Household'
CREATE TABLE [Household] (
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
    [MJDW] nvarchar(60)  NOT NULL,
    [SZC] nvarchar(60)  NOT NULL,
    [QSC] nvarchar(60)  NOT NULL,
    [ZZC] nvarchar(60)  NOT NULL,
    [HH] nvarchar(4000)  NULL,
    [SHBW] nvarchar(60)  NOT NULL,
    [HX] nvarchar(60)  NULL,
    [HXJG] nvarchar(60)  NULL,
    [GHYT] nvarchar(60)  NULL,
    [FWYT1] nvarchar(60)  NULL,
    [FWYT2] nvarchar(60)  NULL,
    [FWYT3] nvarchar(60)  NULL,
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
    [FWLX] nvarchar(60)  NULL,
    [FWJG] nvarchar(60)  NULL,
    [FWXZ] nvarchar(60)  NULL,
    [FWCB] nvarchar(60)  NULL,
    [FDCJYJG] float  NULL,
    [JGSJ] datetime  NULL,
    [CQLY] nvarchar(4000)  NULL,
    [QTGSD] nvarchar(4000)  NULL,
    [QTGSN] nvarchar(4000)  NULL,
    [QTGSX] nvarchar(4000)  NULL,
    [QTGSB] nvarchar(4000)  NULL,
    [TDSYQR] nvarchar(4000)  NULL,
    [FCFHT] nvarchar(4000)  NULL,
    [ZT] nvarchar(60)  NULL,
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

-- Creating primary key on [ID] in table 'Project'
ALTER TABLE [Project]
ADD CONSTRAINT [PK_Project]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'NaturalBuilding'
ALTER TABLE [NaturalBuilding]
ADD CONSTRAINT [PK_NaturalBuilding]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'LogicalBuilding'
ALTER TABLE [LogicalBuilding]
ADD CONSTRAINT [PK_LogicalBuilding]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'Floor'
ALTER TABLE [Floor]
ADD CONSTRAINT [PK_Floor]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'Obligee'
ALTER TABLE [Obligee]
ADD CONSTRAINT [PK_Obligee]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'Household'
ALTER TABLE [Household]
ADD CONSTRAINT [PK_Household]
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

-- Creating foreign key on [Project_ID] in table 'NaturalBuilding'
ALTER TABLE [NaturalBuilding]
ADD CONSTRAINT [FK_ProjectNaturalBuilding]
    FOREIGN KEY ([Project_ID])
    REFERENCES [Project]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectNaturalBuilding'
CREATE INDEX [IX_FK_ProjectNaturalBuilding]
ON [NaturalBuilding]
    ([Project_ID]);
GO

-- Creating foreign key on [Project_ID] in table 'LogicalBuilding'
ALTER TABLE [LogicalBuilding]
ADD CONSTRAINT [FK_ProjectLogicalBuilding]
    FOREIGN KEY ([Project_ID])
    REFERENCES [Project]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectLogicalBuilding'
CREATE INDEX [IX_FK_ProjectLogicalBuilding]
ON [LogicalBuilding]
    ([Project_ID]);
GO

-- Creating foreign key on [Project_ID] in table 'Floor'
ALTER TABLE [Floor]
ADD CONSTRAINT [FK_ProjectFloor]
    FOREIGN KEY ([Project_ID])
    REFERENCES [Project]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectFloor'
CREATE INDEX [IX_FK_ProjectFloor]
ON [Floor]
    ([Project_ID]);
GO

-- Creating foreign key on [Project_ID] in table 'Household'
ALTER TABLE [Household]
ADD CONSTRAINT [FK_ProjectHousehold]
    FOREIGN KEY ([Project_ID])
    REFERENCES [Project]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectHousehold'
CREATE INDEX [IX_FK_ProjectHousehold]
ON [Household]
    ([Project_ID]);
GO

-- Creating foreign key on [Project_ID] in table 'Obligee'
ALTER TABLE [Obligee]
ADD CONSTRAINT [FK_ProjectObligee]
    FOREIGN KEY ([Project_ID])
    REFERENCES [Project]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectObligee'
CREATE INDEX [IX_FK_ProjectObligee]
ON [Obligee]
    ([Project_ID]);
GO

-- Creating foreign key on [NaturalBuilding_ID] in table 'LogicalBuilding'
ALTER TABLE [LogicalBuilding]
ADD CONSTRAINT [FK_NaturalBuildingLogicalBuilding]
    FOREIGN KEY ([NaturalBuilding_ID])
    REFERENCES [NaturalBuilding]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NaturalBuildingLogicalBuilding'
CREATE INDEX [IX_FK_NaturalBuildingLogicalBuilding]
ON [LogicalBuilding]
    ([NaturalBuilding_ID]);
GO

-- Creating foreign key on [NaturalBuilding_ID] in table 'Floor'
ALTER TABLE [Floor]
ADD CONSTRAINT [FK_NaturalBuildingFloor]
    FOREIGN KEY ([NaturalBuilding_ID])
    REFERENCES [NaturalBuilding]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NaturalBuildingFloor'
CREATE INDEX [IX_FK_NaturalBuildingFloor]
ON [Floor]
    ([NaturalBuilding_ID]);
GO

-- Creating foreign key on [NaturalBuilding_ID] in table 'Household'
ALTER TABLE [Household]
ADD CONSTRAINT [FK_NaturalBuildingHousehold]
    FOREIGN KEY ([NaturalBuilding_ID])
    REFERENCES [NaturalBuilding]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NaturalBuildingHousehold'
CREATE INDEX [IX_FK_NaturalBuildingHousehold]
ON [Household]
    ([NaturalBuilding_ID]);
GO

-- Creating foreign key on [Household_ID] in table 'Obligee'
ALTER TABLE [Obligee]
ADD CONSTRAINT [FK_HouseholdObligee]
    FOREIGN KEY ([Household_ID])
    REFERENCES [Household]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HouseholdObligee'
CREATE INDEX [IX_FK_HouseholdObligee]
ON [Obligee]
    ([Household_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------