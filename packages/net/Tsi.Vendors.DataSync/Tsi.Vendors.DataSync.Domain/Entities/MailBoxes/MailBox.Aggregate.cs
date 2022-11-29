using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Entities.MailBoxes.Events;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Entities.MailBoxes
{
    public partial class MailBox : BaseEntity<string>, IAggregateRoot
    {
        public MailBox()
        {
        }

        public MailBox(
            int userId, 
            string id, 
            MailBoxProvider provider, 
            string displayName, 
            bool isActive,
            string? accessToken = null, 
            string? refreshToken = null, 
            DateTimeOffset? createdDate = null,
            DateTimeOffset? lastModifiedDate = null)
        {   
            Update(userId, id, provider, displayName, isActive, accessToken, refreshToken, createdDate);
        }

        public void Activate()
        {
            if (IsActive == true)
            {
                return;
            }

            IsActive = true;

            DomainEvents.Raise(new MailBoxActivated(this));
        }

        public void DeActivate()
        {
            if (IsActive == false)
            {
                return;
            }

            IsActive = false;

            DomainEvents.Raise(new MailBoxDeActivated(this));
        }

        public void Authenticate(string accessToken, string? refreshToken = null)
        {
            Guard.Against.NullOrWhiteSpace(accessToken, "accessToken");
            AccessToken = accessToken;
            RefreshToken = refreshToken;

            DomainEvents.Raise(new MailBoxAuthenticated(this, DateTimeOffset.UtcNow));
        }

        public void Update(int userId, string id, MailBoxProvider provider, string displayName, bool isActive, string? accessToken = null, string? refreshToken = null, DateTimeOffset? createdDate = null)
        {
            Guard.Against.ZeroOrLess(userId, "userId");
            Guard.Against.NullOrWhiteSpace(id, "id");
            Guard.Against.InvalidEmail(id, "id");
            Guard.Against.MaxLength(id, 250, "id");
            Guard.Against.NullOrWhiteSpace(displayName, "displayName");

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                Guard.Against.MaxLength(accessToken, 250, "accessToken");
            }

            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                Guard.Against.MaxLength(refreshToken, 250, "refreshToken");
            }

            Id = id;
            UserId = userId;
            Provider = provider;
            DisplayName = displayName;
            IsActive = isActive;
            AccessToken = accessToken;
            CreatedDate = createdDate ?? DateTimeOffset.UtcNow;
        }
        
        public static MailBox Create(int userId, string emailAddress, MailBoxProvider provider, string displayName, string? accessToken = null, string? refreshToken = null, DateTimeOffset? createdDate = null)
        {
            var mailBox = new MailBox(userId, emailAddress, provider, displayName, true, accessToken, refreshToken, createdDate);
            
            DomainEvents.Raise(new MailboxCreated
            {
                MailBox = mailBox
            });

            return mailBox;
        }
    }
}
