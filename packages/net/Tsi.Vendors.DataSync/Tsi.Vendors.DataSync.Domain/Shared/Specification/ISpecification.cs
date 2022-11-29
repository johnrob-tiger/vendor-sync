using System.Linq.Expressions;

namespace Tsi.Vendors.DataSync.Domain.Shared.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> SpecExpression { get; }
        bool IsSatisfiedBy(T obj);
    }
}
