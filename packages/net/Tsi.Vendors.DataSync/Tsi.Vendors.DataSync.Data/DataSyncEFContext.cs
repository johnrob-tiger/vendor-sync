using Microsoft.EntityFrameworkCore;
using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Entities.Calendars;
using Tsi.Vendors.DataSync.Domain.Entities.MailBoxes;
using Tsi.Vendors.DataSync.Domain.Entities.Users;

namespace Tsi.Vendors.DataSync.Data
{
    public class DataSyncEfContext : DbContext
    {
        public DataSyncEfContext(DbContextOptions<DataSyncEfContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Calendar> Calendars { get; set; } = null!;

        public DbSet<CalendarEvent> CalendarEvents { get; set; } = null!;

        public DbSet<MailBox> MailBoxes { get; set; } = null!;

        public DbSet<DomainEventRecord> DomainEvents { get; set; } = null!;
    }
}
