using NRM.Models.AbstractModels;
using System.ComponentModel.DataAnnotations;

namespace NRM.Models.DataModels
{
    public class Parcel : AbstractParcel
    {
        public string Sender { get; set; } = null!;
        public string Recipient { get; set; } = null!;

        public int TypeId { get; set; }
        public ParcelType Type { get; set; } = null!;
        public int? GroupParcelId { get; set; }
        public GroupParcel? GroupParcel { get; set; }
        public int? PlaceOfDepartureId { get; set; }
        public Place? PlaceOfDeparture { get; set; }
        public int? PlaceOfDeliveryId { get; set; }
        public Place? PlaceOfDelivery { get; set; }

        // public ICollection<LogParcel>? LogParcels { get; set; }
    }
}
