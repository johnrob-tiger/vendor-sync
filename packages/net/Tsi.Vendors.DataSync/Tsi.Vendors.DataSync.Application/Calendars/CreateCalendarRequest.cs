using Tsi.Vendors.DataSync.Domain.Shared;
using Tsi.Vendors.DataSync.Domain.Shared.Extensions;

namespace Tsi.Vendors.DataSync.Application.Calendars
{
    public class CreateCalendarRequest
    {
        public CreateCalendarRequest(
            string timeZone, 
            string title, 
            string description,
            int? userId = null)
        {
            Guard.Against.NullOrWhiteSpace(timeZone, "timeZone");
            Guard.Against.NullOrWhiteSpace(title, "title");

            TimeZone = timeZone;
            Title = title;
            Description = description;
            UserId = UserId;
        }
        public string TimeZone { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int? UserId { get; set; }
    }
}
