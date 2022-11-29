using System.Globalization;
using Newtonsoft.Json;
using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.Calendars.Events
{
    public class CalendarEventSynchronized : BaseDomainEvent
    {
        public CalendarEventSynchronized(
            DateTimeOffset syncDate,
            CalendarEvent calendarEvent, 
            CalendarEvent? calendarEventOriginal = null)
        {
            Guard.Against.Null(calendarEvent, "calendarEvent");
            Guard.Against.OutofSQLDateRange(syncDate.UtcDateTime, "syncDate");
            Guard.Against.InvalidInput(syncDate, "syncDate", x => x > calendarEvent.CreatedDate);
            
            SyncDate = syncDate;
            CalendarEvent = calendarEvent;
            CalendarEventOriginal = calendarEventOriginal;
        }

        public DateTimeOffset SyncDate { get; private set; }
        public CalendarEvent CalendarEvent { get; private set; }
        public CalendarEvent? CalendarEventOriginal { get; private set; }

        public override void Flatten()
        {
            Args.Add("CalendarEventId", CalendarEvent.Id!);
            Args.Add("SyncDate", SyncDate.UtcDateTime);
            Args.Add("CurrentValue", JsonConvert.SerializeObject(CalendarEvent));
            Args.Add("OriginalValue", CalendarEventOriginal != null ? JsonConvert.SerializeObject(CalendarEventOriginal) : "{}");
        }
    }
}
