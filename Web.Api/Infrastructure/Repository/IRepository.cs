using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Models;

namespace Web.Api.Infrastructure.Repository
{
    public interface IRepository
    {
        bool CreateOrUpdateUser(User user);
        List<User> ReadUsers();
        bool DeleteUser(User user);
        bool CreateOrUpdatePhotoItem(PhotoItem item);
        PhotoItem ReadPhotoItem(int id);
        bool DeletePhotoItem(PhotoItem item);
        List<PhotoItem> ReadPhotoItems();
        bool DeleteComment(Comment comment);
        List<Comment> ReadComments();
        bool UpdateComment(Comment comment);
    }
}
