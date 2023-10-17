namespace NRM.Services.Queries
{
    public class ParcelFilterDropdownService
    {
        private AppDbContext _context {  get; set; }

        public ParcelFilterDropdownService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DropdownTuple> GetFilterDropDownValues(ParcelsFilterBy filterBy)
        {
            switch (filterBy)
            {
                case ParcelsFilterBy.NoFilter:
                    return new List<DropdownTuple>();
                case ParcelsFilterBy.ByTypes:
                    return _context.ParcelTypes
                        .Where(p => !p.IsDeleted)
                        .Select(p => new DropdownTuple
                        {
                            Text = p.Name,
                            Value = p.Id.ToString()
                        })
                        .Distinct()
                        .OrderBy(p => p.Text)
                        .ToList();
                case ParcelsFilterBy.ByStatus:
                    return _context.ParcelStatus
                        .Where(p => !p.IsDeleted)
                        .Select(p => new DropdownTuple
                        {
                            Text = p.Name,
                            Value = p.Id.ToString()
                        })
                        .Distinct()
                        .OrderBy(p => p.Text)
                        .ToList();
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterBy), filterBy, null);
            }
        }
    }
}
