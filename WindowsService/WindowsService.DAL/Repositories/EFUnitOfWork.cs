using System;
using WindowsService.DAL.Interfaces;
using Entities;

namespace WindowsService.DAL.Repositories
{
    public class EFUnitOfWork: IUnitOfWork
    {
        private SalesDBContext db;
        private CustomerRepository customerRepository;
        private ManagerRepository managerRepository;
        private ProductRepository productRepository;
        private SalesRepository salesRepository;
        private bool disposed = false;

        public EFUnitOfWork()
        {
            db = new SalesDBContext();
        }

        public IRepository<Customer> Customers
        {
            get
            {
                if (customerRepository == null)
                {
                    customerRepository = new CustomerRepository(db);
                }
                return customerRepository;
            }
        }

        public IRepository<Manager> Managers
        {
            get
            {
                if (managerRepository == null)
                {
                    managerRepository = new ManagerRepository(db);
                }
                return managerRepository;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(db);
                }
                return productRepository;
            }
        }

        public IRepository<Sales> Saleses
        {
            get
            {
                if (salesRepository == null)
                {
                    salesRepository = new SalesRepository(db);
                }
                return salesRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
