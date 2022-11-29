using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.Calendars.Events
{
    public class CalendarEventRescheduled : BaseDomainEvent
    {
        public CalendarEventRescheduled(CalendarEvent calendarEvent)
        {
            Guard.Against.Null(calendarEvent, "calendarEvent");

            CalendarEvent = calendarEvent;
        }
        public CalendarEvent CalendarEvent { get; private set; }

        public override void Flatten()
        {
            Args.Add("CalendarId", CalendarEvent.CalendarId!);
            Args.Add("CalendarEventId", CalendarEvent.Id!);
            Args.Add("RescheduledDate", CalendarEvent.StartDate);
            Args.Add("Duration", CalendarEvent.Duration);
        }
    }
}
