using NRM.Models.DataModels;

namespace NRM.Models.GroupParcelModels
{
    public class TableModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string TrackNumber { get; set; }
        public string PlaceOfDeparture { get; set; }
        public string PlaceOfDelivery { get; set; }
        public int ParcelCount { get; set; }
        public string DepartureDateTime { get; set; }
    }
}
