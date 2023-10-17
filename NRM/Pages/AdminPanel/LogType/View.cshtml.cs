using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.LogType
{
    public class ViewModel : PageModel
    {
        private readonly LogTypeService _logTypeService;

        public ViewModel(LogTypeService logTypeService)
        {
            _logTypeService = logTypeService;
        }

        public Models.LogTypeModels.ViewModel LogType { get; set; }

        public async Task OnGet(int id)
        {
            LogType = await _logTypeService.ViewLogType(id);
        }
    }
}
