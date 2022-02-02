using SupportLibrary.DataAccess;
using SupportLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportLibrary.Data
{
    public class EmployeeSqlDataService : IEmployeeDataService
    {
        private readonly ISqlDataAccess _dataAccess;

        public EmployeeSqlDataService(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task CreateEmployee(IEmployeeModel employee)
        {
            var param = new
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                HireDate = employee.HireDate,
                TermDate = "",
                PhotoLink = employee.PhotoLink,
                IsActive = true
            };
            await _dataAccess.SaveData("dbo.spEmployee_Create", param, "DefaultConnection");
        }

        public async Task<List<IEmployeeModel>> GetEmployees()
        {
            var employees =  await _dataAccess.LoadData<EmployeeModel, dynamic>("dbo.spEmployees_Get", new { }, "DefaultConnection");

            return employees.ToList<IEmployeeModel>();
        }

        public async Task<IEmployeeModel> GetEmployee(int id)
        {
            var employee = await _dataAccess.LoadData<EmployeeModel, dynamic>("dbo.spEmployee_Get", new { Id = id }, "DefaultConnection");

            return employee.FirstOrDefault();
        }

        public async Task UpdateEmployee(IEmployeeModel employee)
        {
            await _dataAccess.SaveData("dbo.spEmployee_Update", employee, "DefaultConnection");
        }

        public async Task DeleteEmployee(int id)
        {
            await _dataAccess.SaveData("dbo.spEmployee_Delete", new { Id = id }, "DefaultConnection");
        }

        public async Task<List<IEmployeeModel>> GetTop100()
        {
            var employees = await _dataAccess.LoadData<EmployeeModel, dynamic>("dbo.spEmployees_GetTop100", new { }, "DefaultConnection");

            return employees.ToList<IEmployeeModel>();
        }

        public async Task<List<IEmployeeModel>> GetAllActiveEmployees()
        {
            var employees = await _dataAccess.LoadData<EmployeeModel, dynamic>("dbo.spEmployees_GetAllActiveEmployees", new { }, "DefaultConnection");

            return employees.ToList<IEmployeeModel>();
        }

        public async Task<List<IEmployeeModel>> GetAllInActiveEmployees()
        {
            var employees = await _dataAccess.LoadData<EmployeeModel, dynamic>("dbo.spEmployees_GetAllInActiveEmployees", new { }, "DefaultConnection");

            return employees.ToList<IEmployeeModel>();
        }

        public async Task<List<IEmployeeModel>> GetEmployeeBonusHours5()
        {
            var employees = await _dataAccess.LoadData<EmployeeModel, dynamic>
                ("dbo.spEmployees_GetLastQuarterBonusHours5", new { }, "DefaultConnection");

            return employees.ToList<IEmployeeModel>();
        }

        public async Task<List<IEmployeeModel>> GetEmployeeBonusHours8()
        {
            var employees = await _dataAccess.LoadData<EmployeeModel, dynamic>
                ("dbo.spEmployees_GetLastQuarterBonusHours8", new { }, "DefaultConnection");

            return employees.ToList<IEmployeeModel>();
        }
    }

    
}
