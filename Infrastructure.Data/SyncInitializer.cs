using Domain.Core;
using System;
using System.Data.Entity;
using System.IO;

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

            string path = AppDomain.CurrentDomain.BaseDirectory + "App_Code\\users.txt";
            string[] lines = File.ReadAllLines(path);
            int count = 4;

            foreach (var line in lines)
            {
                string surname = "", name = "", midname = "", gender = "";
                string[] items = line.Split(' ');
            
                switch (items.Length)
                {
                    case 3:
                        surname = items[0];
                        name = items[1];
                        gender = items[2];
                        break;
                    case 4:
                        surname = items[0];
                        name = items[1];
                        midname = items[2];
                        gender = items[3];
                        break;
                    case 5:
                        surname = items[0];
                        name = items[1];
                        midname = items[2] + " " + items[3];
                        gender = items[4];
                        break;
                    default:
                        break;
                }
                database.User.Add(new User
                {
                    GUID = count.ToString(),
                    Name = name,
                    Surname = surname,
                    Midname = midname,
                    Gender = gender == "1" ? true : false,
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
                ++count;
            }
        }
    }
}