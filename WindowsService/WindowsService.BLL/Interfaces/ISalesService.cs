using System.Collections.Generic;
using WindowsService.BLL.DTO;

namespace WindowsService.BLL.Interfaces
{
    public interface ISalesService
    {
        int AddSales(SalesDTO salesDTO);
        IEnumerable<SalesDTO> GetSales();
        IEnumerable<string> GetAllManagers();
        IEnumerable<string> GetAllCustomers();
        IEnumerable<string> GetAllProducts();
        IEnumerable<ProductSalesDTO> GetProductSales();
        void DeleteById(int id);
    }
}
