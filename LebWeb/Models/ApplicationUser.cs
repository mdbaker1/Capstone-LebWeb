using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LebWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First Name must contain between 2 and 20 characters.")]
        public string FirstName { get; set; } = String.Empty;
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Last Name must contain between 2 and 20 characters.")]
        public string LastName { get; set; } = String.Empty;
        public bool IsActive { get; set; }
    }
}
