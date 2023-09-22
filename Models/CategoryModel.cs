using System.ComponentModel.DataAnnotations;

namespace WebApp01.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [MinLength(4,ErrorMessage ="Yêu cầu nhập tên danh mục")]
        public string Name { get; set; }
        [MinLength(4, ErrorMessage = "Yêu cầu nhập mô tả danh mục")]
        public string Description { get; set; }
   
        public string Slug { get; set; }
        public int Status { get; set; }
    }
}
