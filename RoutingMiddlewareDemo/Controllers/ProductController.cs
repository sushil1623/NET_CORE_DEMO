using Microsoft.AspNetCore.Mvc;

namespace RoutingMiddlewareDemo.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult AddProduct()
        {
            return View();
        }
    }
}
