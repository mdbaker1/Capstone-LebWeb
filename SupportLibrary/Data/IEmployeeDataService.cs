using SupportLibrary.Models;

namespace SupportLibrary.Data
{
    public interface IEmployeeDataService
    {
        Task CreateEmployee(IEmployeeModel employee);
        Task DeleteEmployee(int id);
        Task<IEmployeeModel> GetEmployee(int id);
        Task<List<IEmployeeModel>> GetEmployees();
        Task UpdateEmployee(IEmployeeModel employee);
        Task<List<IEmployeeModel>> GetTop100();
        Task<List<IEmployeeModel>> GetAllActiveEmployees();
        Task<List<IEmployeeModel>> GetAllInActiveEmployees();
        Task<List<IEmployeeModel>> GetEmployeeBonusHours5();
        Task<List<IEmployeeModel>> GetEmployeeBonusHours8();
    }
}