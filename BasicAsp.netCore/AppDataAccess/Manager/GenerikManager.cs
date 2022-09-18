using AppDataAccess.Data;
using AppDataAccess.GenerikInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Manager
{
    public class GenerikManager<T> : IGenerik<T> where T : class
    {

        private AppDbContext _context;
        private DbSet<T> table ;

        public GenerikManager(AppDbContext context)
        {
            _context = context;
            this.table =_context.Set<T>();
        }


        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
             table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            if (existing != null)
                table.Remove(existing);
        }

       
        public void Save()
        {
            _context.SaveChanges();
        }

       
    }
}
