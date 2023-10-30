using NRM.Models.DataModels;

namespace NRM.Models.AbstractModels
{
    public class AbstractParcel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string TrackNumber { get; set; } = null!;
        public DateOnly DepartureDate { get; set; }
        public TimeOnly DepartureTime { get; set; }

        public int StatusId { get; set; }
        public ParcelStatus Status { get; set; } = null!;
    }
}
