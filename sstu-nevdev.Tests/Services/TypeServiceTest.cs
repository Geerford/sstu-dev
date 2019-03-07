using Domain.Contracts;
using Domain.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.Interfaces;
using Service.Interfaces;
using Services.Business.Service;
using System.Collections.Generic;

namespace sstu_nevdev.Tests.Services
{
    [TestClass]
    public class TypeServiceTest
    {
        private ITypeService serviceMock;
        private Mock<IRepository<Type>> repositoryMock;
        private Mock<IUnitOfWork> unitWorkMock;
        private List<Type> items;
        private readonly int id = 1;

        [TestInitialize]
        public void Initialize()
        {
            repositoryMock = new Mock<IRepository<Type>>();
            unitWorkMock = new Mock<IUnitOfWork>();
            serviceMock = new TypeService(unitWorkMock.Object);
            items = new List<Type>()
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
        }

        [TestMethod]
        public void Create()
        {
            //Arrange
            Type item = new Type
            {
                Description = "test",
                Status = "test"
            };
            unitWorkMock.Setup(x => x.Type.Create(item)).Callback(() => {
                item.ID = id;
            });

            //Act
            serviceMock.Create(item);

            //Assert
            Assert.AreEqual(id, item.ID);
        }

        [TestMethod]
        public void Delete()
        {
            //Arrange
            int ID = -1;
            unitWorkMock.Setup(x => x.Type.Delete(items[0].ID)).Callback(() => 
            {
                ID = items[0].ID;
            });

            //Act
            serviceMock.Delete(items[0]);

            //Assert
            Assert.AreNotEqual(-1, ID);
            Assert.AreEqual(id, ID);
        }

        [TestMethod]
        public void Edit()
        {
            //Arrange
            Type item = new Type();
            unitWorkMock.Setup(x => x.Type.Update(items[0])).Callback(() => 
            {
                item.ID = items[0].ID;
                item.Description = items[0].Description;
                item.Status = items[0].Status;
            });

            //Act
            serviceMock.Edit(items[0]);

            //Assert
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.ID);
            Assert.AreEqual(items[0], item);
        }

        [TestMethod]
        public void Get()
        {
            //Arrange
            unitWorkMock.Setup(x => x.Type.Get(id)).Returns(items[0]);

            //Act
            var result = serviceMock.Get(id);
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.ID);
            Assert.IsInstanceOfType(result, typeof(Type));
        }

        [TestMethod]
        public void Get_All()
        {
            //Arrange
            unitWorkMock.Setup(x => x.Type.GetAll()).Returns(items);

            //Act
            var results = serviceMock.GetAll();

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<Type>));
        }

        [TestMethod]
        public void Get_By_Status()
        {
            //Arrange
            unitWorkMock.Setup(x => x.Type.Find(It.IsAny<System.Func<Type, bool>>())).Returns(items);

            //Act
            var results = serviceMock.GetByStatus("Статистический");

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<Type>));
        }
    }
}