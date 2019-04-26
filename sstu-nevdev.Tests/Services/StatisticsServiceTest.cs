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

namespace sstu_nevdev.Tests.Services
{
    [TestClass]
    public class StatisticsServiceTest
    {
        private IStatisticsService serviceMock;
        private Mock<IUnitOfWork> unitWorkMock;
        private Mock<ISyncUnitOfWork> syncUnitOfWork;
        private List<Admission> admissions;
        private List<CheckpointAdmission> admissionsCheckpoints;
        private List<ActivityDTO> activities;
        private List<Activity> activitiesSimple;
        private List<Checkpoint> checkpoints;
        private List<Identity> usersSimple;
        private List<Mode> modes;
        private List<Domain.Core.Type> types;
        private List<User> usersSync;
        private readonly int id = 1;
        private readonly int campus = 1;
        private readonly int section1 = 1;
        private readonly int section2 = 3;
        private readonly int row = 4;
        private readonly int classroom = 425;
        private readonly string group = "ИФСТ";
        private readonly DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
        private DateTime end;
        


        [TestInitialize]
        public void Initialize()
        {
            unitWorkMock = new Mock<IUnitOfWork>();
            syncUnitOfWork = new Mock<ISyncUnitOfWork>();
            serviceMock = new StatisticsService(unitWorkMock.Object, syncUnitOfWork.Object);
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
            admissionsCheckpoints = new List<CheckpointAdmission>()
            {
                new CheckpointAdmission {
                    CheckpointID = 1,
                    AdmissionID = 1
                },
                new CheckpointAdmission
                {
                    CheckpointID = 2,
                    AdmissionID = 1
                },
                new CheckpointAdmission
                {
                    CheckpointID = 2,
                    AdmissionID = 2
                }
            };
            types = new List<Domain.Core.Type>()
            {
                new Domain.Core.Type {
                    Description = "Отмечает посещаемость",
                    Status = "Лекционный"
                },
                new Domain.Core.Type
                {
                    Description = "Собирает статистику",
                    Status = "Статистический"
                }
            };
            activities = new List<ActivityDTO>()
            {
                new ActivityDTO
                {
                    ID = 1,
                    User = new IdentityDTO()
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
                        Role = "Студент",
                        Picture = "cat.jpg"
                    },
                    Checkpoint = new CheckpointDTO()
                    {
                        IP = "192.168.0.1",
                        Campus = 1,
                        Row = 4,
                        Section = 1,
                        Classroom = 425,
                        Description = "В лекционной аудитории",
                        Status = "Отметка",
                        Type = new TypeDTO
                        {
                            Description = "Собирает статистику",
                            Status = "Статистический"
                        }                        
                    },
                    Date = DateTime.Now,
                    Mode = new Mode { Description = "Вход" },
                    Status = true
                },
                new ActivityDTO
                {
                    ID = 1,
                    User = new IdentityDTO()
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
                        Role = "Студент",
                        Picture = "cat.jpg"
                    },
                    Checkpoint = new CheckpointDTO()
                    {
                        IP = "192.168.0.1",
                        Campus = 1,
                        Row = 4,
                        Section = 1,
                        Classroom = 425,
                        Description = "В лекционной аудитории",
                        Status = "Отметка",
                        Type = new TypeDTO
                        {
                            Description = "Собирает статистику",
                            Status = "Статистический"
                        }
                    },
                    Date = DateTime.Now,
                    Mode = new Mode { Description = "Вход" },
                    Status = true
                },
                new ActivityDTO
                {
                    ID = 1,
                    User = new IdentityDTO()
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
                        Role = "Студент",
                        Picture = "cat.jpg"
                    },
                    Checkpoint = new CheckpointDTO()
                    {
                        IP = "192.168.0.1",
                        Campus = 1,
                        Row = 4,
                        Section = 1,
                        Classroom = 425,
                        Description = "В лекционной аудитории",
                        Status = "Отметка",
                        Type = new TypeDTO
                        {
                            Description = "Собирает статистику",
                            Status = "Статистический"
                        }
                    },
                    Date = DateTime.Now,
                    Mode = new Mode { Description = "Выход" },
                    Status = true
                }
            };
            activitiesSimple = new List<Activity>()
            {
                new Activity
                {
                    IdentityGUID = "1",
                    CheckpointIP = "192.168.0.1",
                    Date = DateTime.Now,
                    ModeID = 1,
                    Mode = new Mode { Description = "Вход" },
                    Status = true
                },
                new Activity
                {
                    IdentityGUID = "2",
                    CheckpointIP = "192.168.0.1",
                    Date = DateTime.Now,
                    ModeID = 1,
                    Mode = new Mode { Description = "Вход" },
                    Status = true
                },
                new Activity
                {
                    IdentityGUID = "2",
                    CheckpointIP = "192.168.0.1",
                    Date = DateTime.Now,
                    ModeID = 2,
                    Mode = new Mode { Description = "Выход" },
                    Status = true
                }
            };
            checkpoints = new List<Checkpoint>()
            {
                new Checkpoint
                {
                    ID = 1,
                    IP = "192.168.0.1",
                    Campus = 1,
                    Row = 4,
                    Section = 1,
                    Classroom = 425,
                    Description = "В лекционной аудитории",
                    Status = "Отметка",
                    Type = new Domain.Core.Type
                    {
                        Description = "Собирает статистику",
                        Status = "Статистический"
                    }
                },
                new Checkpoint
                {
                    ID = 2,
                    IP = "192.168.0.15",
                    Campus = 1,
                    Row = 4,
                    Section = 3,
                    Description = "На углу на 4 этаже",
                    Status = "Пропуск",
                    Type = new Domain.Core.Type
                    {
                        Description = "Пропускает через ворота",
                        Status = "Пропускной"
                    }
                }
            };
            modes = new List<Mode>()
            {
                new Mode {
                    Description = "Отмечает событие входа в объект",
                    Status = "Вход"
                },
                new Mode {
                    Description = "Отмечает событие выхода из объекта",
                    Status = "Выход"
                },
                new Mode {
                    Description = "Собирает статистические данные передвижений субъекта",
                    Status = "Статистика"
                }
            };
            usersSimple = new List<Identity>()
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
            usersSync = new List<User>()
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
            end = DateTime.Now;
        }

        [TestMethod]
        public void Get_By_Campus()
        {
            //Arrange
            unitWorkMock.Setup(x => x.Checkpoint.GetAll()).Returns(checkpoints.Where(i => i.Campus == campus));
            unitWorkMock.Setup(x => x.Checkpoint.Get(It.IsAny<int>())).Returns(checkpoints.Where(i => i.ID == id)
                .FirstOrDefault());
            unitWorkMock.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>()))
                .Returns(checkpoints);
            unitWorkMock.Setup(x => x.Activity.GetAll()).Returns(activitiesSimple);
            unitWorkMock.Setup(x => x.Identity.GetAll()).Returns(usersSimple);
            unitWorkMock.Setup(x => x.Mode.GetAll()).Returns(modes);
            unitWorkMock.Setup(x => x.Type.Get(It.IsAny<int>())).Returns(types[0]);
            unitWorkMock.Setup(x => x.Admission.GetAll()).Returns(admissions);
            unitWorkMock.Setup(x => x.CheckpointAdmission.GetAll()).Returns(admissionsCheckpoints);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(usersSync);

            //Act
            var results = serviceMock.GetByCampus(campus, start, end);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<ActivityDTO>));
            Assert.AreEqual(activities.Count, results.ToList().Count);
        }
        
        [TestMethod]
        public void Get_By_Campus_Row()
        {
            //Arrange
            unitWorkMock.Setup(x => x.Checkpoint.GetAll()).Returns(checkpoints.Where(i => i.Campus == campus && i.Row == row));
            unitWorkMock.Setup(x => x.Checkpoint.Get(It.IsAny<int>())).Returns(checkpoints.Where(i => i.ID == id)
                .FirstOrDefault());
            unitWorkMock.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>()))
                .Returns(checkpoints);
            unitWorkMock.Setup(x => x.Activity.GetAll()).Returns(activitiesSimple);
            unitWorkMock.Setup(x => x.Identity.GetAll()).Returns(usersSimple);
            unitWorkMock.Setup(x => x.Mode.GetAll()).Returns(modes);
            unitWorkMock.Setup(x => x.Type.Get(It.IsAny<int>())).Returns(types[0]);
            unitWorkMock.Setup(x => x.Admission.GetAll()).Returns(admissions);
            unitWorkMock.Setup(x => x.CheckpointAdmission.GetAll()).Returns(admissionsCheckpoints);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(usersSync);

            //Act
            var results = serviceMock.GetByCampusRow(campus, row, start, end);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<ActivityDTO>));
            Assert.AreEqual(activities.Count, results.ToList().Count);
        }

        [TestMethod]
        public void Get_By_Classroom()
        {
            //Arrange
            unitWorkMock.Setup(x => x.Checkpoint.GetAll()).Returns(checkpoints.Where(i => i.Classroom == classroom));
            unitWorkMock.Setup(x => x.Checkpoint.Get(It.IsAny<int>())).Returns(checkpoints.Where(i => i.ID == id)
                .FirstOrDefault());
            unitWorkMock.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>()))
                .Returns(checkpoints);
            unitWorkMock.Setup(x => x.Activity.GetAll()).Returns(activitiesSimple);
            unitWorkMock.Setup(x => x.Identity.GetAll()).Returns(usersSimple);
            unitWorkMock.Setup(x => x.Mode.GetAll()).Returns(modes);
            unitWorkMock.Setup(x => x.Type.Get(It.IsAny<int>())).Returns(types[0]);
            unitWorkMock.Setup(x => x.Admission.GetAll()).Returns(admissions);
            unitWorkMock.Setup(x => x.CheckpointAdmission.GetAll()).Returns(admissionsCheckpoints);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(usersSync);

            //Act
            var results = serviceMock.GetByClassroom(classroom, start, end);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<ActivityDTO>));
            Assert.AreEqual(activities.Count, results.ToList().Count);
        }

        [TestMethod]
        public void Get_By_Group()
        {
            //Arrange
            unitWorkMock.Setup(x => x.Checkpoint.GetAll()).Returns(checkpoints.Where(i => i.Campus == campus && i.Row == row));
            unitWorkMock.Setup(x => x.Checkpoint.Get(It.IsAny<int>())).Returns(checkpoints.Where(i => i.ID == id)
                .FirstOrDefault());
            unitWorkMock.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>()))
                .Returns(checkpoints);
            unitWorkMock.Setup(x => x.Activity.GetAll()).Returns(activitiesSimple);
            unitWorkMock.Setup(x => x.Identity.GetAll()).Returns(usersSimple);
            unitWorkMock.Setup(x => x.Mode.GetAll()).Returns(modes);
            unitWorkMock.Setup(x => x.Type.Get(It.IsAny<int>())).Returns(types[0]);
            unitWorkMock.Setup(x => x.Admission.GetAll()).Returns(admissions);
            unitWorkMock.Setup(x => x.CheckpointAdmission.GetAll()).Returns(admissionsCheckpoints);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(usersSync);

            //Act
            var results = serviceMock.GetByGroup(group, start, end);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<ActivityDTO>));
            Assert.AreEqual(activities.Count, results.ToList().Count);
        }

        [TestMethod]
        public void Get_By_Section()
        {
            //Arrange
            unitWorkMock.Setup(x => x.Checkpoint.GetAll()).Returns(checkpoints.Where(i => i.Section == section1));
            unitWorkMock.Setup(x => x.Checkpoint.Get(It.IsAny<int>())).Returns(checkpoints.Where(i => i.ID == id)
                .FirstOrDefault());
            unitWorkMock.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>()))
                .Returns(checkpoints);
            unitWorkMock.Setup(x => x.Activity.GetAll()).Returns(activitiesSimple);
            unitWorkMock.Setup(x => x.Identity.GetAll()).Returns(usersSimple);
            unitWorkMock.Setup(x => x.Mode.GetAll()).Returns(modes);
            unitWorkMock.Setup(x => x.Type.Get(It.IsAny<int>())).Returns(types[0]);
            unitWorkMock.Setup(x => x.Admission.GetAll()).Returns(admissions);
            unitWorkMock.Setup(x => x.CheckpointAdmission.GetAll()).Returns(admissionsCheckpoints);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(usersSync);

            //Act
            var results = serviceMock.GetBySection(section1, start, end);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<ActivityDTO>));
            Assert.AreEqual(activities.Count, results.ToList().Count);
        }

        [TestMethod]
        public void Get_By_User_Name()
        {
            //Arrange
            string name = "Сидр", midname = "Сидорович", surname = "Сидоров";
            unitWorkMock.Setup(x => x.Checkpoint.GetAll()).Returns(checkpoints.Where(i => i.Campus == campus && i.Row == row));
            unitWorkMock.Setup(x => x.Checkpoint.Get(It.IsAny<int>())).Returns(checkpoints.Where(i => i.ID == id)
                .FirstOrDefault());
            unitWorkMock.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>()))
                .Returns(checkpoints);
            unitWorkMock.Setup(x => x.Activity.GetAll()).Returns(activitiesSimple);
            unitWorkMock.Setup(x => x.Identity.GetAll()).Returns(usersSimple);
            unitWorkMock.Setup(x => x.Mode.GetAll()).Returns(modes);
            unitWorkMock.Setup(x => x.Type.Get(It.IsAny<int>())).Returns(types[0]);
            unitWorkMock.Setup(x => x.Admission.GetAll()).Returns(admissions);
            unitWorkMock.Setup(x => x.CheckpointAdmission.GetAll()).Returns(admissionsCheckpoints);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(usersSync);
            syncUnitOfWork.Setup(x => x.User.Find(It.IsAny<Func<User, bool>>())).Returns(usersSync);

            //Act
            var results = serviceMock.GetByUser(name, midname, surname, start, end);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<ActivityDTO>));
            Assert.AreEqual(1, results.ToList().Count);
        }

        [TestMethod]
        public void Get_By_User_GUID()
        {
            //Arrange
            string guid = "1";
            unitWorkMock.Setup(x => x.Checkpoint.GetAll()).Returns(checkpoints.Where(i => i.Campus == campus && i.Row == row));
            unitWorkMock.Setup(x => x.Checkpoint.Get(It.IsAny<int>())).Returns(checkpoints.Where(i => i.ID == id)
                .FirstOrDefault());
            unitWorkMock.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>()))
                .Returns(checkpoints);
            unitWorkMock.Setup(x => x.Activity.GetAll()).Returns(activitiesSimple);
            unitWorkMock.Setup(x => x.Identity.GetAll()).Returns(usersSimple);
            unitWorkMock.Setup(x => x.Mode.GetAll()).Returns(modes);
            unitWorkMock.Setup(x => x.Type.Get(It.IsAny<int>())).Returns(types[0]);
            unitWorkMock.Setup(x => x.Admission.GetAll()).Returns(admissions);
            unitWorkMock.Setup(x => x.CheckpointAdmission.GetAll()).Returns(admissionsCheckpoints);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(usersSync);

            //Act
            var results = serviceMock.GetByUser(guid, start, end);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<ActivityDTO>));
            Assert.AreEqual(1, results.ToList().Count);
        }

        [TestMethod]
        public void Get_Current_By_Campus()
        {
            //Arrange
            unitWorkMock.Setup(x => x.Checkpoint.GetAll()).Returns(checkpoints.Where(i => i.Campus == campus));
            unitWorkMock.Setup(x => x.Checkpoint.Get(It.IsAny<int>())).Returns(checkpoints.Where(i => i.ID == id)
                .FirstOrDefault());
            unitWorkMock.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>()))
                .Returns(checkpoints);
            unitWorkMock.Setup(x => x.Activity.GetAll()).Returns(activitiesSimple);
            unitWorkMock.Setup(x => x.Identity.GetAll()).Returns(usersSimple);
            unitWorkMock.Setup(x => x.Mode.GetAll()).Returns(modes);
            unitWorkMock.Setup(x => x.Type.Get(It.IsAny<int>())).Returns(types[0]);
            unitWorkMock.Setup(x => x.Admission.GetAll()).Returns(admissions);
            unitWorkMock.Setup(x => x.CheckpointAdmission.GetAll()).Returns(admissionsCheckpoints);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(usersSync);

            //Act
            var results = serviceMock.GetCurrentByCampus(campus);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<IdentityDTO>));
            Assert.AreEqual(1, results.ToList().Count);
        }

        [TestMethod]
        public void Get_Current_By_Campus_Row()
        {
            //Arrange
            unitWorkMock.Setup(x => x.Checkpoint.GetAll()).Returns(checkpoints.Where(i => i.Campus == campus && i.Row == row));
            unitWorkMock.Setup(x => x.Checkpoint.Get(It.IsAny<int>())).Returns(checkpoints.Where(i => i.ID == id)
                .FirstOrDefault());
            unitWorkMock.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>()))
                .Returns(checkpoints);
            unitWorkMock.Setup(x => x.Activity.GetAll()).Returns(activitiesSimple);
            unitWorkMock.Setup(x => x.Identity.GetAll()).Returns(usersSimple);
            unitWorkMock.Setup(x => x.Mode.GetAll()).Returns(modes);
            unitWorkMock.Setup(x => x.Type.Get(It.IsAny<int>())).Returns(types[0]);
            unitWorkMock.Setup(x => x.Admission.GetAll()).Returns(admissions);
            unitWorkMock.Setup(x => x.CheckpointAdmission.GetAll()).Returns(admissionsCheckpoints);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(usersSync);

            //Act
            var results = serviceMock.GetCurrentByCampusRow(campus, row);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<IdentityDTO>));
            Assert.AreEqual(1, results.ToList().Count);
        }

        [TestMethod]
        public void Get_Current_By_Classroom()
        {
            //Arrange
            unitWorkMock.Setup(x => x.Checkpoint.GetAll()).Returns(checkpoints.Where(i => i.Classroom == classroom));
            unitWorkMock.Setup(x => x.Checkpoint.Get(It.IsAny<int>())).Returns(checkpoints.Where(i => i.ID == id)
                .FirstOrDefault());
            unitWorkMock.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>()))
                .Returns(checkpoints);
            unitWorkMock.Setup(x => x.Activity.GetAll()).Returns(activitiesSimple);
            unitWorkMock.Setup(x => x.Identity.GetAll()).Returns(usersSimple);
            unitWorkMock.Setup(x => x.Mode.GetAll()).Returns(modes);
            unitWorkMock.Setup(x => x.Type.Get(It.IsAny<int>())).Returns(types[0]);
            unitWorkMock.Setup(x => x.Admission.GetAll()).Returns(admissions);
            unitWorkMock.Setup(x => x.CheckpointAdmission.GetAll()).Returns(admissionsCheckpoints);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(usersSync);

            //Act
            var results = serviceMock.GetCurrentByClassroom(classroom);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<IdentityDTO>));
            Assert.AreEqual(1, results.ToList().Count);
        }

        [TestMethod]
        public void Get_Current_By_Section()
        {
            //Arrange
            unitWorkMock.Setup(x => x.Checkpoint.GetAll()).Returns(checkpoints.Where(i => i.Section == section1));
            unitWorkMock.Setup(x => x.Checkpoint.Get(It.IsAny<int>())).Returns(checkpoints.Where(i => i.ID == id)
                .FirstOrDefault());
            unitWorkMock.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>()))
                .Returns(checkpoints);
            unitWorkMock.Setup(x => x.Activity.GetAll()).Returns(activitiesSimple);
            unitWorkMock.Setup(x => x.Identity.GetAll()).Returns(usersSimple);
            unitWorkMock.Setup(x => x.Mode.GetAll()).Returns(modes);
            unitWorkMock.Setup(x => x.Type.Get(It.IsAny<int>())).Returns(types[0]);
            unitWorkMock.Setup(x => x.Admission.GetAll()).Returns(admissions);
            unitWorkMock.Setup(x => x.CheckpointAdmission.GetAll()).Returns(admissionsCheckpoints);
            syncUnitOfWork.Setup(x => x.User.GetAll()).Returns(usersSync);

            //Act
            var results = serviceMock.GetCurrentBySection(section2);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<IdentityDTO>));
            Assert.AreEqual(0, results.ToList().Count);
        }
    }
}
