using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
    public class PhotoItem : EntityBase<int, Guid>
    {
        private IList<Comment> _comments;

        public PhotoItem()
        {
            _comments = new List<Comment>();
        }
        public string UserName { get; set; }
        public Photo Photo { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Info { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IList<Comment> Comments { get { return _comments; } }
        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }
    }
}