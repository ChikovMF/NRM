using System.ComponentModel.DataAnnotations;
using NRM.Models.AbstractModels;

namespace NRM.Models.DataModels
{
    public class GroupParcel : AbstractParcel
    {
        public int ResponsibleId { get; set; }
        public User Responsible { get; set; } = null!;
        public int PlaceOfDepartureId { get; set; }
        public Place PlaceOfDeparture { get; set; } = null!;
        public int PlaceOfDeliveryId { get; set; }
        public Place PlaceOfDelivery { get; set; } = null!;

        public ICollection<Parcel>? Parcels { get; set; }
        // public List<LogGroupParcel>? LogGroupParcels { get; set; }
    }
}
