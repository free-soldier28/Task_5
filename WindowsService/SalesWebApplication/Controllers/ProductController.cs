using System.Web.Mvc;
using WindowsService.BLL.Interfaces;

namespace SalesWebApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}