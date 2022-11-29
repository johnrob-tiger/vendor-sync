using Tsi.Vendors.DataSync.Domain.Entities.Calendars;

namespace Tsi.Vendors.DataSync.Data.Repositories
{
    public class StubCalendarEventRepository : InMemoryRepository<CalendarEvent, string>, ICalendarEventRepository
    {

        public StubCalendarEventRepository()
        {
        }

        public StubCalendarEventRepository(IEnumerable<CalendarEvent> calendarEvents)
        {
            Entities.AddRange(calendarEvents);
        }

        protected override Func<CalendarEvent, string> IdResolver
        {
            get
            {
                return (calendarEvent) => calendarEvent.Id ?? string.Empty;
            }
        }

        protected override CalendarEvent AssignId(CalendarEvent obj)
        {
            obj.Id = Guid.NewGuid().ToString();
            return obj;
        }
    }
}
