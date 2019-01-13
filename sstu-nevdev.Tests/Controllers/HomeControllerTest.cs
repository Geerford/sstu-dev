using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Interfaces;
using sstu_nevdev.Controllers;

namespace sstu_nevdev.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {


        [TestInitialize]
        public void Initialize()
        {

        }







        [TestMethod]
        public void Index()
        {

            // Arrange
            var mockService = new Mock<IActivityService>();
            var activities = new List<Activity>() {
                new Activity()
            };
            mockService
                .Setup(x => x.GetAll())
                .Returns(activities);

            ActivityController controller = new ActivityController(mockService.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model;
            Assert.IsNotNull(model);
            Assert.IsInstanceOfType(model, typeof(List<Activity>));
            Assert.AreEqual(activities, model);
        }

     /*   [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }*/
    }
}
