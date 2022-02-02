using LebWeb.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SupportLibrary.Data;
using SupportLibrary.Models;

namespace LebWeb.Reports
{
    public partial class BonusHours5
    {
        [Inject]
        IEmployeeDataService _employeeData { get; set; }
        [Inject]
        IJSRuntime _js { get; set; }

        private List<IEmployeeModel> employees;
       
        protected override async Task OnParametersSetAsync()
        {
            employees = (await _employeeData.GetEmployeeBonusHours5()).ToList();
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
    }
}
