using System.Linq.Expressions;
using Tsi.Vendors.DataSync.Domain.Shared.Specification;

namespace Tsi.Vendors.DataSync.Domain.Entities.Users.Specs
{
    public class UserNameAlreadyExistsSpec : BaseSpecification<User>
    {
        private readonly string _userName;

        public UserNameAlreadyExistsSpec(string userName)
        {
            _userName = userName;
        }

        public override Expression<Func<User, bool>> SpecExpression
        {
            get
            {
                return (user) => user.UserName.Equals(_userName, StringComparison.InvariantCultureIgnoreCase);
            }
        }
    }
}
