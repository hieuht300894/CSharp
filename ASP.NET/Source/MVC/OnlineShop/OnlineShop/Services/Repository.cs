using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private zModel context = null;

        public Repository()
        {
            context = new zModel();
        }

        public Repository(zModel db)
        {
            context = db;
        }

        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T FindItem(object ID)
        {
            return context.Set<T>().Find(ID);
        }

        public void AddOrUpdate(T item)
        {
            if (item == null)
                throw new ArgumentNullException();

            context.Set<T>().AddOrUpdate(item);
        }

        public void AddOrUpdate(params T[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentNullException();

            context.Set<T>().AddOrUpdate(items);
        }

        public void Delete(T item)
        {
            if (item == null)
                throw new ArgumentNullException();

            context.Set<T>().Remove(item);
        }

        public void Delete(params T[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentNullException();

            context.Set<T>().RemoveRange(items);
        }
    }
}
