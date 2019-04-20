using Domain.Core;
using System;
using System.Data.Entity;

namespace Infrastructure.Data
{
    internal class Initializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context database)
        {
            Domain.Core.Type type1 = new Domain.Core.Type {
                Description = "Пропускает через ворота",
                Status = "Пропускной"
            };
            Domain.Core.Type type2 = new Domain.Core.Type {
                Description = "Отмечает посещаемость",
                Status = "Лекционный"
            };
            Domain.Core.Type type3 = new Domain.Core.Type {
                Description = "Собирает статистику",
                Status = "Статистический"
            };
            Mode mode1 = new Mode { Description = "Вход" };
            Mode mode2 = new Mode { Description = "Выход" };
            Mode mode3 = new Mode { Description = "Статистика" };
            database.Type.Add(type1);
            database.Type.Add(type2);
            database.Type.Add(type3);
            Admission admission1 = new Admission {
                Role = "Сотрудник",
                Description = "Вход в лабораторию"
            };
            Admission admission2 = new Admission {
                Role = "Студент",
                Description = "Вход в 1-й корпус"
            };
            database.Admission.Add(admission1);
            database.Admission.Add(admission2);
            database.SaveChanges();

            database.Identity.Add(new Identity
            {
                GUID = "1",
                Picture = "cat.jpg"
            });
            database.Identity.Add(new Identity
            {
                GUID = "2",
                Picture = "cat.jpg"
            });
            database.Identity.Add(new Identity
            {
                GUID = "3",
                Picture = "cat.jpg"
            });
            database.Checkpoint.Add(new Checkpoint
            {
                IP = "192.168.0.1",
                Campus = 1,
                Row = 4,
                Section = 1,
                Classroom = 425,
                Description = "В лекционной аудитории",
                Status = "Отметка",
                Type = type2
            });
            database.Checkpoint.Add(new Checkpoint
            {
                IP = "192.168.0.15",
                Campus = 1,
                Row = 4,
                Section = 3,
                Description = "На углу на 4 этаже",
                Status = "Пропуск",
                Type = type1
            });
            database.SaveChanges();

            database.Activity.Add(new Activity
            {
                IdentityGUID = "1",
                CheckpointIP = "192.168.0.1",
                Date = DateTime.Now,
                Mode = mode1,
                Status = true
            });
            database.Activity.Add(new Activity
            {
                IdentityGUID = "2",
                CheckpointIP = "192.168.0.1",
                Date = DateTime.Now,
                Mode = mode2,
                Status = true
            });
            database.Activity.Add(new Activity
            {
                IdentityGUID = "3",
                CheckpointIP = "192.168.0.1",
                Date = DateTime.Now,
                Mode = mode3,
                Status = true
            });
            database.CheckpointAdmission.Add(new CheckpointAdmission {
                CheckpointID = 1,
                AdmissionID = 1
            });
            database.CheckpointAdmission.Add(new CheckpointAdmission {
                CheckpointID = 2,
                AdmissionID = 1
            });
            database.CheckpointAdmission.Add(new CheckpointAdmission {
                CheckpointID = 2,
                AdmissionID = 2
            });
            database.SaveChanges();
        }
    }
}