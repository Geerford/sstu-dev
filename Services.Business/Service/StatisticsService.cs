using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using Repository.Interfaces;
using Service.DTO;
using Service.Interfaces;

namespace Services.Business.Service
{
    /// <summary>
    /// Implements <see cref="IStatisticsService"/>.
    /// </summary>
    public class StatisticsService : IStatisticsService
    {
        /// <summary>
        /// Gets or sets the repository context.
        /// </summary>
        IUnitOfWork Database { get; set; }

        /// <summary>
        /// Gets or sets the repository context.
        /// </summary>
        ISyncUnitOfWork SyncDatabase { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityService"/> class.
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> object.</param>
        /// /// <param name="suow">The <see cref="ISyncUnitOfWork"/> object.</param>
        public StatisticsService(IUnitOfWork uow, ISyncUnitOfWork suow)
        {
            Database = uow;
            SyncDatabase = suow;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.Dispose"/>.
        /// </summary>
        public void Dispose()
        {
            Database.Dispose();
        }


        /// <summary>
        /// Implements <see cref="IStatisticsService.GetStatistics(int?, int?, int?, string, string, string, DateTime?, DateTime?)"/>.
        /// </summary>
        public IEnumerable<IdentityDTO> GetStatistics(int? campus, int? row, int? classroom, string name, string midname, string surname, DateTime? start, DateTime? end)
        {
            Dictionary<string, IdentityDTO> activityDTOs = new Dictionary<string, IdentityDTO>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            //CheckpointService checkpointService = new CheckpointService(Database);

            if(start == (new DateTime(1900, 1, 1, 0,0,0)))
            {
                foreach (var identity in Database.Identity.GetAll())
                {
                    int count = 0;
                    foreach (var checkpoint in Database.Checkpoint.GetAll().Where(
                    y =>
                    (y.Campus.Equals(campus) || campus == 0)
                    && (y.Row.Equals(row) || row == 0)
                    && (y.Classroom.Equals(classroom) || classroom == 0)
                ))
                    {
                        var activities = Database.Activity.GetAll().Where(x => x.IdentityGUID.Equals(identity.GUID) && x.CheckpointIP.Equals(checkpoint.IP));
                        if (activities != null)
                        {
                            count += activities.ToList().Count;
                        }
                    }
                    if (count % 2 != 0)
                    {
                        if (!activityDTOs.ContainsKey(identity.GUID))
                        {
                            var full = identityService.GetFull(identity.GUID);
                            if (name == null || (full.Name + " " + full.Surname + " " + full.Midname).IndexOf(name, StringComparison.CurrentCultureIgnoreCase) != -1)
                            {
                                activityDTOs.Add(identity.GUID, full);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var checkpoint in Database.Checkpoint.GetAll().Where((
                     y =>
                    (y.Campus.Equals(campus) || campus == 0)
                    && (y.Row.Equals(row) || row == 0)
                    && (y.Classroom.Equals(classroom) || classroom == 0)
                )))
                {
                    var abs = Database.Activity.GetAll();
                    foreach (var activity in 
                        abs.SelectMany(a => 
                        abs.Where(x => x.CheckpointIP == checkpoint.IP
                        && (x.Date > start||true) && (x.Date <= end||true)))
                        .GroupBy(z => z.IdentityGUID)
                        .Select(z => z.First())
                        .ToList())
                    {
                        if (!activityDTOs.ContainsKey(activity.IdentityGUID))
                        {
                            var identity = identityService.GetFull(activity.IdentityGUID);
                            if(name == null || (identity.Name + " " + identity.Surname + " " + identity.Midname).IndexOf(name, StringComparison.CurrentCultureIgnoreCase) != -1 )
                            {
                                activityDTOs.Add(activity.IdentityGUID, identity);
                            }
                        }
                    }
                }
            }

            return activityDTOs.Values.ToList();
        }

        //private IEnumerable<Domain.Core.Activity> filter(IEnumerable<Domain.Core.Activity> data, string checkpointIp, int campus, int row, int classroom, string name, string midname, string surname, DateTime start, DateTime end)
        //{
        //    if(campus != 0)
        //    {
        //        data.SelectMany(x => x.)
        //    }

        //    if(row != 0)
        //    {

        //    }

        //    return data;
        //}

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetByCampus(int, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<ActivityDTO> GetByCampus(int campus, DateTime start, DateTime end)
        {
            List<ActivityDTO> activities = new List<ActivityDTO>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            CheckpointService checkpointService = new CheckpointService(Database);
            foreach(var checkpoint in Database.Checkpoint.GetAll().Where(y => y.Campus == campus))
            {
                foreach(var activity in Database.Activity.GetAll().Where(x => x.CheckpointIP == checkpoint.IP 
                    && x.Date > start && x.Date <= end))
                {
                    activities.Add(new ActivityDTO()
                    {
                        ID = activity.ID,
                        User = identityService.GetFull(activity.IdentityGUID),
                        Checkpoint = checkpointService.GetByIP(activity.CheckpointIP),
                        Mode = Database.Mode.GetAll().Where(x => x.ID == activity.ID).FirstOrDefault(),
                        Date = activity.Date,
                        Status = activity.Status
                    });
                }
            }
            return activities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetByCampusRow(int, int, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<ActivityDTO> GetByCampusRow(int campus, int row, DateTime start, DateTime end)
        {
            List<ActivityDTO> activities = new List<ActivityDTO>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            CheckpointService checkpointService = new CheckpointService(Database);
            foreach (var checkpoint in Database.Checkpoint.GetAll().Where(y => y.Campus == campus && y.Row == row))
            {
                foreach (var activity in Database.Activity.GetAll().Where(x => x.CheckpointIP == checkpoint.IP
                     && x.Date > start && x.Date <= end))
                {
                    activities.Add(new ActivityDTO()
                    {
                        ID = activity.ID,
                        User = identityService.GetFull(activity.IdentityGUID),
                        Checkpoint = checkpointService.GetByIP(activity.CheckpointIP),
                        Mode = Database.Mode.GetAll().Where(x => x.ID == activity.ID).FirstOrDefault(),
                        Date = activity.Date,
                        Status = activity.Status
                    });
                }
            }
            return activities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetByClassroom(int, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<ActivityDTO> GetByClassroom(int classroom, DateTime start, DateTime end)
        {
            List<ActivityDTO> activities = new List<ActivityDTO>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            CheckpointService checkpointService = new CheckpointService(Database);
            foreach (var checkpoint in Database.Checkpoint.GetAll().Where(y => y.Classroom == classroom))
            {
                foreach (var activity in Database.Activity.GetAll().Where(x => x.CheckpointIP == checkpoint.IP
                     && x.Date > start && x.Date <= end))
                {
                    activities.Add(new ActivityDTO()
                    {
                        ID = activity.ID,
                        User = identityService.GetFull(activity.IdentityGUID),
                        Checkpoint = checkpointService.GetByIP(activity.CheckpointIP),
                        Mode = Database.Mode.GetAll().Where(x => x.ID == activity.ID).FirstOrDefault(),
                        Date = activity.Date,
                        Status = activity.Status
                    });
                }
            }
            return activities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetByGroup(string, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<ActivityDTO> GetByGroup(string group, DateTime start, DateTime end)
        {
            List<ActivityDTO> activities = new List<ActivityDTO>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            CheckpointService checkpointService = new CheckpointService(Database);
            foreach (var identity in identityService.GetByGroup(group))
            {
                foreach (var activity in Database.Activity.GetAll().Where(x => x.IdentityGUID == identity.GUID 
                    && x.Date > start && x.Date <= end))
                {
                    activities.Add(new ActivityDTO()
                    {
                        ID = activity.ID,
                        User = identityService.GetFull(activity.IdentityGUID),
                        Checkpoint = checkpointService.GetByIP(activity.CheckpointIP),
                        Mode = Database.Mode.GetAll().Where(x => x.ID == activity.ID).FirstOrDefault(),
                        Date = activity.Date,
                        Status = activity.Status
                    });
                }
            }
            return activities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetBySection(int, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<ActivityDTO> GetBySection(int section, DateTime start, DateTime end)
        {
            List<ActivityDTO> activities = new List<ActivityDTO>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            CheckpointService checkpointService = new CheckpointService(Database);
            foreach (var checkpoint in Database.Checkpoint.GetAll().Where(y => y.Section == section))
            {
                foreach (var activity in Database.Activity.GetAll().Where(x => x.CheckpointIP == 
                    checkpoint.IP && x.Date > start && x.Date <= end))
                {
                    activities.Add(new ActivityDTO()
                    {
                        ID = activity.ID,
                        User = identityService.GetFull(activity.IdentityGUID),
                        Checkpoint = checkpointService.GetByIP(activity.CheckpointIP),
                        Mode = Database.Mode.GetAll().Where(x => x.ID == activity.ID).FirstOrDefault(),
                        Date = activity.Date,
                        Status = activity.Status
                    });
                }
            }
            return activities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetByUser(string, string, string, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<ActivityDTO> GetByUser(string name, string midname, string surname, DateTime start, DateTime end)
        {
            List<ActivityDTO> activities = new List<ActivityDTO>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            CheckpointService checkpointService = new CheckpointService(Database);
            var identity = identityService.GetByName(name, midname, surname);
            if (identity != null)
            {
                foreach (var activity in Database.Activity.GetAll().Where(x => x.IdentityGUID == identity.GUID 
                    && x.Date > start && x.Date <= end))
                {
                    activities.Add(new ActivityDTO()
                    {
                        ID = activity.ID,
                        User = identity,
                        Checkpoint = checkpointService.GetByIP(activity.CheckpointIP),
                        Mode = Database.Mode.GetAll().Where(x => x.ID == activity.ID).FirstOrDefault(),
                        Date = activity.Date,
                        Status = activity.Status
                    });
                }
            }
            return activities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetByUser(string, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<ActivityDTO> GetByUser(string guid, DateTime start, DateTime end)
        {
            List<ActivityDTO> activities = new List<ActivityDTO>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            CheckpointService checkpointService = new CheckpointService(Database);
            var identity = identityService.GetByGUID(guid);
            if (identity != null)
            {
                foreach (var activity in Database.Activity.GetAll().Where(x => x.IdentityGUID == guid && x.Date > start && x.Date <= end))
                {
                    activities.Add(new ActivityDTO()
                    {
                        ID = activity.ID,
                        User = identity,
                        Checkpoint = checkpointService.GetByIP(activity.CheckpointIP),
                        Mode = Database.Mode.GetAll().Where(x => x.ID == activity.ID).FirstOrDefault(),
                        Date = activity.Date,
                        Status = activity.Status
                    });
                }
            }
            return activities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetCurrentByCampus(int)"/>.
        /// </summary>
        public IEnumerable<IdentityDTO> GetCurrentByCampus(int campus)
        {
            List<IdentityDTO> identities = new List<IdentityDTO>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            foreach(var identity in Database.Identity.GetAll())
            {
                int count = 0;
                foreach (var checkpoint in Database.Checkpoint.GetAll().Where(y => y.Campus == campus))
                {
                    var activities = Database.Activity.GetAll().Where(x => x.IdentityGUID.Equals(identity.GUID) && x.CheckpointIP.Equals(checkpoint.IP));
                    if (activities != null)
                    {
                        count += activities.ToList().Count;
                    }
                }
                if(count % 2 != 0)
                {
                    identities.Add(identityService.GetFull(identity.GUID));
                }
            }
            return identities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetCurrentByCampusRow(int, int)"/>.
        /// </summary>
        public IEnumerable<IdentityDTO> GetCurrentByCampusRow(int campus, int row)
        {
            List<IdentityDTO> identities = new List<IdentityDTO>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            foreach (var identity in Database.Identity.GetAll())
            {
                int count = 0;
                foreach (var checkpoint in Database.Checkpoint.GetAll().Where(y => y.Campus == campus && y.Row == row))
                {
                    var activities = Database.Activity.GetAll().Where(x => x.IdentityGUID.Equals(identity.GUID) && x.CheckpointIP.Equals(checkpoint.IP));
                    if (activities != null)
                    {
                        count += activities.ToList().Count;
                    }
                }
                if (count % 2 != 0)
                {
                    identities.Add(identityService.GetFull(identity.GUID));
                }
            }
            return identities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetCurrentByClassroom(int)"/>.
        /// </summary>
        public IEnumerable<IdentityDTO> GetCurrentByClassroom(int classroom)
        {
            List<IdentityDTO> identities = new List<IdentityDTO>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            foreach (var identity in Database.Identity.GetAll())
            {
                int count = 0;
                foreach (var checkpoint in Database.Checkpoint.GetAll().Where(y => y.Classroom == classroom))
                {
                    var activities = Database.Activity.GetAll().Where(x => x.IdentityGUID.Equals(identity.GUID) && x.CheckpointIP.Equals(checkpoint.IP));
                    if (activities != null)
                    {
                        count += activities.ToList().Count;
                    }
                }
                if (count % 2 != 0)
                {
                    identities.Add(identityService.GetFull(identity.GUID));
                }
            }
            return identities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetBySection(int)"/>.
        /// </summary>
        public IEnumerable<IdentityDTO> GetCurrentBySection(int section)
        {
            List<IdentityDTO> identities = new List<IdentityDTO>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            foreach (var identity in Database.Identity.GetAll())
            {
                int count = 0;
                foreach (var checkpoint in Database.Checkpoint.GetAll().Where(y => y.Section == section))
                {
                    var activities = Database.Activity.GetAll().Where(x => x.IdentityGUID.Equals(identity.GUID) && x.CheckpointIP.Equals(checkpoint.IP));
                    if (activities != null)
                    {
                        count += activities.ToList().Count;
                    }
                }
                if (count % 2 != 0)
                {
                    identities.Add(identityService.GetFull(identity.GUID));
                }
            }
            return identities;
        }

        public ActivityDTO GetUserLocation(string name, string midname, string surname)
        {
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            CheckpointService checkpointService = new CheckpointService(Database);
            var identity = identityService.GetByName(name, midname, surname);
            if (identity != null)
            {
                var activity = Database.Activity.GetAll().Where(x => x.IdentityGUID == identity.GUID).LastOrDefault();

                return new ActivityDTO()
                {
                    ID = activity.ID,
                    User = identity,
                    Checkpoint = checkpointService.GetByIP(activity.CheckpointIP),
                    Mode = Database.Mode.GetAll().Where(x => x.ID == activity.ID).FirstOrDefault(),
                    Date = activity.Date,
                    Status = activity.Status
                };
            }
            return null;
        }

        public ActivityDTO GetUserLocation(string guid)
        {
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            CheckpointService checkpointService = new CheckpointService(Database);
            var identity = identityService.GetByGUID(guid);
            if (identity != null)
            {
                var activity = Database.Activity.GetAll().Where(x => x.IdentityGUID == identity.GUID).LastOrDefault();

                return new ActivityDTO()
                {
                    ID = activity.ID,
                    User = identity,
                    Checkpoint = checkpointService.GetByIP(activity.CheckpointIP),
                    Mode = Database.Mode.GetAll().Where(x => x.ID == activity.ID).FirstOrDefault(),
                    Date = activity.Date,
                    Status = activity.Status
                };
            }
            return null;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetRoles(string)"/>.
        /// </summary>
        public IEnumerable<string> GetRoles(string domain)
        {
            List<string> roles = new List<string>();
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                using (var searcher = new PrincipalSearcher(new GroupPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        if (!result.SamAccountName.Equals("Administrator") && !result.SamAccountName.Equals("Guest")
                            && !result.SamAccountName.Equals("krbtgt") && !result.SamAccountName.Equals("ASPNET"))
                        {
                            roles.Add(result.Name);
                        }
                    }
                }
            }
            return roles;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetUsers(string)"/>.
        /// </summary>
        public IEnumerable<ADUserDTO> GetUsers(string domain)
        {
            List<ADUserDTO> users = new List<ADUserDTO>();
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        if(!result.SamAccountName.Equals("Administrator") && !result.SamAccountName.Equals("Guest") 
                            && !result.SamAccountName.Equals("krbtgt") && !result.SamAccountName.Equals("ASPNET"))
                        {
                            ADUserDTO user = new ADUserDTO
                            {
                                SamAccountName = result.SamAccountName,
                                DN = result.DistinguishedName,
                                Roles = result.GetGroups().Select(x => 
                                {
                                    switch (x.Name)
                                    {
                                        case "SSTU_Student":
                                            return x.Name;
                                        case "SSTU_Administrator":
                                            return x.Name;
                                        case "SSTU_Deanery":
                                            return x.Name;
                                        case "SSTU_Inspector":
                                            return x.Name;
                                        default:
                                            return null;
                                    }
                                }).Where(y => y != null).ToList()
                            };
                            users.Add(user);
                        }                        
                    }
                }
            }
            return users;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetUsersByRole(string, string)"/>.
        /// </summary>
        public IEnumerable<ADUserDTO> GetUsersByRole(string domain, string role)
        {
            List<ADUserDTO> users = new List<ADUserDTO>();
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll().Where(x => x.GetGroups()
                        .Where(y => y.Name.Equals(role)).ToList().Count > 0))
                    {
                        ADUserDTO user = new ADUserDTO
                        {
                            SamAccountName = result.SamAccountName,
                            DN = result.DistinguishedName,
                            Roles = result.GetGroups().Select(x =>
                            {
                                switch (x.Name)
                                {
                                    case "SSTU_Student":
                                        return x.Name;
                                    case "SSTU_Administrator":
                                        return x.Name;
                                    case "SSTU_Deanery":
                                        return x.Name;
                                    case "SSTU_Inspector":
                                        return x.Name;
                                    default:
                                        return null;
                                }
                            }).Where(y => y != null).ToList()
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }        
    }
}