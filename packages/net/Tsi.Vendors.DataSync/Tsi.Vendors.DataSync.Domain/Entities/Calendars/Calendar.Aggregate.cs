using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Entities.Calendars.Events;
using Tsi.Vendors.DataSync.Domain.Entities.Users;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.Calendars
{
    public partial class Calendar : BaseEntity<Guid>, IAggregateRoot
    {
        public Calendar()
        {
        }

        public Calendar(
            Guid calendarId, 
            string timeZone, 
            string title, 
            string? description = null, 
            DateTimeOffset ? createdDate = null, 
            DateTimeOffset? lastModifiedDate = null,
            int? userId = null,
            User? user = null,
            List<CalendarEvent>? calendarEvents = null)
        {
            Update(calendarId, timeZone, title,  description, createdDate, lastModifiedDate, userId, user, calendarEvents);
        }

        public void AddCalendarEvents(List<CalendarEvent> events)
        {
            Guard.Against.NullOrEmpty(events, "events", "Events cannot be null or empty.");

            events.ForEach(AddCalendarEvent);
        }

        public void AddCalendarEvent(CalendarEvent @event)
        {
            Guard.Against.Null(@event, "event");

            CalendarEvents ??= new List<CalendarEvent>();

            // Check if exists already
            if (CalendarEvents.Any(x => x.Id == @event.Id))
            {
                throw new Exception($"Event {@event.Id} already exists.");
            }
            
            CalendarEvents.Add(@event);
        }

        public void Update(
            Guid id, string timeZone, string title, string? description = null, DateTimeOffset? createdDate = null, DateTimeOffset? lastModifiedDate = null,
            int? userId = null,
            User? user = null,
            List<CalendarEvent>? calendarEvents = null)
        {
            Guard.Against.Null(id, "id");
            Guard.Against.NullOrWhiteSpace(timeZone, "timeZone");
            Guard.Against.NullOrWhiteSpace(title, "title");
            Guard.Against.OutOfRange(title,"title", 3, 255);

            if (!string.IsNullOrWhiteSpace(description))
            {
                Guard.Against.MaxLength(description, 255, "description");
            }

            if (createdDate != null)
            {
                Guard.Against.OutofSQLDateRange(createdDate.Value.UtcDateTime, "createdDate");
            }

            if (lastModifiedDate != null)
            {
                Guard.Against.OutofSQLDateRange(lastModifiedDate.Value.UtcDateTime, "lastModifiedDate");
            }

            if (calendarEvents != null)
            {
                if (calendarEvents.Any(x => x.CalendarId != id))
                {
                    throw new ArgumentException($"Calendar event ids are mismatched.", nameof(calendarEvents));
                }
            }
            
            Id = id;
            TimeZone = timeZone;
            Title = title;
            Description = description;
            CreatedDate = createdDate ?? DateTimeOffset.UtcNow;
            LastModifiedDate = lastModifiedDate ?? createdDate ?? DateTimeOffset.Now;
            User = user;
            UserId = userId;
            CalendarEvents = calendarEvents;
        }
        
        public void RemoveEvent(CalendarEvent @event, string? reason = null)
        {
            Guard.Against.Null(@event, "event");
            Guard.Against.NullOrWhiteSpace(@event.Id, "event.Id");

            var found = CalendarEvents?.FirstOrDefault(x => x.Id == @event.Id);

            if (found != null)
            {
                CalendarEvents?.Remove(found);
            }

            DomainEvents.Raise(new CalendarEventRemoved(@event, reason));
        }

        public static Calendar Create(
            string timeZone,
            string title,
            string? description = null,
            int? userId = null,
            DateTimeOffset? createdDate = null,
            DateTimeOffset? lastModifiedDate = null
        )
        {
            
            return Create(Guid.NewGuid(), timeZone, title,description, createdDate, lastModifiedDate, userId);
        }
        
        public static Calendar Create(
            Guid calendarId,
            string timeZone,
            string title,
            string? description = null,
            DateTimeOffset? createdDate = null,
            DateTimeOffset? lastModifiedDate = null,
            int? userId = null
            )
        {

            var calendar =
                new Calendar(calendarId, timeZone, title, description, createdDate, lastModifiedDate, userId);
                 

            DomainEvents.Raise(new CalendarCreated(calendar));
            
            return calendar;
        }
    }
}
