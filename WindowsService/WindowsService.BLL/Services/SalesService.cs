using System;
using System.Collections.Generic;
using System.Linq;
using WindowsService.BLL.DTO;
using WindowsService.BLL.Interfaces;
using WindowsService.DAL.Interfaces;
using Entities;

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
                salesDTO.Date = sales.Date;
                salesDTO.Amount = sales.Amount;
                salesDTO.ManagerName = sales.Manager.SecondName;
                salesDTO.CustomerName = sales.Customer.FullName;
                salesDTO.ProductName = sales.Product.Name;
                
                allSalesDTO.Add(salesDTO);
            }
            
            return allSalesDTO;
        }


        public IEnumerable<ProductSalesDTO> GetProductSales()
        {
            IEnumerable<Sales> allSales = Database.Saleses.GetAll();
            List<ProductSalesDTO> allProductSalesDTO = new List<ProductSalesDTO>();


            foreach (var sales in allSales)
            {
                ProductSalesDTO productSalesDTO = new ProductSalesDTO();
                productSalesDTO.ProductName = sales.Product.Name;
                productSalesDTO.Amount = sales.Amount;
            }
            return allProductSalesDTO;
        }


        public void DeleteById(int id)
        {
            Database.Saleses.Delete(id);
        }


        public void AddSales(string managerName, string[] substrings)
        {
            Manager manager = Database.Managers.Find(x => x.SecondName == managerName).FirstOrDefault();
            Customer customer = Database.Customers.Find(x => x.FullName == substrings[1]).FirstOrDefault();
            Product product = Database.Products.Find(x => x.Name == substrings[2]).FirstOrDefault();

            Sales sales = new Sales
            {
                Date = Convert.ToDateTime(substrings[0]),
                Amount = Convert.ToDouble(substrings[3]),
            };

            if (manager == null)
            {
                manager = new Manager()
                {
                    SecondName = managerName
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
                    FullName = substrings[1]
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
                    Name = substrings[2]
                };
                sales.Product = product;
            }
            else
            {
                sales.ProductID = product.Id;
            }

            Database.Saleses.Create(sales);
            Database.Save();
        }
    }
}
