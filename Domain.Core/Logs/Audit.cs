using System;

namespace Domain.Core.Logs
{
    /// <summary>
    /// Represents a audit entity.
    /// </summary>
    public class Audit
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the entity name.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets the modified data.
        /// </summary>
        public string Logs { get; set; }

        /// <summary>
        /// Gets or sets the value who is changed the data.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the value from where the change was made.
        /// </summary>
        public string ModifiedFrom { get; set; }

        /// <summary>
        /// Gets or sets the value from when the change was made.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}