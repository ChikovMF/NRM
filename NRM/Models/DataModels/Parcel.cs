using NRM.Models.AbstractModels;
using System.ComponentModel.DataAnnotations;

namespace NRM.Models.DataModels
{
    public class Parcel : AbstractParcel
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }

        public int TypeId { get; set; }
        public ParcelType Type { get; set; }
        public int? GroupParcelId { get; set; }
        public GroupParcel? GroupParcel { get; set; }

        public List<LogParcel>? LogParcels { get; set; }
    }
}
