using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.Users.Events
{
    public class UserCreated : BaseDomainEvent
    {
        public UserCreated(User user)
        {
            Guard.Against.Null(user, "user");
            Guard.Against.NullOrWhiteSpace(user.UserName, "user.UserName");

            User = user;
        }

        public User User { get; private set; }

        public override void Flatten()
        {
            Args.Add("UserId", User.Id ?? 0);
            Args.Add("UserName", User.UserName);
        }
    }
}
