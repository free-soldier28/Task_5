using System.Collections.Generic;
using WindowsService.BLL.DTO;

namespace WindowsService.BLL.Interfaces
{
    public interface ISalesService
    {
        void AddSales(string managerName, string[] substrings);
        IEnumerable<SalesDTO> GetSales();
    }
}
