using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.MailBoxes.Events
{
    public class MailBoxActivated : BaseDomainEvent
    {
        public MailBoxActivated(MailBox mailBox) : this(mailBox, DateTimeOffset.UtcNow)
        {
        }

        public MailBoxActivated(MailBox mailBox, DateTimeOffset dateActivated)
        {
            Guard.Against.Null(mailBox, "mailBox");
            Guard.Against.NullOrWhiteSpace(mailBox.Id, "mailBox.Id");

            Guard.Against.Null(dateActivated, "dateActivated");
            Guard.Against.InvalidInput(dateActivated, "dateActivated", (dt) => dt.UtcDateTime >= mailBox.CreatedDate.UtcDateTime);
            
            MailBox = mailBox;
            DateActivated = dateActivated;
        }

        public MailBox MailBox { get; private set; }

        public DateTimeOffset DateActivated { get; private set; }

        public override void Flatten()
        {
            Args.Add("MailBoxId", MailBox.Id!);
            Args.Add("UserId", MailBox.UserId);
            Args.Add("DateActivated", DateActivated.UtcTicks);
        }
    }
}
