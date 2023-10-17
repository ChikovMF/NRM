using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Models.RoleModels;
using NRM.Services;

namespace NRM.Pages.AdminPanel.Role
{
    public class IndexModel : PageModel
    {
        private readonly RoleService _roleService;

        public IndexModel(RoleService roleService)
        {
            _roleService = roleService;
        }

        public List<TableModel> Roles { get; set; }

        public async Task OnGet()
        {
            Roles = await _roleService.GetTableRoles();
        }
    }
}
