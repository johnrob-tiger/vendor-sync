namespace Tsi.Vendors.DataSync.Domain.Base
{
    public interface IHandles<in TEvent> where TEvent : BaseDomainEvent
    {
        void Handle(TEvent @event);
    }
}
