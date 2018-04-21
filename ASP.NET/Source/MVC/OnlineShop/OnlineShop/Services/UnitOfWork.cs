using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OnlineShop
{
    public class UnitOfWork : IUnitOfWork, IRepositoryCollection
    {
        public zModel Context { get; set; }

        public UnitOfWork(zModel db)
        {
            Context = db;
        }

        public void BeginTransaction()
        {
            Context.Database.BeginTransaction();
        }

        public async Task<int> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }

        public void CommitTransaction()
        {
            if (Context.Database.CurrentTransaction != null)
                Context.Database.CurrentTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            if (Context.Database.CurrentTransaction != null)
                Context.Database.CurrentTransaction.Rollback();
        }

        public Repository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(Context);
        }
    }
}