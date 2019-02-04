using Domain.Contracts;
using Domain.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.Interfaces;
using Service.Interfaces;
using Services.Business.Services;
using System.Collections.Generic;
using System.Linq;

namespace sstu_nevdev.Tests.Services
{
    [TestClass]
    public class AdmissionServiceTest
    {
        private IAdmissionService serviceMock;
        private Mock<IRepository<Admission>> repositoryMock;
        private Mock<IUnitOfWork> unitWorkMoq;
        private List<Admission> itemsAdmission;
        private readonly int id = 1;

        [TestInitialize]
        public void Initialize()
        {
            repositoryMock = new Mock<IRepository<Admission>>();
            repositoryMock = new Mock<IRepository<Admission>>();
            unitWorkMoq = new Mock<IUnitOfWork>();
            serviceMock = new AdmissionService(unitWorkMoq.Object);
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
        }

        [TestMethod]
        public void Create()
        {
            //Arrange
            int createdAdmission = 0;
            Admission item = new Admission()
            {
                Role = "Уборщица",
                Description = "Вход в аудиторию 402"
            };
            unitWorkMoq.Setup(x => x.Admission.Create(It.IsAny<Admission>())).Callback(() =>
            {
                item.ID = 3;
                createdAdmission++;
            });

            //Act
            serviceMock.Create(item);

            //Assert
            Assert.AreEqual(3, item.ID);
            Assert.AreEqual(1, createdAdmission);
        }
        
        [TestMethod]
        public void Delete()
        {
            //Arrange
            int ID = -1;
            unitWorkMoq.Setup(x => x.Admission.Delete(It.IsAny<int>())).Callback(() =>
            {
                ID = itemsAdmission[0].ID;
            });
            //Act
            serviceMock.Delete(itemsAdmission[0]);

            //Assert
            Assert.AreNotEqual(-1, ID);
            Assert.AreEqual(id, ID);
        }

        [TestMethod]
        public void Edit()
        {
            //Arrange
            int updatedAdmission = 0;
            unitWorkMoq.Setup(x => x.Admission.Update(It.IsAny<Admission>())).Callback(() =>
            {
                updatedAdmission++;
            });

            //Act
            serviceMock.Edit(itemsAdmission[0]);

            //Assert
            Assert.AreNotEqual(0, updatedAdmission);
            Assert.AreEqual(1, updatedAdmission);
        }

        [TestMethod]
        public void Get()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Admission.Get(1)).Returns(itemsAdmission[0]);
            unitWorkMoq.Setup(x => x.Admission.Get(2)).Returns(itemsAdmission[1]);

            //Act
            var result = serviceMock.Get(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Admission));
            Assert.AreEqual(id, result.ID);
        }

        [TestMethod]
        public void Get_All()
        {
            //Arrange
            unitWorkMoq.Setup(x => x.Admission.GetAll()).Returns(itemsAdmission);

            //Act
            var result = serviceMock.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Admission>));
            Assert.AreEqual(2, result.Count());
        }
    }
}