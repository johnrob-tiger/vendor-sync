using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Vendors.DataSync.Domain.Base;

namespace Tsi.Vendors.DataSync.Application.History
{
    internal static class HistoryMappings
    {
        internal static EventDto ToEventDto(this DomainEventRecord record)
        {

            var eventDto = new EventDto
            {
                Args = new Dictionary<string, string>(record.Args ?? new List<KeyValuePair<string, string>>()),
                CorrelationId = record.CorrelationId,
                Created = record.Created,
                Type = record.Type
            };

            return eventDto;
        }

        internal static DomainEventRecord ToDomainEventRecord(this EventDto eventDto)
        {
            var domainEventRecord = new DomainEventRecord
            {
                Args = eventDto.Args.ToList(),
                CorrelationId = eventDto.CorrelationId,
                Created = eventDto.Created,
                Type = eventDto.Type
            };

            return domainEventRecord;
        }

        internal static List<EventDto> ToEventDtos(this IEnumerable<DomainEventRecord> records)
        {
            var mapped = records.Select(x => x.ToEventDto())
                .ToList();

            return mapped ?? new List<EventDto>();
        }
    }
}
