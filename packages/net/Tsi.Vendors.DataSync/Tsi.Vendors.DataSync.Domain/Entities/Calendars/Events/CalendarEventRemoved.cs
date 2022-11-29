using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.Calendars.Events
{
    public class CalendarEventRemoved : BaseDomainEvent
    {
        public CalendarEventRemoved(CalendarEvent calendarEvent, string? reason = "Requested")
        {
            Guard.Against.Null(calendarEvent, "calendarEvent");
            Guard.Against.Null(calendarEvent.Id, "calendarEvent.Id");
            Guard.Against.Null(calendarEvent.CalendarId, "calendarEvent.CalendarId");
            Guard.Against.Null(calendarEvent.StartDate, "calendarEvent.StartDate");
            Guard.Against.Null(calendarEvent.Duration, "calendarEvent.Duration");

            CalendarEvent = calendarEvent;
            Reason = reason;
        }
        public CalendarEvent CalendarEvent { get; private set; }

        public string? Reason { get; private set; }

        public override void Flatten()
        {
            Args.Add("CalendarEventId", CalendarEvent.Id!);
            Args.Add("CalendarId", CalendarEvent.CalendarId!);
            Args.Add("Reason", Reason ?? "Requested");
            Args.Add("StartDate", CalendarEvent.StartDate.UtcTicks);
            Args.Add("Duration", CalendarEvent.Duration.Ticks);

        }
    }
}
