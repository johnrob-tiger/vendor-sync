using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Vendors.DataSync.Application.Users;

namespace Tsi.Vendors.DataSync.Application.Calendars
{
    public class CalendarDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
        public int? UserId { get; set; } = null!;
        public UserDto? User { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string TimeZone { get; set; } = null!;
        public IList<CalendarEventDto>? CalendarEvents { get; set; }
    }
}
