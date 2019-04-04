using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Core;
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
        /// Implements <see cref="IStatisticsService.GetByCampus(int, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<Activity> GetByCampus(int campus, DateTime start, DateTime end)
        {
            List<Activity> activities = new List<Activity>();
            foreach(var checkpoint in Database.Checkpoint.GetAll().Where(y => y.Campus == campus))
            {
                activities.AddRange(Database.Activity.GetAll().Where(x => x.CheckpointIP == checkpoint.IP && x.Date > start && x.Date <= end));
            }
            return activities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetByCampusRow(int, int, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<Activity> GetByCampusRow(int campus, int row, DateTime start, DateTime end)
        {
            List<Activity> activities = new List<Activity>();
            foreach (var checkpoint in Database.Checkpoint.GetAll().Where(y => y.Campus == campus && y.Row == row))
            {
                activities.AddRange(Database.Activity.GetAll().Where(x => x.CheckpointIP == checkpoint.IP && x.Date > start && x.Date <= end));
            }
            return activities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetByClassroom(int, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<Activity> GetByClassroom(int classroom, DateTime start, DateTime end)
        {
            List<Activity> activities = new List<Activity>();
            foreach (var checkpoint in Database.Checkpoint.GetAll().Where(y => y.Classroom == classroom))
            {
                activities.AddRange(Database.Activity.GetAll().Where(x => x.CheckpointIP == checkpoint.IP && x.Date > start && x.Date <= end));
            }
            return activities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetByGroup(string, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<Activity> GetByGroup(string group, DateTime start, DateTime end)
        {
            List<Activity> activities = new List<Activity>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            foreach (var identity in identityService.GetByGroup(group))
            {
                activities.AddRange(Database.Activity.GetAll().Where(x => x.IdentityGUID == identity.GUID && x.Date > start && x.Date <= end));
            }
            identityService.Dispose();
            return activities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetBySection(int, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<Activity> GetBySection(int section, DateTime start, DateTime end)
        {
            List<Activity> activities = new List<Activity>();
            foreach (var checkpoint in Database.Checkpoint.GetAll().Where(y => y.Section == section))
            {
                activities.AddRange(Database.Activity.GetAll().Where(x => x.CheckpointIP == checkpoint.IP && x.Date > start && x.Date <= end));
            }
            return activities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetByUser(string, string, string, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<Activity> GetByUser(string name, string midname, string surname, DateTime start, DateTime end)
        {
            List<Activity> activities = new List<Activity>();
            IdentityService identityService = new IdentityService(Database, SyncDatabase);
            var identity = identityService.GetByName(name, midname, surname);
            if(identity != null)
            {
                activities.AddRange(Database.Activity.GetAll().Where(x => x.IdentityGUID == identity.GUID && x.Date > start && x.Date <= end));
            }
            identityService.Dispose();
            return activities;
        }

        /// <summary>
        /// Implements <see cref="IStatisticsService.GetByUser(string, DateTime, DateTime)"/>.
        /// </summary>
        public IEnumerable<Activity> GetByUser(string guid, DateTime start, DateTime end)
        {
            List<Activity> activities = new List<Activity>();
            activities.AddRange(Database.Activity.GetAll().Where(x => x.IdentityGUID == guid && x.Date > start && x.Date <= end));
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
            identityService.Dispose();
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
            identityService.Dispose();
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
            identityService.Dispose();
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
            identityService.Dispose();
            return identities;
        }
    }
}