using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Models.LogParcelModels;
using NRM.Services;

namespace NRM.Pages.AdminPanel.LogParcel
{
    public class IndexModel : PageModel
    {
        private readonly LogParcelService _logParcelService;

        public IndexModel(LogParcelService logParcelService)
        {
            _logParcelService = logParcelService;
        }

        public List<TableModel> LogParcel { get; set; }

        public async Task OnGet()
        {
            LogParcel = await _logParcelService.GetTableLogParcels();
        }
    }
}
