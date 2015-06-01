/*
	Copyright ©2002-2014 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/


SET NOCOUNT ON
GO


CREATE TABLE [dbo].[CheckingAccountSource]
(
	[RecordId] [int] IDENTITY(0,1) NOT NULL,
	[TransactionId] [int] NULL, -- none
	[InstitutionName] [nvarchar](255) NULL, -- masking
	[RefNumber] [nvarchar](255) NULL, -- shuffling
	[TheDate] [datetime] NULL, -- variance
	[PayeeName] [nvarchar](255) NULL, -- substitution
	[TheAmount] [float] NULL, -- variance
	[IsCleared] [bit] NULL DEFAULT(0), -- none
	[CategoryName] [nvarchar](255) NULL, -- masking
	[DueDate] [datetime] NULL, -- defaulting
	[Comments] [nvarchar](2047) NULL, -- ciphering
	
	CONSTRAINT [pk_CheckingAccountSource] PRIMARY KEY
	(
		[RecordId]
	)
)	
GO


CREATE TABLE [dbo].[Institution]
(
	[InstitutionId] [int] IDENTITY(0,1) NOT NULL,
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
	[PayeeId] [int] IDENTITY(0,1) NOT NULL,
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
	[CategoryId] [int] IDENTITY(0,1) NOT NULL,
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


CREATE TABLE [dbo].[CheckingAccount]
(
	[TransactionId] [int] IDENTITY(0,1) NOT NULL,
	[InstitutionId] [int] NOT NULL,
	[RefNumber] [nvarchar](255) NOT NULL,
	[TheDate] [datetime] NOT NULL,
	[PayeeId] [int] NOT NULL,
	[TheAmount] [float] NOT NULL,
	[IsCleared] [bit] NOT NULL DEFAULT(0),
	[CategoryId] [int] NOT NULL,
	[DueDate] [datetime] NULL,
	[Comments] [nvarchar](2047) NOT NULL,
	
	CONSTRAINT [pk_CheckingAccount] PRIMARY KEY
	(
		[TransactionId]
	),

	CONSTRAINT [fk_CheckingAccount_Institution] FOREIGN KEY
	(
		[InstitutionId]
	)
	REFERENCES [dbo].[Institution]
	(
		[InstitutionId]
	),

	CONSTRAINT [fk_CheckingAccount_Payee] FOREIGN KEY
	(
		[PayeeId]
	)
	REFERENCES [dbo].[Payee]
	(
		[PayeeId]
	),

	CONSTRAINT [fk_CheckingAccount_Category] FOREIGN KEY
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


CREATE TABLE [dbo].[Ox_CheckingAccount]
(
	[TransactionId] [int] IDENTITY(0,1) NOT NULL,
	[InstitutionId] [int] NOT NULL,
	[RefNumber] [nvarchar](255) NOT NULL,
	[TheDate] [datetime] NOT NULL,
	[PayeeId] [int] NOT NULL,
	[TheAmount] [float] NOT NULL,
	[IsCleared] [bit] NOT NULL DEFAULT(0),
	[CategoryId] [int] NOT NULL,
	[DueDate] [datetime] NULL,
	[Comments] [nvarchar](2047) NOT NULL,
	
	CONSTRAINT [pk_Ox_CheckingAccount] PRIMARY KEY
	(
		[TransactionId]
	),

	/*CONSTRAINT [fk_Ox_CheckingAccount_Institution] FOREIGN KEY
	(
		[InstitutionId]
	)
	REFERENCES [dbo].[Institution]
	(
		[InstitutionId]
	),

	CONSTRAINT [fk_Ox_CheckingAccount_Payee] FOREIGN KEY
	(
		[PayeeId]
	)
	REFERENCES [dbo].[Payee]
	(
		[PayeeId]
	),

	CONSTRAINT [fk_Ox_CheckingAccount_Category] FOREIGN KEY
	(
		[CategoryId]
	)
	REFERENCES [dbo].[Category]
	(
		[CategoryId]
	)*/
)	
GO


CREATE VIEW [dbo].[CheckingAccountDailyLedger]
AS
	SELECT
		CAST(t.[TheDate] AS [date]) AS [TheDate],

		COUNT(t.[TransactionId]) AS [DailyCount],

		SUM(CAST(t.[TheAmount] AS [money])) AS [DailyDelta],

		SUM(SUM(CAST(t.[TheAmount] AS [money]))) OVER
			(ORDER BY CAST(t.[TheDate] AS [date])) AS [RunningBalance]
	FROM
		[dbo].[CheckingAccount] t
	GROUP BY
		CAST(t.[TheDate] AS [date])
GO


CREATE VIEW [dbo].[Ox_CheckingAccountDailyLedger]
AS
	SELECT
		CAST(t.[TheDate] AS [date]) AS [TheDate],

		COUNT(t.[TransactionId]) AS [DailyCount],

		SUM(CAST(t.[TheAmount] AS [money])) AS [DailyDelta],

		SUM(SUM(CAST(t.[TheAmount] AS [money]))) OVER
			(ORDER BY CAST(t.[TheDate] AS [date])) AS [RunningBalance]
	FROM
		[dbo].[Ox_CheckingAccount] t
	GROUP BY
		CAST(t.[TheDate] AS [date])
GO


-- *************************************************************************************************
-- *************************************************************************************************


/*
-- Get Balance - sanity checks
SELECT SUM([TheAmount]) FROM [dbo].[CheckingAccountSource];
SELECT SUM([TheAmount]) FROM [dbo].[CheckingAccount];


-- Get Institutions
SELECT DISTINCT [InstitutionName] FROM [dbo].[CheckingAccountSource] WHERE [InstitutionName] IS NOT NULL ORDER BY [InstitutionName] ASC;


-- Get Payees
SELECT DISTINCT [PayeeName] FROM [dbo].[CheckingAccountSource] WHERE [PayeeName] IS NOT NULL ORDER BY [PayeeName] ASC;


-- Get Categories
SELECT DISTINCT [CategoryName] FROM [dbo].[CheckingAccountSource] WHERE [CategoryName] IS NOT NULL ORDER BY [CategoryName] ASC;


-- Get CheckingAccount items
SELECT
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





SELECT
	ca.[TransactionId],
	i.[InstitutionName],
	ca.[RefNumber],
	ca.[TheDate],
	p.[PayeeName],
	ca.[TheAmount],
	ca.[IsCleared],
	c.[CategoryName],
	ca.[DueDate],
	ca.[Comments]
FROM
	[dbo].[CheckingAccount] ca
	INNER JOIN [dbo].[Institution] i ON i.[InstitutionId] = ca.[InstitutionId]
	INNER JOIN [dbo].[Payee] p ON p.[PayeeId] = ca.[PayeeId]
	INNER JOIN [dbo].[Category] c ON c.[CategoryId] = ca.[CategoryId]
ORDER BY
	ca.[TheDate] ASC


SELECT
	ca.[TheDate],
	SUM(ca.[TheAmount]) AS [DailyChange]
FROM
	[dbo].[CheckingAccount] ca
GROUP BY
	ca.[TheDate]
ORDER BY
	ca.[TheDate] ASC



SELECT
	ca.[TheDate],
	SUM(SUM(ca.[TheAmount])) OVER
		(ORDER BY [TheDate]) AS [DailyBalance]
FROM
	[dbo].[CheckingAccount] ca
GROUP BY
	ca.[TheDate]
ORDER BY
	ca.[TheDate] ASC

GO
*/
