using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using Web.Infrastructure;
using Web.Infrastructure.Repository;
using Web.Models;

namespace Web.Tests.IntegrationTest
{
    [TestFixture]
    public class DbIntegrationTest
    {
        /****** Script for veirfy results  ******/
        //select a.*, b.RoleName from[imagegallery].[dbo].[users] a join [imagegallery].[dbo].roles b on a.Role_FK = b.Id
        //go
        //select* from[imagegallery].[dbo].photoitems
        //go
        //select* from [imagegallery].[dbo].photos
        //go
        //select* from [imagegallery].[dbo].comments
        //go
        //select* from [imagegallery].[dbo].roles

        IRepository _repository;

        [SetUp]
        public void Init()
        {
            _repository = new Repository();
            var localPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "");
            AppDomain.CurrentDomain.SetData("DataDirectory", localPath); //test projects need DataDirectory to be specified
        }

        [Test]
        public void CreateAndUpdateUserTest()
        {
            var context = new EFDbContext();

            User user = new User() { Name = "John", Role_FK = (int)Permission.Guest };
            _repository.CreateOrUpdateUser(user);

            user.Name = "Jane";
            user.Role_FK = (int)Permission.Contributor;

            _repository.CreateOrUpdateUser(user);
                            
            Assert.AreEqual("Jane", context.Users.Find(user.Id).Name);          
        }

        [Test]
        public void DeleteUserTest()
        {
            var userToBeDeleted = _repository.ReadUsers().FirstOrDefault();
            _repository.DeleteUser(userToBeDeleted);

            var result = _repository.ReadUsers().Where(x => x.Id == userToBeDeleted.Id).SingleOrDefault();
            Assert.IsNull(result);
        }

        [Test]
        public void ReadUsersTest()
        {
            Assert.IsTrue(_repository.ReadUsers().Count() > 0);
        }


        [Test]
        public void CreatePhotoItemWithCommentsTest()
        {
            //Arrange
            //Setup a PhotoItem
            var userA = new User() { Id = 1, Name = "Teddy", Role_FK = (int)Permission.Guest };
            var userB = new User() { Id = 2, Name = "Cathrine", Role_FK = (int)Permission.Administrator };

            PhotoItem photoitem = new PhotoItem() { UserName = userA.Name, Info = "In the Dessert..", Country = "USA", Location = "Salt Lake City", Latitude = 1.1, Longitude = 2.2, TimeStamp = DateTime.Now, Photo = new Photo() { Binary = new byte[] { 1, 0, 1 } } };
            _repository.CreateOrUpdatePhotoItem(photoitem);

            var commentA = new Comment() { Text = "What a day!", UserName = userA.Name, TimeStamp = DateTime.Now };
            var commentB = new Comment() { Text = "Good photo", UserName = userB.Name, TimeStamp = DateTime.Now };

            //Act
            photoitem.AddComment(commentA);
            photoitem.AddComment(commentB);

            _repository.CreateOrUpdatePhotoItem(photoitem);

            //Assert                        
            PhotoItem result = _repository.ReadPhotoItem(photoitem.Id);
                     
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Comments.Count == 2);            
        }
        
        [Test]
        public void DeleteCommentTest()
        {
            var commenToBeremoved = _repository.ReadComments().FirstOrDefault();
            
            _repository.DeleteComment(commenToBeremoved);
                              
            var result = _repository.ReadPhotoItems().Where(x => x.Id == commenToBeremoved.PhotoItem_FK).ToList();

            Assert.IsEmpty(result.Where(x => x.Comments.Contains(commenToBeremoved)));
        }

        [Test]
        public void UpdateCommentsTest()
        {
            //Arrange
            var context = new EFDbContext();

            var userA = new User() { Id = 1, Name = "Bill" };
            var userB = new User() { Id = 2, Name = "Steven" };
            var commentA = new Comment() { Text = "Lovely dress!", UserName = userB.Name, TimeStamp = DateTime.Now };
            PhotoItem photoitem = new PhotoItem() { UserName = userA.Name, Info = "Inside Opera", Country = "Australia", Location = "Sidney", Latitude = 11.2, Longitude = 2.6, TimeStamp = DateTime.Now, Photo = new Photo() { Binary = new byte[] { 1, 0, 1 } } };

            photoitem.AddComment(commentA);

            _repository.CreateOrUpdatePhotoItem(photoitem);

            //Act            
            var comment = _repository.ReadComments().FirstOrDefault();
            comment.Text = "Changed my mind about your dress! It is amazing!";

            _repository.UpdateComment(comment);

            var result = context.Comments.Where(x => x.Id == comment.Id).FirstOrDefault();

            Assert.AreEqual("Changed my mind about your dress! It is amazing!", result.Text);
        }
        
        [Test]
        public void CreatePhotoItemTest()
        {
            var context = new EFDbContext();

            var userA = new User() { Id = 1, Name = "Henning" };
            PhotoItem photoitem = new PhotoItem() { UserName = userA.Name, Info = "Central City", Country = "Germany", Location = "Berlin", Latitude = 1.2, Longitude = 2.6, TimeStamp = DateTime.Now, Photo = new Photo() { Binary = new byte[] { 1, 0, 1 } } };

            _repository.CreateOrUpdatePhotoItem(photoitem);
            
            var result = context.PhotoItems.FirstOrDefault();

            Assert.IsNotNull(result);
        }

        [Test]
        public void DeletePhotoItemTest()
        {
            var item = _repository.ReadPhotoItems().FirstOrDefault();
            _repository.DeletePhotoItem(item);
            
            var context = new EFDbContext();
            var itemresult = context.PhotoItems.Find(item.Id);
            var photoresult = context.Photos.Where(p => p.PhotoItem.Id == item.Id);
            var commentresult = context.Comments.Where(p => p.PhotoItem_FK == item.Id);
            
            Assert.IsNull(itemresult);
            Assert.IsEmpty(photoresult);
            Assert.IsEmpty(commentresult);           
        }
    }
}

