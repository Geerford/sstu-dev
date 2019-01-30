using Domain.Core.Logs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Interfaces;
using sstu_nevdev.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace sstu_nevdev.Tests.Controllers
{
    [TestClass]
    public class AuditControllerTest
    {
        private Mock<IAuditService> auditServiceMock;
        private AuditController controllerWEB;
        private AuditsController controllerAPI;
        private List<Audit> items;

        [TestInitialize]
        public void Initialize()
        {
            auditServiceMock = new Mock<IAuditService>();
            controllerWEB = new AuditController(auditServiceMock.Object);
            controllerAPI = new AuditsController(auditServiceMock.Object);
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
        public void Index()
        {
            //Arrange
            auditServiceMock.Setup(x => x.GetAll()).Returns(items);

            //Act
            var result = ((controllerWEB.Index() as ViewResult).Model) as List<Audit>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(items, result);
        }

        [TestMethod]
        public void Get_All_API()
        {
            //Arrange
            auditServiceMock.Setup(x => x.GetAll()).Returns(items);

            //Act
            var actionResult = controllerAPI.Get();
            var createdResult = actionResult as OkNegotiatedContentResult<IEnumerable<Audit>>;

            //Assert
            Assert.IsNotNull(createdResult);
            Assert.IsInstanceOfType(createdResult.Content, typeof(IEnumerable<Audit>));
            Assert.AreEqual(items, createdResult.Content);
        }
    }
}
