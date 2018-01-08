using WindowsService.BLL.Interfaces;
using WindowsService.DAL.Interfaces;
using Entities;

namespace WindowsService.BLL.Services
{
    public class CustomerService: ICustomerService
    {
        IUnitOfWork Database { get; set; }

        public CustomerService(IUnitOfWork uow)
        {
            Database = uow;
        }
    }
}
