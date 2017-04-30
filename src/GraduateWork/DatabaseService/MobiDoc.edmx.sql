
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/30/2017 19:20:34
-- Generated from EDMX file: C:\Users\naziway\Source\Repos\GraduateWork\src\GraduateWork\DatabaseService\MobiDoc.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DoctorPhone];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DevicesDb_ToClient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DevicesDbs] DROP CONSTRAINT [FK_DevicesDb_ToClient];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_ToDevices]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_ToDevices];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_ToParts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_ToParts];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_ToSparePhones]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_ToSparePhones];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_ToUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_ToUser];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_ToWorks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_ToWorks];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ClientsDbs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientsDbs];
GO
IF OBJECT_ID(N'[dbo].[DevicesDbs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DevicesDbs];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[PartsDbs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PartsDbs];
GO
IF OBJECT_ID(N'[dbo].[UsersDbs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersDbs];
GO
IF OBJECT_ID(N'[dbo].[WorksDbs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorksDbs];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ClientsDbs'
CREATE TABLE [dbo].[ClientsDbs] (
    [Id] int  NOT NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [PassportData] nvarchar(50)  NOT NULL,
    [Phone] varchar(12)  NOT NULL,
    [IsAdmin] bit  NOT NULL,
    [SignInDate] datetime  NOT NULL
);
GO

-- Creating table 'DevicesDbs'
CREATE TABLE [dbo].[DevicesDbs] (
    [Id] int  NOT NULL,
    [ClientId] int  NULL,
    [PhoneModel] nvarchar(30)  NOT NULL,
    [PhoneMarka] nvarchar(30)  NOT NULL,
    [DeviceType] nvarchar(20)  NOT NULL,
    [SerialNumber] nvarchar(14)  NOT NULL,
    [ManufactureDate] datetime  NOT NULL,
    [IsRepair] bit  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int  NOT NULL,
    [OrderKods] int  NOT NULL,
    [PartId] int  NOT NULL,
    [WorkId] int  NULL,
    [UserId] int  NOT NULL,
    [DeviceId] int  NOT NULL,
    [OrderType] nvarchar(50)  NOT NULL,
    [SparePhone] int  NULL
);
GO

-- Creating table 'PartsDbs'
CREATE TABLE [dbo].[PartsDbs] (
    [Id] int  NOT NULL,
    [Title] nvarchar(50)  NOT NULL,
    [Model] nvarchar(50)  NOT NULL,
    [Marka] nvarchar(50)  NOT NULL,
    [Price] float  NOT NULL,
    [IsAvailable] bit  NOT NULL
);
GO

-- Creating table 'UsersDbs'
CREATE TABLE [dbo].[UsersDbs] (
    [Id] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [Login] varchar(50)  NOT NULL,
    [Password] varchar(10)  NOT NULL
);
GO

-- Creating table 'WorksDbs'
CREATE TABLE [dbo].[WorksDbs] (
    [Id] int  NOT NULL,
    [Title] nvarchar(50)  NOT NULL,
    [Price] float  NOT NULL,
    [ExecutionTime] nchar(10)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ClientsDbs'
ALTER TABLE [dbo].[ClientsDbs]
ADD CONSTRAINT [PK_ClientsDbs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DevicesDbs'
ALTER TABLE [dbo].[DevicesDbs]
ADD CONSTRAINT [PK_DevicesDbs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PartsDbs'
ALTER TABLE [dbo].[PartsDbs]
ADD CONSTRAINT [PK_PartsDbs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsersDbs'
ALTER TABLE [dbo].[UsersDbs]
ADD CONSTRAINT [PK_UsersDbs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorksDbs'
ALTER TABLE [dbo].[WorksDbs]
ADD CONSTRAINT [PK_WorksDbs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ClientId] in table 'DevicesDbs'
ALTER TABLE [dbo].[DevicesDbs]
ADD CONSTRAINT [FK_DevicesDb_ToClient]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[ClientsDbs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DevicesDb_ToClient'
CREATE INDEX [IX_FK_DevicesDb_ToClient]
ON [dbo].[DevicesDbs]
    ([ClientId]);
GO

-- Creating foreign key on [DeviceId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_ToDevices]
    FOREIGN KEY ([DeviceId])
    REFERENCES [dbo].[DevicesDbs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_ToDevices'
CREATE INDEX [IX_FK_Orders_ToDevices]
ON [dbo].[Orders]
    ([DeviceId]);
GO

-- Creating foreign key on [SparePhone] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_ToSparePhones]
    FOREIGN KEY ([SparePhone])
    REFERENCES [dbo].[DevicesDbs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_ToSparePhones'
CREATE INDEX [IX_FK_Orders_ToSparePhones]
ON [dbo].[Orders]
    ([SparePhone]);
GO

-- Creating foreign key on [PartId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_ToParts]
    FOREIGN KEY ([PartId])
    REFERENCES [dbo].[PartsDbs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_ToParts'
CREATE INDEX [IX_FK_Orders_ToParts]
ON [dbo].[Orders]
    ([PartId]);
GO

-- Creating foreign key on [UserId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_ToUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UsersDbs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_ToUser'
CREATE INDEX [IX_FK_Orders_ToUser]
ON [dbo].[Orders]
    ([UserId]);
GO

-- Creating foreign key on [WorkId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_ToWorks]
    FOREIGN KEY ([WorkId])
    REFERENCES [dbo].[WorksDbs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_ToWorks'
CREATE INDEX [IX_FK_Orders_ToWorks]
ON [dbo].[Orders]
    ([WorkId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------