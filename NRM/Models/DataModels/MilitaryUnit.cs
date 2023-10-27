namespace NRM.Models.DataModels
{
    public class MilitaryUnit
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int PlaceId { get; set; }
        public Place Place { get; set; } = null!;
    }
}
