using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApp01.Models;
using WebApp01.Models.ViewModels;
using WebApp01.Repository;

namespace WebApp01.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private readonly DataContext _dataContext;
        public CartController (DataContext _context)
        {
            _dataContext = _context;
        }
        public IActionResult Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemViewModel cartVM = new()
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(x => x.Price * x.Quantity),
            };
            return View(cartVM);
        }

        public IActionResult Checkout()
        {
            return View("~/Views/Checkout/Index.cshtml");
        }
        
        public async Task<IActionResult> Add(int Id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(Id);
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemModel cartItems = cart.Where(c => c.ProductId == Id).FirstOrDefault();

            if(cartItems == null)
            {
                cart.Add(new CartItemModel(product));
            }
            else
            {
                cartItems.Quantity += 1;
            }
            HttpContext.Session.SetJson("Cart", cart);

            TempData["success"] = "Đã thêm thành công";
			return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Decrease(int Id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();
            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity; // nhấn nút Decrease (dấu trừ) thì sẽ giảm số lượng trong giỏ hàng
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == Id); // nếu số lượng = 0 thì cart = 0
            }
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart"); // số lượng cart (phiên) = 0 thì hủy cart
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
                // thiet lap  cart mới
            }
            TempData["success"] = "Đã giảm 1 đơn vị";
            return RedirectToAction("Index");
        }

        public IActionResult Increase(int Id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();
            if (cartItem.Quantity > 0)
            {
                ++cartItem.Quantity; // nhấn nút Increase (dấu cong) thì sẽ tang số lượng trong giỏ hàng
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == Id); // nếu số lượng = 0 thì cart = 0
            }
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart"); // số lượng cart (phiên) = 0 thì hủy cart
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
                // thiet lap  cart mới
            }
            TempData["success"] = "Đã tăng 1 đơn vị";
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int Id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            cart.RemoveAll(p => p.ProductId == Id);
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            TempData["success"] = "Đã xóa thành công";
            return RedirectToAction("Index");
        }

        public IActionResult Clear(int Id) // hủy hoàn toàn cart
        {
            HttpContext.Session.Remove("Cart");
            TempData["success"] = "Đã xóa thành công";

            return RedirectToAction("Index");
        }
    }
}
