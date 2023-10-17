using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Models.UserModels;
using NRM.Services;
using System.Net;

namespace NRM.Pages.AdminPanel.User
{
    public class IndexModel : PageModel
    {
        private readonly AuthorizationService _authorizationService;

        public IndexModel(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public List<TableModel> Users { get; set; }

        public async Task OnGet()
        {
            Users = await _authorizationService.GetTableUsers();
        }
    }
}
