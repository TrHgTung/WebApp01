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
			return Redirect(Request.Headers["Referer"].ToString());
        }
        
        public async Task<IActionResult> Decrease (int Id)
        {
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();
            if(cartItem.Quantity > 1) 
            {
                --cartItem.Quantity; // nhấn nút Decrease (dấu trừ) thì sẽ giảm số lượng trong giỏ hàng
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == Id); // nếu số lượng = 0 thì cart = 0
            }
            if(cart.Count == 0) {
				HttpContext.Session.Remove("Cart"); // số lượng cart (phiên) = 0 thì hủy cart
			}
			else
            {
				HttpContext.Session.SetJson("Cart", cart);
				// thiet lap  cart mới
			}
			return RedirectToAction("Index");
        }

		public async Task<IActionResult> Increase(int Id)
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
			return RedirectToAction("Index");
		}

        public async Task<IActionResult> Remove(int Id)
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
            return RedirectToAction("Index");
		}

        public async Task<IActionResult> Clear(int Id) // hủy hoàn toàn cart
        {
			HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
		}
	}
}
