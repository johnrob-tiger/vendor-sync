using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Domain.Base
{
    public abstract class BaseEntity
    {
        private readonly List<BaseDomainEvent> _events;
        public IReadOnlyList<BaseDomainEvent> Events => _events.AsReadOnly();

        protected BaseEntity()
        {
            _events = new List<BaseDomainEvent>();
        }

        protected void AddEvent(BaseDomainEvent @event)
        {
            _events.Add(@event);
        }

        protected void RemoveEvent(BaseDomainEvent @event)
        {
            _events.Remove(@event);
        }

        
    }

    public abstract class BaseEntity<TKey> : BaseEntity
    {
        [Key]
        public virtual TKey? Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual DateTimeOffset CreatedDate { get; set; }
        
        public virtual DateTimeOffset LastModifiedDate { get; set; }

        protected void GuardTimeStamps()
        {
            Guard.Against.OutofSQLDateRange(CreatedDate.UtcDateTime, "CreatedDate");

            Guard.Against.OutofSQLDateRange(LastModifiedDate.UtcDateTime, "LastModifiedDate");
        }
    }
}
