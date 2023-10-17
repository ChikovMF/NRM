using NRM.Models.AbstractModels;

namespace NRM.Models.DataModels
{
    public class ParcelStatus
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }

        public List<AbstractParcel>? Parcels { get; set; }
    }
}
