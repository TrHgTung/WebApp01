using Microsoft.AspNetCore.Mvc;

namespace WebApp01.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
