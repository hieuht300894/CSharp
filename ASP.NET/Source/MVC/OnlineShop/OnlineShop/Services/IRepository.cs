using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T FindItem(object ID);
        bool AddOrUpdate(T Item);
        bool AddOrUpdate(params T[] Items);
        bool Delete(T Item);
        bool Delete(params T[] Items);
    }
}
