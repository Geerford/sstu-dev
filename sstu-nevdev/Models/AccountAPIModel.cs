namespace sstu_nevdev.Models
{
    /// <summary>
    /// Represents account model for post requests.
    /// </summary>
    public class AccountAPIModel
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        public string Domain { get; set; }
    }
}