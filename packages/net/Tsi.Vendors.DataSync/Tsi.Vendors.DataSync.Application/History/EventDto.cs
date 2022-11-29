using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Vendors.DataSync.Application.History
{
    public class EventDto
    {
        public string Type { get; set; } = null!;
        public Dictionary<string, string> Args { get; set; } = null!;
        public DateTime Created { get; set; }
        public string? CorrelationId { get; set; } = null;

    }
}
