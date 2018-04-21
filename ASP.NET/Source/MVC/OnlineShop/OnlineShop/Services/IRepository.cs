using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop
{
    public interface IRepositoryCollection<T> where T : class, new()
    {
        List<T> GetAll();
        T FindItem(object ID);
        bool AddOrUpdate(T Item);
        bool AddOrUpdate(params T[] Items);
        bool Delete(T Item);
        bool Delete(params T[] Items);
    }
}
