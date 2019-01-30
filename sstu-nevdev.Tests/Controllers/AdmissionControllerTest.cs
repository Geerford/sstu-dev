using Domain.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Interfaces;
using sstu_nevdev.Controllers;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Web.Mvc;
using RedirectToRouteResult = System.Web.Mvc.RedirectToRouteResult;

namespace sstu_nevdev.Tests.Controllers
{
    [TestClass]
    public class AdmissionControllerTest
    {
        private Mock<IAdmissionService> admissionServiceMock;
        private AdmissionController controllerWEB;
        private AdmissionsController controllerAPI;
        private List<Admission> items;
        private readonly int id = 1;

        [TestInitialize]
        public void Initialize()
        {
            admissionServiceMock = new Mock<IAdmissionService>();
            controllerWEB = new AdmissionController(admissionServiceMock.Object);
            controllerAPI = new AdmissionsController(admissionServiceMock.Object);
            items = new List<Admission>()
            {
                new Admission {
                    Role = "Сотрудник",
                    Description = "Вход в лабораторию"
                },
                new Admission {
                    Role = "Студент",
                    Description = "Вход в 1-й корпус"
                },
            };
        }

        [TestMethod]
        public void Index()
        {
            //Arrange
            admissionServiceMock.Setup(x => x.GetAll()).Returns(items);

            //Act
            var result = ((controllerWEB.Index() as ViewResult).Model) as List<Admission>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(items, result);
        }

        [TestMethod]
        public void Get_Details()
        {
            //Arrange
            admissionServiceMock.Setup(x => x.Get(id)).Returns(items[0]);

            //Act
            var result = ((controllerWEB.Details(id) as ViewResult).Model) as Admission;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(items[0], result);
        }

        [TestMethod]
        public void Create_Valid()
        {
            //Arrange
            AdmissionViewModel model = new AdmissionViewModel()
            {
                Description = "test",
                Role = "test"
            };

            //Act
            var result = (RedirectToRouteResult)controllerWEB.Create(model);

            //Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Create_Invalid()
        {
            // Arrange
            AdmissionViewModel model = new AdmissionViewModel()
            {
                Description = "test",
                Role = "test"
            };
            controllerWEB.ModelState.AddModelError("Error", "Что-то пошло не так");

            //Act
            var result = (ViewResult)controllerWEB.Create(model);

            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Edit_Valid()
        {
            //Arrange
            AdmissionViewModel model = new AdmissionViewModel()
            {
                Description = "test",
                Role = "test"
            };

            //Act
            var result = (RedirectToRouteResult)controllerWEB.Edit(model);

            //Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Edit_Invalid()
        {
            //Arrange
            AdmissionViewModel model = new AdmissionViewModel() { };
            controllerWEB.ModelState.AddModelError("Error", "Что-то пошло не так");

            //Act
            var result = (ViewResult)controllerWEB.Edit(model);

            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Delete()
        {
            //Arrange
            Admission model = new Admission()
            {
                Description = "test",
                Role = "test"
            };

            //Act
            var result = (RedirectToRouteResult)controllerWEB.Delete(model);

            //Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Get_All_API()
        {
            //Arrange
            admissionServiceMock.Setup(x => x.GetAll()).Returns(items);

            //Act
            var actionResult = controllerAPI.Get();
            var createdResult = actionResult as OkNegotiatedContentResult<IEnumerable<Admission>>;

            //Assert
            Assert.IsNotNull(createdResult);
            Assert.IsInstanceOfType(createdResult.Content, typeof(IEnumerable<Admission>));
            Assert.AreEqual(items, createdResult.Content);
        }

        [TestMethod]
        public void Get_API()
        {
            //Arrange
            admissionServiceMock.Setup(x => x.Get(id)).Returns(items[0]);

            //Act
            var response = controllerAPI.Get(id);
            var result = response as OkNegotiatedContentResult<Admission>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(Admission));
        }

        [TestMethod]
        public void Get_Not_Found_API()
        {
            //Act
            var result = controllerAPI.Get(int.MaxValue);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Post_Valid_API()
        {
            //Arrange
            Admission item = new Admission()
            {
                Description = "test",
                Role = "test"
            };

            //Act
            var actionResult = controllerAPI.Post(item);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Admission>;

            //Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(item, createdResult.Content);
            Assert.IsInstanceOfType(createdResult.Content, typeof(Admission));
            Assert.IsNotNull(createdResult.RouteValues["id"]);
        }

        [TestMethod]
        public void Post_Invalid_API()
        {
            //Arrange
            Admission item = null;

            //Act
            var actionResult = controllerAPI.Post(item);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Admission>;

            //Assert
            Assert.IsNull(createdResult);
        }

        [TestMethod]
        public void Put_Valid_API()
        {
            //Arrange
            Admission item = new Admission()
            {
                ID = id,
                Description = "test",
                Role = "test"
            };

            //Act
            var actionResult = controllerAPI.Put(id, item);
            var contentResult = actionResult as OkNegotiatedContentResult<Admission>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsInstanceOfType(contentResult.Content, typeof(Admission));
        }

        [TestMethod]
        public void Put_Invalid_API()
        {
            //Arrange
            int newid = 2;
            Admission item = new Admission()
            {
                ID = newid,
                Description = "test",
                Role = "test"
            };

            //Act
            var actionResult = controllerAPI.Put(id, item);
            var contentResult = actionResult as BadRequestResult;

            //Assert
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void Delete_API()
        {
            //Arrange
            Admission item = new Admission()
            {
                ID = id,
                Description = "test",
                Role = "test"
            };
            admissionServiceMock.Setup(x => x.Get(id)).Returns(item);

            //Act
            var actionResult = controllerAPI.Delete(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Admission>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsInstanceOfType(contentResult.Content, typeof(Admission));
            Assert.AreEqual(item, contentResult.Content);
        }
    }
}