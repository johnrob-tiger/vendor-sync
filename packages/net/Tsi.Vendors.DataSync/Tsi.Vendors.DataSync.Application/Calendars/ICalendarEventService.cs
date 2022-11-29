using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Vendors.DataSync.Application.Calendars
{
    public interface ICalendarEventService
    {
        Task<List<CalendarEventDto>> ListCalendarEventsAsync(Guid calendarId, DateTimeOffset startDate, DateTimeOffset endDate);

        Task<CalendarEventDto> CreateEvent(CreateCalendarEventRequest request);
    }
}
