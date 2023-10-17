namespace NRM.Models.ParcelTypeModels
{
    public class ViewModel
    {
        public string Name { get; set; }
        public List<ParcelItem>? Parcels { get; set; }

        public class ParcelItem
        {
            public int Id { get; set; }
            public string TrackNumber { get; set; }
            public string Sender { get; set; }
            public string Recipient { get; set; }
        }
    }
}
