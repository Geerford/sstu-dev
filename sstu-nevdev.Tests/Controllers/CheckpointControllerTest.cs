using Domain.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.DTO;
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
    public class CheckpointControllerTest
    {
        private Mock<ICheckpointService> checkpointServiceMock;
        private Mock<ITypeService> typeServiceMock;
        private Mock<IAdmissionService> admissionServiceMock;
        private CheckpointController controllerWEB;
        private CheckpointsController controllerAPI;
        private List<CheckpointDTO> items;
        private List<Type> types;
        private List<Admission> admissions;
        private readonly int id = 1;

        [TestInitialize]
        public void Initialize()
        {
            checkpointServiceMock = new Mock<ICheckpointService>();
            typeServiceMock = new Mock<ITypeService>();
            admissionServiceMock = new Mock<IAdmissionService>();
            controllerWEB = new CheckpointController(checkpointServiceMock.Object,
                typeServiceMock.Object, admissionServiceMock.Object);
            controllerAPI = new CheckpointsController(checkpointServiceMock.Object);
            items = new List<CheckpointDTO>()
            {
                new CheckpointDTO
                {
                    IP = "192.168.0.1",
                    Campus = 1,
                    Row = 4,
                    Description = "На 4 этаже, 425 аудитория",
                    Status = "Отметка",
                    Type = new TypeDTO {
                        Description = "Пропускает через ворота",
                        Status = "Пропускной"
                    },
                    Admissions = new List<Admission>(){
                        new Admission {
                            Role = "Сотрудник",
                            Description = "Вход в лабораторию"
                        }
                    }
                },
                new CheckpointDTO
                {
                    IP = "192.168.0.15",
                    Campus = 1,
                    Row = 4,
                    Description = "На 1 этаже на входе",
                    Status = "Пропуск",
                    Type = new TypeDTO {
                        Description = "Отмечает посещаемость",
                        Status = "Лекционный"
                    },
                    Admissions = new List<Admission>(){
                        new Admission {
                            Role = "Сотрудник",
                            Description = "Вход в лабораторию"
                        },
                        new Admission {
                            Role = "Студент",
                            Description = "Вход в 1-й корпус"
                        }
                    }
                }
            };
            types = new List<Type>()
            {
                new Type
                {
                    ID = 1,
                    Description = "Пропускает через ворота",
                    Status = "Пропускной"
                },
                new Type
                {
                    ID = 2,
                    Description = "Отмечает посещаемость",
                    Status = "Лекционный"
                },
                new Type
                {
                    ID = 3,
                    Description = "Собирает статистику",
                    Status = "Статистический"
                }
        };
            admissions = new List<Admission>()
            {
               new Admission {
                   ID = 1,
                    Role = "Сотрудник",
                    Description = "Вход в лабораторию"
                },
                new Admission
                {
                    ID = 2,
                    Role = "Студент",
                    Description = "Вход в 1-й корпус"
                }
            };
        }

        [TestMethod]
        public void Index()
        {
            //Arrange
            checkpointServiceMock.Setup(x => x.GetAll()).Returns(items);

            //Act
            var result = ((controllerWEB.Index() as ViewResult).Model) as List<CheckpointDTO>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(items, result);
        }

        [TestMethod]
        public void Get_Details()
        {
            //Arrange
            checkpointServiceMock.Setup(x => x.Get(id)).Returns(items[0]);

            //Act
            var result = ((controllerWEB.Details(id) as ViewResult).Model) as CheckpointDTO;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(items[0], result);
        }

        [TestMethod]
        public void Create_Valid()
        {
            //Arrange
            typeServiceMock.Setup(x => x.Get(id)).Returns(types[0]);
            admissionServiceMock.Setup(x => x.GetAll()).Returns(admissions);
            CheckpointViewModel model = new CheckpointViewModel()
            {
                IP = "192.168.0.1",
                Campus = 1,
                Row = 4,
                Description = "На 4 этаже, 425 аудитория",
                Status = "Отметка",
                TypeID = "1",
                AdmissionList = new List<CheckboxItem>(){
                    new CheckboxItem {
                        ID = 1,
                        Display = "Сотрудник (Вход в лабораторию)",
                        IsChecked = true
                    }
                }
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
            typeServiceMock.Setup(x => x.Get(id)).Returns(types[0]);
            admissionServiceMock.Setup(x => x.GetAll()).Returns(admissions);
            CheckpointViewModel model = new CheckpointViewModel()
            {
                IP = "192.168.0.1",
                Campus = 1,
                Row = 4,
                Description = "На 4 этаже, 425 аудитория",
                Status = "Отметка",
                AdmissionList = new List<CheckboxItem>(){
                    new CheckboxItem {
                        ID = 1,
                        Display = "Сотрудник (Вход в лабораторию)",
                        IsChecked = true
                    }
                }
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
            typeServiceMock.Setup(x => x.Get(1)).Returns(types[0]);
            admissionServiceMock.Setup(x => x.GetAll()).Returns(admissions);
            CheckpointViewModel model = new CheckpointViewModel()
            {
                ID = id,
                IP = "192.168.0.1",
                Campus = 1,
                Row = 4,
                Description = "На 4 этаже, 425 аудитория",
                Status = "Отметка",
                TypeID = "1",
                AdmissionList = new List<CheckboxItem>(){
                    new CheckboxItem {
                        ID = 1,
                        Display = "Сотрудник (Вход в лабораторию)",
                        IsChecked = true
                    }
                }
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
            typeServiceMock.Setup(x => x.Get(1)).Returns(types[0]);
            admissionServiceMock.Setup(x => x.GetAll()).Returns(admissions);
            CheckpointViewModel model = new CheckpointViewModel() { };
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
            CheckpointDTO model = new CheckpointDTO()
            {
                ID = id,
                IP = "192.168.0.1",
                Campus = 1,
                Row = 4,
                Description = "На 4 этаже, 425 аудитория",
                Status = "Отметка",
                Admissions = new List<Admission>(),
                Type = new TypeDTO(),
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
            checkpointServiceMock.Setup(x => x.GetAll()).Returns(items);

            //Act
            var actionResult = controllerAPI.Get();
            var createdResult = actionResult as OkNegotiatedContentResult<IEnumerable<CheckpointDTO>>;

            //Assert
            Assert.IsNotNull(createdResult);
            Assert.IsInstanceOfType(createdResult.Content, typeof(IEnumerable<CheckpointDTO>));
            Assert.AreEqual(items, createdResult.Content);
        }

        [TestMethod]
        public void Get_API()
        {
            //Arrange
            checkpointServiceMock.Setup(x => x.Get(id)).Returns(items[0]);

            //Act
            var response = controllerAPI.Get(id);
            var result = response as OkNegotiatedContentResult<CheckpointDTO>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(CheckpointDTO));
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
            CheckpointDTO item = new CheckpointDTO()
            {
                IP = "192.168.0.1",
                Campus = 1,
                Row = 4,
                Description = "На 4 этаже, 425 аудитория",
                Status = "Отметка",
                Type = (TypeDTO)types[0],
                Admissions = admissions
            };

            //Act
            var actionResult = controllerAPI.Post(item);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<CheckpointDTO>;

            //Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(item, createdResult.Content);
            Assert.IsInstanceOfType(createdResult.Content, typeof(CheckpointDTO));
            Assert.IsNotNull(createdResult.RouteValues["id"]);
        }

        [TestMethod]
        public void Post_Invalid_API()
        {
            //Arrange
            CheckpointDTO item = null;

            //Act
            var actionResult = controllerAPI.Post(item);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<CheckpointDTO>;

            //Assert
            Assert.IsNull(createdResult);
        }

        [TestMethod]
        public void Put_Valid_API()
        {
            //Arrange
            CheckpointDTO item = new CheckpointDTO()
            {
                ID = id,
                IP = "192.168.0.15",
                Campus = 3,
                Row = 4,
                Description = "На 4 этаже, 425 аудитория",
                Status = "Отметка",
                Type = (TypeDTO)types[0],
                Admissions = admissions
            };

            //Act
            var actionResult = controllerAPI.Put(id, item);
            var contentResult = actionResult as OkNegotiatedContentResult<CheckpointDTO>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsInstanceOfType(contentResult.Content, typeof(CheckpointDTO));
        }

        [TestMethod]
        public void Put_Invalid_API()
        {
            //Arrange
            int newid = 2;
            CheckpointDTO item = new CheckpointDTO()
            {
                ID = newid,
                IP = "192.168.0.15",
                Campus = 3,
                Row = 4,
                Description = "На 4 этаже, 425 аудитория",
                Status = "Отметка",
                Type = (TypeDTO)types[0],
                Admissions = admissions
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
            Checkpoint item = new Checkpoint()
            {
                ID = id,
                IP = "192.168.0.15",
                Campus = 3,
                Row = 4,
                Description = "На 4 этаже, 425 аудитория",
                Status = "Отметка"
            };
            checkpointServiceMock.Setup(x => x.GetSimple(id)).Returns(item);

            //Act
            var actionResult = controllerAPI.Delete(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Checkpoint>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsInstanceOfType(contentResult.Content, typeof(Checkpoint));
            Assert.AreEqual(item, contentResult.Content);
        }
    }
}