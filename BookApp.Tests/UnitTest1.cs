using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using BookApp.Controllers;
using BookApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetMethod()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            mockRepository.Setup(x => x.GetBook(10))
                .Returns(new Book { Id = 10 });

            var controller = new BooksController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetBook(10);
            var contentResult = actionResult as OkNegotiatedContentResult<Book>;

            // Assert
            //Assert.AreEqual(6, 5);
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(10, contentResult.Content.Id);
        }

        [TestMethod]
        public void PostMethod()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            var controller = new BooksController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.PostBook(new Book { Id = 10, Name = "Book1", Author = "Author1", Year = 9999 });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Book>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(10, createdResult.RouteValues["id"]);
        }

        [TestMethod]
        public void DeleteMethod()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            mockRepository.Setup(x => x.GetBook(10))
                .Returns(new Book { Id = 10 });
            var controller = new BooksController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.DeleteBook(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void PutMethod()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            var controller = new BooksController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.PutBook(10, new Book { Id = 10, Name = "Book2" });
            var contentResult = actionResult as NegotiatedContentResult<Book>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(10, contentResult.Content.Id);
        }
    }
}
