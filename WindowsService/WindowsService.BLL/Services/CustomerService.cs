using System.Collections.Generic;
using System.Linq;
using WindowsService.BLL.DTO;
using WindowsService.BLL.Interfaces;
using WindowsService.DAL.Interfaces;
using AutoMapper;
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

        public IEnumerable<CustomerDTO> GetCustomers()
        {
            IEnumerable<Customer> customers = Database.Customers.GetAll();
            IEnumerable<CustomerDTO> customersDTO = Mapper.Map<IEnumerable<CustomerDTO>>(customers);
            return customersDTO;
        }

        public int AddCustomer(string name)
        {
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.FullName = name;
            Customer customres = Mapper.Map<Customer>(customerDTO);
            Database.Customers.Create(customres);

            int id = Database.Customers.GetAll().OrderByDescending(x => x.Id).Select(z => z.Id).FirstOrDefault();
            return id;
        }

        public void EditCustomer(CustomerDTO customerDTO)
        {
            Customer customer = Mapper.Map<Customer>(customerDTO);
            Database.Customers.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            Database.Customers.Delete(id);
        }
    }
}
