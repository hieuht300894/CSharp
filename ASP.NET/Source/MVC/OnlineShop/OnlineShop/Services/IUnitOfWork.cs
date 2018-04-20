using System.Threading.Tasks;

namespace OnlineShop
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        Task<int> SaveChanges();
        void CommitTransaction();
        void RollbackTransaction();
    }
}