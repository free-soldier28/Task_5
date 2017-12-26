using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WindowsService.BLL.DTO;
using WindowsService.BLL.Infrastructure;
using AutoMapper;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using SalesWebApplication.Models;
using SalesWebApplication.Util;
using Entities;

namespace SalesWebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // внедрение зависимостей
            NinjectModule orderModule = new OrderModule();
            NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            var kernel = new StandardKernel(orderModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SalesDTO, SalesViewModel> ();
                cfg.CreateMap<SalesDTO,Sales>();
                cfg.CreateMap<ProductSalesDTO, ProductSalesViewModel>();
            });
        }
    }
}
