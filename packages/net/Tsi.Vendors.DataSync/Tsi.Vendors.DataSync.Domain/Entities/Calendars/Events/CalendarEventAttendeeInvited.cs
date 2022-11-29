using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Entities.Attendees;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.Calendars.Events
{
    public class CalendarEventAttendeeInvited : BaseDomainEvent
    {
        public CalendarEventAttendeeInvited(Attendee attendee, CalendarEvent calendarEvent)
        {
            Guard.Against.Null(attendee, "attendee");
            Guard.Against.Null(calendarEvent, "calendarEvent");
           
            Attendee = attendee;
            CalendarEvent = calendarEvent;
        }
        public Attendee Attendee { get; private set; }
        public CalendarEvent CalendarEvent { get; private set; }

        public override void Flatten()
        {
            Args.Add("AttendeeEmail", Attendee.EmailAddress);
            Args.Add("AttendeeDisplayName", Attendee.DisplayName ?? Attendee.EmailAddress);
            Args.Add("CalendarEventId", CalendarEvent.Id!);
            Args.Add("EventStartDate", CalendarEvent.StartDate!);
        }
    }
}
