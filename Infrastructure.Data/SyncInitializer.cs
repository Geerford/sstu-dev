using Domain.Core;
using System;
using System.Data.Entity;

namespace Infrastructure.Data
{
    internal class SyncInitializer : DropCreateDatabaseAlways<SyncContext>
    {
        protected override void Seed(SyncContext database)
        {
            database.User.Add(new User
            {
                GUID = "1",
                Name = "Сидр",
                Surname = "Сидоров",
                Midname = "Сидорович",
                Gender = true,
                Birthdate = Convert.ToDateTime("2000-01-25"),
                Country = "Россия",
                City = "Саратов",
                Department = "ИнПИТ",
                Group = "ИФСТ",
                Status = "Отчислен",
                Email = "email@gmail.com",
                Phone = "+79993499334",
                Role = "Студент"
            });

            database.User.Add(new User
            {
                GUID = "2",
                Name = "Петр",
                Surname = "Петров",
                Midname = "Петрович",
                Gender = true,
                Birthdate = Convert.ToDateTime("2000-01-25"),
                Country = "Россия",
                City = "Саратов",
                Department = "ИнПИТ",
                Group = "ИФСТ",
                Status = "Обучающийся",
                Email = "email@gmail.com",
                Phone = "+79993499334",
                Role = "Студент"
            });

            database.User.Add(new User
            {
                GUID = "3",
                Name = "Иван",
                Surname = "Иванов",
                Midname = "Иванович",
                Gender = true,
                Birthdate = Convert.ToDateTime("2000-01-25"),
                Country = "Россия",
                City = "Саратов",
                Department = "ИнПИТ",
                Group = "ИФСТ",
                Status = "Отпуск",
                Email = "email@gmail.com",
                Phone = "+79993499334",
                Role = "Преподаватель"
            });
        }
    }
}