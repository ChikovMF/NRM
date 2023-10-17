namespace NRM.Models.RoleModels
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserItem>? Users { get; set; }

        public class UserItem
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Login { get; set; }
        }
    }
}
