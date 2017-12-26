using System.Collections.Generic;
using WindowsService.BLL.DTO;

namespace WindowsService.BLL.Interfaces
{
    public interface ISalesService
    {
        void AddSales(SalesDTO salesDTO);
        IEnumerable<SalesDTO> GetSales();
        IEnumerable<ProductSalesDTO> GetProductSales();
        void DeleteById(int id);
    }
}
