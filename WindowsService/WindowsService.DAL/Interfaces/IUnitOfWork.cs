using System;
using Entities;

namespace WindowsService.DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Manager> Managers { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Sales> Saleses { get; }
        IRepository<Product> Products { get; }

        void Save();
    }
}
