using System;

namespace sstu_nevdev.Models
{
    /// <summary>
    /// Represents a statistics model for API.
    /// </summary>
    public class StatisticsAPIModel
    {
        /// <summary>
        /// Gets or sets the left endpoint of time interval.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets the right endpoint of time interval.
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// Gets or sets the campus.
        /// </summary>
        public int? Campus { get; set; }

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        public int? Row { get; set; }

        /// <summary>
        /// Gets or sets the classroom.
        /// </summary>
        public int? Classroom { get; set; }

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        public int? Section { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the midname.
        /// </summary>
        public string Midname { get; set; }

        /// <summary>
        /// Gets or sets the guid.
        /// </summary>
        public string GUID { get; set; }
    }

    /// <summary>
    /// Represents a active directory statistics model for API.
    /// </summary>
    public class StatisticsActiveDirectoryAPIModel
    {
        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public string Role { get; set; }
    }
}