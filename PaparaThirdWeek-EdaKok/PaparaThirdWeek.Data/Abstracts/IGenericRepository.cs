using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Data.Abstracts
{
  public  interface IGenericRepository<T> where T:class
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        List<T> Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
