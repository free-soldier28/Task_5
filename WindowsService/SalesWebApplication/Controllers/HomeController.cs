using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WindowsService.BLL.DTO;
using WindowsService.BLL.Interfaces;
using AutoMapper;
using SalesWebApplication.Models;

namespace SalesWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private ISalesService _salesService;

        public HomeController(ISalesService alesService)
        {
           _salesService = alesService;
        }

        public ActionResult Index()
        {
           return View();
        }


        [JsonNetFilter]
        public JsonResult FilterGetSales(FilterSalesViewModel filter)
        {
            IEnumerable<SalesViewModel> allSales = Mapper.Map<IEnumerable<SalesViewModel>>(_salesService.GetSales());

            if (filter.BeginDateTime != null && filter.EndDateTime != null)
            {
                allSales = allSales.Where(x => x.DateTime >= filter.BeginDateTime && x.DateTime <= filter.EndDateTime);
            }
            if (filter.ManagerFiltr != null)
            {
                allSales = allSales.Where(x => x.ManagerName == filter.ManagerFiltr);
            }
            if (filter.CustomerFiltr != null)
            {
                allSales = allSales.Where(x => x.CustomerName == filter.CustomerFiltr);
            }
            if (filter.ProductFiltr != null)
            {
                allSales = allSales.Where(x => x.ProductName == filter.ProductFiltr);
            }

            return Json(allSales, JsonRequestBehavior.AllowGet);
        }

        [JsonNetFilter]
        public JsonResult GetSales()
        {
            var allSales = Mapper.Map<IEnumerable<SalesViewModel>>(_salesService.GetSales());
            return Json(allSales, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductSales()
        {
            var allProductSalese = Mapper.Map<IEnumerable<ProductSalesViewModel>>(_salesService.GetProductSales());
            return Json(allProductSalese, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllManagers()
        {
            var allMagers = _salesService.GetAllManagers();
            return Json(allMagers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCustomers()
        {
            var allCustomers = _salesService.GetAllCustomers();
            return Json(allCustomers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllProducts()
        {
            var allProducts = _salesService.GetAllProducts();
            return Json(allProducts, JsonRequestBehavior.AllowGet);
        }


        public int AddSales(SalesViewModel salesViewModel)
        {
            if (salesViewModel != null)
            {
                SalesDTO salesDto = Mapper.Map<SalesDTO>(salesViewModel);
                int id = _salesService.AddSales(salesDto);
                return id;
            }
            else
            {
                return 0;
            } 
        }

        public void EditSales(SalesViewModel salesViewModel)
        {
            if (salesViewModel != null)
            {
                SalesDTO salesDto = Mapper.Map<SalesDTO>(salesViewModel);
               _salesService.EditSales(salesDto);
            }
        }


        public void DeleteByIdSales(int id)
        {
            _salesService.DeleteById(id);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}