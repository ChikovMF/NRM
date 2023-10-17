using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.LogType
{
    public class CreateModel : PageModel
    {
        private readonly LogTypeService _logTypeService;

        public CreateModel(LogTypeService logTypeService)
        {
            _logTypeService = logTypeService;
        }

        [BindProperty]
        public Models.LogTypeModels.CreateModel Input { get; set; }

        public IActionResult OnGet()
        {
            return NotFound(); //!
        }

        public async Task<IActionResult> OnPost()
        {
            return NotFound(); //!
            if (ModelState.IsValid)
            {
                bool b = await _logTypeService.CreateLogType(Input);
                if (b) return RedirectToPage("Index");
                else ModelState.AddModelError(String.Empty, "Ошибка создания типа логов");
            }
            return Page();
        }
    }
}
