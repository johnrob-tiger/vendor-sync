namespace Tsi.Vendors.DataSync.Domain.Base
{
    public class DispatchEvent<TModel, TEvent> 
        where TModel : BaseEntity 
        where TEvent : BaseDomainEvent
    {
        private readonly Action<TModel, TEvent>? _wireAction;

        public DispatchEvent(
            TModel model, 
            TEvent @event,
            Action<TModel, TEvent>? wireAction = null)
        {
            Model = model;
            Event = @event;
            _wireAction = wireAction;
        }
        
        public void Dispatch(TModel? newModel = null)
        {
            _wireAction?.Invoke(newModel ?? Model, Event);

            DomainEvents.Raise(Event);
        }

        public TModel Model { get; }
        public TEvent Event { get; }
    }
}
