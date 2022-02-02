using Microsoft.AspNetCore.Identity;

namespace LebWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public bool IsActive { get; set; }
    }
}
