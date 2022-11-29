using System.ComponentModel.DataAnnotations;
using Tsi.Vendors.DataSync.Domain.Entities.Calendars;
using Tsi.Vendors.DataSync.Domain.Entities.MailBoxes;

namespace Tsi.Vendors.DataSync.Domain.Entities.Users
{
    public partial class User
    {
        [Required, StringLength(100)]
        public string UserName { get; internal set; } = "guest";

        [StringLength(100)]
        public string? FirstName { get; internal set; }

        [StringLength(250)]
        public string? LastName { get; internal set; }

        [StringLength(250)]
        public string? EmailAddress { get; internal set; }

        public DateTime? BirthDate { get; internal set; }

        public ICollection<Calendar>? Calendars { get; internal set; }

        public ICollection<MailBox>? MailBoxes { get; internal set; }
    }
}
