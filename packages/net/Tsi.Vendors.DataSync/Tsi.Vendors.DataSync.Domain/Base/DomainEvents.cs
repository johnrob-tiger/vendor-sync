using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Tsi.Vendors.DataSync.Domain.Base
{
    public static class DomainEvents
    {
        [ThreadStatic] // so that each thread has it's own callbacks
        private static List<Delegate>? _actions;

        private static IServiceScope? _serviceScope;

        public static void Init(IServiceScope? serviceScope)
        {
            _serviceScope = serviceScope ?? throw new ArgumentNullException(nameof(serviceScope));
        }

        public static void Register<T>(Action<T> callback) where T : BaseDomainEvent
        {
            _actions ??= new List<Delegate>();

            _actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            _actions = null;
        }

        public static void Raise<T>(T @event) where T : BaseDomainEvent
        {
            if (_serviceScope != null)
            {
                foreach (var handler in _serviceScope.ServiceProvider.GetServices<IHandles<T>>())
                {
                    handler.Handle(@event);
                }
            }

            if (_actions == null) return;

            foreach (var action in _actions)
            {
                if (action is Action<T> actor)
                {
                    actor(@event);
                }
            }
        }
    }
}
