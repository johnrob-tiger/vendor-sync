using Tsi.Vendors.DataSync.Domain.Entities.MailBoxes;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Application.MailBoxes
{
    public class CreateMailBoxRequest
    {
        public CreateMailBoxRequest(int userId, MailBoxProvider provider, string emailAddress, string? displayName = null)
        {
            Guard.Against.ZeroOrLess(userId, "userId");
            Guard.Against.NullOrWhiteSpace(emailAddress, "emailAddress");
            Guard.Against.InvalidEmail(emailAddress, "emailAddress");
            Guard.Against.MaxLength(emailAddress, 250, "emailAddress");

            UserId = userId;
            Provider = provider;
            EmailAddress = emailAddress;
            DisplayName = displayName;
        }
        public int UserId { get; private set; }
        public MailBoxProvider Provider { get; private set; }
        public string EmailAddress { get; private set; }
        public string? DisplayName { get; private set; }
    }
}
