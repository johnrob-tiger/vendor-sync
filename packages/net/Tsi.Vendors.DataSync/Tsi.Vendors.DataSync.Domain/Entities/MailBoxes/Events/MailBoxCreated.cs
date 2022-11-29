using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.MailBoxes.Events
{
    public class MailboxCreated : BaseDomainEvent
    {
        public MailBox MailBox { get; set; } = null!;

        public override void Flatten()
        {
            Guard.Against.Null(MailBox, "MailBox");

            Guard.Against.ZeroOrLess(MailBox.UserId, "MailBox.UserId");
            Guard.Against.NullOrWhiteSpace(MailBox.Id, "MailBox.Id");
            Guard.Against.InvalidEmail(MailBox.Id, "MailBox.Id");

            Guard.Against.EnumOutOfRange<MailBoxProvider>(MailBox.Provider, "MailBox.Provider");
            
            Args.Add("MailBoxUserId", MailBox.UserId);
            Args.Add("MailBoxId", MailBox.Id);
            Args.Add("MailBoxDisplayName", MailBox.DisplayName);
            Args.Add("MailBoxProvider", MailBox.Provider);
        }
    }
}
