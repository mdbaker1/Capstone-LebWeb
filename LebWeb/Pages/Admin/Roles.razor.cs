using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;

namespace LebWeb.Pages.Admin
{
    public partial class Roles
{
        [Inject] RoleManager<IdentityRole> _roleManager { get; set; }
        [Inject] IHttpContextAccessor httpContextAccessor { get;set; }
        private string RoleToAdd { get; set; }

        // Collection to display the existing users
        List<IdentityRole> AllRoles = new List<IdentityRole>();

        private async void AddRole()
        {
            if (RoleToAdd != string.Empty)
            {
                if (!await _roleManager.RoleExistsAsync(RoleToAdd))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = RoleToAdd
                    });
                    await GetAllRoles();
                }
                else
                {
                    return;
                }
            }
        }


        public bool IsAdmin;
        protected override async Task OnInitializedAsync()
        {
            IsAdmin = httpContextAccessor.HttpContext.User.IsInRole("Administrator");
            await Task.Delay(25);
            await GetAllRoles();
        }

        public async Task GetAllRoles()
        {
            AllRoles = new List<IdentityRole>();
            var role = _roleManager.Roles.Select(x => new IdentityRole
            {
                Id = x.Id,
                Name = x.Name
            });
            foreach (var item in role)
            {
                AllRoles.Add(item);
            }
        }
    }
}
