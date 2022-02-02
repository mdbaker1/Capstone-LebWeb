using LebWeb.Helpers;
using LebWeb.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System;
using Microsoft.JSInterop;

namespace LebWeb.Pages.Admin
{
    
    public partial class Administration
    {
        [Inject] 
        UserManager<ApplicationUser> _userManager { get; set; }
        
        [Inject] 
        RoleManager<IdentityRole> _roleManager { get; set; }

        [Inject]
        IJSRuntime _js { get; set; }


        [CascadingParameter]
        private Task<AuthenticationState>? authenticationStateTask { get; set; }

        private readonly string ADMINISTRATION_ROLE = "Administrator";

        private ClaimsPrincipal? CurrentUser;

        private ApplicationUser objUser = new ApplicationUser();


        private string CurrentUserRole { get; set; } = "User";

        private List<ApplicationUser> ColUsers = new List<ApplicationUser>();

        private List<string> Options = new List<string>()
    {
        "User", "Administrator"
    };

        string strError = "";

        private bool ShowPopup = false;

        private string? modalHeader;

        protected override async Task OnInitializedAsync()
        {
            //await _js.InvokeVoidAsync("alert", "Hit Code Behind");
            await Task.Delay(1000);
            var RoleResult = await _roleManager.FindByNameAsync(ADMINISTRATION_ROLE);
            if (RoleResult == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(ADMINISTRATION_ROLE));
            }

            var user = await _userManager.FindByNameAsync("michael.baker@emerson.com");
            if (user != null)
            {
                var UserResult = await _userManager.IsInRoleAsync(user, ADMINISTRATION_ROLE);
                if (!UserResult)
                {
                    await _userManager.AddToRoleAsync(user, ADMINISTRATION_ROLE);
                }
            }

            CurrentUser = (await authenticationStateTask).User;

            await GetUsers();
        }

        protected void AddNewUser()
        {
            objUser = new ApplicationUser();

            objUser.Id = "";
            ShowPopup = true;
            modalHeader = "Add User";
        }

        protected async Task SaveUser()
        {
            try
            {
                string passwordHash = "********";
                // Is this an existing user?
                if (objUser.Id != "")
                {
                    // Get the user
                    var user = await _userManager.FindByIdAsync(objUser.Id);
                    // Update Username;
                    user.UserName = objUser.Email.ToLower();
                    // Update Email
                    user.Email = objUser.Email.ToLower();
                    // Update FirstName
                    user.FirstName = objUser.FirstName.ToLower();
                    // Update LastName
                    user.LastName = objUser.LastName.ToLower();
                    // Update the user
                    await _userManager.UpdateAsync(user);
                    // Only update password if the current value
                    // is not the default value
                    if (objUser.PasswordHash != passwordHash)
                    {
                        var resetToken =
                            await _userManager.GeneratePasswordResetTokenAsync(user);
                        var passworduser =
                            await _userManager.ResetPasswordAsync(
                                user,
                                resetToken,
                                objUser.PasswordHash);
                        if (!passworduser.Succeeded)
                        {
                            if (passworduser.Errors.FirstOrDefault() != null)
                            {
                                strError =
                                    passworduser
                                    .Errors
                                    .FirstOrDefault()
                                    .Description;
                            }
                            else
                            {
                                strError = "Pasword error";
                            }
                            // Keep the popup opened
                            return;
                        }
                    }
                    // Handle Roles
                    // Is user in administrator role?
                    var UserResult =
                        await _userManager
                        .IsInRoleAsync(user, ADMINISTRATION_ROLE);
                    // Is Administrator role selected
                    // but user is not an Administrator?
                    if (
                        (CurrentUserRole == ADMINISTRATION_ROLE)
                        &
                        (!UserResult))
                    {
                        // Put admin in Administrator role
                        await _userManager
                            .AddToRoleAsync(user, ADMINISTRATION_ROLE);
                    }
                    else
                    {
                        // Is Administrator role not selected
                        // but user is an Administrator?
                        if ((CurrentUserRole != ADMINISTRATION_ROLE)
                            &
                            (UserResult))
                        {
                            // Remove user from Administrator role
                            await _userManager
                                .RemoveFromRoleAsync(user, ADMINISTRATION_ROLE);
                        }
                    }
                }
                else
                {
                    // Insert new user
                    var NewUser =
                        new ApplicationUser
                        {
                            UserName = objUser.Email.ToLower(),
                            Email = objUser.Email.ToLower(),
                            FirstName = objUser.FirstName.ToLower(),
                            LastName = objUser.LastName.ToLower(),
                            EmailConfirmed = true,
                            IsActive = true
                        };
                    var CreateResult =
                        await _userManager
                        .CreateAsync(NewUser, objUser.PasswordHash);
                    if (!CreateResult.Succeeded)
                    {
                        if (CreateResult
                            .Errors
                            .FirstOrDefault() != null)
                        {
                            strError = CreateResult
                                .Errors
                                .FirstOrDefault()
                                .Description;
                        }
                        else
                        {
                            strError = "Create error";
                        }
                        // Keep the popup opened
                        return;
                    }
                    else
                    {
                        // Handle Roles
                        if (CurrentUserRole == ADMINISTRATION_ROLE)
                        {
                            // Put admin in Administrator role
                            await _userManager
                                .AddToRoleAsync(NewUser, ADMINISTRATION_ROLE);
                        }
                    }
                }
                // Close the Popup
                ShowPopup = false;
                // Refresh Users
                await GetUsers();
            }
            catch (Exception ex)
            {
                strError = ex.GetBaseException().Message;
            }
        }

        protected async Task EditUser(ApplicationUser _IdentityUser)
        {
            // Set the selected user as the current user
            objUser = _IdentityUser;
            objUser.FirstName = @StringUtilities.CustomToUpper(objUser.FirstName);
            objUser.LastName = @StringUtilities.CustomToUpper(objUser.LastName);

            // Set modal title to Edit User
            modalHeader = "Edit User";
            // Get the user
            var user = await _userManager.FindByIdAsync(objUser.Id);
            if (user != null)
            {
                // Is user in administrator role?
                var UserResult =
                    await _userManager
                    .IsInRoleAsync(user, ADMINISTRATION_ROLE);
                if (UserResult)
                {
                    CurrentUserRole = ADMINISTRATION_ROLE;
                }
                else
                {
                    CurrentUserRole = "User";
                }
            }
            // Open the Popup
            ShowPopup = true;
        }

        protected async Task DeleteUser()
        {
            var result = await _js.InvokeAsync<bool>("confirm", "Are you sure you want to delete this record?");
            if (result)
            {
                // Close the Popup
                ShowPopup = false;
                // Get the user
                var user = await _userManager.FindByIdAsync(objUser.Id);
                if (user != null)
                {
                    // Delete the user
                    await _userManager.DeleteAsync(user);
                }
                // Refresh Users
                await GetUsers();
            }
            else
            {
                return;
            }
        }

        protected void ClosePopup()
        {
            ShowPopup = false;
            objUser = null;
            CurrentUserRole = null;
            strError = "";
        }

        protected async Task GetUsers()
        {
            strError = "";
            ColUsers = new List<ApplicationUser>();
            IQueryable<ApplicationUser> users = _userManager.Users.Select(x => new ApplicationUser
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PasswordHash = "********",
                FirstName = x.FirstName,
                LastName = x.LastName
            })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName);

            foreach (var item in users)
            {
                ColUsers.Add(item);
            }
        }
    }
}
