using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.LogParcel
{
    public class ViewModel : PageModel
    {
        private readonly LogParcelService _logParcelService;

        public ViewModel(LogParcelService logParcelService)
        {
            _logParcelService = logParcelService;
        }

        public Models.LogParcelModels.ViewModel LogParcel { get; set; }

        public async Task OnGet(int id)
        {
            LogParcel = await _logParcelService.ViewLogParcel(id);
        }
    }
}
