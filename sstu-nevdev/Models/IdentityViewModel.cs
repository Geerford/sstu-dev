using Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace sstu_nevdev.Models
{
    public class IdentityViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "GUID должен быть заполнен")]
        public string GUID { get; set; }
        public string Picture { get; set; }

        public IdentityViewModel() { }

        public IdentityViewModel(Identity item)
        {
            ID = item.ID;
            GUID = item.GUID;
            Picture = item.Picture;
        }

        public static explicit operator IdentityViewModel(Identity item)
        {
            return new IdentityViewModel(item);
        }
    }
}