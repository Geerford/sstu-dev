using Domain.Core.Logs;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace sstu_nevdev.Tests.Repositories
{
    [TestClass]
    public class AuditRepositoryTest
    {
        DbConnection connection;
        Context context;
        AuditRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            connection = Effort.DbConnectionFactory.CreateTransient();
            context = new Context(connection);
            repository = new AuditRepository(context);
        }

        [TestMethod]
        public void Create()
        {
            //Arrange
            Audit item = new Audit
            {
                EntityName = "test",
                Logs = "test",
                ModifiedBy = "test",
                ModifiedDate = DateTime.Now,
                ModifiedFrom = "test"
            };

            //Act
            repository.Create(item);
            context.SaveChanges();
            var result = repository.GetAll().ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Audit>));
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result.Last().ID);
            Assert.AreEqual(item, result.Last());
        }

        [TestMethod]
        public void Get_All()
        {
            //Act
            var result = repository.GetAll().ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Audit>));
            Assert.AreEqual(0, result.ToList().Count);
        }
    }
}
