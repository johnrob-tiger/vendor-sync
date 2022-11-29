using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.MailBoxes.Events
{
    public class MailBoxDeActivated : BaseDomainEvent
    {
        public MailBoxDeActivated(MailBox mailBox) : this(mailBox, DateTimeOffset.UtcNow)
        {
        }

        public MailBoxDeActivated(MailBox mailBox, DateTimeOffset dateDeActivated)
        {
            Guard.Against.Null(mailBox, "mailBox");
            Guard.Against.NullOrWhiteSpace(mailBox.Id, "mailBox.Id");

            Guard.Against.Null(dateDeActivated, "dateDeActivated");
            Guard.Against.InvalidInput(dateDeActivated, "dateDeActivated", (dt) => dt.UtcDateTime >= mailBox.CreatedDate.UtcDateTime);

            MailBox = mailBox;
            DateDeActivated = dateDeActivated;
        }

        public MailBox MailBox { get; private set; }

        public DateTimeOffset DateDeActivated { get; private set; }

        public override void Flatten()
        {
            Args.Add("MailBoxId", MailBox.Id!);
            Args.Add("UserId", MailBox.UserId);
            Args.Add("DateDeActivated", DateDeActivated.UtcTicks);
        }
    }
}
