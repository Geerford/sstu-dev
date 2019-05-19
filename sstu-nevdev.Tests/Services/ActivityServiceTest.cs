using Domain.Contracts;
using Domain.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.Interfaces;
using Service.DTO;
using Service.Interfaces;
using Services.Business.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Type = Domain.Core.Type;

namespace sstu_nevdev.Tests.Services
{
    [TestClass]
    public class ActivityServiceTest
    {
        private IActivityService serviceMock;
        private Mock<IRepository<Activity>> repositoryMock;
        private Mock<IRepository<CheckpointAdmission>> repositoryCheckpointAdmissionMock;
        private Mock<IRepository<Type>> repositoryTypeMock;
        private Mock<IRepository<Admission>> repositoryAdmissionMock;
        private Mock<IRepository<Mode>> modeRepositoryMock;
        private Mock<IUnitOfWork> unitWorkMoq;
        private List<Activity> itemsActivity;
        private List<CheckpointAdmission> itemsCheckpointAdmission;
        private List<Admission> itemsAdmission;
        private List<Type> itemsType;
        private List<Mode> itemsMode;
        private readonly int id = 1;

        [TestInitialize]
        public void Initialize()
        {
            repositoryMock = new Mock<IRepository<Activity>>();
            repositoryCheckpointAdmissionMock = new Mock<IRepository<CheckpointAdmission>>();
            repositoryTypeMock = new Mock<IRepository<Type>>();
            repositoryAdmissionMock = new Mock<IRepository<Admission>>();
            modeRepositoryMock = new Mock<IRepository<Mode>>();
            unitWorkMoq = new Mock<IUnitOfWork>();
            serviceMock = new ActivityService(unitWorkMoq.Object);
            itemsCheckpointAdmission = new List<CheckpointAdmission>()
            {
                new CheckpointAdmission
                {
                    ID = 1,
                    CheckpointID = 1,
                    AdmissionID = 1
                },
                new CheckpointAdmission
                {
                    ID = 2,
                    CheckpointID = 2,
                    AdmissionID = 1
                },
                new CheckpointAdmission
                {
                    ID = 3,
                    CheckpointID = 2,
                    AdmissionID = 2
                },
                new CheckpointAdmission
                {
                    ID = 4,
                    CheckpointID = 1,
                    AdmissionID = 2
                }
            };
            itemsAdmission = new List<Admission>()
            {
                new Admission
                {
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
            itemsType = new List<Type>()
            {
                new Type {
                    ID = 1,
                    Description = "Пропускает через ворота",
                    Status = "Пропускной"
                },
                new Type {
                    ID = 2,
                    Description = "Отмечает посещаемость",
                    Status = "Лекционный"
                },
                new Type {
                    ID = 3,
                    Description = "Собирает статистику",
                    Status = "Статистический"
                }
            };
            itemsActivity = new List<Activity>()
            {
                new Activity
                {
                    ID = 1,
                    IdentityGUID = "1",
                    CheckpointIP = "192.168.0.1",
                    Date = DateTime.Now,
                    ModeID = 1,
                    Mode =  new Mode
                    {
                        Description = "Отмечает событие входа в объект",
                        Status = "Вход"
                    },
                    Status = true
                },
                new Activity
                {
                    ID = 2,
                    IdentityGUID = "2",
                    CheckpointIP = "192.168.0.1",
                    Date = DateTime.Now,
                    ModeID = 2,
                    Mode = new Mode
                    {
                        Description = "Отмечает событие выхода из объекта",
                        Status = "Выход"
                    },
                    Status = true
                }, 
                new Activity
                {
                    ID = 3,
                    IdentityGUID = "3",
                    CheckpointIP = "192.168.0.1",
                    Date = DateTime.Now,
                    ModeID = 1,
                    Mode = new Mode
                    {
                        Description = "Отмечает событие входа в объект",
                        Status = "Вход"
                    },
                    Status = true
                }
            };
            itemsMode = new List<Mode>()
            {
                new Mode
                {
                    Description = "Отмечает событие входа в объект",
                    Status = "Вход"
                },
                new Mode
                {
                    Description = "Отмечает событие выхода из объекта",
                    Status = "Выход"
                },
                new Mode
                {
                    Description = "Собирает статистические данные передвижений субъекта",
                    Status = "Статистика"
                }
        };
        }

        [TestMethod]
        public void Create()
        {
            //Arrange
            int createdActivity = 0;
            Activity item = new Activity()
            {
                IdentityGUID = itemsActivity[2].IdentityGUID,
                CheckpointIP = itemsActivity[2].CheckpointIP,
                Date = itemsActivity[2].Date,
                Status = itemsActivity[2].Status,
                Mode = itemsActivity[2].Mode
            };
            List<Activity> items = new List<Activity>()
            {
                new Activity
                {
                    ID = 1,
                    IdentityGUID = "1",
                    CheckpointIP = "192.168.0.1",
                    Date = DateTime.Now,
                    ModeID = 1,
                    Mode = new Mode { Description = "Вход" },
                    Status = true
                },
                new Activity
                {
                    ID = 2,
                    IdentityGUID = "2",
                    CheckpointIP = "192.168.0.1",
                    Date = DateTime.Now,
                    ModeID = 2,
                    Mode = new Mode { Description = "Выход" },
                    Status = true
                },
            };
            unitWorkMoq.Setup(x => x.Activity.Create(It.IsAny<Activity>())).Callback(() =>
            {
                item.ID = 3;
                createdActivity++;
            });

            //Act
            serviceMock.Create(item);

            //Assert
            Assert.AreEqual(3, item.ID);
            Assert.AreEqual(1, createdActivity);
        }

        [TestMethod]
        public void Delete()
        {
            //Arrange
            int ID = -1;
            unitWorkMoq.Setup(x => x.Activity.Delete(It.IsAny<int>())).Callback(() =>
            {
                ID = itemsActivity[0].ID;
            });
            //Act
            serviceMock.Delete(itemsActivity[0]);

            //Assert
            Assert.AreNotEqual(-1, ID);
            Assert.AreEqual(id, ID);
        }

        [TestMethod]
        public void Edit()
        {
            //Arrange
            int updatedActivity = 0;
            unitWorkMoq.Setup(x => x.Activity.Update(It.IsAny<Activity>())).Callback(() =>
            {
                updatedActivity++;
            });
            
            //Act
            serviceMock.Edit(itemsActivity[0]);

            //Assert
            Assert.AreNotEqual(0, updatedActivity);
            Assert.AreEqual(1, updatedActivity);
        }

        [TestMethod]
        public void Get()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Activity.Get(1)).Returns(itemsActivity[0]);
            unitWorkMoq.Setup(x => x.Activity.Get(2)).Returns(itemsActivity[1]);
            unitWorkMoq.Setup(x => x.Activity.Get(3)).Returns(itemsActivity[2]);
            unitWorkMoq.Setup(x => x.Mode.Get(It.IsAny<int>())).Returns(itemsMode[0]);

            //Act
            var result = serviceMock.Get(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Activity));
            Assert.AreEqual(id, result.ID);
        }

        [TestMethod]
        public void Get_All()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Activity.GetAll()).Returns(itemsActivity);
            unitWorkMoq.Setup(x => x.Mode.Get(It.IsAny<int>())).Returns(itemsMode[0]);

            //Act
            var result = serviceMock.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Activity>));
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void Get_By_Status()
        {
            //Arrange
            bool status = true;
            unitWorkMoq.Setup(x => x.Activity.Find(It.IsAny<Func<Activity, bool>>()))
                .Returns(itemsActivity.Where(x => x.Status == status));
            unitWorkMoq.Setup(x => x.Mode.Get(It.IsAny<int>())).Returns(itemsMode[0]);

            //Act
            var result = serviceMock.GetByStatus(status);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Activity>));
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void Is_Admission()
        {
            //Arrange
            string role = "Сотрудник";
            unitWorkMoq.Setup(x => x.CheckpointAdmission.GetAll())
                .Returns(itemsCheckpointAdmission.Where(x => x.CheckpointID == id));
            unitWorkMoq.Setup(x => x.Admission.GetAll()).Returns(itemsAdmission);

            //Act
            var result = serviceMock.IsAdmission(id, role);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Is_Passed()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Activity.GetAll()).Returns(itemsActivity.Where(x => x.IdentityGUID == id.ToString()));

            //Act
            var result = serviceMock.IsPassed(id.ToString());

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Is_OK_No_Response()
        {
            //Arrange
            CheckpointDTO checkpoint = new CheckpointDTO()
            {
                ID = 1,
                IP = "192.168.0.1",
                Campus = 1,
                Row = 4,
                Description = "На 4 этаже, 425 аудитория",
                Status = "Отметка",
                Type = (TypeDTO)itemsType[2],
                Admissions = itemsAdmission
            };
            IdentityDTO identity = new IdentityDTO()
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
            };
            unitWorkMoq.Setup(x => x.Type.Get(1)).Returns(itemsType[0]);
            unitWorkMoq.Setup(x => x.Type.Get(2)).Returns(itemsType[1]);
            unitWorkMoq.Setup(x => x.Type.Get(3)).Returns(itemsType[2]);
            unitWorkMoq.Setup(x => x.Activity.GetAll())
                .Returns(itemsActivity.Where(x => x.IdentityGUID.Equals(id.ToString())));
            unitWorkMoq.Setup(x => x.Admission.GetAll())
                .Returns(itemsAdmission);
            unitWorkMoq.Setup(x => x.CheckpointAdmission.GetAll())
                .Returns(itemsCheckpointAdmission.Where(x => x.CheckpointID == checkpoint.ID));

            //Act
            var result = serviceMock.IsOk(checkpoint, identity);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Is_OK_Security_Allowed()
        {
            //Arrange
            CheckpointDTO checkpoint = new CheckpointDTO()
            {
                ID = 1,
                IP = "192.168.0.1",
                Campus = 1,
                Row = 4,
                Description = "На 4 этаже, 425 аудитория",
                Status = "Отметка",
                Type = (TypeDTO)itemsType[0],
                Admissions = itemsAdmission
            };
            IdentityDTO identity = new IdentityDTO()
            {
                ID = 2,
                GUID = "2",
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
            };
            unitWorkMoq.Setup(x => x.Type.Get(1)).Returns(itemsType[0]);
            unitWorkMoq.Setup(x => x.Type.Get(2)).Returns(itemsType[1]);
            unitWorkMoq.Setup(x => x.Type.Get(3)).Returns(itemsType[2]);
            unitWorkMoq.Setup(x => x.Activity.GetAll())
                .Returns(itemsActivity.Where(x => x.IdentityGUID == id.ToString()));
            unitWorkMoq.Setup(x => x.CheckpointAdmission.GetAll())
                .Returns(itemsCheckpointAdmission.Where(x => x.CheckpointID == checkpoint.ID));
            unitWorkMoq.Setup(x => x.Admission.GetAll()).Returns(itemsAdmission);

            //Act
            var result = serviceMock.IsOk(checkpoint, identity);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(200, result);
        }

        [TestMethod]
        public void Is_OK_Security_Prohibited()
        {
            //Arrange
            CheckpointDTO checkpoint = new CheckpointDTO()
            {
                ID = 1,
                IP = "192.168.0.1",
                Campus = 1,
                Row = 4,
                Description = "На 4 этаже, 425 аудитория",
                Status = "Отметка",
                Type = (TypeDTO)itemsType[0],
                Admissions = itemsAdmission
            };
            IdentityDTO identity = new IdentityDTO()
            {
                ID = 2,
                GUID = "2",
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
            };
            List<CheckpointAdmission> admissions = new List<CheckpointAdmission>()
            {
                new CheckpointAdmission
                {
                    ID = 1,
                    CheckpointID = 2,
                    AdmissionID = 1
                },
                new CheckpointAdmission
                {
                    ID = 2,
                    CheckpointID = 2,
                    AdmissionID = 2
                }
            };
            unitWorkMoq.Setup(x => x.Type.Get(1)).Returns(itemsType[0]);
            unitWorkMoq.Setup(x => x.Type.Get(2)).Returns(itemsType[1]);
            unitWorkMoq.Setup(x => x.Type.Get(3)).Returns(itemsType[2]);
            unitWorkMoq.Setup(x => x.Activity.GetAll())
                .Returns(itemsActivity.Where(x => x.IdentityGUID == id.ToString()));
            unitWorkMoq.Setup(x => x.CheckpointAdmission.GetAll())
                .Returns(admissions.Where(x => x.CheckpointID == checkpoint.ID));
            unitWorkMoq.Setup(x => x.Admission.GetAll()).Returns(itemsAdmission);

            //Act
            var result = serviceMock.IsOk(checkpoint, identity);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(500, result);
        }
    }
}