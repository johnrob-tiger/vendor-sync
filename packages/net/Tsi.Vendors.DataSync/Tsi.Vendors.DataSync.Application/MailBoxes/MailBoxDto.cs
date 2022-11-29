using Tsi.Vendors.DataSync.Application.Users;

namespace Tsi.Vendors.DataSync.Application.MailBoxes
{
    public class MailBoxDto
    {
        public string Id { get; set; } = null!;
        public string? DisplayName { get; set; }
        public bool IsActive { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? Provider { get; set; }
        public int UserId { get; set; } = 0!;
        public UserDto? User { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }
}
