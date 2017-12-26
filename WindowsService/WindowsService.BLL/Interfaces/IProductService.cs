using System.Collections.Generic;
using WindowsService.BLL.DTO;

namespace WindowsService.BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetProduct();
    }
}
