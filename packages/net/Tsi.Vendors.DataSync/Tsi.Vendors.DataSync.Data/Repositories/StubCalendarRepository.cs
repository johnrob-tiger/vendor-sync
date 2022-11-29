using Tsi.Vendors.DataSync.Domain.Entities.Calendars;

namespace Tsi.Vendors.DataSync.Data.Repositories
{
    public class StubCalendarRepository : InMemoryRepository<Calendar, Guid>, ICalendarRepository
    {

        public StubCalendarRepository()
        {
        }

        public StubCalendarRepository(IEnumerable<Calendar> calendars)
        {
            Entities.AddRange(calendars);
        }

        protected override Func<Calendar, Guid> IdResolver
        {
            get
            {
                return (calendar) => calendar.Id;
            }
        }

        protected override Calendar AssignId(Calendar obj)
        {
            obj.Id = Guid.NewGuid();
            return obj;
        }
    }
}
