using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Vendors.DataSync.Data.Repositories;
using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Interfaces;

namespace Tsi.Vendors.DataSync.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DataSyncEfContext _dbContext;

        public UnitOfWork(DataSyncEfContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IAsyncRepository<T> Repository<T>() where T : BaseEntity
        {
            return new RepositoryBase<T>(_dbContext);
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

    }
}
