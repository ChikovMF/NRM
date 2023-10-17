using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.Parcel
{
    public class ExcelDataModel : PageModel
    {
        private readonly ExcelService _excelService;
        private readonly IWebHostEnvironment _env;

        public ExcelDataModel(ExcelService excelService, IWebHostEnvironment env)
        {
            _excelService = excelService;
            _env = env;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(IFormFile file)
        {
            if (ModelState.IsValid)
            {
                await _excelService.ImportExcel(file, User.Identity.Name);
                return RedirectToPage("Index");
            }
            return Page();
        }

        public IActionResult OnPostExport()
        {
            _excelService.ExportExcel();
            return File(Path.Combine(_env.WebRootPath, @"\excel\parcels.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"parcels {DateTime.Now}.xlsx");
        }
    }
}
