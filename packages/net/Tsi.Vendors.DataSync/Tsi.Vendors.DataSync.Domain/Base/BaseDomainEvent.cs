using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tsi.Vendors.DataSync.Domain.Base
{
    public abstract class BaseDomainEvent : INotification
    {
        public string Type => this.GetType().Name;

        public virtual Guid EventId { get; init; } = Guid.NewGuid();

        public virtual DateTime Created { get; init; } = DateTime.UtcNow;

        public IDictionary<string, object> Args { get; private set; }

        public string? CorrelationId { get; set; } = null!;

        protected BaseDomainEvent()
        {
            this.Args = new Dictionary<string, object>();
        }

        public abstract void Flatten();
    }
}
