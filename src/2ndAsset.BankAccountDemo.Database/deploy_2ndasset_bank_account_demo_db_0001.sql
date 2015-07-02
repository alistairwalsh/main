/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/


SET NOCOUNT ON
GO


CREATE TABLE [dbo].[CheckingAccountSource]
(
	[RecordId] [bigint] IDENTITY(0,1) NOT NULL,
	[TransactionId] [int] NULL,
	[InstitutionName] [nvarchar](255) NULL,
	[RefNumber] [nvarchar](255) NULL,
	[TheDate] [datetime] NULL,
	[PayeeName] [nvarchar](255) NULL,
	[TheAmount] [float] NULL,
	[IsCleared] [bit] NULL DEFAULT(0),
	[CategoryName] [nvarchar](255) NULL,
	[DueDate] [datetime] NULL,
	[Comments] [nvarchar](2047) NULL,
	
	CONSTRAINT [pk_CheckingAccountSource] PRIMARY KEY
	(
		[RecordId]
	)
)	
GO


CREATE TABLE [dbo].[Institution]
(
	[InstitutionId] [bigint] IDENTITY(0,1) NOT NULL,
	[InstitutionName] [nvarchar](255) NULL,
		
	CONSTRAINT [pk_Institution] PRIMARY KEY
	(
		[InstitutionId]
	),

	CONSTRAINT [uk_Institution] UNIQUE
	(
		[InstitutionName]
	)
)	
GO


CREATE TABLE [dbo].[Payee]
(
	[PayeeId] [bigint] IDENTITY(0,1) NOT NULL,
	[PayeeName] [nvarchar](255) NULL,
		
	CONSTRAINT [pk_Payee] PRIMARY KEY
	(
		[PayeeId]
	),

	CONSTRAINT [uk_Payee] UNIQUE
	(
		[PayeeName]
	)
)	
GO


CREATE TABLE [dbo].[Category]
(
	[CategoryId] [bigint] IDENTITY(0,1) NOT NULL,
	[CategoryName] [nvarchar](255) NULL,
		
	CONSTRAINT [pk_Category] PRIMARY KEY
	(
		[CategoryId]
	),

	CONSTRAINT [uk_Category] UNIQUE
	(
		[CategoryName]
	)
)	
GO


CREATE TABLE [dbo].[AccountLedger]
(
	[AccountLedgerId] [bigint] IDENTITY(0,1) NOT NULL,
	[SourceRecordId] [bigint] NOT NULL, -- no FK
	[OriginalTransactionId] [int] NOT NULL, -- no FK
	[InstitutionId] [bigint] NOT NULL,
	[RefNumber] [nvarchar](255) NOT NULL,
	[TheDate] [datetime] NOT NULL,
	[PayeeId] [bigint] NOT NULL,
	[TheAmount] [float] NOT NULL,
	[IsCleared] [bit] NOT NULL DEFAULT(0),
	[CategoryId] [bigint] NOT NULL,
	[DueDate] [datetime] NULL,
	[Comments] [nvarchar](2047) NOT NULL,
	
	CONSTRAINT [pk_AccountLedger] PRIMARY KEY
	(
		[AccountLedgerId]
	),

	CONSTRAINT [fk_AccountLedger_Institution] FOREIGN KEY
	(
		[InstitutionId]
	)
	REFERENCES [dbo].[Institution]
	(
		[InstitutionId]
	),

	CONSTRAINT [fk_AccountLedger_Payee] FOREIGN KEY
	(
		[PayeeId]
	)
	REFERENCES [dbo].[Payee]
	(
		[PayeeId]
	),

	CONSTRAINT [fk_AccountLedger_Category] FOREIGN KEY
	(
		[CategoryId]
	)
	REFERENCES [dbo].[Category]
	(
		[CategoryId]
	)
)
GO


INSERT INTO [dbo].[Institution] VALUES ('Unknown');
INSERT INTO [dbo].[Payee] VALUES ('Unknown');
INSERT INTO [dbo].[Category] VALUES ('Unknown');
GO


CREATE TABLE [dbo].[Ox_AccountReporting]
(
	[AccountReportingId] [bigint] NOT NULL,
	[TransactionNumber] [int] NOT NULL,
	[InstitutionName] [nvarchar](255) NOT NULL,
	[RefNumber] [nvarchar](255) NOT NULL,
	[TheDate] [datetime] NOT NULL,
	[PayeeName] [nvarchar](255) NOT NULL,
	[TheAmount] [float] NOT NULL,
	[IsCleared] [bit] NOT NULL DEFAULT(0),
	[CategoryName] [nvarchar](255) NOT NULL,
	[DueDate] [datetime] NULL,
	[Comments] [nvarchar](2047) NOT NULL,

	CONSTRAINT [pk_Ox_AccountReporting] PRIMARY KEY
	(
		[AccountReportingId]
	)
)
GO


CREATE TABLE [dbo].[Ox_AccountDailySnapshot]
(
	[AccountDailySnapshotId] [bigint] IDENTITY(0,1) NOT NULL,
	[TheDate] [datetime] NOT NULL,
	[DailyCount] [int] NOT NULL,
	[DailyDelta] [money] NOT NULL,
	[RunningBalance] [money] NOT NULL,

	CONSTRAINT [pk_Ox_AccountDailySnapshot] PRIMARY KEY
	(
		[AccountDailySnapshotId]
	)
)
GO


CREATE PROCEDURE [dbo].[ExecuteExport_AccountReporting]
AS
	SELECT
	t.[AccountLedgerId] AS [AccountReportingId],
	t.[OriginalTransactionId] as [TransactionNumber],
	UPPER(ti.[InstitutionName]) AS [InstitutionName],
	UPPER(t.[RefNumber]) AS [RefNumber],
	t.[TheDate] AS [TheDate],
	UPPER(tp.[PayeeName]) AS [PayeeName],
	t.[TheAmount] AS [TheAmount],
	t.[IsCleared] AS [IsCleared],
	UPPER(tc.[CategoryName]) AS [CategoryName],
	t.[DueDate] AS [DueDate],
	UPPER(t.[Comments]) AS [Comments]
	FROM
	[dbo].[AccountLedger] t
	INNER JOIN [dbo].[Institution] ti ON ti.[InstitutionId] = t.[InstitutionId]
	INNER JOIN [dbo].[Payee] tp ON tp.[PayeeId] = t.[PayeeId]
	INNER JOIN [dbo].[Category] tc ON tc.[CategoryId] = t.[CategoryId]
GO


CREATE PROCEDURE [dbo].[ExecuteExport_AccountDailySnapshot]
AS
	SELECT
		CAST(t.[TheDate] AS [date]) AS [TheDate],

		COUNT(t.[AccountLedgerId]) AS [DailyCount],

		SUM(CAST(t.[TheAmount] AS [money])) AS [DailyDelta],

		SUM(SUM(CAST(t.[TheAmount] AS [money]))) OVER
			(ORDER BY CAST(t.[TheDate] AS [date])) AS [RunningBalance]
	FROM
		[dbo].[AccountLedger] t
	GROUP BY
		CAST(t.[TheDate] AS [date])
GO


CREATE PROCEDURE [dbo].[ExecuteBeforeImport_Clean]
AS
	TRUNCATE TABLE [dbo].[Ox_AccountReporting];
	TRUNCATE TABLE [dbo].[AccountLedger];
	TRUNCATE TABLE [dbo].[CheckingAccountSource];

	DELETE FROM [dbo].[Payee] WHERE [PayeeId] <> 0;
	DELETE FROM [dbo].[Category] WHERE [CategoryId] <> 0;
	DELETE FROM [dbo].[Institution] WHERE [InstitutionId] <> 0;
GO


CREATE PROCEDURE [dbo].[ExecuteImport_Main]
AS
	SELECT
	t.[RecordId] as [RecordId],
	t.[TransactionId] as [TransactionId],
	ISNULL((SELECT TOP 1 z.[InstitutionId] FROM [dbo].[Institution] z WHERE z.[InstitutionName] = t.[InstitutionName]), 0) AS [InstitutionId],
	ISNULL(t.[RefNumber], '') AS [RefNumber],
	t.[TheDate],
	ISNULL((SELECT TOP 1 z.[PayeeId] FROM [dbo].[Payee] z WHERE z.[PayeeName] = t.[PayeeName]), 0) AS [PayeeId],
	t.[TheAmount],
	t.[IsCleared],
	ISNULL((SELECT TOP 1 z.[CategoryId] FROM [dbo].[Category] z WHERE z.[CategoryName] = t.[CategoryName]), 0) AS [CategoryId],
	t.[DueDate],
	ISNULL(t.[Comments], '') AS [Comments]
	FROM
	[dbo].[CheckingAccountSource] t
	ORDER BY [InstitutionName] DESC, [TransactionId] ASC
GO


CREATE PROCEDURE [dbo].[ExecuteImport_Institutions]
AS
	SELECT DISTINCT [InstitutionName] FROM [dbo].[CheckingAccountSource] WHERE [InstitutionName] IS NOT NULL ORDER BY [InstitutionName] ASC;
GO


CREATE PROCEDURE [dbo].[ExecuteImport_Payees]
AS
	SELECT DISTINCT [PayeeName] FROM [dbo].[CheckingAccountSource] WHERE [PayeeName] IS NOT NULL ORDER BY [PayeeName] ASC;
GO


CREATE PROCEDURE [dbo].[ExecuteImport_Categories]
AS
	SELECT DISTINCT [CategoryName] FROM [dbo].[CheckingAccountSource] WHERE [CategoryName] IS NOT NULL ORDER BY [CategoryName] ASC;
GO
