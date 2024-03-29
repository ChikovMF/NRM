﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using NRM.Services;

namespace NRM.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ParcelService _parcelService;

        public IndexModel(ParcelService parcelService)
        {
            _parcelService = parcelService;
        }

        [BindProperty,
            Required(ErrorMessage = "Введите трек-номер"), DisplayName("Трек-номер")]
        public string TrackNumber { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var id = await _parcelService.ExistenceCheck(TrackNumber);
                if (id != 0) return RedirectToPage("Parcel", new { id = id });
                else ModelState.AddModelError(String.Empty, $"РПО с трек-номером \"{TrackNumber}\" не существует");
            }
            return Page();
        }
    }
}