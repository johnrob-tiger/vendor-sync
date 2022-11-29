namespace Tsi.Vendors.DataSync.Application.Calendars
{
    public class CalendarEventDto
    {
        public string Id { get; set; } = null!;

        public Guid? CalendarId { get; set; } = null;

        public CalendarDto? Calendar { get; set; }

        public string Title { get; set; } = null!;

        public string? Body { get; set; }

        public bool IsBodyHtml { get; set; }

        public int? OwnerId { get; set; } = null!;

        public bool IsReminderOn { get; set; }

        public int? ReminderMinutesBefore { get; set; } = null!;

        public bool IsReadOnly { get; set; } = false;

        public DateTimeOffset StartDate { get; set; }
        public TimeSpan Duration { get; set; }

        public bool IsAllDay { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }
}
