/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/


SET NOCOUNT ON
GO


--USE master
--GO


IF EXISTS (SELECT sys_d.[database_id] FROM [sys].[databases] sys_d WHERE sys_d.[name] = '$(VAR_DB_DATABASE)')
BEGIN

	ALTER DATABASE [$(VAR_DB_DATABASE)] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

	DROP DATABASE [$(VAR_DB_DATABASE)];

END
GO


IF EXISTS (SELECT sys_spl.[principal_id] FROM [sys].[server_principals] sys_spl WHERE sys_spl.[name] = N'$(VAR_DB_LOGIN)')
BEGIN

	DROP LOGIN [$(VAR_DB_LOGIN)];

END
GO


CREATE DATABASE [$(VAR_DB_DATABASE)]
GO


CREATE LOGIN [$(VAR_DB_LOGIN)] WITH PASSWORD = '$(VAR_DB_PASSWORD)', CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF, DEFAULT_LANGUAGE=[us_english]
GO


GRANT CONNECT SQL TO [$(VAR_DB_LOGIN)]
GO


USE [$(VAR_DB_DATABASE)]
GO


CREATE USER [$(VAR_DB_USER)] FOR LOGIN [$(VAR_DB_LOGIN)] WITH DEFAULT_SCHEMA=[dbo]
GO


EXEC sp_addrolemember 'db_owner', '$(VAR_DB_USER)'
GO


ALTER LOGIN [$(VAR_DB_LOGIN)] WITH DEFAULT_DATABASE=[$(VAR_DB_DATABASE)]
GO
