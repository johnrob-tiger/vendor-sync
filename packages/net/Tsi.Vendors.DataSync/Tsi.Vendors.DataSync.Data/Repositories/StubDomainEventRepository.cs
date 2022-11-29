using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Interfaces;

namespace Tsi.Vendors.DataSync.Data.Repositories
{
    public class StubDomainEventRepository : IDomainEventRepository
    {
        private readonly List<DomainEventRecord> _domainEvents = new List<DomainEventRecord>();

        public void Add<TEvent>(TEvent @event) where TEvent : BaseDomainEvent
        {
            this._domainEvents.Add(
                new DomainEventRecord
                {
                    Created = @event.Created,
                    Type = @event.Type,
                    Args = @event.Args.Select(kv => new KeyValuePair<string, string>(kv.Key, kv.Value.ToString() ?? string.Empty)).ToList(),
                    CorrelationId = @event.CorrelationId
                });
        }

        public IEnumerable<DomainEventRecord> FindAll()
        {
            return this._domainEvents;
        }
    }
}
