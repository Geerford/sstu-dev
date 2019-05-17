using System;
using System.Collections.Generic;

namespace sstu_nevdev.Models
{
    /// <summary>
    /// Represents a database menu model for MVC.
    /// </summary>
    public class DatabaseViewModel
    {
        /// <summary>
        /// Gets or sets the collection of backup name.
        /// </summary>
        public Dictionary<string, DateTime> Backups { get; set; }

        /// <summary>
        /// Gets or sets the backup name.
        /// </summary>
        public string Backup { get; set; }

        /// <summary>
        /// Gets or sets the operation status.
        /// </summary>
        public bool? IsRecovery { get; set; }

        /// <summary>
        /// Gets or sets the operation status.
        /// </summary>
        public bool? IsSync { get; set; }

        /// <summary>
        /// Initializes a <see cref="DatabaseViewModel"/> class.
        /// </summary>
        public DatabaseViewModel()
        {
            Backups = new Dictionary<string, DateTime>();
            IsRecovery = null;
            IsSync = null;
        }
    }
}