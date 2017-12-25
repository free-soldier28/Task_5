using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WindowsService.DAL.Interfaces;
using Entities;

namespace WindowsService.DAL.Repositories
{
    public class ManagerRepository: IRepository<Manager>
    {
        private SalesDBContext db;

        public ManagerRepository(SalesDBContext context)
        {
            db = context;
        }

        public IEnumerable<Manager> GetAll()
        {
            return db.Managers;
        }

        public Manager Get(int id)
        {
            return db.Managers.Find(id);
        }

        public IEnumerable<Manager> Find(Func<Manager, Boolean> predicate)
        {
            return db.Managers.Where(predicate).ToList();
        }

        public void Create(Manager item)
        {
            db.Managers.Add(item);
        }

        public void Update(Manager item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Manager item = db.Managers.Find(id);
            db.Managers.Remove(item);
        }
    }
}
