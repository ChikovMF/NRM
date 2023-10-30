namespace NRM.Models.ParcelModels
{
    public class TableModel
    {
        public int Id { get; set; }
        public string TrackNumber { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string DateTime { get; set; }
        public string PlaceOfDelivery { get; set; }
    }
}
