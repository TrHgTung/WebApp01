using Microsoft.AspNetCore.Mvc;

namespace WebApp01.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult ShipCod()
        {
            return View();
        }
        
        public IActionResult MoMo()
        {
            return View();
        }
    }
}
