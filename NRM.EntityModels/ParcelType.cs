namespace NRM.Models.DataModels
{
    public class ParcelType
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Parcel>? Parcels { get; set; }
    }
}
