using Microsoft.AspNetCore.Mvc;
using WebApp01.Models;
using WebApp01.Models.ViewModels;
using WebApp01.Repository;

namespace WebApp01.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _dataContext;
        public CartController (DataContext _context)
        {
            _dataContext = _context;
        }
        public ActionResult Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemViewModel cartVM = new()
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(x => x.Price * x.Quantity),
            };
            return View(cartVM);
        }

        public ActionResult Checkout()
        {
            return View("~/Views/Checkout/Index.cshtml");
        }
    }
}
