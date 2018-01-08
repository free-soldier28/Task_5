using System.Collections.Generic;
using WindowsService.BLL.DTO;
using Entities;

namespace WindowsService.BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetProduct();
    }
}
