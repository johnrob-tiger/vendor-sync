using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.Calendars.Events
{
    public class CalendarCreated : BaseDomainEvent
    {
        public Calendar Calendar { get; private set; }

        public CalendarCreated(Calendar calendar)
        {
            Guard.Against.Null(calendar, "calendar");
            Guard.Against.Null(calendar.Id, "calendar.Id");
            Guard.Against.Null(calendar.Title, "calendar.Title");
            Guard.Against.NullOrWhiteSpace(calendar.TimeZone, "calendar.TimeZone");

            Calendar = calendar;
        }

        public override void Flatten()
        {
            Args.Add("CalendarId", Calendar.Id);
            Args.Add("TimeZone", Calendar.TimeZone);
            Args.Add("Title", Calendar.Title);
            Args.Add("CreatedDate", Calendar.CreatedDate);
            Args.Add("UserId", Calendar.UserId ?? -1);
        }
    }
}
