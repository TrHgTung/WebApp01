using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp01.Models;
using WebApp01.Repository;

namespace WebApp01.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        public ProductController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Products.OrderByDescending(p => p.Id).Include(p => p.Category).Include(p => p.Brand).ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name");
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name");
            return View();
        }

        //public async Task<IActionResult> Create(ProductModel product)
        //{
        //    ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
        //    ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);
        //    return View(product);
        //}
    }
}
