using System.ComponentModel.DataAnnotations;
using NRM.Models.AbstractModels;

namespace NRM.Models.DataModels
{
    public class GroupParcel : AbstractParcel
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int? PlaceOfDepartureId { get; set; }
        public Place? PlaceOfDeparture { get; set; }
        public int? PlaceOfDeliveryId { get; set; }
        public Place? PlaceOfDelivery { get; set; }

        public List<Parcel>? Parcels { get; set; }
        public List<LogGroupParcel>? LogGroupParcels { get; set; }
    }
}
