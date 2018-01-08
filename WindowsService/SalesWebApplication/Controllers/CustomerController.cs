using System.Web.Mvc;
using WindowsService.BLL.DTO;
using WindowsService.BLL.Interfaces;

namespace SalesWebApplication.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GetCustomers()
        {
            var customers = _customerService.GetCustomers();
            return Json(customers);
        }


        [HttpPost]
        public JsonResult AddCustomer(string CustomerName)
        {
            int id = _customerService.AddCustomer(CustomerName);
            return Json(id);
        }



        [HttpPost]
        public void EditCustomer(CustomerDTO customer)
        {
            _customerService.EditCustomer(customer);
        }


        [HttpPost]
        public void DeleteCustomer(int Id)
        {
            _customerService.DeleteCustomer(Id);
        }
    }
}