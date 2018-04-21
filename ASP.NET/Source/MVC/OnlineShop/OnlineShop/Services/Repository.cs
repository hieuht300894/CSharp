using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop
{
    public class Repository<T> : IRepositoryCollection<T> where T : class, new()
    {
        private zModel Context;

        public Repository()
        {
        }

        public Repository(zModel db)
        {
            Context = db;
        }

        public List<T> GetAll()
        {
            return new List<T>();
        }

        public T FindItem(object ID)
        {
            return new T();
        }

        public bool AddOrUpdate(T Item)
        {
            return true;
        }

        public bool AddOrUpdate(params T[] Items)
        {
            return true;
        }

        public bool Delete(T Item)
        {
            return true;
        }

        public bool Delete(params T[] Items)
        {
            return true;
        }
    }
}
