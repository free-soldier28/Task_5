using System.Collections.Generic;
using System.Linq;
using WindowsService.BLL.DTO;
using WindowsService.BLL.Interfaces;
using WindowsService.DAL.Interfaces;
using AutoMapper;
using Entities;

namespace WindowsService.BLL.Services
{
    public class ManagerService:  IManagerService
    {
        IUnitOfWork Database { get; set; }

        public ManagerService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<ManagerDTO> GetManagers()
        {
            IEnumerable<Manager> managers = Database.Managers.GetAll();
            IEnumerable<ManagerDTO> managerDTO = Mapper.Map<IEnumerable<ManagerDTO>>(managers);
            return managerDTO;
        }

        public int AddManager(string name)
        {
            Manager manager = new Manager();
            manager.SecondName = name;
            Database.Managers.Create(manager);

            int id = Database.Managers.GetAll().OrderByDescending(x=>x.Id).Select(z=>z.Id).FirstOrDefault();
            return id;
        }

        public void EditManager(ManagerDTO managerDTO)
        {
            Manager manager = Mapper.Map<Manager>(managerDTO);
            Database.Managers.Update(manager);
        }

        public void DeleteManager(int id)
        {
            Database.Managers.Delete(id);
        }

    }
}
