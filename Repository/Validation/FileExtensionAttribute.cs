using System.ComponentModel.DataAnnotations;

namespace WebApp01.Repository.Validation
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName); // image1.jpg
                string[] extensions = { "jpg", "png", "jpeg"}; // cho phep extension hinh anh
                bool result = extensions.Any(x => extension.EndsWith(x));

                if(!result)
                {
                    return new ValidationResult("Required JPG PNG JPEG");
                }
            }
            return ValidationResult.Success;
        }
    }
}
