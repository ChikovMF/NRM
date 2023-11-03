using NRM.Models.DataModels;
using static NRM.Models.ParcelModels.ViewModel;

namespace NRM.Models.GroupParcelModels
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string TrackNumber { get; set; }
        public DateOnly DepartureDate { get; set; }
        public TimeOnly DepartureTime { get; set; }

        public Item PlaceOfDeparture { get; set; }
        public Item PlaceOfDelivery { get; set; }
        public Item Status { get; set; }

        public List<ParcelItem>? Parcels { get; set; }
        public List<ItemLogGroupParcels>? LogGroupParcels { get; set; }

        public class Item
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
        }

        public class ParcelItem
        {
            public int Id { get; set; }
            public string TrackNumber { get; set; }
            public Item Type { get; set; }
            public string Sender { get; set; }
            public string Recipient { get; set; }
            public string MilitaryUnit { get; set; }
        }

        public class ItemLogGroupParcels
        {
            public int Id { get; set; }
            public string Message { get; set; }
            public string Type { get; set; }
        }
    }
}
