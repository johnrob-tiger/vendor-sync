using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Entities.Calendars;
using Tsi.Vendors.DataSync.Domain.Entities.MailBoxes;
using Tsi.Vendors.DataSync.Domain.Entities.Users;
using Tsi.Vendors.DataSync.Domain.Interfaces;

namespace Tsi.Vendors.DataSync.Data.Repositories
{
    public class MemoryUnitOfWork : IUnitOfWork
    {
        private static readonly IAsyncRepository<User> UserRepository;
        private static readonly IAsyncRepository<Calendar> CalendarRepository;
        private static readonly IAsyncRepository<CalendarEvent> CalendarEventRepository;
        private static readonly IAsyncRepository<MailBox> MailBoxRepository;

        static MemoryUnitOfWork()
        {
            UserRepository = new StubUserRepository(new List<User>()
            {
                new User(1, "john.doe", "John", "Doe", new DateTime(1950, 1, 1))
            });

            CalendarRepository = new StubCalendarRepository();
            CalendarEventRepository = new StubCalendarEventRepository();
            MailBoxRepository = new StubMailBoxRepository();
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }

        public IAsyncRepository<T> Repository<T>() where T : BaseEntity
        {
            if (typeof(T) == typeof(CalendarEvent))
            {
                return (IAsyncRepository<T>)CalendarEventRepository;
            }

            if (typeof(T) == typeof(Calendar))
            {
                return (IAsyncRepository<T>)CalendarRepository;
            }

            if (typeof(T) == typeof(User))
            {
                return (IAsyncRepository<T>)UserRepository;
            }

            if (typeof(T) == typeof(MailBox))
            {
                return (IAsyncRepository<T>)MailBoxRepository;
            }

            throw new Exception($"Repository not registered for type {typeof(T)}.");
        }
    }
}
