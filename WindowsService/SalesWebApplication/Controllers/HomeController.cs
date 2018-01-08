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


        [Authorize(Roles = "admin")]
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
            var result = Mapper.Map<IEnumerable<SalesViewModel>>(_salesService.GetSales());
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "admin")]
        public JsonResult GetManagersSales()
        {
            List<ManagerSalesViewModel> result = Mapper.Map<IEnumerable<ManagerSalesViewModel>>(_salesService.GetManagersSales()).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "admin")]
        public JsonResult GetAllManagers()
        {
            var result = _salesService.GetAllManagers();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "admin")]
        public JsonResult GetAllCustomers()
        {
            var result = _salesService.GetAllCustomers();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "admin")]
        public JsonResult GetAllProducts()
        {
            var result = _salesService.GetAllProducts();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "admin")]
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


        [Authorize(Roles = "admin")]
        public int EditSales(SalesViewModel salesViewModel)
        {
            if (salesViewModel != null)
            {
                SalesDTO salesDto = Mapper.Map<SalesDTO>(salesViewModel);
               _salesService.EditSales(salesDto);
                return 1;
            }
            else
            {
                return 0;
            }
        }


        [Authorize(Roles = "admin")]
        public void DeleteByIdSales(int id)
        {
            _salesService.DeleteById(id);
        }

    }
}