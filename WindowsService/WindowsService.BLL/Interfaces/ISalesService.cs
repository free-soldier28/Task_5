using System.Collections.Generic;
using WindowsService.BLL.DTO;

namespace WindowsService.BLL.Interfaces
{
    public interface ISalesService
    {
        int AddSales(SalesDTO salesDTO);
        void EditSales(SalesDTO salesDTO);
        void DeleteById(int id);
        IEnumerable<SalesDTO> GetSales();
        IEnumerable<string> GetAllManagers();
        IEnumerable<string> GetAllCustomers();
        IEnumerable<string> GetAllProducts();
        IEnumerable<ManagerSalesDTO> GetManagersSales();
    }
}
