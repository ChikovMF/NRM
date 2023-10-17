using NRM.Models.AbstractModels;

namespace NRM.Models.DataModels
{
    public class LogParcel : AbstractLog
    {
        public int ParcelId { get; set; }
        public Parcel Parcel { get; set; }
    }
}