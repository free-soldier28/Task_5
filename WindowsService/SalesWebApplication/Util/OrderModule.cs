using WindowsService.BLL;
using WindowsService.BLL.Interfaces;
using WindowsService.BLL.Services;
using Ninject.Modules;

namespace SalesWebApplication.Util
{
    public class OrderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISalesService>().To<SalesService>();
            Bind<IManagerService>().To<ManagerService>();
            Bind<ICustomerService>().To<CustomerService>();
            Bind<IProductService>().To<ProductService>();
        }
    }
}