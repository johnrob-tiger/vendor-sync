using Tsi.Vendors.DataSync.Domain.Entities.Users;

namespace Tsi.Vendors.DataSync.Data.Repositories
{
    public class StubUserRepository : InMemoryRepository<User, int>, IUserRepository
    {
        private static int _id = 1;

        public StubUserRepository()
        {
        }

        public StubUserRepository(IEnumerable<User> users)
        {
            Entities.AddRange(users);
            var maxId = Entities.Max(u => u.Id);
            _id = maxId != null ? maxId.Value + 1 : 1;
        }

        protected override User AssignId(User obj)
        {
            obj.Id = _id;
            _id++;

            return obj;
        }

        protected override Func<User, int> IdResolver
        {
            get
            {
                return (user) => user.Id ?? 0;
            }
        }
    }
}
