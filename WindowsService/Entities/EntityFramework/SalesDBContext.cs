using System.Data.Entity;

namespace Entities
{
    public partial class SalesDBContext : DbContext
    {
        public SalesDBContext()
            : base("name=SalesDBContext")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sales> Saleses { get; set; }
    }
}
