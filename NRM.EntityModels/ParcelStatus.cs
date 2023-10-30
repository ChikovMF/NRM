using NRM.Models.AbstractModels;

namespace NRM.Models.DataModels
{
    public class ParcelStatus
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<AbstractParcel>? Parcels { get; set; }
    }
}
