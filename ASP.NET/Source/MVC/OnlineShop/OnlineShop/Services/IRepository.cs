using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop
{
    public interface IRepository<T> where T : class, new()
    {
        List<T> GetAll();
        T FindItem(object ID);
        void AddOrUpdate(T Item);
        void AddOrUpdate(params T[] Items);
        void Delete(T Item);
        void Delete(params T[] Items);
    }
}
