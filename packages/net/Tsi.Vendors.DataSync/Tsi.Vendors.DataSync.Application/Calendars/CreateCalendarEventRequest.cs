namespace Tsi.Vendors.DataSync.Application.Calendars
{
    public class CreateCalendarEventRequest
    {
        public CreateCalendarEventRequest(
            Guid calendarId,
            string title, 
            DateTimeOffset startDate, 
            TimeSpan duration,
            bool isAllDay = false,
            bool isReminderOn = false,
            bool isBodyHtml = false,
            bool isReadOnly = false,
            int? reminderMinutesBefore = null,
            string? body = null
            )
        {
            CalendarId = calendarId;
            Title = title;
            StartDate = startDate;
            Duration = duration;
            IsAllDay = isAllDay;
            IsReminderOn = isReminderOn;
            IsBodyHtml = isBodyHtml;
            IsReadOnly = isReadOnly;
            Body = body;
            ReminderMinutesBefore = reminderMinutesBefore;
        }
        public Guid CalendarId { get; private set; }
        public string Title { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public TimeSpan Duration { get; private set; }

        public bool IsReadOnly { get; private set; }
        public bool IsAllDay { get; private set; }
        public bool IsReminderOn { get; private set; }
        public int? ReminderMinutesBefore { get; private set; }

        public string? Body { get; private set; }
        public bool IsBodyHtml { get; private set; }
    }
}
