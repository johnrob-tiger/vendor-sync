using System.Linq.Expressions;

namespace Tsi.Vendors.DataSync.Domain.Shared.Specification
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        private Func<T, bool> _compiledExpression = null!;

        private Func<T, bool> CompiledExpression
        {
            get
            {
                return _compiledExpression ??= SpecExpression.Compile(); 
            }
        }

        public abstract Expression<Func<T, bool>> SpecExpression { get; }

        public bool IsSatisfiedBy(T obj)
        {
            return CompiledExpression(obj);
        }
    }
}
