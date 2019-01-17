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

        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is TypeDTO item)
            {
                result = ID == item.ID;
                result &= Description.Equals(item.Description);
                result &= Status.Equals(item.Status);
                return result;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashcode = ID.GetHashCode();
            hashcode ^= Description.GetHashCode();
            hashcode ^= Status.GetHashCode();
            return hashcode;
        }
    }
}
