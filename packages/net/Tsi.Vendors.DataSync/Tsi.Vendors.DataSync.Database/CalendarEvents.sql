CREATE TABLE [dbo].[CalendarEvents]
(
	[Id] VARCHAR(250) NOT NULL PRIMARY KEY,
	[Title] VARCHAR(250) NOT NULL,
	[Body] VARCHAR(4000) NULL,
	[IsBodyHtml] BIT NOT NULL DEFAULT(0),
	[StartDate] DATETIME NOT NULL,
	[Duration] INT NOT NULL,
	[IsAllDay] BIT NOT NULL DEFAULT(0),
	[IsReminderOn] BIT NOT NULL DEFAULT(0),
	[ReminderMinutesBefore] INT NULL DEFAULT(0),
	[IsReadOnly] BIT NOT NULL DEFAULT(0),
	[CalendarId] UNIQUEIDENTIFIER NOT NULL,
	[OwnerId] INT NULL, 
    CONSTRAINT [FK_CalendarEvents_Calendar] FOREIGN KEY ([CalendarId]) REFERENCES [Calendars]([Id]),
	CONSTRAINT [FK_CalendarEvents_Owner] FOREIGN KEY ([OwnerId]) REFERENCES [Users]([Id])
)
