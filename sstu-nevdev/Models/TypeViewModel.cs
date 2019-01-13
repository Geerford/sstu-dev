using Domain.Core;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sstu_nevdev.Models
{
    public class TypeViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Описание должно быть заполнено")]
        [StringLength(100)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Выберите статус")]
        public string Status { get; set; }
        public SelectList StatusList { get; set; }
        
        public TypeViewModel() { }

        public TypeViewModel(Type item)
        {
            ID = item.ID;
            Description = item.Description;
            Status = item.Status;
        }

        public static explicit operator TypeViewModel(Type item)
        {
            return new TypeViewModel(item);
        }
    }
}