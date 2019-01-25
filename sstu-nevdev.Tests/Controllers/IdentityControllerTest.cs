using Domain.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.DTO;
using Service.Interfaces;
using sstu_nevdev.Controllers;
using sstu_nevdev.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;
using System.Web.Mvc;
using RedirectToRouteResult = System.Web.Mvc.RedirectToRouteResult;

namespace sstu_nevdev.Tests.Controllers
{
    [TestClass]
    public class IdentityControllerTest
    {
        private Mock<IIdentityService> identityServiceMock;
        private IdentityController controllerWEB;
        private IdentitiesController controllerAPI;
        private List<IdentityDTO> items;
        private readonly int id = 1;

        [TestInitialize]
        public void Initialize()
        {
            identityServiceMock = new Mock<IIdentityService>();
            controllerWEB = new IdentityController(identityServiceMock.Object);
            controllerAPI = new IdentitiesController(identityServiceMock.Object);
            items = new List<IdentityDTO>()
            {
                new IdentityDTO()
                {
                    ID = 1,
                    GUID = "1",
                    Picture = "cat.jpg",
                    Name = "Сидр",
                    Surname = "Сидоров",
                    Midname = "Сидорович",
                    Gender = true,
                    Birthdate = Convert.ToDateTime("2000-01-25"),
                    Country = "Россия",
                    City = "Саратов",
                    Department = "ИнПИТ",
                    Group = "ИФСТ",
                    Status = "Отчислен",
                    Email = "email@gmail.com",
                    Phone = "+79993499334",
                    Role = "Студент"
                },
                new IdentityDTO()
                {
                    ID = 2,
                    GUID = "2",
                    Picture = "cat.jpg",
                    Name = "Петр",
                    Surname = "Петров",
                    Midname = "Петрович",
                    Gender = true,
                    Birthdate = Convert.ToDateTime("2000-01-25"),
                    Country = "Россия",
                    City = "Саратов",
                    Department = "ИнПИТ",
                    Group = "ИФСТ",
                    Status = "Обучающийся",
                    Email = "email@gmail.com",
                    Phone = "+79993499334",
                    Role = "Студент"
                },
                new IdentityDTO()
                {
                    ID = 3,
                    GUID = "3",
                    Picture = "cat.jpg",
                    Name = "Иван",
                    Surname = "Иванов",
                    Midname = "Иванович",
                    Gender = true,
                    Birthdate = Convert.ToDateTime("2000-01-25"),
                    Country = "Россия",
                    City = "Саратов",
                    Department = "ИнПИТ",
                    Group = "ИФСТ",
                    Status = "Отпуск",
                    Email = "email@gmail.com",
                    Phone = "+79993499334",
                    Role = "Преподаватель"
                }
            };
        }

        [TestMethod]
        public void Index()
        {
            //Arrange
            identityServiceMock.Setup(x => x.GetAll()).Returns(items);

            //Act
            var result = ((controllerWEB.Index() as ViewResult).Model) as List<IdentityDTO>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(items, result);
        }

        [TestMethod]
        public void Get_Details()
        {
            //Arrange
            identityServiceMock.Setup(x => x.Get(id)).Returns(items[0]);

            //Act
            var result = ((controllerWEB.Details(id) as ViewResult).Model) as IdentityDTO;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(items[0], result);
        }

        [TestMethod]
        public void Create_Valid()
        {
            //Arrange
            IdentityViewModel model = new IdentityViewModel() {
                GUID = "test"
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
            IdentityViewModel model = new IdentityViewModel() { };
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
            IdentityViewModel model = new IdentityViewModel() {
                GUID = "test"
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
            IdentityViewModel model = new IdentityViewModel() { };
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
            Identity model = new Identity() {
                GUID = "test"
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
            identityServiceMock.Setup(x => x.GetAll()).Returns(items);

            //Act
            var actionResult = controllerAPI.Get();
            var createdResult = actionResult as OkNegotiatedContentResult<IEnumerable<IdentityDTO>>;

            //Assert
            Assert.IsNotNull(createdResult);
            Assert.IsInstanceOfType(createdResult.Content, typeof(IEnumerable<IdentityDTO>));
            Assert.AreEqual(items, createdResult.Content);
        }

        [TestMethod]
        public void Get_API()
        {
            //Arrange
            identityServiceMock.Setup(x => x.Get(id)).Returns(items[0]);

            //Act
            var response = controllerAPI.Get(id);
            var result = response as OkNegotiatedContentResult<IdentityDTO>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(IdentityDTO));
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
            Identity item = new Identity() {
                GUID = "test",
                Picture = "test"
            };

            //Act
            var actionResult = controllerAPI.Post(item);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Identity>;

            //Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(item, createdResult.Content);
            Assert.IsInstanceOfType(createdResult.Content, typeof(Identity));
            Assert.IsNotNull(createdResult.RouteValues["id"]);
        }

        [TestMethod]
        public void Post_Invalid_API()
        {
            //Arrange
            Identity item = null;

            //Act
            var actionResult = controllerAPI.Post(item);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Identity>;

            //Assert
            Assert.IsNull(createdResult);
        }

        [TestMethod]
        public void Put_Valid_API()
        {
            //Arrange
            Identity item = new Identity() {
                ID = id,
                GUID = "test",
                Picture = "test"
            };

            //Act
            var actionResult = controllerAPI.Put(id, item);
            var contentResult = actionResult as OkNegotiatedContentResult<Identity>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsInstanceOfType(contentResult.Content, typeof(Identity));
        }

        [TestMethod]
        public void Put_Invalid_API()
        {
            //Arrange
            int newid = 2;
            Identity item = new Identity() {
                ID = newid,
                GUID = "test",
                Picture = "test"
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
            Identity item = new Identity() {
                ID = id,
                GUID = "test",
                Picture = "test"
            };
            identityServiceMock.Setup(x => x.GetSimple(id)).Returns(item);

            //Act
            var actionResult = controllerAPI.Delete(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Identity>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsInstanceOfType(contentResult.Content, typeof(Identity));
            Assert.AreEqual(item, contentResult.Content);
        }
    }
}