using Tsi.Vendors.DataSync.Domain.Entities.Users;

namespace Tsi.Vendors.DataSync.Domain.Entities.MailBoxes
{
    public partial class MailBox
    {
        public string DisplayName { get; internal set; } = null!;
        public string? AccessToken { get; internal set; } = null;

        public string? RefreshToken { get; internal set; } = null;

        public MailBoxProvider Provider { get; internal set; }

        public bool IsActive { get; internal set; }
        
        public int UserId { get; internal set; }

        public User? User { get; internal set; }
    }
}
