using Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    public class TypeDTO
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public TypeDTO() { }

        public TypeDTO(Type item)
        {
            ID = item.ID;
            Description = item.Description;
            Status = item.Status;
        }

        public static explicit operator TypeDTO(Type item)
        {
            return new TypeDTO(item);
        }
    }
}
