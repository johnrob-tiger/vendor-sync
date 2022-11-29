using System.ComponentModel.DataAnnotations;
using Tsi.Vendors.DataSync.Domain.Entities.Users;

namespace Tsi.Vendors.DataSync.Domain.Entities.Calendars
{
    public partial class Calendar 
    {
        [Required, StringLength(150)]
        public string Title { get; internal set; } = "";

        [StringLength(250)]
        public string? Description { get; internal set; } = null;

        public string TimeZone { get; internal set; } = "";

        public int? UserId { get; internal set; }

        public User? User { get; internal set; }

        public virtual ICollection<CalendarEvent>? CalendarEvents { get; internal set; }
    }
}
