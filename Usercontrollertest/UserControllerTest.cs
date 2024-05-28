using NUnit.Framework;
using System.Web.Mvc;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CRUD_application_2.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        private UserController _controller;

        private List<User> _users;

        [SetUp]
        public void Setup()
        {
            // Initialize the controller and a list of users
            _controller = new UserController();
            _users = new List<User>
            {
                new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" },
                new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" },
                new User { Id = 3, Name = "Test User 3", Email = "test3@example.com" },
            };

            // Set the static userlist in the controller to our list of users
            UserController.userlist = _users;
        }

        [Test]
        public void Index_ReturnsCorrectView()
        {
            // Act
            var result = _controller.Index(null) as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void Index_ReturnsCorrectModel()
        {
            // Act
            var result = _controller.Index("") as ViewResult;

            // Assert
            Assert.AreEqual(3, ((List<User>)result.Model).Count);
        }

        //test that Index method returns the correct model when a search string is provided
        [Test]
        public void Index_ReturnsCorrectModel_WhenSearchStringIsProvided()
        {
            // Act
            var result = _controller.Index("Test User 1") as ViewResult;

            // Assert
            Assert.AreEqual(_users.First(u => u.Name == "Test User 1").Email, ((List<User>)result.Model).First().Email);
            Assert.AreEqual(1, ((List<User>)result.Model).Count);
        }

        [Test]
        public void Details_ReturnsCorrectView()
        {
            // Act
            var result = _controller.Details(1) as ViewResult;

            // Assert
            Assert.AreEqual("Details", result.ViewName);
        }

        [Test]
        public void Details_ReturnsCorrectModel()
        {
            // Act
            var result = _controller.Details(1) as ViewResult;

            // Assert
            Assert.AreEqual(_users.First(u => u.Id == 1), result.Model);
        }

        //dispose _controller
        [TearDown]
        public void Dispose()
        {
            this._controller.Dispose();
        }
    }
}
