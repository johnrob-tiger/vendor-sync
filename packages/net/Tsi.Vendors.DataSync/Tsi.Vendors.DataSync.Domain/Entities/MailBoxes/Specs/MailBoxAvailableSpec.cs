using System.Linq.Expressions;
using Tsi.Vendors.DataSync.Domain.Shared.Specification;

namespace Tsi.Vendors.DataSync.Domain.Entities.MailBoxes.Specs
{
    public class MailBoxAvailableSpec : BaseSpecification<MailBox>
    {
        private readonly string _emailAddress;

        public MailBoxAvailableSpec(string emailAddress)
        {
            _emailAddress = emailAddress;
        }

        public override Expression<Func<MailBox, bool>> SpecExpression
        {
            get
            {
                return (mailBox) => mailBox.Id != null && !mailBox.Id.Equals(_emailAddress, StringComparison.InvariantCultureIgnoreCase);
            }
        }
    }
}
