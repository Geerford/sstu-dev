using Domain.Core;
using Infrastructure.Data.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;

namespace Infrastructure.Data
{
    internal class Initializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context database)
        {
            #region TYPES
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
            database.Type.Add(type1);
            database.Type.Add(type2);
            database.Type.Add(type3);
            #endregion
            #region MODES
            Mode mode1 = new Mode {
                Description = "Отмечает событие входа в объект",
                Status = "Вход"
            };
            Mode mode2 = new Mode {
                Description = "Отмечает событие выхода из объекта",
                Status = "Выход"
            };
            Mode mode3 = new Mode {
                Description = "Собирает статистические данные передвижений субъекта",
                Status = "Статистика"
            };
            #endregion
            #region ADMISSIONS
            Admission admission1 = new Admission {
                Role = "Сотрудник",
                Description = "Вход в лабораторию"
            };
            Admission admission2 = new Admission {
                Role = "Студент",
                Description = "Вход в 1-й корпус"
            };
            Admission admission3 = new Admission
            {
                Role = "Преподаватель",
                Description = "Вход в 1-й корпус"
            };
            database.Admission.Add(admission1);
            database.Admission.Add(admission2);
            database.Admission.Add(admission3);
            database.SaveChanges();
            #endregion
            #region GUESTS
            int guestNumber = 15;
            while(guestNumber > 0)
            {
                database.Identity.Add(new Identity
                {
                    GUID = "GUEST_" + guestNumber.ToString(),
                    Picture = "cat.jpg"
                });
                --guestNumber;
            }
            database.SaveChanges();
            #endregion
            #region USERS
            database.Identity.Add(new Identity
            {
                GUID = "milantev_sa#1516",
                Picture = "milantev_sa#1516.jpg"
            });
            database.Identity.Add(new Identity
            {
                GUID = "konyaev_yy#1517",
                Picture = "konyaev_yy#1517.jpg"
            });
            database.Identity.Add(new Identity
            {
                GUID = "eremenko_d#1518",
                Picture = "eremenko_d#1518.jpg"
            });
            database.Identity.Add(new Identity
            {
                GUID = "abor#1519",
                Picture = "cat.jpg"
            });
            #endregion
            #region GUID BINDING
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
            Translator translator = new Translator();
            foreach (var line in lines)
            {
                string[] items = line.Split(' ');
                string guid = translator.Bind(items[0].ToLower());
                database.Identity.Add(new Identity
                {
                    GUID = guid + count.ToString(),
                    Picture = "cat.jpg"
                });
                ++count;
            }
            #endregion
            #region CHECKPOINTS
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
            #endregion
            #region ACTIVITIES
            database.Activity.Add(new Activity
            {
                IdentityGUID = "milantev_sa#1516",
                CheckpointIP = "192.168.0.15",
                Date = DateTime.Now,
                Mode = mode1,
                Status = true
            });
            database.Activity.Add(new Activity
            {
                IdentityGUID = "milantev_sa#1516",
                CheckpointIP = "192.168.0.1",
                Date = DateTime.Now,
                Mode = mode3,
                Status = true
            });
            database.Activity.Add(new Activity
            {
                IdentityGUID = "milantev_sa#1516",
                CheckpointIP = "192.168.0.15",
                Date = DateTime.Now,
                Mode = mode2,
                Status = true
            });
            #endregion
            #region ADMISSIONS TO CHECKPOINTS
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
            database.CheckpointAdmission.Add(new CheckpointAdmission
            {
                CheckpointID = 1,
                AdmissionID = 3
            });
            database.CheckpointAdmission.Add(new CheckpointAdmission
            {
                CheckpointID = 2,
                AdmissionID = 3
            });
            database.SaveChanges();
            #endregion
        }
    }
}