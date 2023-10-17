using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Models.ParcelStatusModels;
using NRM.Services;

namespace NRM.Pages.AdminPanel.ParcelStatus
{
    public class IndexModel : PageModel
    {
        private readonly ParcelStatusService _parcelStatusService;

        public IndexModel(ParcelStatusService parcelStatusService)
        {
            _parcelStatusService = parcelStatusService;
        }

        public List<TableModel> ParcelStatus { get; set; }

        public async Task OnGet()
        {
            ParcelStatus = await _parcelStatusService.GetTableParcelStatus();
        }
    }
}
