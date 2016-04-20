using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Infrastructure.Repository
{
    public class Repository : IRepository
    {
        private EFDbContext _context;
        public Repository()
        {
            _context = new EFDbContext();
        }

        public bool CreateOrUpdatePhotoItem(PhotoItem item)
        {
            var entry = _context.Entry(item);
            
            if (entry.State == EntityState.Detached)
                if (item.Id < 1)
                    _context.PhotoItems.Add(item);

            _context.SaveChanges();
            
            if (_context.PhotoItems.Where(x => x.Id == item.Id).Count() > 0)
                return true;
            else
                return false;
        }

        public bool CreateOrUpdateUser(User user)
        {
            var entry = _context.Entry(user);

            if (entry.State == EntityState.Detached)
                if (user.Id < 1)
                    _context.Users.Add(user);

            _context.SaveChanges();

            if (_context.Users.Where(x => x.Id == user.Id).Count() > 0)
                return true;
            else
                return false;
        }

        public PhotoItem ReadPhotoItem(int id)
        {
            return _context.PhotoItems.Find(id);
        }

        public List<PhotoItem> ReadPhotoItems()
        {
            return _context.PhotoItems.ToList();
        }

        public User ReadUser(int id)
        {
            return _context.Users.Find(id);
        }

        public List<User> ReadUsers()
        {
            throw new NotImplementedException();
        }        

        public bool DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeletePhotoItem(PhotoItem item)
        {
            _context.PhotoItems.Remove(item);
            _context.SaveChanges();

            if (_context.PhotoItems.Find(item.Id) != null)
                return false;
            else return true;
        }

        public bool DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();

            if (_context.Comments.Find(comment.Id) != null)
                return false;
            else return true;
        }

        public List<Comment> ReadComments()
        {
            return _context.Comments.ToList();
        }

        public bool UpdateComment(Comment comment)
        {
            _context.SaveChanges();

            if (_context.Comments.Where(x => x.Id == comment.Id).FirstOrDefault().Text != comment.Text)
                return true;
            else
                return false;
        }
    }
}