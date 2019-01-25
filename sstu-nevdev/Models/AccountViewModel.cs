using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sstu_nevdev.Models
{
    public class AccountViewModel
    {
        [AllowHtml]
        [Required(ErrorMessage = "Логин должен быть заполнен")]
        public string Username { get; set; }

        [AllowHtml]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Логин должен быть заполнен")]
        public string Password { get; set; }
    }
}