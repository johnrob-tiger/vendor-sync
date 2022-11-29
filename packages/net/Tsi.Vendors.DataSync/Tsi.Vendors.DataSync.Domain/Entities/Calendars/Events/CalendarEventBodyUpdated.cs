using Tsi.Vendors.DataSync.Domain.Base;

namespace Tsi.Vendors.DataSync.Domain.Entities.Calendars.Events
{
    public class CalendarEventBodyUpdated : BaseDomainEvent
    {
        public CalendarEventBodyUpdated(
            Guid calendarId, 
            string calendarEventId, 
            string body,
            bool isBodyHtml)
        {
            CalendarId = calendarId;
            CalendarEventId = calendarEventId;
            Body = body;
            IsBodyHtml = isBodyHtml;
        }
        public Guid CalendarId { get; private set; }
        public string CalendarEventId { get; private set; }
        public string Body { get; private set; }

        public bool IsBodyHtml { get; private set; }

        public override void Flatten()
        {
            Args.Add("CalendarId", CalendarId);
            Args.Add("CalendarEventId", CalendarEventId);
            Args.Add("Body", Body);
            Args.Add("IsBodyHtml", IsBodyHtml);
        }
    }
}
