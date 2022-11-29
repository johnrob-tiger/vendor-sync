using Tsi.Vendors.DataSync.Domain.Interfaces;

namespace Tsi.Vendors.DataSync.Application.History
{
    public class HistoryService : IHistoryService
    {
        private readonly IDomainEventRepository _domainEventRepository;

        public HistoryService(IDomainEventRepository domainEventRepository)
        {
            _domainEventRepository = domainEventRepository;
        }

        public HistoryDto GetHistory()
        {
            var events = this._domainEventRepository.FindAll();
            

            var history = new HistoryDto
            {
                Events = events.ToEventDtos()
            };

            return history;
        }
    }
}
