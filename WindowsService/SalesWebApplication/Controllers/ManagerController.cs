using System.Web.Mvc;
using WindowsService.BLL.DTO;
using WindowsService.BLL.Interfaces;

namespace SalesWebApplication.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }


        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GetManagers()
        {
            var managers = _managerService.GetManagers();
            return Json(managers);
        }


        [HttpPost]
        public JsonResult AddManager(string ManagerName)
        {
            int id = _managerService.AddManager(ManagerName);
            return Json(id);
        }



        [HttpPost]
        public void EditManager(ManagerDTO manager)
        {
            _managerService.EditManager(manager);
        }


        [HttpPost]
        public void DeleteManager(int Id)
        {
            _managerService.DeleteManager(Id);
        }



    }
}