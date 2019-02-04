namespace sstu_nevdev.Models
{
    /// <summary>
    /// Represents account model for post requests.
    /// </summary>
    public class AccountRequestAPIModel
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

        /// <summary>
        /// Gets or sets the client public key.
        /// </summary>
        public string PublicKey { get; set; }
    }
}