using System;

namespace Web.Models
{
    public class User : EntityBase<int, Guid>
    {
        public string Name { get; set; }
        public Role Role { get; set; }
        public int Role_FK { get; set; }
    }
    
    public enum Permission
    {
        Guest,
        Contributor,
        Administrator
    }
}