using NRM.Models.DataModels;

namespace NRM.Models.ParcelModels
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string TrackNumber { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string PlaceOfDeliver { get; set; }
        public string PlaceOfDeparture { get; set; }
        public string MilitaryUnit { get; set; } = null!;
        public DateOnly DepartureDate { get; set; }
        public TimeOnly DepartureTime { get; set; }

        public Item Status { get; set; }
        public Item Type { get; set; }
        public ItemGroupParcel? GroupParcel { get; set; }

        public List<ItemLogParcels>? LogParcels { get; set; }

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class ItemGroupParcel
        {
            public int Id { get; set; }
            public string TrackNumber { get; set; }
        }

        public class ItemLogParcels
        {
            public int Id { get; set; }
            public string Message { get; set; }
            public string Type { get; set; }
        }
    }
}
