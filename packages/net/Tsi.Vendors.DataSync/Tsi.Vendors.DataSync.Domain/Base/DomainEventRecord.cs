using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Vendors.DataSync.Domain.Base
{
    public class DomainEventRecord
    {
        public string Type { get; set; } = null!;
        public List<KeyValuePair<string, string>>? Args { get; set; } = null!;
        public string? CorrelationId { get; set; } = null!;
        public DateTime Created { get; set; }

    }
}
