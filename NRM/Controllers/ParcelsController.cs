using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NRM.Controllers
{
    public class ParcelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ParcelsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("api/[controller]/GetUnused")]
        public async Task<List<Item>> GetUnusedParcels(int? placeId)
        {
            return await _context.Parcels
                .Where(p => p.PlaceOfDeliveryId == placeId || p.PlaceOfDeliveryId == null)
                .Where(p => p.GroupParcelId == null)
                .Select(p => new Item
                {
                    Text = p.TrackNumber,
                    Value = p.Id
                })
                .ToListAsync();
        }

        public class Item
        {
            public int Value { get; set; }
            public string Text { get; set; } = null!;
        }
    }
}
