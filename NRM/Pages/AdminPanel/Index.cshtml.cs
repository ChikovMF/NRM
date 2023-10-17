using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;
using System.Text.Json;

namespace NRM.Pages.AdminPanel
{
    public class IndexModel : PageModel
    {
        private readonly ChartService _chartService;

        public IndexModel(ChartService chartService)
        {
            _chartService = chartService;
        }

        public string Data1 { get; set; }

        public async Task OnGet()
        {
            var data = await _chartService.GetDataForChart1();
            Data1 = JsonSerializer.Serialize(data);
        }
    }
}
