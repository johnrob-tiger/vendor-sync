using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.MailBoxes.Events
{
    public class MailBoxAuthenticated : BaseDomainEvent
    {

        public MailBoxAuthenticated(MailBox mailBox, DateTimeOffset? dateAuthenticated = null)
        {
            Guard.Against.Null(mailBox, "mailBox");
            Guard.Against.ZeroOrLess(mailBox.UserId, "mailBox.UserId");
            Guard.Against.NullOrWhiteSpace(mailBox.Id, "mailBox.Id");
            Guard.Against.NullOrWhiteSpace(mailBox.AccessToken, "mailBox.AccessToken");

            MailBox = mailBox;
            DateAuthenticated = dateAuthenticated ?? DateTimeOffset.UtcNow;
        }

        public MailBox MailBox { get; private set; }
        public DateTimeOffset DateAuthenticated { get; private set; }

        public override void Flatten()
        {
            Guard.Against.NullOrWhiteSpace(MailBox.Id, "MailBox.Id");

            Args.Add("MailBoxUserId", MailBox.UserId);
            Args.Add("MailBoxId", MailBox.Id);
            Args.Add("Provider", MailBox.Provider);
            Args.Add("AccessToken", MailBox.AccessToken!);
            Args.Add("RefreshToken", MailBox.RefreshToken ?? "");
            Args.Add("DateAuthenticate", DateAuthenticated);
        }
    }
}
