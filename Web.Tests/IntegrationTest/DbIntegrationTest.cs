﻿using NUnit.Framework;
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

            User user = new User() { Name = "John" };
            _repository.CreateOrUpdateUser(user);

            user.Name = "Jane";

            _repository.CreateOrUpdateUser(user);
                            
            Assert.AreEqual("Jane", context.Users.Find(user.Id).Name);          
        }        

        [Test]
        public void CreatePhotoItemWithCommentsTest()
        {
            //Arrange
            //Setup a PhotoItem
            var userA = new User() { Id = 1, Name = "Teddy" };
            var userB = new User() { Id = 2, Name = "Cathrine" };

            PhotoItem photoitem = new PhotoItem() { UserName = userA.Name, Info = "Nice Picture", Country = "USA", Location = "Salt Lake City", Latitude = 1.1, Longitude = 2.2, TimeStamp = DateTime.Now, Photo = new Photo() { Binary = new byte[] { 1, 0, 1 } } };
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
            //
            //_repository.CreateOrUpdatePhotoItem(_item);

            //PhotoItem item = _repository.ReadPhotoItems().FirstOrDefault();

            //var list = _repository.ReadPhotoItems().Where(x => x.Id == item.Id).ToList();

            //Assert.IsNotNull(list);
        }
        
        [Test]
        public void CreatePhotoItemTest()
        {
            var userA = new User() { Id = 1, Name = "Henning" };
            PhotoItem photoitem = new PhotoItem() { UserName = userA.Name, Info = "Central City", Country = "Germany", Location = "Berlin", Latitude = 1.2, Longitude = 2.6, TimeStamp = DateTime.Now, Photo = new Photo() { Binary = new byte[] { 1, 0, 1 } } };

            using (var context = new EFDbContext())
            {
               context.PhotoItems.Add(photoitem);
               context.SaveChanges();

               var item = context.PhotoItems.FirstOrDefault();
               Assert.IsNotNull(item);
            }
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
