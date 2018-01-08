using System.Collections.Generic;
using WindowsService.BLL.DTO;

namespace WindowsService.BLL.Interfaces
{
    public interface ICustomerService 
    {
        IEnumerable<CustomerDTO> GetCustomers();
        int AddCustomer(string name);
        void EditCustomer(CustomerDTO customerDTO);
        void DeleteCustomer(int id);
    }
}
