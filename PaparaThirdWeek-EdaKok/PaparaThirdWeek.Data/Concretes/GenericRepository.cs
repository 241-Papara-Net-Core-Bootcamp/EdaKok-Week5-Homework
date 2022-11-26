using NPOI.SS.Formula.Functions;
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;

using PaparaThirdWeek.Data.Context;
using PaparaThirdWeek.Domain;
using Microsoft.EntityFrameworkCore;

namespace PaparaThirdWeek.Data.Concretes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<T> Add(T entity)
        {
            entity.Id = 0; // identity column can not be inserted by modal.
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            var cachedList = _context.Set<T>().ToList(); // get users from db to cache updated users.
            return cachedList;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Remove(T entity)
        {
            var modal = _context.Set<T>().Find(entity.Id);
            if (modal != null)
            {
                _context.Entry(modal).State = EntityState.Detached;
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            var modal = _context.Set<T>().Find(entity.Id);
            //if (modal != null)
            //{
            _context.Entry(modal).State = EntityState.Detached;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            //         }
            //   }
        }
    }
}