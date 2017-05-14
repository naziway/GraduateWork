
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/14/2017 18:27:34
-- Generated from EDMX file: C:\Users\naziway\Source\Repos\GraduateWork\src\GraduateWork\DatabaseService\DataMobile.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MobiDoc];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Devices_ToClient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Devices] DROP CONSTRAINT [FK_Devices_ToClient];
GO
IF OBJECT_ID(N'[dbo].[FK_Repairs_To_Device]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Repairs] DROP CONSTRAINT [FK_Repairs_To_Device];
GO
IF OBJECT_ID(N'[dbo].[FK_Repairs_To_Part]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Repairs] DROP CONSTRAINT [FK_Repairs_To_Part];
GO
IF OBJECT_ID(N'[dbo].[FK_Repairs_To_RepairDevice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Repairs] DROP CONSTRAINT [FK_Repairs_To_RepairDevice];
GO
IF OBJECT_ID(N'[dbo].[FK_Repairs_To_Work]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Repairs] DROP CONSTRAINT [FK_Repairs_To_Work];
GO
IF OBJECT_ID(N'[dbo].[FK_Repairs_To_Worker]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Repairs] DROP CONSTRAINT [FK_Repairs_To_Worker];
GO
IF OBJECT_ID(N'[dbo].[FK_Reviews_To_Device]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reviews] DROP CONSTRAINT [FK_Reviews_To_Device];
GO
IF OBJECT_ID(N'[dbo].[FK_Reviews_To_Repair]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reviews] DROP CONSTRAINT [FK_Reviews_To_Repair];
GO
IF OBJECT_ID(N'[dbo].[FK_Reviews_To_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reviews] DROP CONSTRAINT [FK_Reviews_To_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Reviews_To_Worker]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reviews] DROP CONSTRAINT [FK_Reviews_To_Worker];
GO
IF OBJECT_ID(N'[dbo].[FK_Sellings_To_Client]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sellings] DROP CONSTRAINT [FK_Sellings_To_Client];
GO
IF OBJECT_ID(N'[dbo].[FK_Sellings_To_Part]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sellings] DROP CONSTRAINT [FK_Sellings_To_Part];
GO
IF OBJECT_ID(N'[dbo].[FK_Sellings_To_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sellings] DROP CONSTRAINT [FK_Sellings_To_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_ToPersonalData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_ToPersonalData];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[Devices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Devices];
GO
IF OBJECT_ID(N'[dbo].[Parts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parts];
GO
IF OBJECT_ID(N'[dbo].[PersonalData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonalData];
GO
IF OBJECT_ID(N'[dbo].[RepairDevices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RepairDevices];
GO
IF OBJECT_ID(N'[dbo].[Repairs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Repairs];
GO
IF OBJECT_ID(N'[dbo].[Reviews]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reviews];
GO
IF OBJECT_ID(N'[dbo].[Sellings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sellings];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Works]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Works];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [Id] int  NOT NULL,
    [FirstName] nvarchar(15)  NOT NULL,
    [LastName] nvarchar(15)  NOT NULL,
    [PasportData] nvarchar(15)  NOT NULL,
    [PhoneNumber] nvarchar(15)  NOT NULL,
    [RegistrationDate] datetime  NOT NULL
);
GO

-- Creating table 'Devices'
CREATE TABLE [dbo].[Devices] (
    [Id] int  NOT NULL,
    [Marka] nvarchar(50)  NOT NULL,
    [Model] nvarchar(50)  NOT NULL,
    [DeviceType] int  NOT NULL,
    [SerialNumber] nvarchar(50)  NOT NULL,
    [ManufactureDate] datetime  NOT NULL,
    [ClientId] int  NOT NULL
);
GO

-- Creating table 'Parts'
CREATE TABLE [dbo].[Parts] (
    [Id] int  NOT NULL,
    [Title] nvarchar(500)  NOT NULL,
    [Price] float  NOT NULL,
    [Count] int  NOT NULL,
    [Marka] nchar(50)  NOT NULL,
    [Model] nchar(50)  NOT NULL
);
GO

-- Creating table 'PersonalData'
CREATE TABLE [dbo].[PersonalData] (
    [Id] int  NOT NULL,
    [FirstName] nvarchar(15)  NOT NULL,
    [LastName] nvarchar(15)  NOT NULL,
    [PasportData] nvarchar(15)  NOT NULL,
    [BirthDate] datetime  NOT NULL
);
GO

-- Creating table 'RepairDevices'
CREATE TABLE [dbo].[RepairDevices] (
    [Id] int  NOT NULL,
    [Marka] nvarchar(50)  NOT NULL,
    [Model] nvarchar(50)  NOT NULL,
    [DeviceType] int  NOT NULL,
    [SerialNumber] nvarchar(50)  NOT NULL,
    [ManufactureDate] datetime  NOT NULL,
    [Count] int  NOT NULL
);
GO

-- Creating table 'Repairs'
CREATE TABLE [dbo].[Repairs] (
    [Id] int  NOT NULL,
    [Kod] int  NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [Status] int  NOT NULL,
    [IsWarranty] bit  NOT NULL,
    [RepairDeviceId] int  NOT NULL,
    [WorkerId] int  NOT NULL,
    [DeviceId] int  NOT NULL,
    [PartId] int  NULL,
    [WorkId] int  NOT NULL
);
GO

-- Creating table 'Reviews'
CREATE TABLE [dbo].[Reviews] (
    [Id] int  NOT NULL,
    [Kod] int  NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [Status] int  NOT NULL,
    [UserId] int  NOT NULL,
    [DeviceId] int  NOT NULL,
    [WorkerId] int  NOT NULL,
    [RepairId] int  NULL
);
GO

-- Creating table 'Sellings'
CREATE TABLE [dbo].[Sellings] (
    [Id] int  NOT NULL,
    [Kod] int  NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [Status] int  NOT NULL,
    [UserId] int  NOT NULL,
    [ClientId] int  NOT NULL,
    [PartId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int  NOT NULL,
    [Login] nvarchar(15)  NOT NULL,
    [Password] nvarchar(15)  NOT NULL,
    [RegistrationDate] datetime  NOT NULL,
    [UserType] int  NOT NULL,
    [PersonalDataId] int  NOT NULL
);
GO

-- Creating table 'Works'
CREATE TABLE [dbo].[Works] (
    [Id] int  NOT NULL,
    [Title] nvarchar(500)  NOT NULL,
    [Price] float  NOT NULL,
    [Time] time  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Devices'
ALTER TABLE [dbo].[Devices]
ADD CONSTRAINT [PK_Devices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Parts'
ALTER TABLE [dbo].[Parts]
ADD CONSTRAINT [PK_Parts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonalData'
ALTER TABLE [dbo].[PersonalData]
ADD CONSTRAINT [PK_PersonalData]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RepairDevices'
ALTER TABLE [dbo].[RepairDevices]
ADD CONSTRAINT [PK_RepairDevices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Repairs'
ALTER TABLE [dbo].[Repairs]
ADD CONSTRAINT [PK_Repairs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [PK_Reviews]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sellings'
ALTER TABLE [dbo].[Sellings]
ADD CONSTRAINT [PK_Sellings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Works'
ALTER TABLE [dbo].[Works]
ADD CONSTRAINT [PK_Works]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ClientId] in table 'Devices'
ALTER TABLE [dbo].[Devices]
ADD CONSTRAINT [FK_Devices_ToClient]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[Clients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Devices_ToClient'
CREATE INDEX [IX_FK_Devices_ToClient]
ON [dbo].[Devices]
    ([ClientId]);
GO

-- Creating foreign key on [ClientId] in table 'Sellings'
ALTER TABLE [dbo].[Sellings]
ADD CONSTRAINT [FK_Sellings_To_Client]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[Clients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sellings_To_Client'
CREATE INDEX [IX_FK_Sellings_To_Client]
ON [dbo].[Sellings]
    ([ClientId]);
GO

-- Creating foreign key on [DeviceId] in table 'Repairs'
ALTER TABLE [dbo].[Repairs]
ADD CONSTRAINT [FK_Repairs_To_Device]
    FOREIGN KEY ([DeviceId])
    REFERENCES [dbo].[Devices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Repairs_To_Device'
CREATE INDEX [IX_FK_Repairs_To_Device]
ON [dbo].[Repairs]
    ([DeviceId]);
GO

-- Creating foreign key on [DeviceId] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [FK_Reviews_To_Device]
    FOREIGN KEY ([DeviceId])
    REFERENCES [dbo].[Devices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Reviews_To_Device'
CREATE INDEX [IX_FK_Reviews_To_Device]
ON [dbo].[Reviews]
    ([DeviceId]);
GO

-- Creating foreign key on [PartId] in table 'Repairs'
ALTER TABLE [dbo].[Repairs]
ADD CONSTRAINT [FK_Repairs_To_Part]
    FOREIGN KEY ([PartId])
    REFERENCES [dbo].[Parts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Repairs_To_Part'
CREATE INDEX [IX_FK_Repairs_To_Part]
ON [dbo].[Repairs]
    ([PartId]);
GO

-- Creating foreign key on [PartId] in table 'Sellings'
ALTER TABLE [dbo].[Sellings]
ADD CONSTRAINT [FK_Sellings_To_Part]
    FOREIGN KEY ([PartId])
    REFERENCES [dbo].[Parts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sellings_To_Part'
CREATE INDEX [IX_FK_Sellings_To_Part]
ON [dbo].[Sellings]
    ([PartId]);
GO

-- Creating foreign key on [PersonalDataId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_ToPersonalData]
    FOREIGN KEY ([PersonalDataId])
    REFERENCES [dbo].[PersonalData]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_ToPersonalData'
CREATE INDEX [IX_FK_Users_ToPersonalData]
ON [dbo].[Users]
    ([PersonalDataId]);
GO

-- Creating foreign key on [RepairDeviceId] in table 'Repairs'
ALTER TABLE [dbo].[Repairs]
ADD CONSTRAINT [FK_Repairs_To_RepairDevice]
    FOREIGN KEY ([RepairDeviceId])
    REFERENCES [dbo].[RepairDevices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Repairs_To_RepairDevice'
CREATE INDEX [IX_FK_Repairs_To_RepairDevice]
ON [dbo].[Repairs]
    ([RepairDeviceId]);
GO

-- Creating foreign key on [WorkId] in table 'Repairs'
ALTER TABLE [dbo].[Repairs]
ADD CONSTRAINT [FK_Repairs_To_Work]
    FOREIGN KEY ([WorkId])
    REFERENCES [dbo].[Works]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Repairs_To_Work'
CREATE INDEX [IX_FK_Repairs_To_Work]
ON [dbo].[Repairs]
    ([WorkId]);
GO

-- Creating foreign key on [WorkerId] in table 'Repairs'
ALTER TABLE [dbo].[Repairs]
ADD CONSTRAINT [FK_Repairs_To_Worker]
    FOREIGN KEY ([WorkerId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Repairs_To_Worker'
CREATE INDEX [IX_FK_Repairs_To_Worker]
ON [dbo].[Repairs]
    ([WorkerId]);
GO

-- Creating foreign key on [RepairId] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [FK_Reviews_To_Repair]
    FOREIGN KEY ([RepairId])
    REFERENCES [dbo].[Repairs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Reviews_To_Repair'
CREATE INDEX [IX_FK_Reviews_To_Repair]
ON [dbo].[Reviews]
    ([RepairId]);
GO

-- Creating foreign key on [UserId] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [FK_Reviews_To_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Reviews_To_User'
CREATE INDEX [IX_FK_Reviews_To_User]
ON [dbo].[Reviews]
    ([UserId]);
GO

-- Creating foreign key on [WorkerId] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [FK_Reviews_To_Worker]
    FOREIGN KEY ([WorkerId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Reviews_To_Worker'
CREATE INDEX [IX_FK_Reviews_To_Worker]
ON [dbo].[Reviews]
    ([WorkerId]);
GO

-- Creating foreign key on [UserId] in table 'Sellings'
ALTER TABLE [dbo].[Sellings]
ADD CONSTRAINT [FK_Sellings_To_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sellings_To_User'
CREATE INDEX [IX_FK_Sellings_To_User]
ON [dbo].[Sellings]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------