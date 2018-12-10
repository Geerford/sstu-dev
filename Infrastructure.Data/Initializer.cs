using Domain.Core;
using System;
using System.Data.Entity;

namespace Infrastructure.Data
{
    internal class Initializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context database)
        {
            Domain.Core.Type type1 = new Domain.Core.Type { Description = "Пропускает через ворота", Status = "Пропускной" };
            Domain.Core.Type type2 = new Domain.Core.Type { Description = "Отмечает посещаемость", Status = "Лекционный" };
            Domain.Core.Type type3 = new Domain.Core.Type { Description = "Собирает статистику", Status = "Статистический" };
            database.Type.Add(type1);
            database.Type.Add(type2);
            database.Type.Add(type3);
            Admission admission1 = new Admission { Role = "Сотрудник", Description = "Вход в лабораторию" };
            Admission admission2 = new Admission { Role = "Студент", Description = "Вход в 1-й корпус" };
            database.Admission.Add(admission1);
            database.Admission.Add(admission2);
            database.SaveChanges();

            database.Identity.Add(new Identity
            {
                GUID = "1",
                RFID = "RFID",
                QR = "QR",
                Picture = "cat.jpg"
            });
            database.Identity.Add(new Identity
            {
                GUID = "2",
                RFID = "RFID",
                QR = "QR",
                Picture = "cat.jpg"
            });
            database.Identity.Add(new Identity
            {
                GUID = "3",
                RFID = "RFID",
                QR = "QR",
                Picture = "cat.jpg"
            });
            database.Checkpoint.Add(new Checkpoint
            {
                IP = "192.168.0.1",
                Campus = 1,
                Row = 4,
                Description = "На 4 этаже, 425 аудитория",
                Status = "Отметка",
                Type = type2
            });
            database.Checkpoint.Add(new Checkpoint
            {
                IP = "192.168.0.15",
                Campus = 1,
                Row = 4,
                Description = "На 1 этаже на входе",
                Status = "Пропуск",
                Type = type1
            });
            database.SaveChanges();

            database.Activity.Add(new Activity
            {
                IdentityGUID = "1",
                CheckpointIP = "192.168.0.1",
                Date = DateTime.Now,
                Mode = "Вход",
                Status = true
            });
            database.Activity.Add(new Activity
            {
                IdentityGUID = "2",
                CheckpointIP = "192.168.0.1",
                Date = DateTime.Now,
                Mode = "Выход",
                Status = true
            });
            database.Activity.Add(new Activity
            {
                IdentityGUID = "3",
                CheckpointIP = "192.168.0.1",
                Date = DateTime.Now,
                Mode = "Вход",
                Status = true
            });
            database.CheckpointAdmission.Add(new CheckpointAdmission { CheckpointID = 1, AdmissionID = 1 });
            database.CheckpointAdmission.Add(new CheckpointAdmission { CheckpointID = 1, AdmissionID = 2 });
            database.CheckpointAdmission.Add(new CheckpointAdmission { CheckpointID = 2, AdmissionID = 1 });
            database.SaveChanges();
        }
    }
}