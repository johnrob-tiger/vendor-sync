namespace Tsi.Vendors.DataSync.Application.Calendars
{
    public interface ICalendarService
    {
        Task<List<CalendarDto>> ListCalendarsAsync();

        Task<List<CalendarDto>> ListCalendarsForUserAsync(int userId);

        Task<CalendarDto> CreateCalendarAsync(CreateCalendarRequest request);
    }
}
