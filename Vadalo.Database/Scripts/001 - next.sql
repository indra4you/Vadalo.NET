/*
CREATE TABLE [dbo].[Country]
(
	[ID]				UNIQUEIDENTIFIER			NOT NULL	DEFAULT (NEWID()),
	[Name]				NVARCHAR(250)				NOT NULL,
	[Code]				NVARCHAR(50)				NOT NULL,
	CONSTRAINT [PK_Country] PRIMARY KEY ( [ID] ),
	CONSTRAINT [UK_Country_Name] UNIQUE ( [Name] ),
) AS NODE;

INSERT INTO [dbo].[Country] ([Name], [Code])
VALUES ('India', 'IN');

DECLARE @IndiaCountryID UNIQUEIDENTIFIER;
SELECT @IndiaCountryID = [ID] FROM [dbo].[Country] WHERE [Code] = 'IN';

CREATE TABLE [dbo].[State]
(
	[ID]				UNIQUEIDENTIFIER			NOT NULL	DEFAULT (NEWID()),
	[CountryID]			UNIQUEIDENTIFIER			NOT NULL,
	[Name]				NVARCHAR(250)				NOT NULL,
	[Code]				NVARCHAR(50)				NOT NULL,
	CONSTRAINT [PK_State] PRIMARY KEY ( [ID], [CountryID] ),
	CONSTRAINT [UK_State_Name] UNIQUE ( [CountryID], [Name] ),
	CONSTRAINT [FK_State_Country] FOREIGN KEY ( [CountryID] ) REFERENCES [dbo].[Country] ( [ID] ),
) AS NODE;


INSERT INTO [dbo].[State] ([CountryID], [Name], [Code])
VALUES (@IndiaCountryID, 'Gujarat', 'GJ');
INSERT INTO [dbo].[State] ([CountryID], [Name], [Code])
VALUES (@IndiaCountryID, 'Telangana', 'TS');


CREATE TABLE [dbo].[PassCode]
(
	[ID]				UNIQUEIDENTIFIER			NOT NULL,
	[CreatedAt]			DATETIME2					NOT NULL	DEFAULT (SYSUTCDATETIME()),
	[ExpireAt]			DATETIME2					NOT NULL	DEFAULT (DATEADD(MINUTE, 30, SYSUTCDATETIME())),
	[PassSalt]			NVARCHAR(1024)				NOT NULL,
	[PassHash]			NVARCHAR(1024)				NOT NULL,
	CONSTRAINT [PK_PassCode] PRIMARY KEY ( [ID] ),
) AS NODE;

CREATE TABLE [dbo].[PassCodeOf] AS EDGE;
*/