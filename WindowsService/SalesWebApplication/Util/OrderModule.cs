using WindowsService.BLL;
using WindowsService.BLL.Interfaces;
using Ninject.Modules;

namespace SalesWebApplication.Util
{
    public class OrderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISalesService>().To<SalesService>();
        }
    }
}