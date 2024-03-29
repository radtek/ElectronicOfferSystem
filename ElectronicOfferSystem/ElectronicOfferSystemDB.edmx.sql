
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server Compact Edition
-- --------------------------------------------------
-- Date Created: 08/01/2019 21:49:21
-- Generated from EDMX file: D:\vs-workspace\ElectronicOfferSystem\ElectronicOfferSystem\ElectronicOfferSystemDB.edmx
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- NOTE: if the table does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    DROP TABLE [Project];
GO
    DROP TABLE [NaturalBuilding];
GO
    DROP TABLE [LogicalBuilding];
GO
    DROP TABLE [Floor];
GO
    DROP TABLE [Obligee];
GO
    DROP TABLE [Household];
GO
    DROP TABLE [BDCS_CONST];
GO
    DROP TABLE [BDCS_CONSTCLS];
GO
    DROP TABLE [Applicant];
GO
    DROP TABLE [Transfer];
GO
    DROP TABLE [FileInfo];
GO
    DROP TABLE [FileType];
GO
    DROP TABLE [Mortgage];
GO
    DROP TABLE [Sequestration];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Project'
CREATE TABLE [Project] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectName] nvarchar(60)  NOT NULL,
    [DeveloperName] nvarchar(60)  NULL,
    [Type] nvarchar(60)  NULL,
    [State] nvarchar(4000)  NULL,
    [OwnershipType] nvarchar(60)  NULL,
    [MappingType] nvarchar(60)  NULL,
    [Remark] nvarchar(400)  NULL,
    [UptateTime] datetime  NOT NULL,
    [CreateTime] datetime  NULL
);
GO

-- Creating table 'NaturalBuilding'
CREATE TABLE [NaturalBuilding] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [BSM] nvarchar(60)  NOT NULL,
    [YSDM] nvarchar(60)  NOT NULL,
    [BDCDYH] nvarchar(60)  NULL,
    [ZDDM] nvarchar(60)  NOT NULL,
    [ZRZH] nvarchar(60)  NOT NULL,
    [XMMC] nvarchar(60)  NULL,
    [JZWMC] nvarchar(60)  NULL,
    [JGRQ] datetime  NULL,
    [JZWGD] nvarchar(60)  NULL,
    [ZZDMJ] nvarchar(60)  NOT NULL,
    [ZYDMJ] nvarchar(60)  NOT NULL,
    [YCJZMJ] nvarchar(60)  NULL,
    [SCJZMJ] nvarchar(60)  NULL,
    [ZCS] nvarchar(60)  NOT NULL,
    [DSCS] nvarchar(60)  NOT NULL,
    [DXCS] nvarchar(60)  NOT NULL,
    [DXSD] nvarchar(60)  NULL,
    [GHYT] nvarchar(60)  NOT NULL,
    [FWJG] nvarchar(60)  NOT NULL,
    [ZTS] nvarchar(4000)  NOT NULL,
    [JZWJBYT] nvarchar(60)  NULL,
    [BZ] nvarchar(200)  NULL,
    [ZT] nvarchar(60)  NOT NULL,
    [UpdateTime] datetime  NOT NULL
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
    [YCJZMJ] nvarchar(60)  NULL,
    [YCDXMJ] nvarchar(60)  NULL,
    [YCQTMJ] nvarchar(60)  NULL,
    [SCJZMJ] nvarchar(60)  NULL,
    [SCDXMJ] nvarchar(60)  NULL,
    [SCQTMJ] nvarchar(60)  NULL,
    [JGRQ] datetime  NULL,
    [FWJG1] nvarchar(60)  NULL,
    [FWJG2] nvarchar(60)  NULL,
    [FWJG3] nvarchar(60)  NULL,
    [JZWZT] nvarchar(60)  NULL,
    [FWYT1] nvarchar(60)  NULL,
    [FWYT2] nvarchar(60)  NULL,
    [FWYT3] nvarchar(60)  NULL,
    [ZCS] nvarchar(60)  NULL,
    [DSCS] nvarchar(60)  NULL,
    [DXCS] nvarchar(60)  NULL,
    [BZ] nvarchar(200)  NULL,
    [UpdateTime] datetime  NOT NULL
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
    [CJZMJ] nvarchar(60)  NULL,
    [CTNJZMJ] nvarchar(60)  NULL,
    [CYTMJ] nvarchar(60)  NULL,
    [CGYJZMJ] nvarchar(60)  NULL,
    [CFTJZMJ] nvarchar(60)  NULL,
    [CBQMJ] nvarchar(60)  NULL,
    [CG] nvarchar(60)  NULL,
    [SPTYMJ] nvarchar(60)  NULL,
    [UpdateTime] datetime  NOT NULL
);
GO

-- Creating table 'Obligee'
CREATE TABLE [Obligee] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [HBSM] nvarchar(60)  NOT NULL,
    [QLRMC] nvarchar(60)  NOT NULL,
    [BDCQZH] nvarchar(60)  NULL,
    [ZJZL] nvarchar(60)  NOT NULL,
    [ZJH] nvarchar(60)  NOT NULL,
    [GJ] nvarchar(60)  NOT NULL,
    [XB] nvarchar(60)  NOT NULL,
    [QLRLX] nvarchar(60)  NOT NULL,
    [DH] nvarchar(4000)  NULL,
    [YB] nvarchar(4000)  NULL,
    [DZ] nvarchar(4000)  NULL,
    [QLLX] nvarchar(60)  NOT NULL,
    [GYFS] nvarchar(60)  NOT NULL,
    [QLMJ] nvarchar(60)  NULL,
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
    [DBR] nvarchar(60)  NULL,
    [BZ] nvarchar(300)  NULL,
    [UpdateTime] datetime  NOT NULL
);
GO

-- Creating table 'Household'
CREATE TABLE [Household] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [HBSM] nvarchar(60)  NOT NULL,
    [YXTBS] nvarchar(60)  NULL,
    [BDCDYH] nvarchar(60)  NULL,
    [FWBM] nvarchar(60)  NOT NULL,
    [YSDM] nvarchar(4000)  NOT NULL,
    [ZRZH] nvarchar(60)  NULL,
    [LJZH] nvarchar(60)  NULL,
    [DYH] nvarchar(60)  NULL,
    [ZCS] nvarchar(60)  NULL,
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
    [YCJZMJ] nvarchar(60)  NULL,
    [YCTNJZMJ] nvarchar(60)  NULL,
    [YCFTJZMJ] nvarchar(60)  NULL,
    [YCDXBFJZMJ] nvarchar(60)  NULL,
    [YCQTJZMJ] nvarchar(60)  NULL,
    [YCFTXS] nvarchar(60)  NULL,
    [SCJZMJ] nvarchar(60)  NULL,
    [SCTNJZMJ] nvarchar(60)  NULL,
    [SCFTJZMJ] nvarchar(60)  NULL,
    [SCDXBFJZMJ] nvarchar(60)  NULL,
    [SCQTJZMJ] nvarchar(60)  NULL,
    [SCFTXS] nvarchar(4000)  NULL,
    [GYTDMJ] nvarchar(60)  NULL,
    [FTTDMJ] nvarchar(60)  NULL,
    [DYTDMJ] nvarchar(60)  NULL,
    [FWLX] nvarchar(60)  NULL,
    [FWJG] nvarchar(60)  NULL,
    [FWXZ] nvarchar(60)  NULL,
    [FWCB] nvarchar(60)  NULL,
    [FDCJYJG] nvarchar(60)  NULL,
    [JGSJ] datetime  NULL,
    [CQLY] nvarchar(4000)  NULL,
    [QTGSD] nvarchar(4000)  NULL,
    [QTGSN] nvarchar(4000)  NULL,
    [QTGSX] nvarchar(4000)  NULL,
    [QTGSB] nvarchar(4000)  NULL,
    [TDSYQR] nvarchar(4000)  NULL,
    [FCFHT] nvarchar(4000)  NULL,
    [ZT] nvarchar(60)  NOT NULL,
    [UpdateTime] datetime  NOT NULL
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

-- Creating table 'Applicant'
CREATE TABLE [Applicant] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [BDCDYH] nvarchar(60)  NOT NULL,
    [BDCQZH] nvarchar(60)  NOT NULL,
    [SQRXM] nvarchar(60)  NULL,
    [XB] nvarchar(60)  NULL,
    [ZJLX] nvarchar(60)  NULL,
    [ZJLXMC] nvarchar(60)  NULL,
    [ZJH] nvarchar(60)  NULL,
    [FZJG] nvarchar(60)  NULL,
    [GJDQ] nvarchar(60)  NULL,
    [HJSZSS] nvarchar(60)  NULL,
    [GZDW] nvarchar(60)  NULL,
    [SSHY] nvarchar(60)  NULL,
    [LXDH] nvarchar(60)  NULL,
    [TXDZ] nvarchar(60)  NULL,
    [YZBM] nvarchar(60)  NULL,
    [DZYJ] nvarchar(60)  NULL,
    [FRXM] nvarchar(60)  NULL,
    [FRZJLX] nvarchar(60)  NULL,
    [FRDH] nvarchar(60)  NULL,
    [SQRLX] nvarchar(60)  NULL,
    [SQRLXMC] nvarchar(60)  NULL,
    [DLRXM] nvarchar(60)  NULL,
    [DLJGMC] nvarchar(60)  NULL,
    [DLRDH] nvarchar(60)  NULL,
    [YXBZ] nvarchar(60)  NULL,
    [GYFS] nvarchar(60)  NULL,
    [SQRLB] nvarchar(60)  NULL,
    [SQRLBMC] nvarchar(60)  NULL,
    [SFCZR] nvarchar(60)  NULL,
    [BDCDYLX] nvarchar(60)  NULL,
    [UpdateTime] datetime  NULL
);
GO

-- Creating table 'Transfer'
CREATE TABLE [Transfer] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [ProjectName] nvarchar(60)  NULL,
    [DeveloperName] nvarchar(60)  NULL,
    [ContractNum] nvarchar(60)  NULL,
    [UpdateTime] datetime  NULL,
    [Remark] nvarchar(300)  NULL
);
GO

-- Creating table 'FileInfo'
CREATE TABLE [FileInfo] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(60)  NULL,
    [Path] nvarchar(4000)  NULL,
    [Type] nvarchar(60)  NULL,
    [UpdateTime] datetime  NULL,
    [Extension] nvarchar(60)  NOT NULL
);
GO

-- Creating table 'FileType'
CREATE TABLE [FileType] (
    [ID] nvarchar(4000)  NOT NULL,
    [Name] nvarchar(60)  NULL
);
GO

-- Creating table 'Mortgage'
CREATE TABLE [Mortgage] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [HBSM] nvarchar(60)  NOT NULL,
    [QLRMC] nvarchar(60)  NOT NULL,
    [BDCQZH] nvarchar(60)  NULL,
    [ZJLX] nvarchar(60)  NOT NULL,
    [ZJH] nvarchar(60)  NOT NULL,
    [DH] nvarchar(60)  NULL,
    [YB] nvarchar(60)  NULL,
    [DZ] nvarchar(60)  NULL,
    [FRXM] nvarchar(60)  NULL,
    [FRZJLX] nvarchar(60)  NULL,
    [FRZJH] nvarchar(60)  NULL,
    [FRDH] nvarchar(60)  NULL,
    [BZ] nvarchar(300)  NULL,
    [DYFS] nvarchar(60)  NOT NULL,
    [ZQDW] nvarchar(60)  NULL,
    [DYR] nvarchar(60)  NOT NULL,
    [DYRZJLX] nvarchar(60)  NOT NULL,
    [DYRZJH] nvarchar(60)  NOT NULL,
    [DYBDCLX] nvarchar(60)  NOT NULL,
    [CZFS] nvarchar(60)  NOT NULL,
    [DYPGJZ] nvarchar(60)  NULL,
    [BDBZZQSE] nvarchar(60)  NULL,
    [ZGZQSE] nvarchar(60)  NULL,
    [ZWR] nvarchar(60)  NOT NULL,
    [ZGZQQDSS] nvarchar(60)  NULL,
    [ZJJZWZL] nvarchar(100)  NULL,
    [ZJJZWDYFW] nvarchar(60)  NULL,
    [ZWLXQSSJ] datetime  NULL,
    [ZWLXJSSJ] datetime  NULL,
    [DJSJ] datetime  NULL,
    [DBR] nvarchar(60)  NOT NULL,
    [FJ] nvarchar(300)  NULL,
    [UpdateTime] datetime  NOT NULL
);
GO

-- Creating table 'Sequestration'
CREATE TABLE [Sequestration] (
    [ID] uniqueidentifier  NOT NULL,
    [ProjectID] uniqueidentifier  NOT NULL,
    [HBSM] nvarchar(60)  NOT NULL,
    [CFJG] nvarchar(60)  NULL,
    [CFLX] nvarchar(60)  NOT NULL,
    [CFWJ] nvarchar(60)  NULL,
    [CFWH] nvarchar(60)  NULL,
    [JFWJ] nvarchar(60)  NULL,
    [JFWH] nvarchar(60)  NULL,
    [CFQSSJ] datetime  NULL,
    [CFJSSJ] datetime  NULL,
    [DJSJ] datetime  NULL,
    [DBR] nvarchar(60)  NOT NULL,
    [LHCX] nvarchar(60)  NULL,
    [CFSJ] datetime  NOT NULL,
    [FJ] nvarchar(60)  NULL,
    [UpdateTime] datetime  NOT NULL
);
GO

-- Creating table 'UserInfo'
CREATE TABLE [UserInfo] (
    [ID] uniqueidentifier  NOT NULL,
    [Account] nvarchar(60)  NULL,
    [UserName] nvarchar(60)  NULL,
    [Password] nvarchar(60)  NULL,
    [Tel] nvarchar(60)  NULL,
    [Email] nvarchar(60)  NULL,
    [Address] nvarchar(60)  NULL,
    [LastLoginTime] datetime  NULL,
    [LastLogoutTime] datetime  NULL,
    [CreateTime] datetime  NULL,
    [IsAdmin] nvarchar(4000)  NOT NULL
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

-- Creating primary key on [ID] in table 'Applicant'
ALTER TABLE [Applicant]
ADD CONSTRAINT [PK_Applicant]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'Transfer'
ALTER TABLE [Transfer]
ADD CONSTRAINT [PK_Transfer]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'FileInfo'
ALTER TABLE [FileInfo]
ADD CONSTRAINT [PK_FileInfo]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'FileType'
ALTER TABLE [FileType]
ADD CONSTRAINT [PK_FileType]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'Mortgage'
ALTER TABLE [Mortgage]
ADD CONSTRAINT [PK_Mortgage]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'Sequestration'
ALTER TABLE [Sequestration]
ADD CONSTRAINT [PK_Sequestration]
    PRIMARY KEY ([ID] );
GO

-- Creating primary key on [ID] in table 'UserInfo'
ALTER TABLE [UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY ([ID] );
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------