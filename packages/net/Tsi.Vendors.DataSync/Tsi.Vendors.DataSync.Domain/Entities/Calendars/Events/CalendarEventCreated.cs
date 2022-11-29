using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.Calendars.Events
{
    public class CalendarEventCreated : BaseDomainEvent
    {
        public CalendarEventCreated(CalendarEvent calendarEvent)
        {
            CalendarEvent = calendarEvent;
        }

        public CalendarEvent CalendarEvent { get; set; }

        public override void Flatten()
        {
            Guard.Against.Null(CalendarEvent.Id, "Id");
            Guard.Against.Null(CalendarEvent.CalendarId, "CalendarId");
            Guard.Against.Null(CalendarEvent.StartDate, "StartDate");
            Guard.Against.Null(CalendarEvent.Duration, "Duration");

            Args.Add("CalendarEventId", CalendarEvent.Id);
            Args.Add("CalendarId", CalendarEvent.CalendarId);
            Args.Add("StartDate", CalendarEvent.StartDate.UtcTicks);
            Args.Add("Duration", CalendarEvent.Duration.Ticks);
        }
    }
}
