using Microsoft.AspNetCore.Mvc;

namespace WebApp01.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index() { 
            return View(); 
        }
        public IActionResult Details() { 
            return View(); 
        }
    }
}
