using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.Calendars.Events
{
    public class CalendarEventReminderAdded : BaseDomainEvent
    {
        public CalendarEventReminderAdded(CalendarEvent calendarEvent)
        {
            Guard.Against.Null(calendarEvent, "calendarEvent");
            Guard.Against.Null(calendarEvent.StartDate, "calendarEvent.StartDate");
            Guard.Against.Null(calendarEvent.ReminderMinutesBefore, "calendarEvent.ReminderMinutesBefore");

            CalendarEvent = calendarEvent;
        }

        public CalendarEvent CalendarEvent { get; private set; }
        
        public override void Flatten()
        {
            Args.Add("CalendarId", CalendarEvent.CalendarId!);
            Args.Add("CalendarEventId", CalendarEvent.Id!);
            Args.Add("EventStartDate", CalendarEvent.StartDate);
            Args.Add("ReminderMinutesBefore", CalendarEvent.ReminderMinutesBefore!);
        }
    }
}
