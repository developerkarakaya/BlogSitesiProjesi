using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BussinessLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private DbSet<T> tables;
        MyBlog.DataEntitiess.MYBLOGDBEntities db = new MyBlog.DataEntitiess.MYBLOGDBEntities();


        public Repository()
        {
            tables = db.Set<T>();
        }

        public IEnumerable<T> getAll()
        {
            var result = tables.ToList();
            return result;
        }

        public void Update(T model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public T Find(int Id)
        {
            return tables.Find(Id);
        }

        public void Create(T model)
        {
            tables.Add(model);
            db.SaveChanges();
        }

        public void Delete(T model)
        {
            tables.Remove(model);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            T finded = Find(Id);
            if (finded == null) return;
            tables.Remove(finded);
            db.SaveChanges();
        }
    }
}
