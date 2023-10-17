using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.Place
{
    public class CreateModel : PageModel
    {
        private readonly PlaceService _placeService;

        public CreateModel(PlaceService placeService)
        {
            _placeService = placeService;
        }

        [BindProperty]
        public Models.PlaceModels.CreateModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                bool b = await _placeService.CreatePlace(Input);
                if (b) return RedirectToPage("Index");
                else ModelState.AddModelError(String.Empty, "Ошибка создания места");
            }
            return Page();
        }
    }
}
