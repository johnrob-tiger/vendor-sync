
namespace Tsi.Vendors.DataSync.Domain.Entities.Attendees
{
    public partial class Attendee
    {
        public int Id { get; set; }

        public string EmailAddress { get; set; } = null!;

        public string? DisplayName { get; set; }

        public string? PrimaryPhone { get; set; } = null!;

        public bool? Accepted { get; set; } = null;
    }
}
