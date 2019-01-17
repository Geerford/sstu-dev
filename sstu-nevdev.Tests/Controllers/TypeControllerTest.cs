using Domain.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Interfaces;
using sstu_nevdev.Controllers;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;
using System.Web.Mvc;
using RedirectToRouteResult = System.Web.Mvc.RedirectToRouteResult;

namespace sstu_nevdev.Tests.Controllers
{
    [TestClass]
    public class TypeControllerTest
    {
        private Mock<ITypeService> typeServiceMock;
        private TypeController controllerWEB;
        private TypesController controllerAPI;
        private List<Type> items;
        private readonly int id = 1;

        [TestInitialize]
        public void Initialize()
        {
            typeServiceMock = new Mock<ITypeService>();
            controllerWEB = new TypeController(typeServiceMock.Object);
            controllerAPI = new TypesController(typeServiceMock.Object);
            items = new List<Type>()
            {
                new Type {
                    Description = "Пропускает через ворота",
                    Status = "Пропускной"
                },
                new Type {
                    Description = "Отмечает посещаемость",
                    Status = "Лекционный"
                },
                new Type {
                    Description = "Собирает статистику",
                    Status = "Статистический"
                }
            };
        }

        [TestMethod]
        public void Index()
        {
            //Arrange
            typeServiceMock.Setup(x => x.GetAll()).Returns(items);

            //Act
            var result = ((controllerWEB.Index() as ViewResult).Model) as List<Type>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(items, result);
        }

        [TestMethod]
        public void Get_Details()
        {
            //Arrange
            typeServiceMock.Setup(x => x.Get(id)).Returns(items[0]);

            //Act
            var result = ((controllerWEB.Details(id) as ViewResult).Model) as Type;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(items[0], result);
        }

        [TestMethod]
        public void Create_Valid()
        {
            //Arrange
            TypeViewModel model = new TypeViewModel() {
                Description = "test",
                Status = "test"
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
            TypeViewModel model = new TypeViewModel()
            {
                Description = "test",
                Status = "test"
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
            TypeViewModel model = new TypeViewModel() {
                Description = "test",
                Status = "test"
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
            TypeViewModel model = new TypeViewModel() { };
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
            Type model = new Type()
            {
                Description = "test",
                Status = "test"
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
            typeServiceMock.Setup(x => x.GetAll()).Returns(items);

            //Act
            var actionResult = controllerAPI.Get();
            var createdResult = actionResult as OkNegotiatedContentResult<IEnumerable<Type>>;

            //Assert
            Assert.IsNotNull(createdResult);
            Assert.IsInstanceOfType(createdResult.Content, typeof(IEnumerable<Type>));
            Assert.AreEqual(items, createdResult.Content);
        }

        [TestMethod]
        public void Get_API()
        {
            //Arrange
            typeServiceMock.Setup(x => x.Get(id)).Returns(items[0]);

            //Act
            var response = controllerAPI.Get(id);
            var result = response as OkNegotiatedContentResult<Type>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(Type));
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
            Type item = new Type()
            {
                Description = "test",
                Status = "test"
            };

            //Act
            var actionResult = controllerAPI.Post(item);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Type>;

            //Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(item, createdResult.Content);
            Assert.IsInstanceOfType(createdResult.Content, typeof(Type));
            Assert.IsNotNull(createdResult.RouteValues["id"]);
        }

        [TestMethod]
        public void Post_Invalid_API()
        {
            //Arrange
            Type item = null;

            //Act
            var actionResult = controllerAPI.Post(item);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Type>;

            //Assert
            Assert.IsNull(createdResult);
        }

        [TestMethod]
        public void Put_Valid_API()
        {
            //Arrange
            Type item = new Type()
            {
                ID = id,
                Description = "test",
                Status = "test"
            };

            //Act
            var actionResult = controllerAPI.Put(id, item);
            var contentResult = actionResult as NegotiatedContentResult<Type>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
        }

        [TestMethod]
        public void Put_Invalid_API()
        {
            //Arrange
            int newid = 2;
            Type item = new Type()
            {
                ID = newid,
                Description = "test",
                Status = "test"
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
            Type item = new Type()
            {
                ID = id,
                Description = "test",
                Status = "test"
            };
            typeServiceMock.Setup(x => x.Get(id)).Returns(item);

            //Act
            var actionResult = controllerAPI.Delete(id);
            var contentResult = actionResult as NegotiatedContentResult<Type>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.AreEqual(item, contentResult.Content);
        }
    }
}