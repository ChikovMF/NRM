using NRM.Models.DataModels;

namespace NRM.Models.ParcelModels
{
    public class PublicViewModel
    {
        public int Id { get; set; }
        public string TrackNumber { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public DateOnly DepartureDate { get; set; }
        public TimeOnly DepartureTime { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
