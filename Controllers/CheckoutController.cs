using Microsoft.AspNetCore.Mvc;
using WebApp01.Models;
using WebApp01.Repository;

namespace WebApp01.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DataContext _dataContext;
        public CheckoutController (DataContext context)
        {
            _dataContext = context;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CheckoutModel checkout)
        {
            if(ModelState.IsValid)
            {
                _dataContext.Add(checkout);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Đã thêm thành công";
                return RedirectToAction("Create");
            }
            else
            {
                TempData["error"] = "Có lỗi xảy ra";
                List<string> errors = new List<string>();
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
        }
    }
}
