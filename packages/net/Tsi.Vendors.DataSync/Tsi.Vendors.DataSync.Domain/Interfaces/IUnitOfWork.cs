using Tsi.Vendors.DataSync.Domain.Base;

namespace Tsi.Vendors.DataSync.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();

        IAsyncRepository<T> Repository<T>() where T : BaseEntity;
    }
}
