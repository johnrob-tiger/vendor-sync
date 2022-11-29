CREATE TABLE [dbo].[CalendarEventAttendees]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[CalendarEventId] VARCHAR(250) NOT NULL,
	[EmailAddress] VARCHAR(250) NOT NULL,
	[DisplayName] VARCHAR(250) NULL,
	[PrimaryPhone] VARCHAR(100) NULL,
	[Accepted] BIT NULL DEFAULT(0), 
    CONSTRAINT [FK_CalendarEventAttendees_CalendarEvent] FOREIGN KEY ([CalendarEventId]) REFERENCES [CalendarEvents]([Id])
)
