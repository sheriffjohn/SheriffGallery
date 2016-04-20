using System;

namespace Web.Models
{
    public class Comment : EntityBase<int, Guid>
    {
        public int PhotoItem_FK { get; set; }
        public virtual PhotoItem PhotoItem { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}