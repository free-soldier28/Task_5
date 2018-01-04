using System;
using System.Collections.Generic;
using System.Linq;
using WindowsService.BLL.DTO;
using WindowsService.BLL.Interfaces;
using WindowsService.DAL.Interfaces;
using AutoMapper;
using Entities;
using Ninject.Infrastructure.Language;

namespace WindowsService.BLL
{
    public class SalesService: ISalesService
    {
        IUnitOfWork Database { get; set; }

        public SalesService(IUnitOfWork uow)
        {          
            Database = uow;
        }


        public IEnumerable<SalesDTO> GetSales()
        {
            IEnumerable<Sales> allSales = Database.Saleses.GetAll();
            List<SalesDTO> allSalesDTO = new List<SalesDTO>();

            foreach (var sales in allSales)
            {
                SalesDTO salesDTO = new SalesDTO();
                salesDTO.Id = sales.Id;
                salesDTO.DateTime = sales.DateTime;
                salesDTO.Amount = sales.Amount;
                salesDTO.ManagerName = sales.Manager.SecondName;
                salesDTO.CustomerName = sales.Customer.FullName;
                salesDTO.ProductName = sales.Product.Name;
                
                allSalesDTO.Add(salesDTO);
            }
            
            return allSalesDTO;
        }


        public IEnumerable<string> GetAllManagers()
        {
            IEnumerable<string> managers = Database.Managers.GetAll().Select(x => x.SecondName);
            return managers;
        }


        public IEnumerable<string> GetAllCustomers()
        {
            IEnumerable<string> customers = Database.Customers.GetAll().Select(x => x.FullName);
            return customers;
        }


        public IEnumerable<string> GetAllProducts()
        {
            IEnumerable<string> products = Database.Products.GetAll().Select(x => x.Name);
            return products;
        }


        public IEnumerable<ManagerSalesDTO> GetManagersSales()
        {
           var managersSalesDTO = Database.Saleses.GetAll().GroupBy(x => x.Manager.SecondName)
                                                           .Select(z=> new ManagerSalesDTO
                                                            {
                                                                ManagerName = z.First().Manager.SecondName,
                                                                SumAmount = z.Sum(s=>s.Amount)
                                                            });
            return managersSalesDTO;
        }


        public int AddSales(SalesDTO salesDTO)
        {
            Manager manager = Database.Managers.Find(x => x.SecondName == salesDTO.ManagerName).FirstOrDefault();
            Customer customer = Database.Customers.Find(x => x.FullName == salesDTO.CustomerName).FirstOrDefault();
            Product product = Database.Products.Find(x => x.Name == salesDTO.ProductName).FirstOrDefault();

            Sales sales = new Sales
            {
                DateTime = salesDTO.DateTime,
                Amount = salesDTO.Amount
            };

            if (manager == null)
            {
                manager = new Manager()
                {
                    SecondName = salesDTO.ManagerName
                };
                sales.Manager = manager;
            }
            else
            {
                sales.ManagerID = manager.Id;
            }


            if (customer == null)
            {
                customer = new Customer()
                {
                    FullName = salesDTO.CustomerName
                };
                sales.Customer = customer;
            }
            else
            {
                sales.CustomerID = customer.Id;
            }


            if (product == null)
            {
                product = new Product()
                {
                    Name = salesDTO.ProductName
                };
                sales.Product = product;
            }
            else
            {
                sales.ProductID = product.Id;
            }

            Database.Saleses.Create(sales);

            int id = Database.Saleses.GetAll().OrderByDescending(x=>x.Id).Select(z=>z.Id).FirstOrDefault();
            return id;
        }


        public void EditSales(SalesDTO salesDTO)
        {
            Manager manager = Database.Managers.Find(x => x.SecondName == salesDTO.ManagerName).FirstOrDefault();
            Customer customer = Database.Customers.Find(x => x.FullName == salesDTO.CustomerName).FirstOrDefault();
            Product product = Database.Products.Find(x => x.Name == salesDTO.ProductName).FirstOrDefault();

            Sales sales = Mapper.Map<Sales>(salesDTO);
            sales.ManagerID = manager.Id;
            sales.CustomerID = customer.Id;
            sales.ProductID = product.Id;

            Database.Saleses.Update(sales);
        }


        public void DeleteById(int id)
        {
            Database.Saleses.Delete(id);
        }
    }
}
