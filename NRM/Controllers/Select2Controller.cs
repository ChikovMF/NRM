using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace NRM.Controllers
{
    public class Select2Controller : Controller
    {
        private AppDbContext _context { get; set; }

        public Select2Controller(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Select2/Parcels")]
        public async  Task<JsonResult> Parcels(string term)
        {
                var list = await _context.Parcels.Where(p => !p.IsDeleted && p.TrackNumber.StartsWith(term)).Select(s => new
                {
                    text = s.TrackNumber,
                    id = s.Id.ToString()
                }).ToListAsync();

                return new JsonResult(new
                {
                    items = list
                });
        }

        [HttpGet]
        [Route("Select2/Users")]
        public async Task<JsonResult> Users(string term)
        {
            var list = await _context.Users
                .Where(p => !p.IsDeleted && (p.LastName.StartsWith(term) || p.FirstName.StartsWith(term) || p.Patronymic.StartsWith(term) || p.Login.StartsWith(term)))
                .Select(s => new
            {
                text = $"{s.LastName} {s.FirstName} {s.Patronymic} ({s.Login})",
                id = s.Id.ToString()
            }).ToListAsync();

            return new JsonResult(new
            {
                items = list
            });
        }

        [HttpGet]
        [Route("Select2/Places")]
        public async Task<JsonResult> Places(string term)
        {
            var list = await _context.Places
                .Where(p => !p.IsDeleted && p.Name.StartsWith(term))
                .Select(s => new
                {
                    text = s.Name,
                    id = s.Id.ToString()
                }).ToListAsync();

            return new JsonResult(new
            {
                items = list
            });
        }
    }
}
