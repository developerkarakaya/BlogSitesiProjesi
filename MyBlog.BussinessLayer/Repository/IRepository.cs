using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BussinessLayer.Repository
{
   public interface IRepository<T> where T:class
    {
       IEnumerable<T> getAll();
       void Update(T model);
       T Find(int Id);
       void Create(T model);
       void Delete(T model);
       void Delete(int Id);
    }
}
