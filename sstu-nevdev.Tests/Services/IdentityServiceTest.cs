using Domain.Contracts;
using Domain.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.Interfaces;
using Service.DTO;
using Service.Interfaces;
using Services.Business.Services;
using System;
using System.Collections.Generic;

namespace sstu_nevdev.Tests.Services
{
    [TestClass]
    public class IdentityServiceTest
    {
        private IIdentityService serviceMock;
        private Mock<IRepository<Identity>> repositoryMock;
        private Mock<IUnitOfWork> unitWorkMoq;
        private Mock<ISyncUnitOfWork> syncUnitOfWork;
        private List<IdentityDTO> itemsDTO;
        private List<Identity> itemsSimple;
        private List<User> itemsUser;
        private readonly int id = 1;

        [TestInitialize]
        public void Initialize()
        {
            repositoryMock = new Mock<IRepository<Identity>>();
            unitWorkMoq = new Mock<IUnitOfWork>();
            syncUnitOfWork = new Mock<ISyncUnitOfWork>();
            serviceMock = new IdentityService(unitWorkMoq.Object, syncUnitOfWork.Object);
            itemsDTO = new List<IdentityDTO>()
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
            itemsSimple = new List<Identity>()
            {
                new Identity
                {
                    ID = 1,
                    GUID = "1",
                    Picture = "cat.jpg"
                },
                new Identity
                {
                    ID = 2,
                    GUID = "2",
                    Picture = "cat.jpg"
                },
                new Identity
                {
                    ID = 3,
                    GUID = "3",
                    Picture = "cat.jpg"
                }
            };
            itemsUser = new List<User>()
            {
                new User
                {
                    ID = 1,
                    GUID = "1",
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
                new User()
                {
                    ID = 2,
                    GUID = "2",
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
                new User()
                {
                    ID = 3,
                    GUID = "3",
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
        public void Create()
        {
            //Arrange
            Identity item = new Identity()
            {
                GUID = "3",
                Picture = "test"
            };
            List<Identity> items = new List<Identity>()
            {
                itemsSimple[0],
                itemsSimple[1]
            };
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(itemsUser);
            unitWorkMoq.Setup(x => x.Identity.GetAll()).Returns(items);
            unitWorkMoq.Setup(x => x.Identity.Create(item)).Callback(() => 
            {
                item.ID = 3;
            });
                
            //Act
            serviceMock.Create(item);

            //Assert
            Assert.AreEqual(3, item.ID);
        }
        
        [TestMethod]
        public void Delete()
        {
            //Arrange
            int ID = -1;
            unitWorkMoq.Setup(x => x.Identity.Delete(itemsSimple[0].ID)).Callback((int callbackID) =>
            {
                ID = itemsSimple[0].ID;
            });

            //Act
            serviceMock.Delete(itemsSimple[0]);

            //Assert
            Assert.AreNotEqual(-1, ID);
            Assert.AreEqual(id, ID);
        }

        [TestMethod]
        public void Edit()
        {
            //Arrange
            Identity item = new Identity();
            unitWorkMoq.Setup(x => x.Identity.Update(itemsSimple[0])).Callback(() =>
            {
                item.ID = itemsSimple[0].ID;
                item.GUID = itemsSimple[0].GUID;
                item.Picture = itemsSimple[0].Picture;
            });

            //Act
            serviceMock.Edit(itemsSimple[0]);

            //Assert
            Assert.IsNotNull(item.ID);
            Assert.IsNotNull(item.GUID);
            Assert.IsNotNull(item.Picture);
            Assert.AreEqual(itemsSimple[0], item);
        }

        [TestMethod]
        public void Find()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Identity.GetAll()).Returns(itemsSimple);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(itemsUser);

            //Act
            var result = serviceMock.Find(itemsSimple[0]);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IdentityDTO));
            Assert.IsNotNull(result.ID);
            Assert.AreEqual(itemsDTO[0], result);
        }

        [TestMethod]
        public void Get_Simple_Model()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Identity.Get(It.IsAny<int>())).Returns(itemsSimple[0]);

            //Act
            var result = serviceMock.GetSimple(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Identity));
            Assert.AreEqual(id, result.ID);
        }

        [TestMethod]
        public void Get()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Identity.Get(It.IsAny<int>())).Returns(itemsSimple[0]);
            unitWorkMoq.Setup(x => x.Identity.GetAll()).Returns(itemsSimple);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(itemsUser);

            //Act
            var result = serviceMock.Get(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IdentityDTO));
            Assert.AreEqual(id, result.ID);
        }

        [TestMethod]
        public void Get_By_GUID()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Identity.GetAll()).Returns(itemsSimple);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(itemsUser);

            //Act
            var result = serviceMock.GetByGUID(id.ToString());

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IdentityDTO));
            Assert.AreEqual(id, result.ID);
        }

        [TestMethod]
        public void Get_By_Name()
        {
            //Arrange
            string name = "Сидр", midname = "Сидорович", surname = "Сидоров";
            syncUnitOfWork.Setup(x => x.User.Find(It.IsAny<Func<User, bool>>())).Returns(itemsUser);
            unitWorkMoq.Setup(x => x.Identity.GetAll()).Returns(itemsSimple);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(itemsUser);

            //Act
            var result = serviceMock.GetByName(name, midname, surname);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IdentityDTO));
            Assert.AreEqual(id, result.ID);
        }

        [TestMethod]
        public void Get_All()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Identity.GetAll()).Returns(itemsSimple);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(itemsUser);

            //Act
            var results = serviceMock.GetAll();

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<IdentityDTO>));
        }

        [TestMethod]
        public void Get_By_Status()
        {
            //Arrange
            var status = "Отчислен";
            unitWorkMoq.Setup(x => x.Identity.GetAll()).Returns(itemsSimple);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(itemsUser);

            //Act
            var result = serviceMock.GetByStatus(status);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<IdentityDTO>));
        }

        [TestMethod]
        public void Get_By_Department()
        {
            //Arrange
            var department = "ИнПИТ";
            unitWorkMoq.Setup(x => x.Identity.GetAll()).Returns(itemsSimple);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(itemsUser);

            //Act
            var result = serviceMock.GetByDepartment(department);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<IdentityDTO>));
        }

        [TestMethod]
        public void Get_By_Group()
        {
            //Arrange
            var group = "ИФСТ";
            unitWorkMoq.Setup(x => x.Identity.GetAll()).Returns(itemsSimple);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(itemsUser);

            //Act
            var result = serviceMock.GetByGroup(group);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<IdentityDTO>));
        }

        [TestMethod]
        public void Get_Full()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Identity.GetAll()).Returns(itemsSimple);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(itemsUser);

            //Act
            var result = serviceMock.GetFull(id.ToString());

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IdentityDTO));
        }
    }
}