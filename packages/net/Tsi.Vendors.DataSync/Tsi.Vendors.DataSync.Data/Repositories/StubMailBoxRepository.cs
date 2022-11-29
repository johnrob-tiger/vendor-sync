using Tsi.Vendors.DataSync.Domain.Entities.MailBoxes;

namespace Tsi.Vendors.DataSync.Data.Repositories
{
    public class StubMailBoxRepository : InMemoryRepository<MailBox, string>, IMailBoxRespository
    {
        public StubMailBoxRepository()
        {
        }

        public StubMailBoxRepository(IEnumerable<MailBox> mailBoxes)
        {
            Entities.AddRange(mailBoxes);
        }

        protected override Func<MailBox, string> IdResolver
        {
            get
            {
                return (mailBox) => mailBox.Id ?? string.Empty;
            }
        }
    }
}