using Microsoft.EntityFrameworkCore;
using WebApp01.Models;

namespace WebApp01.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
            _context.Database.Migrate();
            if (!_context.Products.Any())
            {
                // danh muc :: CategoryModel
                CategoryModel sach_giao_khoa = new CategoryModel{ Name="Sach giao khoa", Slug="sgk", Description="Study Book", Status= 1};
                CategoryModel truyen_tranh = new CategoryModel{ Name="Truyen tranh", Slug="tt", Description="Comic", Status= 1};
                // nha xuat ban :: BrandModel
                BrandModel NXB_Giao_duc = new BrandModel { Name = "NXB Giao duc", Slug = "GD", Description = "For Student", Status = 1 };
                BrandModel NXB_Kim_Dong = new BrandModel { Name = "NXB Kim Dong", Slug = "KD", Description = "For Children", Status = 1 };

                _context.Products.AddRange(
                    new ProductModel { Name = "Toan lop 1", Slug = "sgk toan 1", Description = "tieu hoc", Image = "1.jpg", Category = sach_giao_khoa, Brand = NXB_Giao_duc, Price = 15000 },
                    new ProductModel { Name = "Co tich Viet Nam", Slug = "co tich VN", Description = "dan gian", Image = "2.jpg", Category = truyen_tranh, Brand = NXB_Kim_Dong, Price = 12000 }
                );
                _context.SaveChanges();
            }
        }
    }
}
