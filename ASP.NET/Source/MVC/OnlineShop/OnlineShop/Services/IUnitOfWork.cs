using System.Threading.Tasks;

namespace OnlineShop
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        int SaveChanges();
        void CommitTransaction();
        void RollbackTransaction();
    }
}