using Microsoft.AspNetCore.Components.Forms;
using SupportLibrary.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LebWeb.Models
{
    public class DisplayEmployeeModel : IEmployeeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First Name must contain between 2 and 20 characters.")]
        public string FirstName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Last Name must contain between 2 and 20 characters.")]
        public string LastName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Hire Date is required.")]
        public DateTime HireDate { get; set; } = DateTime.Now;

        public DateTime? TermDate { get; set; } = null;

        public string? PhotoLink { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
