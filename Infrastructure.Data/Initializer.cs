using Domain.Core;
using System;
using System.Data.Entity;

namespace Infrastructure.Data
{
    internal class Initializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context database)
        {
            Role role1 = new Role { Description = "Студент", CreatedBy = "Создатель" };
            Role role2 = new Role { Description = "Сотрудник", CreatedBy = "Создатель" };
            database.Role.Add(role1);
            database.Role.Add(role2);
            Domain.Core.Type type1 = new Domain.Core.Type { Description = "Пропускает через ворота", Status = "Пропускной", CreatedBy = "Создатель" };
            Domain.Core.Type type2 = new Domain.Core.Type { Description = "Отмечает посещаемость", Status = "Лекционный", CreatedBy = "Создатель" };
            database.Type.Add(type1);
            database.Type.Add(type2);
            Admission admission1 = new Admission { Description = "Сотрудник", CreatedBy = "Создатель" };
            Admission admission2 = new Admission { Description = "Студент", CreatedBy = "Создатель" };
            database.Admission.Add(admission1);
            database.Admission.Add(admission2);
            database.SaveChanges();

            database.Identity.Add(new Identity
            {
                RFID = "RFID",
                QR = "QR",
                Name = "Сидр",
                Surname = "Сидоров",
                Midname = "Сидорович",
                Gender = true,
                Birthdate = Convert.ToDateTime("2000-01-25"),
                Picture = "Picture",
                Country = "Россия",
                City = "Саратов",
                Department = "ИнПИТ",
                Group = "ИФСТ",
                Status = "Активирован",
                CreatedBy = "Создатель",
                Email = "email@gmail.com",
                Phone = "+79993499334",
                Role = role1
            });
            database.Identity.Add(new Identity
            {
                RFID = "RFID",
                QR = "QR",
                Name = "Петр",
                Surname = "Петров",
                Midname = "Петрович",
                Gender = true,
                Birthdate = Convert.ToDateTime("2000-01-25"),
                Picture = "Picture",
                Country = "Россия",
                City = "Саратов",
                Department = "ИнПИТ",
                Group = "ИФСТ",
                Status = "Активирован",
                CreatedBy = "Создатель",
                Email = "email@gmail.com",
                Phone = "+79993499334",
                Role = role1
            });
            database.Identity.Add(new Identity
            {
                RFID = "RFID",
                QR = "QR",
                Name = "Иван",
                Surname = "Иванов",
                Midname = "Иванович",
                Gender = true,
                Birthdate = Convert.ToDateTime("2000-01-25"),
                Picture = "Picture",
                Country = "Россия",
                City = "Саратов",
                Department = "ИнПИТ",
                Group = "ИФСТ",
                Status = "Деактивирован",
                CreatedBy = "Создатель",
                Email = "email@gmail.com",
                Phone = "+79993499334",
                Role = role1
            });
            database.Checkpoint.Add(new Checkpoint
            {
                Campus = 1,
                Row = 4,
                Description = "На 4 этаже около 425",
                Status = "Отметка",
                Type = type2,
                CreatedBy = "Создатель"
            });
            database.Checkpoint.Add(new Checkpoint
            {
                Campus = 1,
                Row = 4,
                Description = "На 1 этаже на входе",
                Status = "Пропуск",
                Type = type1,
                CreatedBy = "Создатель"
            });
            database.SaveChanges();

            database.Activity.Add(new Activity
            {
                IdentityID = 1,
                CheckpointID = 1,
                Date = DateTime.Now,
                Mode = "Вход",
                Status = true
            });
            database.Activity.Add(new Activity
            {
                IdentityID = 1,
                CheckpointID = 1,
                Date = DateTime.Now,
                Mode = "Выход",
                Status = true
            });
            database.Activity.Add(new Activity
            {
                IdentityID = 1,
                CheckpointID = 1,
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