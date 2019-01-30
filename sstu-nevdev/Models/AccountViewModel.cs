using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sstu_nevdev.Models
{
    /// <summary>
    /// Represents a account model for MVC.
    /// </summary>
    public class AccountViewModel
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [AllowHtml]
        [Required(ErrorMessage = "Логин должен быть заполнен")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [AllowHtml]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Логин должен быть заполнен")]
        public string Password { get; set; }
    }
}