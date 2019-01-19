using Domain.Core;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace sstu_nevdev.Tests.Repositories
{
    [TestClass]
    public class TypeRepositoryTest
    {
        DbConnection connection;
        Context context;
        TypeRepository repository;
        private readonly int id = 1;

        [TestInitialize]
        public void Initialize()
        {
            connection = Effort.DbConnectionFactory.CreateTransient();
            context = new Context(connection);
            repository = new TypeRepository(context);
        }

        [TestMethod]
        public void Create()
        {
            //Arrange
            Type item = new Type()
            {
                Description = "test",
                Status = "test"
            };

            //Act
            repository.Create(item);
            context.SaveChanges();
            var result = repository.GetAll().ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Type>));
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(4, result.Last().ID);
            Assert.AreEqual(item, result.Last());
        }

        [TestMethod]
        public void Delete()
        {
            //Act
            repository.Delete(id);
            context.SaveChanges();
            var result = repository.GetAll().ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreNotEqual(1, result[0].ID);
        }

        [TestMethod]
        public void Find()
        {
            //Act
            var result = repository.Find(x => x.ID > 0).ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Type>));
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void Find_First()
        {
            //Act
            var result = repository.FindFirst(x => x.ID > 0);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Type));
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void Get()
        {
            //Act
            var result = repository.Get(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Type));
            Assert.AreEqual(id, result.ID);
        }

        [TestMethod]
        public void Get_All()
        {
            //Act
            var result = repository.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Type>));
            Assert.AreEqual(3, result.ToList().Count);
        }

        [TestMethod]
        public void Update()
        {
            //Arrange
            Type item = new Type()
            {
                ID = 1,
                Description = "test",
                Status = "test"
            };

            //Act
            repository.Update(item);
            context.SaveChanges();
            var count = repository.GetAll().ToList().Count;
            var result = repository.Get(item.ID);

            //Assert
            Assert.IsNotNull(count);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Type));
            Assert.AreEqual(3, count);
            Assert.AreEqual(item, result);
        }
    }
}