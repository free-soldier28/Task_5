using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WindowsService.DAL.Interfaces;
using Entities;

namespace WindowsService.DAL.Repositories
{
    public class SalesRepository: IRepository<Sales>
    {
        private SalesDBContext db;

        public SalesRepository(SalesDBContext context)
        {
            db = context;
        }

        public IEnumerable<Sales> GetAll()
        {
            return db.Saleses;
        }

        public Sales Get(int id)
        {
            return db.Saleses.Find(id);
        }

        public IEnumerable<Sales> Find(Func<Sales, Boolean> predicate)
        {
            return db.Saleses.Where(predicate).ToList();
        }

        public void Create(Sales item)
        {
            db.Saleses.Add(item);
        }

        public void Update(Sales item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Sales item = db.Saleses.Find(id);
            db.Saleses.Remove(item);
        }
    }
}
