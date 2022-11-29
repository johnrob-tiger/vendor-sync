using Tsi.Vendors.DataSync.Application.Users;
using Tsi.Vendors.DataSync.Domain.Entities.MailBoxes;

namespace Tsi.Vendors.DataSync.Application.MailBoxes
{
    internal static class MailBoxMappings
    {
        internal static MailBoxDto ToMailBoxDto(this MailBox mailBox)
        {
            return new MailBoxDto
            {
                Id = mailBox.Id!,
                UserId = mailBox.UserId,
                User = mailBox.User?.ToUserDto(),
                DisplayName = mailBox.DisplayName,
                IsActive = mailBox.IsActive,
                Provider = mailBox.Provider.ToString("G"),
                AccessToken = mailBox.AccessToken,
                RefreshToken = mailBox.RefreshToken,
                CreatedDate = mailBox.CreatedDate,
                LastModifiedDate = mailBox.LastModifiedDate
            };
        }

        internal static MailBox ToMailBox(this MailBoxDto mailBoxDto)
        {
            Enum.TryParse<MailBoxProvider>(mailBoxDto.Provider, out var provider);
            
            return new MailBox(
                mailBoxDto.UserId!, 
                mailBoxDto.Id, 
                provider, 
                mailBoxDto.DisplayName ?? mailBoxDto.Id,
                mailBoxDto.IsActive,
                mailBoxDto.AccessToken, 
                mailBoxDto.RefreshToken, 
                mailBoxDto.CreatedDate);

        }
        internal static List<MailBoxDto> ToMailBoxDtos(this List<MailBox> mailBoxes)
        {
            return mailBoxes?.Select(ToMailBoxDto)?.ToList() ?? new List<MailBoxDto>();
        }

        internal static List<MailBox> ToMailBoxes(this List<MailBoxDto> mailBoxDtos)
        {
            return mailBoxDtos?.Select(ToMailBox)?.ToList() ?? new List<MailBox>();
        }
    }
}
