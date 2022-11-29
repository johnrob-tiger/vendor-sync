using Tsi.Vendors.DataSync.Domain.Base;

namespace Tsi.Vendors.DataSync.Domain.Interfaces
{
    public interface IDomainEventRepository
    {
        void Add<TEvent>(TEvent @event) where TEvent : BaseDomainEvent;
        IEnumerable<DomainEventRecord> FindAll();
    }
}
