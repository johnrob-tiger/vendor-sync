using Tsi.Vendors.DataSync.Domain.Entities.Calendars;
using Tsi.Vendors.DataSync.Domain.Interfaces;

namespace Tsi.Vendors.DataSync.Application.Calendars
{
    public class CalendarService : ICalendarService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CalendarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CalendarDto>> ListCalendarsAsync()
        {
            var calendarRepository = _unitOfWork.Repository<Calendar>();

            var calendars = await calendarRepository.ListAsync(x =>true);

            return calendars.ToList().ToCalendarDtos() ?? new List<CalendarDto>();
        }

        public async Task<List<CalendarDto>> ListCalendarsForUserAsync(int userId)
        {
            var calendarRepository = _unitOfWork.Repository<Calendar>();

            var calendars = await calendarRepository.ListAsync(x => x.UserId == userId);

            return calendars.ToList().ToCalendarDtos() ?? new List<CalendarDto>();

        }

        public async Task<CalendarDto> CreateCalendarAsync(CreateCalendarRequest request)
        {
            var calendarRepository = _unitOfWork.Repository<Calendar>();

            var calendar = Calendar.Create(
                Guid.NewGuid(),
                request.TimeZone,
                request.Title,
                request.Description,
                DateTimeOffset.UtcNow,
                DateTimeOffset.UtcNow,
                request.UserId);

            await calendarRepository.AddAsync(calendar);

            await _unitOfWork.SaveChangesAsync();

            return calendar.ToCalendarDto();
        }
    }
}
