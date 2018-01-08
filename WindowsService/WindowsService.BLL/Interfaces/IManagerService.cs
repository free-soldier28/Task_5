using System.Collections.Generic;
using WindowsService.BLL.DTO;
using Entities;

namespace WindowsService.BLL.Interfaces
{
    public interface IManagerService
    {
        IEnumerable<ManagerDTO> GetManagers();
        int AddManager(string name);
        void EditManager(ManagerDTO manager);
        void DeleteManager(int id);
    }
}
