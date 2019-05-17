using Domain.Core;
using Infrastructure.Data.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;

namespace Infrastructure.Data
{
    internal class SyncInitializer : DropCreateDatabaseAlways<SyncContext>
    {
        protected override void Seed(SyncContext database)
        {
            #region USERS
            database.User.Add(new User
            {
                GUID = "milantev_sa#1516",
                Name = "Сергей",
                Surname = "Милантьев",
                Midname = "Андреевич",
                Gender = true,
                Birthdate = Convert.ToDateTime("1995-03-29"),
                Country = "Россия",
                City = "Саратов",
                Department = "ИнПИТ",
                Group = "ИФСТ",
                Status = "Обучающийся",
                Email = "geerkus@gmail.com",
                Phone = "+79020447508",
                Role = "Студент"
            });
            database.User.Add(new User
            {
                GUID = "konyaev_yy#1517",
                Name = "Юрий",
                Surname = "Коняев-Гурченко",
                Midname = "Юрьевич",
                Gender = true,
                Birthdate = Convert.ToDateTime("1997-10-05"),
                Country = "Россия",
                City = "Саратов",
                Department = "ИнПИТ",
                Group = "ИФСТ",
                Status = "Отпуск",
                Email = "konyaev_yurii@mail.ru",
                Phone = "+79061508501",
                Role = "Студент"
            });
            database.User.Add(new User
            {
                GUID = "eremenko_d#1518",
                Name = "Денис",
                Surname = "Еременко",
                Midname = "Сергеевич",
                Gender = true,
                Birthdate = Convert.ToDateTime("1997-01-08"),
                Country = "Россия",
                City = "Саратов",
                Department = "ИнПИТ",
                Group = "ИФСТ",
                Status = "Отчислен",
                Email = "x2denissergeevich@gmail.com",
                Phone = "+79370220973",
                Role = "Студент"
            });
            database.User.Add(new User
            {
                GUID = "abor#1519",
                Name = "Андрей",
                Surname = "Бороздюхин",
                Midname = "Александрович",
                Gender = true,
                Birthdate = Convert.ToDateTime("1970-01-25"),
                Country = "Россия",
                City = "Саратов",
                Department = "ИнПИТ",
                Group = "ИФСТ",
                Status = "Отпуск",
                Email = "abor@sstu.ru",
                Phone = "+79030459403",
                Role = "Преподаватель"
            });
            #endregion
            #region 1C SYNC
            string path = AppDomain.CurrentDomain.BaseDirectory + "App_Code\\users.txt";
            string[] lines;
            if (File.Exists(path))
            {
                lines = File.ReadAllLines(path);
            }
            else
            {
                lines = File.ReadAllLines("D:\\users.txt");
            }
            int count = 1;
            Random random = new Random();
            DateTime start = new DateTime(1995, 1, 1), end = new DateTime(1999, 1, 1);
            int range = (end - start).Days;
            Translator translator = new Translator();
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
                string phone = string.Empty;
                for (int i = 0; i < 10; i++)
                {
                    phone += random.Next(0, 9).ToString();
                }
                string guid = translator.Bind(surname.ToLower());
                database.User.Add(new User
                {
                    GUID = guid + count.ToString(),
                    Name = name,
                    Surname = surname,
                    Midname = midname,
                    Gender = gender == "1" ? true : false,
                    Birthdate = start.AddDays(random.Next(range)),
                    Country = "Россия",
                    City = "Саратов",
                    Department = "ИнПИТ",
                    Group = "ИФСТ",
                    Status = "Обучающийся",
                    Email = $"{guid}@gmail.com",
                    Phone = "+7" + phone,
                    Role = "Студент"
                });
                ++count;
            }
            #endregion
        }
    }
}