using System;

namespace WindowsService.BLL.Interfaces
{
    public interface ISalesService
    {
        void AddSales(string managerName, string[] substrings);
    }
}
