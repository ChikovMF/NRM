using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.Role
{
    public class ViewModel : PageModel
    {
        private readonly RoleService _roleService;

        public ViewModel(RoleService roleService)
        {
            _roleService = roleService;
        }

        public Models.RoleModels.ViewModel Role { get; set; }

        public async Task OnGet(int id)
        {
            Role = await _roleService.ViewRole(id);
        }
    }
}
