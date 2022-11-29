using Tsi.Vendors.DataSync.Domain.Entities.Calendars;
using Tsi.Vendors.DataSync.Domain.Entities.Calendars.Specs;
using Tsi.Vendors.DataSync.Domain.Interfaces;

namespace Tsi.Vendors.DataSync.Application.Calendars
{
    public class CalendarEventService : ICalendarEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalendarEventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CalendarEventDto>> ListCalendarEventsAsync(Guid calendarId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var calendarEventRepository = _unitOfWork.Repository<CalendarEvent>();

            var spec = new CalendarEventDateRangeSpec(
                calendarId.ToString(), 
                startDate, 
                endDate);

            var calendarEvents = 
                await calendarEventRepository.ListAsync(spec.SpecExpression);

            return calendarEvents.ToCalendarEventDtos();
        }

        public async Task<CalendarEventDto> CreateEvent(CreateCalendarEventRequest request)
        {

            var calendarEventRepository = _unitOfWork.Repository<CalendarEvent>();

            var createdDate = DateTimeOffset.UtcNow;

            var calendarEventDispatch = CalendarEvent.Create(
                request.CalendarId,
                request.Title,
                request.StartDate,
                request.Duration,
                request.IsAllDay,
                request.IsReminderOn,
                request.ReminderMinutesBefore,
                request.Body,
                request.IsBodyHtml,
                request.IsReadOnly,
                createdDate,
                createdDate);
            
            var newEvent = await calendarEventRepository.AddAsync(
                calendarEventDispatch.Model);
         
            calendarEventDispatch.Dispatch(newEvent);

            return calendarEventDispatch.Model.ToCalendarEventDto();
        }
    }
}
