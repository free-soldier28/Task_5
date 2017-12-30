using System.Collections.Generic;
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