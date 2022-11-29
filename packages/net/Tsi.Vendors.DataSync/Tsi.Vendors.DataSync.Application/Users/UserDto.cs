using Tsi.Vendors.DataSync.Application.Calendars;
using Tsi.Vendors.DataSync.Application.MailBoxes;

namespace Tsi.Vendors.DataSync.Application.Users
{
    public class UserDto
    {
        public int? Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<MailBoxDto>? MailBoxes { get; set; }
        public List<CalendarDto>? Calendars { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }
}
