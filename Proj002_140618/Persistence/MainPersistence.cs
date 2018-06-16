using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proj002_140618.Persistence
{
    public class MainPersistence<T> where T : class
    {
        protected ModelContext db = new ModelContext();

        public T Get(int? id)
        {
            return db.Set<T>().Find(id);
        }

        public List<T> List()
        {
            return db.Set<T>().ToList();
        }

        public void Add(T obj)
        {
            db.Set<T>().Add(obj);
            db.SaveChanges();
        }

        public void Edit(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(T obj)
        {
            db.Set<T>().Remove(obj);
            db.SaveChanges();
        }

        public IQueryable<T> GetQueryable()
        {
            return db.Set<T>().AsQueryable();
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}