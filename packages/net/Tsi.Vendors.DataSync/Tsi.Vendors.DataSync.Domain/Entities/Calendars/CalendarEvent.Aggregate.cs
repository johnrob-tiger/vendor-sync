using System.Globalization;
using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Entities.Attendees;
using Tsi.Vendors.DataSync.Domain.Entities.Calendars.Events;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.Calendars
{
    public partial class CalendarEvent
    {
        public CalendarEvent()
        {
        }

        public CalendarEvent(
            Guid calendarId,
            string title,
            DateTimeOffset startDate,
            TimeSpan duration,
            bool isAllDay = false,
            bool isReminderOn = false,
            int? reminderMinutesBefore = null,
            string? body = null,
            bool isBodyHtml = false,
            bool isReadOnly = false,
            DateTimeOffset? createdDate = null,
            DateTimeOffset? lastModifiedDate = null) : this()
        {
            this.Update(
                calendarId, 
                title, 
                startDate, 
                duration, 
                isAllDay,
                isReminderOn, 
                reminderMinutesBefore, 
                body, 
                isBodyHtml,
                isReadOnly,
                createdDate,
                lastModifiedDate);
        }

        public CalendarEvent(
            Calendar calendar,
            string title,
            DateTimeOffset startDate,
            TimeSpan duration,
            bool isAllDay = false,
            bool isReminderOn = false,
            int? reminderMinutesBefore = null,
            string? body = null,
            bool isBodyHtml = false,
            bool isReadOnly = false,
            DateTimeOffset? createdDate = null,
            DateTimeOffset? lastModifiedDate = null
        )
        {
            Guard.Against.Null(calendar, "calendar");
            
            Update(calendar.Id, title, startDate, duration, isAllDay, isReminderOn, reminderMinutesBefore, body, isBodyHtml, isReadOnly, createdDate, lastModifiedDate, calendar);
        }

        public static CalendarEvent Clone(CalendarEvent @event)
        {
            if (@event.Calendar != null)
            {
                return new CalendarEvent(
                    @event.Calendar,
                    @event.Title,
                    @event.StartDate,
                    @event.Duration,
                    @event.IsAllDay,
                    @event.IsReminderOn,
                    @event.ReminderMinutesBefore,
                    @event.Body,
                    @event.IsBodyHtml,
                    @event.IsReadOnly,
                    @event.CreatedDate,
                    @event.LastModifiedDate);
            }

            Guard.Against.Null(@event.CalendarId, "event.CalendarId");

            return new CalendarEvent(
                @event.CalendarId.Value,
                @event.Title,
                @event.StartDate,
                @event.Duration,
                @event.IsAllDay,
                @event.IsReminderOn,
                @event.ReminderMinutesBefore,
                @event.Body,
                @event.IsBodyHtml,
                @event.IsReadOnly,
                @event.CreatedDate,
                @event.LastModifiedDate);

        }

        public void SetReminderOn(int reminderMinutesBefore)
        {
            if (StartDate.UtcDateTime <= DateTime.UtcNow)
            {
                // Already past. Throw ???
                return;
            }

            Guard.Against.Null(Id, "Id");
            Guard.Against.Null(CalendarId, "CalendarId");
            Guard.Against.ZeroOrLess(reminderMinutesBefore, "@reminderMinutesBefore");
            
            ReminderMinutesBefore = reminderMinutesBefore;
            IsReminderOn = true;

            var domainEvent = new CalendarEventReminderAdded(this);

            DomainEvents.Raise(domainEvent);
        }

        public void AddAttendees(IList<Attendee> attendees)
        {
            Guard.Against.Null(attendees, "attendees");

            foreach (var attendee in attendees)
            {
                AddAttendee(attendee);
            }
        }

        public void AddAttendee(Attendee attendee)
        {
            Guard.Against.Null(attendee, "attendee");
            Guard.Against.NullOrWhiteSpace(attendee.EmailAddress, "attendee.EmailAddress");

            Attendees ??= new List<Attendee>();

            if (Attendees.Any(x => x.EmailAddress == attendee.EmailAddress))
            {
                throw new InvalidOperationException($"Attendee {attendee.EmailAddress} has already been added to the event: {Id}");
            }

            Attendees.Add(attendee);
        }

        public void InviteAttendee(Attendee attendee)
        {
            Guard.Against.NullOrWhiteSpace(Id, "Id", "Cannot invite attendee. Save calendar event first.");
            
            AddAttendee(attendee);

            DomainEvents.Raise(new CalendarEventAttendeeInvited(attendee, this));
        }

        public void AcceptInvitation(Attendee attendee)
        {
            Guard.Against.NullOrWhiteSpace(Id, "Id", "Cannot accept invitation. Save calendar event first.");
            Guard.Against.Null(attendee, "attendee");

            var found = this.Attendees?.FirstOrDefault(x => x.EmailAddress == attendee.EmailAddress);

            if (found != null)
            {
                found.Accepted = true;
            }

            DomainEvents.Raise(new CalendarEventInvitationAccepted(attendee, this));
        }

        public void DeclineInvitation(Attendee attendee)
        {
            Guard.Against.NullOrWhiteSpace(Id, "Id", "Cannot decline invitation. Save calendar event first.");
            Guard.Against.Null(attendee, "attendee");

            var found = this.Attendees?.FirstOrDefault(x => x.EmailAddress == attendee.EmailAddress);

            if (found != null)
            {
                found.Accepted = false;
            }

            DomainEvents.Raise(new CalendarEventInvitationDeclined(attendee, this));
        }

        public void SetReminderOff()
        {
            if (StartDate.UtcDateTime <= DateTime.UtcNow)
            {
                // Already past. Throw ???
                IsReminderOn = false;
                return;
            }

            GuardAgainstInvalidCalendarId(CalendarId);

            Guard.Against.NullOrWhiteSpace(Id, "Id");
            Guard.Against.Null(CalendarId, "CalendarId");
            Guard.Against.Null(StartDate, "StartDate");
            Guard.Against.OutofSQLDateRange(StartDate.UtcDateTime, "StartDate");
            
            IsReminderOn = false;

            var domainEvent = 
                new CalendarEventReminderRemoved(this);

            DomainEvents.Raise(domainEvent);
        }

        public void Reschedule(DateTimeOffset rescheduleDate, TimeSpan duration)
        {
            GuardAgainstInvalidCalendarId(CalendarId);

            Guard.Against.NullOrWhiteSpace(Id, "Id");
            Guard.Against.Null(rescheduleDate, "rescheduleDate");
            Guard.Against.OutofSQLDateRange(rescheduleDate.UtcDateTime, "rescheduleDate");
            Guard.Against.Null(duration, "duration");
            Guard.Against.ZeroOrLess(duration.Ticks, "duration");

            StartDate = rescheduleDate;
            Duration = duration;

            DomainEvents.Raise(new CalendarEventRescheduled(this));
        }

        public void SetBody(string body, bool isHtml = false)
        {
            Guard.Against.Null(CalendarId, "CalendarId", "Cannot set body. Invalid calendar id.");
            Guard.Against.NullOrWhiteSpace(Id, "Id", "Cannot set body on unsaved calendar event. Save it first.");

            Guard.Against.NullOrWhiteSpace(body, "body");
            Guard.Against.MaxLength(body, 4000, "body", $"Body must be 4000 characters or less. Got: {body.Length}");

            // Do some stripping / parsing here ??

            Body = body;
            IsBodyHtml = isHtml;

            DomainEvents.Raise(
                new CalendarEventBodyUpdated(
                    CalendarId.Value, 
                    Id, 
                    Body, IsBodyHtml));
        }
        
        public void Synchronize(CalendarEvent @event, DateTimeOffset? syncDate = null)
        {
            Guard.Against.Null(@event, "event", "Cannot synchronize a null event.");
            
            if (@event.LastModifiedDate.UtcDateTime < LastModifiedDate.UtcDateTime)
            {
                // Nothing to synchronize - stale data
                return;
            }

            Guard.Against.NullOrWhiteSpace(@event.Title, "event.Title");
            if (!string.IsNullOrWhiteSpace(@event.Body))
            {
                Guard.Against.MaxLength(@event.Body, 4000, "event.Body");
            }

            var clone = CalendarEvent.Clone(this);


            LastModifiedDate = @event.LastModifiedDate;
            Title = @event.Title;
            Body = @event.Body;
            IsBodyHtml = @event.IsBodyHtml;
            CalendarId = @event.CalendarId;
            Calendar = @event.Calendar;
            Attendees = @event.Attendees;
            StartDate = @event.StartDate;
            Duration = @event.Duration;
            IsAllDay = @event.IsAllDay;
            IsReminderOn = @event.IsReminderOn;
            ReminderMinutesBefore = @event.ReminderMinutesBefore;
            OwnerId = @event.OwnerId;
            Owner = @event.Owner;

            DomainEvents.Raise(new CalendarEventSynchronized(syncDate ?? DateTimeOffset.UtcNow, this, clone));
        }

        public void Update(
            Guid calendarId,
            string title,
            DateTimeOffset startDate,
            TimeSpan duration,
            bool isAllDay = false,
            bool isReminderOn = false,
            int? reminderMinutesBefore = null,
            string? body = null,
            bool isBodyHtml = false,
            bool isReadOnly = false,
            DateTimeOffset? createdDate = null,
            DateTimeOffset? lastModifiedDate = null,
            Calendar? calendar = null)
        {
            GuardAgainstInvalidCalendarId(calendarId);

            Guard.Against.NullOrWhiteSpace(title, "title");
            Guard.Against.OutOfRange(title, "title", 3, 250);

            Guard.Against.Null(startDate, "startDate");

            Guard.Against.OutofSQLDateRange(startDate.DateTime, "startDate");

            Guard.Against.Null(duration, "duration");

            if (!string.IsNullOrEmpty(body))
            {
                Guard.Against.MaxLength(body, 4000, "body", "Body must be 4000 characters or less.");
            }

            if (isReminderOn && reminderMinutesBefore != null)
            {
                Guard.Against.ZeroOrLess(reminderMinutesBefore.Value, "reminderMinutesBefore");

                IsReminderOn = true;
                ReminderMinutesBefore = reminderMinutesBefore;
            }

            if (createdDate != null)
            {
                Guard.Against.OutofSQLDateRange(createdDate.Value.UtcDateTime, "createdDate");
            }

            if (lastModifiedDate != null)
            {
                Guard.Against.OutofSQLDateRange(lastModifiedDate.Value.UtcDateTime, "lastModifiedDate");
            }

            if (calendar != null)
            {
                Guard.Against.InvalidInput(calendar, "calendar.Id", x => calendar.Id == calendarId, 
                    "Calendar reference and calendar id's are not the same.");
                
            }

            // strip / parse body here ?

            CalendarId = calendarId;
            Title = title;
            StartDate = startDate;
            Duration = duration;
            IsAllDay = isAllDay;
            Body = body;
            IsBodyHtml = isBodyHtml;
            IsReadOnly = isReadOnly;
            CreatedDate = createdDate ?? DateTimeOffset.UtcNow;
            LastModifiedDate = lastModifiedDate ?? DateTimeOffset.UtcNow;
            Calendar = calendar;
        }

        public static DispatchEvent<CalendarEvent, CalendarEventCreated> Create(
            Guid calendarId, 
            string title, 
            DateTimeOffset startDate, 
            TimeSpan duration,
            bool isAllDay,
            bool isReminderOn = false,
            int? reminderMinutesBefore = null,
            string? body = null,
            bool isBodyHtml = false,
            bool isReadOnly = false,
            DateTimeOffset? createdDate = null, 
            DateTimeOffset? lastModifiedDate = null)
        {
            var calendarEvent = new CalendarEvent(calendarId, title, startDate, duration, isAllDay, isReminderOn, reminderMinutesBefore, body, isBodyHtml, isReadOnly, createdDate, lastModifiedDate);

            return new DispatchEvent<CalendarEvent, CalendarEventCreated>(
                calendarEvent, new CalendarEventCreated(calendarEvent), (m, e) =>
                {
                    e.CalendarEvent = m;
                });
        }

        private static void GuardAgainstInvalidCalendarId(Guid? calendarId)
        {
            Guard.Against.Null(calendarId, "calendarId");
            Guard.Against.InvalidInput(calendarId, "calendarId", (x) => x != Guid.Empty);
        }
    }
}
