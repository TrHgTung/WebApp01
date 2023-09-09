using Microsoft.AspNetCore.Mvc;

namespace WebApp01.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View("~/Views/Checkout/Index.cshtml");
        }
    }
}
