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
    public class CheckpointAdmissionRepositoryTest
    {
        DbConnection connection;
        Context context;
        //AdmissionRepository admissionRepository;
        //CheckpointRepository checkpointRepository;
        //TypeRepository typeRepository;
        CheckpointAdmissionRepository repository;
        private readonly int id = 1;

        [TestInitialize]
        public void Initialize()
        {
            connection = Effort.DbConnectionFactory.CreateTransient();
            context = new Context(connection);
            //admissionRepository = new AdmissionRepository(context);
            //checkpointRepository = new CheckpointRepository(context);
            //typeRepository = new TypeRepository(context);
            repository = new CheckpointAdmissionRepository(context);
            //Admission admission = new Admission()
            //{
            //    ID = 1,
            //    Description = "Description",
            //    Role = "Role"
            //};
            //Checkpoint checkpoint = new Checkpoint()
            //{
            //    ID = 1,
            //    Campus = 1,
            //    Classroom = 420,
            //    Description = "Description",
            //    IP = "10.0.0.5",
            //    Row = 4,
            //    Status = "Status",
            //    TypeID = 1
            //};
            //Type type = new Type()
            //{
            //    ID = 1,
            //    Description = "Description",
            //    Status = "Status"
            //};
            //admissionRepository.Create(admission);
            //checkpointRepository.Create(checkpoint);
            //typeRepository.Create(type);
        }

        [TestMethod]
        public void Create()
        {
            //Arrange
            CheckpointAdmission item = new CheckpointAdmission()
            {
                AdmissionID = 2,
                CheckpointID = 2
            };

            //Act
            repository.Create(item);
            context.SaveChanges();
            var result = repository.GetAll().ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<CheckpointAdmission>));
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
            Assert.IsInstanceOfType(result, typeof(IEnumerable<CheckpointAdmission>));
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void Find_First()
        {
            //Act
            var result = repository.FindFirst(x => x.ID > 0);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CheckpointAdmission));
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void Get()
        {
            //Act
            var result = repository.Get(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CheckpointAdmission));
            Assert.AreEqual(id, result.ID);
        }

        [TestMethod]
        public void Get_All()
        {
            //Act
            var result = repository.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<CheckpointAdmission>));
            Assert.AreEqual(3, result.ToList().Count);
        }

        [TestMethod]
        public void Update()
        {
            //Arrange
            CheckpointAdmission item = new CheckpointAdmission()
            {
                ID = 1,
                AdmissionID = 2,
                CheckpointID = 2
            };

            //Act
            repository.Update(item);
            context.SaveChanges();
            var count = repository.GetAll().ToList().Count;
            var result = repository.Get(item.ID);

            //Assert
            Assert.IsNotNull(count);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CheckpointAdmission));
            Assert.AreEqual(3, count);
            Assert.AreEqual(item, result);
        }
    }
}