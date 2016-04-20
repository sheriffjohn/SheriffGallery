using System;

namespace Web.Models
{
    public class Photo : EntityBase<int, Guid>
    {
        public byte[] Binary { get; set; }
        public PhotoItem PhotoItem { get; set; }
    }
}