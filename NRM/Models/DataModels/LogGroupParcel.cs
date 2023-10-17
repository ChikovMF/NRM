using NRM.Models.AbstractModels;

namespace NRM.Models.DataModels
{
    public class LogGroupParcel : AbstractLog
    {
        public int GroupParcelId { get; set; }
        public GroupParcel GroupParcel { get; set; }
    }
}
