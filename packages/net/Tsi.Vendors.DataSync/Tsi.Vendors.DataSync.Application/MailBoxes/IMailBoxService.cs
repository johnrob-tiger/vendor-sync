
namespace Tsi.Vendors.DataSync.Application.MailBoxes
{
    public interface IMailBoxService
    {
        Task<List<MailBoxDto>> ListUserMailBoxesAsync(int userId);

        Task<MailBoxDto?> GetMailBoxAsync(string id);

        Task<MailBoxDto> CreateMailBox(CreateMailBoxRequest request);

        Task DeActivate(string id);

        Task Activate(string id);
    }
}
