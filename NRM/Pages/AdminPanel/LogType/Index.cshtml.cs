using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Models.LogTypeModels;
using NRM.Services;

namespace NRM.Pages.AdminPanel.LogType
{
    public class IndexModel : PageModel
    {
        private readonly LogTypeService _logTypeService;

        public IndexModel(LogTypeService logTypeService)
        {
            _logTypeService = logTypeService;
        }

        public List<TableModel> LogTypes { get; set; }

        public async Task OnGet()
        {
            LogTypes = await _logTypeService.GetTableLogTypes();
        }
    }
}
