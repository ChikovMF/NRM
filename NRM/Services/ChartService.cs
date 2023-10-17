using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace NRM.Services
{
    public class ChartService
    {
        private AppDbContext _context { get; set; }

        public ChartService(AppDbContext context)
        {
            _context = context;
        }

        public class Data
        {
            public string label { get; set; }
            public int count { get; set; }
        }

        public async Task<List<Data>> GetDataForChart1()
        {
            return await _context.GroupParcels.Where(w => !w.IsDeleted).GroupBy(g => g.PlaceOfDeliveryId).Select(s => new Data
            {
                label = s.First().PlaceOfDelivery.Name,
                count = s.Count()
            }).ToListAsync();
        }
    }
}
