using System;

namespace Web.Models
{
    public class User : EntityBase<int, Guid>
    {
        public string Name { get; set; }
    }
}