using Microsoft.AspNetCore.Mvc;

namespace RoutingMiddlewareDemo.Controllers
{
    
    public class ProductController : Controller
    {
        [Route("AddProduct")]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [Route("InsertProduct")]
        
        public IActionResult AddProduct([FromBody] Products product)
        {
            return Ok(new { message = "Product has been added successfully" });
        }
    }
}
