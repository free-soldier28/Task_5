using System;
using System.Collections;
using System.Collections.Generic;
using WindowsService.BLL.DTO;
using WindowsService.BLL.Interfaces;
using WindowsService.DAL.Interfaces;
using Entities;

namespace WindowsService.BLL.Services
{
    public class ProductService: IProductService
    {
        IUnitOfWork Database { get; set; }

        public ProductService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<ProductDTO> GetProduct()
        {
            IEnumerable<Product> products = Database.Products.GetAll();
            List<ProductDTO> allProductDTO = new List<ProductDTO>();

            foreach (var product in products)
            {
                ProductDTO productDto = new ProductDTO();
                productDto.Name = product.Name;
                allProductDTO.Add(productDto);
            }
            ;
            return allProductDTO;
        }
    }
}
