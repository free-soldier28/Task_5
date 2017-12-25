using WindowsService.BLL.DTO;
using WindowsService.DAL.Interfaces;
using WindowsService.DAL.Repositories;
using AutoMapper;
using Entities;
using Ninject.Modules;

namespace WindowsService.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;


        public ServiceModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
