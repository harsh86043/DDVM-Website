using Microsoft.AspNetCore.Identity;

namespace DDVM_Website.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}