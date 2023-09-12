using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp01.Models;
using WebApp01.Repository;

namespace WebApp01.Controllers
{
    public class CategoryController : Controller
    {
		private readonly DataContext _dataContext;
		public CategoryController(DataContext context) 
        { 
            _dataContext = context; // lay du lieu
        }
		public async Task<IActionResult> Index(string Slug = "")
        {
            CategoryModel category = _dataContext.Categories.Where(c =>  c.Slug == Slug).FirstOrDefault();
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var productsByCategory = _dataContext.Products.Where(p => p.CategoryId == category.Id);
            return View(await productsByCategory.OrderByDescending(p => p.Id).ToListAsync());
        }

      
    }
}
