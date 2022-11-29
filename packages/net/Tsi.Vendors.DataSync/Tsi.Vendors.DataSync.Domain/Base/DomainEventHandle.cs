using Tsi.Vendors.DataSync.Domain.Interfaces;

namespace Tsi.Vendors.DataSync.Domain.Base
{
    public class DomainEventHandle<TEvent> : IHandles<TEvent> where TEvent : BaseDomainEvent
    {
        private readonly IDomainEventRepository _domainEventRepository;
        private readonly IRequestCorrelationIdentifier _requestCorrelationIdentifier;

        public DomainEventHandle(
            IDomainEventRepository domainEventRepository,
            IRequestCorrelationIdentifier requestCorrelationIdentifier)
        {
            _domainEventRepository = domainEventRepository;
            _requestCorrelationIdentifier = requestCorrelationIdentifier;
        }

        public void Handle(TEvent @event)
        {
            @event.Flatten();
            @event.CorrelationId = _requestCorrelationIdentifier.CorrelationId;
            _domainEventRepository.Add(@event);
        }

    }
}
