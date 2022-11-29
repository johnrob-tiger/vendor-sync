using System.Linq.Expressions;
using Tsi.Vendors.DataSync.Domain.Shared.Specification;

namespace Tsi.Vendors.DataSync.Domain.Entities.Calendars.Specs
{
    public class CalendarEventDateRangeSpec : BaseSpecification<CalendarEvent>
    {
        private readonly string _calendarId;
        private readonly DateTimeOffset _fromDate;
        private readonly DateTimeOffset _toDate;

        public CalendarEventDateRangeSpec(
            string calendarId, 
            DateTimeOffset fromDate, 
            DateTimeOffset toDate)
        {
            _calendarId = calendarId;
            _fromDate = fromDate;
            _toDate = toDate;
        }

        public override Expression<Func<CalendarEvent, bool>> SpecExpression
        {
            get
            {
                return (calendarEvent) => calendarEvent.Id != null 
                        && calendarEvent.Id.Equals(_calendarId)
                        && calendarEvent.StartDate.UtcDateTime
                            >= _fromDate.UtcDateTime
                        && calendarEvent.StartDate.UtcDateTime
                            <= _toDate.UtcDateTime;

            }
        }
    }
}
