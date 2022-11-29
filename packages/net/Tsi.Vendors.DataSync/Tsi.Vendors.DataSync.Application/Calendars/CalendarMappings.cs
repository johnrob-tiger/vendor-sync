using Tsi.Vendors.DataSync.Application.Users;
using Tsi.Vendors.DataSync.Domain.Entities.Calendars;

namespace Tsi.Vendors.DataSync.Application.Calendars
{
    internal static class CalendarMappings
    {
        internal static CalendarDto ToCalendarDto(this Calendar calendar)
        {
            return new CalendarDto
            {
                Id = calendar.Id!,
                UserId = calendar.UserId,
                User = calendar.User?.ToUserDto(),
                Title = calendar.Title,
                Description = calendar.Description,
                TimeZone = calendar.TimeZone,
                CreatedDate = calendar.CreatedDate,
                LastModifiedDate = calendar.LastModifiedDate,
                CalendarEvents = calendar.CalendarEvents?.ToList().ToCalendarEventDtos()
            };
        }

        internal static Calendar ToCalendar(this CalendarDto calendarDto)
        {
            var user = calendarDto.User?.ToUser();
            var calendarEvents = 
                calendarDto.CalendarEvents?.ToList().ToCalendarEvents();

            return new Calendar(
                calendarDto.Id, 
                calendarDto.TimeZone, 
                calendarDto.Title, 
                calendarDto.Description,
                calendarDto.CreatedDate, 
                calendarDto.LastModifiedDate, 
                user?.Id,
                user,
                calendarEvents);
        }


        internal static List<CalendarDto> ToCalendarDtos(this List<Calendar> calendars)
        {
            return calendars?.Select(ToCalendarDto)?.ToList() ?? new List<CalendarDto>();
        }

        internal static List<Calendar> ToCalendars(this List<CalendarDto> calendarDtos)
        {
            return calendarDtos?.Select(ToCalendar)?.ToList() ?? new List<Calendar>();
        }

        internal static CalendarEventDto ToCalendarEventDto(this CalendarEvent calendarEvent)
        {
            return new CalendarEventDto
            {
                Id = calendarEvent.Id!,
                Body = calendarEvent.Body,
                IsBodyHtml = calendarEvent.IsBodyHtml,
                CalendarId = calendarEvent.CalendarId!,
                StartDate = calendarEvent.StartDate,
                Duration = calendarEvent.Duration,
                IsReminderOn = calendarEvent.IsReminderOn,
                ReminderMinutesBefore = calendarEvent.ReminderMinutesBefore,
                OwnerId = calendarEvent.OwnerId,
                Title = calendarEvent.Title,
                CreatedDate = calendarEvent.CreatedDate,
                LastModifiedDate = calendarEvent.LastModifiedDate
            };

        }

        internal static CalendarEvent ToCalendarEvent(this CalendarEventDto calendarEventDto)
        {

            return new CalendarEvent(
                calendarEventDto.CalendarId!.Value,
                calendarEventDto.Title,
                calendarEventDto.StartDate,
                calendarEventDto.Duration,
                calendarEventDto.IsAllDay,
                calendarEventDto.IsReminderOn,
                calendarEventDto.ReminderMinutesBefore,
                calendarEventDto.Body,
                calendarEventDto.IsBodyHtml,
                calendarEventDto.IsReadOnly,
                calendarEventDto.CreatedDate,
                calendarEventDto.LastModifiedDate);
        }
        
        internal static List<CalendarEventDto> ToCalendarEventDtos(this List<CalendarEvent> calendarEvents)
        {

            return calendarEvents?.Select(ToCalendarEventDto)?.ToList() ?? new List<CalendarEventDto>();
        }

        internal static List<CalendarEvent> ToCalendarEvents(this List<CalendarEventDto> calendarEventDtos)
        {
            return calendarEventDtos?.Select(ToCalendarEvent)?.ToList() ?? new List<CalendarEvent>();
        }
    }
}
