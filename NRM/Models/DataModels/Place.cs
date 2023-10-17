namespace NRM.Models.DataModels
{
    public class Place
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        
        public List<User> Users { get; set; }
    }
}
