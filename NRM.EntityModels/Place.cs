namespace NRM.Models.DataModels
{
    public class Place
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; } = null!;
        
        public ICollection<MilitaryUnit>? MilitaryUnits { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
