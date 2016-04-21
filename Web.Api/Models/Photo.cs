using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Models
{
    public class Photo : EntityBase<int, Guid>
    {
        public byte[] Binary { get; set; }
        public virtual PhotoItem PhotoItem { get; set; }
    }
}