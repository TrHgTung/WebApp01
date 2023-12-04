using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp01.Models
{
    public class CheckoutModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "Vui lòng nhập địa chỉ hợp lệ")]
        public string Address { get; set; }
        [Required, MinLength(4, ErrorMessage = "Vui lòng nhập tên thành phố hợp lệ")]
        public string City { get; set; }
        [Required, MinLength(9, ErrorMessage = "Vui lòng nhập số điện thoại hợp lệ")]
        public string Phone { get; set; }
        public string? Note { get; set; }
    }
}
