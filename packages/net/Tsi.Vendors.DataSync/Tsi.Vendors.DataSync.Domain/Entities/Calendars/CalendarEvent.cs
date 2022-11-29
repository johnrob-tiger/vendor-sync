using System.ComponentModel.DataAnnotations;
using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Entities.Attendees;
using Tsi.Vendors.DataSync.Domain.Entities.Users;

namespace Tsi.Vendors.DataSync.Domain.Entities.Calendars
{
    public partial class CalendarEvent : BaseEntity<string>, IAggregateRoot
    {
        [Required(), StringLength(250)]
        public string Title { get; internal set; } = "";

        [StringLength(4000)]
        public string? Body { get; internal set; }

        public bool IsBodyHtml { get; internal set; } = false;

        public DateTimeOffset StartDate { get; internal set; }
        
        public TimeSpan Duration { get; internal set; }

        public bool IsAllDay { get; internal set; }

        public bool IsReadOnly { get; internal set; }

        public bool IsReminderOn { get; internal set; } = false;

        public int? ReminderMinutesBefore { get; internal set; }

        public Guid? CalendarId { get; internal set; }

        public virtual Calendar? Calendar { get; internal set; }

        public int? OwnerId { get; internal set; }

        public virtual User? Owner { get; internal set; }

        public ICollection<Attendee>? Attendees { get; internal set; }

    }
}
