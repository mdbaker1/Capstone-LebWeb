
namespace SupportLibrary.Models
{
    public interface IEmployeeModel
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime HireDate { get; set; }
        DateTime? TermDate { get; set; }
        string? PhotoLink { get; set; }
        bool IsActive { get; set; }
    }
}