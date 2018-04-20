using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop
{
    public class Repository<T> : IRepository<T> where T : class
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
            throw new System.NotImplementedException();
        }

        public T FindItem(object ID)
        {
            throw new System.NotImplementedException();
        }

        public bool AddOrUpdate(T Item)
        {
            throw new System.NotImplementedException();
        }

        public bool AddOrUpdate(params T[] Items)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(T Item)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(params T[] Items)
        {
            throw new System.NotImplementedException();
        }
    }

    public class RepositoryCollection : IRepositoryCollection
    {
        private zModel Context;

        public RepositoryCollection(zModel db)
        {
            this.Context = db;
        }

        public Repository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(Context);
        }
    }
}
