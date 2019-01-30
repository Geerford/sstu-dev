namespace sstu_nevdev.Models
{
    /// <summary>
    /// Represents activity model for post requests.
    /// </summary>
    public class ActivityAPIModel
    {
        /// <summary>
        /// Gets or sets the checkpoint IP-address.
        /// </summary>
        public string CheckpointIP { get; set; }

        /// <summary>
        /// Gets or sets the user GUID.
        /// </summary>
        public string GUID { get; set; }
    }
}