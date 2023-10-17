namespace NRM.Services.Queries
{
    public class SortFilterParcelPageOptions
    {
        public OrderByOptions OrderByOptions { get; set; }
        public ParcelsFilterBy FilterBy { get; set; }
        public string FilterValue { get; set; }
    }
}
