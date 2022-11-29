CREATE TABLE [dbo].[MailBoxes]
(
	[Id] VARCHAR(250) NOT NULL PRIMARY KEY,
	[DisplayName] VARCHAR(250) NULL,
	[AccessToken] VARCHAR(250) NULL,
	[RefreshToken] VARCHAR(250) NULL,
	[Provider] VARCHAR(150) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT(1),
	[UserId] INT NOT NULL, 
    CONSTRAINT [FK_MailBoxes_User] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
)
