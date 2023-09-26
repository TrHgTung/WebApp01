using Microsoft.AspNetCore.Identity;

namespace WebApp01.Models
{
    public class AppUserModel :IdentityUser
    {
        public string Occupation { get; set; }
    }
}
