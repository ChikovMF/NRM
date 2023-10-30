﻿namespace NRM.Models.DataModels
{
    public class Role
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<User>? Users { get; set; }
    }
}
