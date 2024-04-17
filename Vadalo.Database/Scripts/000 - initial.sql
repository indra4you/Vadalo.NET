/*
	- User cannot sign-up
	- Someone must invite new user using email id which inserts row to [Identity] and send a mail with link to sign-in
	- Once user clicks on invite link, he/she can enter email id on sign-in page and click on sign-in which generate OTP & inserts to [IdentityPassCode] & send OTP over the email
	- User to enter OTP sent over the mail on sign-in page and click on sign-in button
	- System to validate OTP and generate JWT token & send it back to UI
	- UI has to check profile exists; if NOT, UI to prompt for profile creation
	- 
*/

DECLARE @AdminEmailAddress NVARCHAR(250);
SET @AdminEmailAddress = LOWER('admin@vadalo.com');

CREATE TABLE [dbo].[identities]
(
	[id]					UNIQUEIDENTIFIER			NOT NULL	DEFAULT (NEWID()),
	[created_at]			DATETIME2					NOT NULL	DEFAULT (SYSUTCDATETIME()),
	[invited_by]			UNIQUEIDENTIFIER			NOT NULL,
	[sign_in_id]			NVARCHAR(250)				NOT NULL,
	CONSTRAINT [pk_identities] PRIMARY KEY ( [id] ),
	CONSTRAINT [uk_identities_sign_in_id] UNIQUE ( [sign_in_id] ),
) AS NODE;

CREATE TABLE [dbo].[genders]
(
	[id]					UNIQUEIDENTIFIER			NOT NULL	DEFAULT (NEWID()),
	[name]					NVARCHAR(50)				NOT NULL,
	CONSTRAINT [pk_genders] PRIMARY KEY ( [id] ),
	CONSTRAINT [uk_genders_name] UNIQUE ( [name] ),
) AS NODE;

CREATE TABLE [dbo].[members]
(
	[id]					UNIQUEIDENTIFIER			NOT NULL	DEFAULT (NEWID()),
	[status]				TINYINT						NOT NULL	DEFAULT (0),
	[email_address]			NVARCHAR(250)				NOT NULL,
	[last_name]				NVARCHAR(50)				NOT NULL,
	[first_name]			NVARCHAR(50)				NOT NULL,
	[middle_name]			NVARCHAR(50)				NULL,
	CONSTRAINT [pk_members] PRIMARY KEY ( [id] )
) AS NODE;

CREATE TABLE [dbo].[identities_of] AS EDGE;

CREATE TABLE [dbo].[genders_of] AS EDGE;

INSERT INTO [dbo].[identities] ([invited_by], [sign_in_id])
VALUES ('00000000-0000-0000-0000-000000000000', @AdminEmailAddress);

INSERT INTO [dbo].[genders] ([name])
VALUES ('Not Disclosed');
INSERT INTO [dbo].[genders] ([name])
VALUES ('Female');
INSERT INTO [dbo].[genders] ([name])
VALUES ('Male');

INSERT INTO [dbo].[members] ([email_address], [last_name], [first_name], [middle_name])
VALUES (@AdminEmailAddress, 'User', 'Admin', NULL);

INSERT INTO [dbo].[identities_of]
VALUES (
	( SELECT i.$node_id FROM [dbo].[identities] AS i WHERE i.[sign_in_id] = @AdminEmailAddress ),
	( SELECT m.$node_id FROM [dbo].[members] AS m WHERE m.[email_address] = @AdminEmailAddress )
);

INSERT INTO [dbo].[genders_of]
VALUES (
	( SELECT g.$node_id FROM [dbo].[genders] AS g WHERE g.[name] = 'Not Disclosed' ),
	( SELECT m.$node_id FROM [dbo].[members] AS m WHERE m.[email_address] = @AdminEmailAddress )
);