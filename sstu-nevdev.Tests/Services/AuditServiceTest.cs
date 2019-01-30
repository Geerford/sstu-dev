using Domain.Contracts;
using Domain.Core.Logs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.Interfaces;
using Service.Interfaces;
using Services.Business.Services;
using System;
using System.Collections.Generic;

namespace sstu_nevdev.Tests.Services
{
    [TestClass]
    public class AuditServiceTest
    {
        private IAuditService serviceMock;
        private Mock<IAuditRepository<Audit>> repositoryMock;
        private Mock<IUnitOfWork> unitWorkMock;
        private List<Audit> items;

        [TestInitialize]
        public void Initialize()
        {
            repositoryMock = new Mock<IAuditRepository<Audit>>();
            unitWorkMock = new Mock<IUnitOfWork>();
            serviceMock = new AuditService(unitWorkMock.Object);
            items = new List<Audit>()
            {
                new Audit
                {
                    ID = 1,
                    EntityName = "test",
                    Logs = "test",
                    ModifiedBy = "test",
                    ModifiedDate = DateTime.Now,
                    ModifiedFrom = "test"
                },
                new Audit
                {
                    ID = 2,
                    EntityName = "test",
                    Logs = "test",
                    ModifiedBy = "test",
                    ModifiedDate = DateTime.Now,
                    ModifiedFrom = "test"
                },
                new Audit
                {
                    ID = 3,
                    EntityName = "test",
                    Logs = "test",
                    ModifiedBy = "test",
                    ModifiedDate = DateTime.Now,
                    ModifiedFrom = "test"
                }
            };
        }

        [TestMethod]
        public void Get_All()
        {
            //Arrange
            unitWorkMock.Setup(x => x.Audit.GetAll()).Returns(items);

            //Act
            var results = serviceMock.GetAll();

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<Audit>));
        }
    }
}
