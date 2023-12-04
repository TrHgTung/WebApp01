using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebApp01.Controllers
{
    [Authorize(Roles = "Customer")]
    public class PaymentController : Controller
    {
        public IActionResult ShipCod()
        {
            return View();
        }
        
        //public IActionResult MoMo()
        //{
        //    return View();
        //}
    }
}
