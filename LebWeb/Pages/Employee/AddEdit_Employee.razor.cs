using LebWeb.Helpers;
using LebWeb.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using SupportLibrary.Data;
using SupportLibrary.Interfaces;
using SupportLibrary.Models;

namespace LebWeb.Pages.Employee
{
    public partial class AddEdit_Employee
    {
        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public EventCallback<IEmployeeModel> OnUpdate { get; set; }

        [Parameter]
        public EventCallback OnCancelledUpdate { get; set; }

        [Inject]
        IJSRuntime _js { get; set; }

        [Inject]
        NavigationManager _navManager { get; set; }

        [Inject]
        IFileUpload FileUpload { get; set; }

        [Inject]
        IEmployeeDataService _employeeData { get; set; }

        private IEmployeeModel employee = new DisplayEmployeeModel();

        private string formTitle = "";
        private bool IsEdit;
        private string ButtonText = "";
        private string ErrorMessage = "";
        private string? CurrentPhoto;
        private string? OriginalEmployeeImage;
        private bool InputDisabled = false;

        protected override async Task OnParametersSetAsync()
        {
            if (Id > 0)
            {
                var e = await _employeeData.GetEmployee(Id);

                employee.Id = e.Id;
                employee.FirstName = StringUtilities.CustomToUpper(e.FirstName);
                employee.LastName = StringUtilities.CustomToUpper(e.LastName);
                employee.HireDate = e.HireDate;
                employee.TermDate = e.TermDate;
                employee.PhotoLink = e.PhotoLink;
                employee.IsActive = e.IsActive;

                formTitle = "Update Employee";
                ButtonText = "Save Changes";
                IsEdit = true;
                OriginalEmployeeImage = employee.PhotoLink;
                if (employee.PhotoLink != null)
                {
                    InputDisabled = true;
                }
            }
            else
            {
                employee.IsActive = true;
                formTitle = "Create Employee";
                ButtonText = "Save";
                IsEdit = false;
            }
        }


        public async Task Cancel()
        {
            if (employee.PhotoLink != OriginalEmployeeImage)
            {
                DeleteImage(employee.PhotoLink);
            }
            await OnCancelledUpdate.InvokeAsync();
        }

        private async void DeleteImage(string? imageUrl)
        {
            try
            {
                if (!IsEdit && imageUrl != null)
                {
                    FileUpload.DeleteFile(imageUrl);
                    employee.PhotoLink = null;
                }
                else if (IsEdit && OriginalEmployeeImage != employee.PhotoLink && employee.PhotoLink != null)
                {

                    FileUpload.DeleteFile(employee.PhotoLink);
                    employee.PhotoLink = null;
                }
                else
                {
                    CurrentPhoto = imageUrl;
                    employee.PhotoLink = null;
                }

                InputDisabled = false;
            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private async Task HandleImageUpload(InputFileChangeEventArgs e)
        {
            try
            {
                if (e.GetMultipleFiles().Count > 0)
                {

                    foreach (var image in e.GetMultipleFiles())
                    {
                        FileInfo fileInfo = new FileInfo(image.Name);
                        if (fileInfo.Extension.ToLower() == ".jpg" ||
                            fileInfo.Extension.ToLower() == ".jpeg" ||
                            fileInfo.Extension.ToLower() == ".png")
                        {
                            var uploadedImagePath = await FileUpload.UploadFile(image);
                            if (CurrentPhoto != null)
                            {
                                DeleteImage(CurrentPhoto);
                            }
                            CurrentPhoto = uploadedImagePath;
                        }
                        else
                        {
                            await _js.InvokeVoidAsync("alert", "Please select .jpg/.jpeg/.png file only");
                            return;
                        }
                    }

                    if (CurrentPhoto != null)
                    {
                        employee.PhotoLink = CurrentPhoto;
                        InputDisabled = true;
                    }
                    else
                    {
                        await _js.InvokeVoidAsync("alert", "Image uploading failed");
                        InputDisabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task HandleValidSubmit()
        {
            if (IsEdit)
            {
                if (employee.HireDate > employee.TermDate)
                {
                    ErrorMessage = "Employee Hire Date cannot be greater than Employee Term Date";
                    return;
                }

                employee.FirstName = StringUtilities.CustomToLower(employee.FirstName);
                employee.LastName = StringUtilities.CustomToLower(employee.LastName);

                await _employeeData.UpdateEmployee(employee);

                if (OriginalEmployeeImage != null && OriginalEmployeeImage != employee.PhotoLink)
                {
                    FileUpload.DeleteFile(OriginalEmployeeImage);
                }
                await OnUpdate.InvokeAsync(employee);
            }
            else
            {
                await _employeeData.CreateEmployee(employee);
                await OnUpdate.InvokeAsync(employee);
            }
        }
    }
}
