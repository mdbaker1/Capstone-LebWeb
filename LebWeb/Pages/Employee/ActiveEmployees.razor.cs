using LebWeb.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SupportLibrary.Data;
using SupportLibrary.Models;

namespace LebWeb.Pages.Employee
{
    public partial class ActiveEmployees
    {
        [Inject]
        IEmployeeDataService _employeeData { get; set; }
        [Inject]
        IJSRuntime _js { get; set; }

        private List<IEmployeeModel> employees;
        private bool showEditForm = false;
        private int idToUpdate = 0;

        private async Task HandleOnUpdate(IEmployeeModel employee)
        {
            if (idToUpdate > 0)
            {
                var e = employees.Where(x => x.Id == employee.Id).FirstOrDefault();

                if (e != null)
                {
                    e.FirstName = employee.FirstName;
                    e.LastName = employee.LastName;
                    e.HireDate = employee.HireDate;
                    e.TermDate = employee.TermDate;
                    e.PhotoLink = employee.PhotoLink;
                    e.IsActive = employee.IsActive;
                }
            }

            employees = (await _employeeData.GetAllActiveEmployees())
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ThenBy(x => x.HireDate).ToList();

            showEditForm = false;
        }

        private void UpdateEmployee(int id)
        {
            idToUpdate = id;
            showEditForm = true;
        }

        private async Task OnDeleteEmployee(int id, string firstname, string lastname)
        {
            if (!await _js.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {StringUtilities.CustomToUpper(firstname)} {StringUtilities.CustomToUpper(lastname)}?"))
            {
                return;
            }

            try
            {
                employees.RemoveAt(employees.FindIndex(x => x.Id == id));
                await _employeeData.DeleteEmployee(id);

            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", $"An error occurred {ex.Message}");
            }

        }

        protected override async Task OnParametersSetAsync()
        {

            employees = (await _employeeData.GetAllActiveEmployees())
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ThenBy(x => x.HireDate).ToList();
        }

        protected override async Task OnInitializedAsync()
        {

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    await _js.InvokeVoidAsync("SearchTable");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void OnCancelledUpdate()
        {
            showEditForm = false;
            idToUpdate = 0;
        }
    }
}
