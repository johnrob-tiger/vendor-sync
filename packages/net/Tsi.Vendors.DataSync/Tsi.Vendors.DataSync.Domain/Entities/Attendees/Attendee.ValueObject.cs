using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.Attendees
{
    public partial class Attendee
    {
        public Attendee(string emailAddress, string? displayName = null, string? primaryPhone = null, bool? accepted = null)
        {
            Update(emailAddress, displayName, primaryPhone, accepted);
        }
        
        public void Update(string emailAddress, string? displayName = null, string? primaryPhone = null, bool? accepted = null)
        {
            Guard.Against.NullOrWhiteSpace(emailAddress, "emailAddress");
            Guard.Against.MaxLength(emailAddress, 250, "emailAddress");

            if (!string.IsNullOrWhiteSpace(displayName))
            {
                Guard.Against.OutOfRange(displayName, "displayName", 3, 150);
            }

            if (!string.IsNullOrWhiteSpace(primaryPhone))
            {
                Guard.Against.MaxLength(primaryPhone, 50, "primaryPhone");
            }

            EmailAddress = emailAddress;
            DisplayName = displayName;
            PrimaryPhone = primaryPhone;
            Accepted = accepted;
        }

        public static Attendee Create(string emailAddress, string? displayName = null, string? primaryPhone = null, bool? accepted = null)
        {
            var attendee = new Attendee(emailAddress, displayName, primaryPhone, accepted);
            
            return attendee;
        }
    }
}
