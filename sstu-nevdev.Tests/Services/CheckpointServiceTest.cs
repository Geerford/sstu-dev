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
using System.Linq;
using Type = Domain.Core.Type;

namespace sstu_nevdev.Tests.Services
{
    [TestClass]
    public class CheckpointServiceTest
    {
        private ICheckpointService serviceMock;
        private Mock<IRepository<Checkpoint>> repositoryMock;
        private Mock<IRepository<CheckpointAdmission>> repositoryCheckpointAdmissionMock;
        private Mock<IRepository<Type>> repositoryTypeMock;
        private Mock<IRepository<Admission>> repositoryAdmissionMock;
        private Mock<IUnitOfWork> unitWorkMoq;
        private List<CheckpointDTO> itemsDTO;
        private List<Checkpoint> itemsSimple;
        private List<CheckpointAdmission> itemsCheckpointAdmission;
        private List<Admission> itemsAdmission;
        private List<Type> itemsType;
        private readonly int id = 1;

        [TestInitialize]
        public void Initialize()
        {
            repositoryMock = new Mock<IRepository<Checkpoint>>();
            repositoryCheckpointAdmissionMock = new Mock<IRepository<CheckpointAdmission>>();
            repositoryTypeMock = new Mock<IRepository<Type>>();
            repositoryAdmissionMock = new Mock<IRepository<Admission>>();
            unitWorkMoq = new Mock<IUnitOfWork>();
            serviceMock = new CheckpointService(unitWorkMoq.Object);
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
            itemsDTO = new List<CheckpointDTO>()
            {
                new CheckpointDTO()
                {
                    ID = 1,
                    IP = "192.168.0.1",
                    Campus = 1,
                    Row = 4,
                    Description = "На 4 этаже, 425 аудитория",
                    Status = "Отметка",
                    Type = (TypeDTO)itemsType[1],
                    Admissions = itemsAdmission
                },
                new CheckpointDTO()
                {
                    ID = 2,
                    IP = "192.168.0.15",
                    Campus = 1,
                    Row = 4,
                    Description = "На 1 этаже на входе",
                    Status = "Пропуск",
                    Type = (TypeDTO)itemsType[0],
                    Admissions = itemsAdmission
                }
            };
            itemsSimple = new List<Checkpoint>()
            {
                new Checkpoint
                {
                    ID = 1,
                    IP = "192.168.0.1",
                    Campus = 1,
                    Row = 4,
                    Description = "На 4 этаже, 425 аудитория",
                    Status = "Отметка",
                    TypeID = 2
                },
                new Checkpoint
                {
                    ID = 2,
                    IP = "192.168.0.15",
                    Campus = 1,
                    Row = 4,
                    Description = "На 1 этаже на входе",
                    Status = "Пропуск",
                    TypeID = 1
                }
            };
        }

        [TestMethod]
        public void Create()
        {
            //Arrange
            int createdCheckpoint = 0, createdAdmission = 0;
            Checkpoint itemSimple = new Checkpoint()
            {
                Campus = itemsDTO[1].Campus,
                Row = itemsDTO[1].Row,
                Description = itemsDTO[1].Description,
                Status = itemsDTO[1].Status,
                TypeID = itemsDTO[1].Type.ID,
                IP = itemsDTO[1].IP
            };
            List<Checkpoint> items = new List<Checkpoint>()
            {
                new Checkpoint
                {
                    ID = 1,
                    IP = "192.168.0.1",
                    Campus = 1,
                    Row = 4,
                    Description = "На 4 этаже, 425 аудитория",
                    Status = "Отметка",
                    TypeID = 2,
                },
                new Checkpoint
                {
                    ID = 2,
                    IP = "192.168.0.15",
                    Campus = 1,
                    Row = 4,
                    Description = "На 1 этаже на входе",
                    Status = "Пропуск",
                    TypeID = 1
                }
            };
            unitWorkMoq.Setup(x => x.Checkpoint.GetAll()).Returns(items);
            unitWorkMoq.Setup(x => x.Checkpoint.Create(It.IsAny<Checkpoint>())).Callback(() =>
            {
                itemSimple.ID = 3;
                createdCheckpoint++;
            });
            unitWorkMoq.Setup(x => x.CheckpointAdmission.Create(It.IsAny<CheckpointAdmission>()))
                .Callback(() =>
            {
                createdAdmission++;
            });

            //Act
            serviceMock.Create(itemsDTO[1]);

            //Assert
            Assert.AreEqual(3, itemSimple.ID);
            Assert.AreEqual(1, createdCheckpoint);
            Assert.AreEqual(2, createdAdmission);
        }

        [TestMethod]
        public void Delete()
        {
            //Arrange
            int ID = -1;
            unitWorkMoq.Setup(x => x.Checkpoint.Delete(It.IsAny<int>())).Callback(() =>
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
        public void Delete_Admission()
        {
            //Arrange
            int admissionID = -1, checkpointID = -1;
            unitWorkMoq.Setup(x => x.CheckpointAdmission.GetAll()).Returns(itemsCheckpointAdmission);
            unitWorkMoq.Setup(x => x.CheckpointAdmission.Delete(It.IsAny<int>())).Callback((int callbackID) =>
            {
                admissionID = itemsAdmission[0].ID;
                checkpointID = itemsSimple[0].ID;
            });

            //Act
            serviceMock.Delete(itemsSimple[0].ID, itemsAdmission[0].ID);

            //Assert
            Assert.AreNotEqual(-1, admissionID);
            Assert.AreNotEqual(-1, checkpointID);
            Assert.AreEqual(id, admissionID);
            Assert.AreEqual(id, checkpointID);
        }

        [TestMethod]
        public void Delete_All_Admission()
        {
            //Arrange
            int checkpointID = -1;
            unitWorkMoq.Setup(x => x.CheckpointAdmission.GetAll()).Returns(itemsCheckpointAdmission);
            unitWorkMoq.Setup(x => x.CheckpointAdmission.Delete(It.IsAny<int>())).Callback((int callbackID) =>
            {
                checkpointID = itemsSimple[0].ID;
            });

            //Act
            serviceMock.DeleteAllAdmission(itemsSimple[0].ID);

            //Assert
            Assert.AreNotEqual(-1, checkpointID);
            Assert.AreEqual(id, checkpointID);
        }

        [TestMethod]
        public void Edit()
        {
            //Arrange
            int updatedCheckpoint = 0, updatedAdmission = 0, deletedAdmission = 0;
            unitWorkMoq.Setup(x => x.Checkpoint.Update(It.IsAny<Checkpoint>())).Callback(() =>
            {
                updatedCheckpoint++;
            });
            unitWorkMoq.Setup(x => x.Checkpoint.GetAll()).Returns(itemsSimple);
            unitWorkMoq.Setup(x => x.CheckpointAdmission.GetAll()).Returns(itemsCheckpointAdmission);
            unitWorkMoq.Setup(x => x.CheckpointAdmission.Create(It.IsAny<CheckpointAdmission>()))
                .Callback(() =>
                {
                    updatedAdmission++;
                });
            unitWorkMoq.Setup(x => x.CheckpointAdmission.Delete(It.IsAny<int>()))
                .Callback(() =>
                {
                    deletedAdmission++;
                });

            //Act
            serviceMock.Edit(itemsDTO[0]);

            //Assert
            Assert.AreNotEqual(0, updatedCheckpoint);
            Assert.AreNotEqual(0, updatedAdmission);
            Assert.AreNotEqual(0, deletedAdmission);
            Assert.AreEqual(1, updatedCheckpoint);
            Assert.AreEqual(2, updatedAdmission);
            Assert.AreEqual(2, deletedAdmission);
        }

        [TestMethod]
        public void Get_Simple_Model()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Checkpoint.Get(1)).Returns(itemsSimple[0]);
            unitWorkMoq.Setup(x => x.Checkpoint.Get(2)).Returns(itemsSimple[1]);

            //Act
            var result = serviceMock.GetSimple(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Checkpoint));
            Assert.AreEqual(id, result.ID);
        }

        [TestMethod]
        public void Get()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Checkpoint.Get(1)).Returns(itemsSimple[0]);
            unitWorkMoq.Setup(x => x.Checkpoint.Get(2)).Returns(itemsSimple[0]);
            unitWorkMoq.Setup(x => x.Type.Get(1)).Returns(itemsType[0]);
            unitWorkMoq.Setup(x => x.Type.Get(2)).Returns(itemsType[1]);
            unitWorkMoq.Setup(x => x.Type.Get(3)).Returns(itemsType[2]);
            unitWorkMoq.Setup(x => x.CheckpointAdmission.GetAll()).Returns(itemsCheckpointAdmission);
            unitWorkMoq.Setup(x => x.Admission.Get(It.IsAny<int>())).Returns(itemsAdmission[0]);

            //Act
            var result = serviceMock.Get(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CheckpointDTO));
            Assert.IsNotNull(result.ID);
            Assert.IsNotNull(result.IP);
            Assert.IsNotNull(result.Campus);
            Assert.IsNotNull(result.Row);
            Assert.IsNotNull(result.Description);
            Assert.IsNotNull(result.Status);
            Assert.IsNotNull(result.Type);
            Assert.IsNotNull(result.Admissions);
            Assert.AreEqual(id, result.ID);
            Assert.IsTrue(result.Admissions.ToList().Count > 0);
        }

        [TestMethod]
        public void Get_By_IP()
        {
            //Arrange 
            string ip = "192.168.0.1";
            unitWorkMoq.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>())).Returns(itemsSimple);
            unitWorkMoq.Setup(x => x.Checkpoint.Get(1)).Returns(itemsSimple[0]);
            unitWorkMoq.Setup(x => x.Checkpoint.Get(2)).Returns(itemsSimple[0]);
            unitWorkMoq.Setup(x => x.Type.Get(1)).Returns(itemsType[0]);
            unitWorkMoq.Setup(x => x.Type.Get(2)).Returns(itemsType[1]);
            unitWorkMoq.Setup(x => x.Type.Get(3)).Returns(itemsType[2]);
            unitWorkMoq.Setup(x => x.CheckpointAdmission.GetAll()).Returns(itemsCheckpointAdmission);
            unitWorkMoq.Setup(x => x.Admission.Get(1)).Returns(itemsAdmission[0]);
            unitWorkMoq.Setup(x => x.Admission.Get(2)).Returns(itemsAdmission[1]);

            //Act
            var result = serviceMock.GetByIP(ip);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CheckpointDTO));
            Assert.AreEqual(id, result.ID);
            Assert.IsNotNull(result.ID);
            Assert.IsNotNull(result.IP);
            Assert.IsNotNull(result.Campus);
            Assert.IsNotNull(result.Row);
            Assert.IsNotNull(result.Description);
            Assert.IsNotNull(result.Status);
            Assert.IsNotNull(result.Type);
            Assert.IsNotNull(result.Admissions);
            Assert.IsTrue(result.Admissions.ToList().Count > 0);
        }

        [TestMethod]
        public void Get_All()
        {
            //Arrange 
            unitWorkMoq.Setup(x => x.Checkpoint.GetAll()).Returns(itemsSimple);
            unitWorkMoq.Setup(x => x.Checkpoint.Get(1)).Returns(itemsSimple[0]);
            unitWorkMoq.Setup(x => x.Checkpoint.Get(2)).Returns(itemsSimple[1]);
            unitWorkMoq.Setup(x => x.Type.Get(1)).Returns(itemsType[0]);
            unitWorkMoq.Setup(x => x.Type.Get(2)).Returns(itemsType[1]);
            unitWorkMoq.Setup(x => x.Type.Get(3)).Returns(itemsType[2]);
            unitWorkMoq.Setup(x => x.CheckpointAdmission.GetAll()).Returns(itemsCheckpointAdmission);
            unitWorkMoq.Setup(x => x.Admission.Get(1)).Returns(itemsAdmission[0]);
            unitWorkMoq.Setup(x => x.Admission.Get(2)).Returns(itemsAdmission[1]);

            //Act
            var result = serviceMock.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<CheckpointDTO>));
            Assert.AreEqual(2, result.ToList().Count);
        }

        [TestMethod]
        public void Get_By_Status()
        {
            //Arrange
            string status = "Отметка";
            unitWorkMoq.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>()))
                .Returns(itemsSimple.Where(x => x.Status == status));
            unitWorkMoq.Setup(x => x.Checkpoint.Get(1)).Returns(itemsSimple[0]);
            unitWorkMoq.Setup(x => x.Checkpoint.Get(2)).Returns(itemsSimple[1]);
            unitWorkMoq.Setup(x => x.Type.Get(1)).Returns(itemsType[0]);
            unitWorkMoq.Setup(x => x.Type.Get(2)).Returns(itemsType[1]);
            unitWorkMoq.Setup(x => x.Type.Get(3)).Returns(itemsType[2]);
            unitWorkMoq.Setup(x => x.CheckpointAdmission.GetAll()).Returns(itemsCheckpointAdmission);
            unitWorkMoq.Setup(x => x.Admission.Get(1)).Returns(itemsAdmission[0]);
            unitWorkMoq.Setup(x => x.Admission.Get(2)).Returns(itemsAdmission[1]);

            //Act
            var result = serviceMock.GetByStatus(status);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<CheckpointDTO>));
            Assert.AreEqual(1, result.ToList().Count);
        }

        [TestMethod]
        public void Get_By_Type()
        {
            //Arrange
            string type = "Лекционный";
            unitWorkMoq.Setup(x => x.Type.Find(It.IsAny<Func<Type, bool>>()))
                .Returns(itemsType.Where(x => x.Status == type));
            unitWorkMoq.Setup(x => x.Checkpoint.Find(It.IsAny<Func<Checkpoint, bool>>()))
                .Returns(itemsSimple.Where(x => x.TypeID == 2));
            unitWorkMoq.Setup(x => x.Checkpoint.Get(1)).Returns(itemsSimple[0]);
            unitWorkMoq.Setup(x => x.Checkpoint.Get(2)).Returns(itemsSimple[1]);
            unitWorkMoq.Setup(x => x.Type.Get(1)).Returns(itemsType[0]);
            unitWorkMoq.Setup(x => x.Type.Get(2)).Returns(itemsType[1]);
            unitWorkMoq.Setup(x => x.Type.Get(3)).Returns(itemsType[2]);
            unitWorkMoq.Setup(x => x.CheckpointAdmission.GetAll()).Returns(itemsCheckpointAdmission);
            unitWorkMoq.Setup(x => x.Admission.Get(1)).Returns(itemsAdmission[0]);
            unitWorkMoq.Setup(x => x.Admission.Get(2)).Returns(itemsAdmission[1]);

            //Act
            var result = serviceMock.GetByType(type);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<CheckpointDTO>));
            Assert.AreEqual(1, result.ToList().Count);
        }

        [TestMethod]
        public void Get_Full()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Checkpoint.Get(1)).Returns(itemsSimple[0]);
            unitWorkMoq.Setup(x => x.Checkpoint.Get(2)).Returns(itemsSimple[1]);
            unitWorkMoq.Setup(x => x.Type.Get(1)).Returns(itemsType[0]);
            unitWorkMoq.Setup(x => x.Type.Get(2)).Returns(itemsType[1]);
            unitWorkMoq.Setup(x => x.Type.Get(3)).Returns(itemsType[2]);
            unitWorkMoq.Setup(x => x.CheckpointAdmission.GetAll()).Returns(itemsCheckpointAdmission);
            unitWorkMoq.Setup(x => x.Admission.Get(1)).Returns(itemsAdmission[0]);
            unitWorkMoq.Setup(x => x.Admission.Get(2)).Returns(itemsAdmission[1]);

            //Act
            var result = serviceMock.GetFull(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CheckpointDTO));
            Assert.IsNotNull(result.ID);
            Assert.IsNotNull(result.IP);
            Assert.IsNotNull(result.Campus);
            Assert.IsNotNull(result.Row);
            Assert.IsNotNull(result.Description);
            Assert.IsNotNull(result.Status);
            Assert.IsNotNull(result.Type);
            Assert.IsNotNull(result.Admissions);
            Assert.AreEqual(id, result.ID);
            Assert.AreEqual(2, result.Admissions.ToList().Count);
        }

        [TestMethod]
        public void Is_Match_Admission()
        {
            //Arrange
            int checkpointID = id, admissionID = id;
            unitWorkMoq.Setup(x => x.CheckpointAdmission.FindFirst(It.IsAny<Func<CheckpointAdmission, bool>>()))
                .Returns(itemsCheckpointAdmission.Where(i => i.CheckpointID == checkpointID &&
                i.AdmissionID == admissionID).FirstOrDefault());

            //Act
            var result = serviceMock.IsMatchAdmission(checkpointID, admissionID);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }
    }
}