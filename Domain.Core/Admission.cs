using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    public class Admission
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        public string Role { get; set; }

        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is Admission item)
            {
                result = ID == item.ID;
                result &= Description.Equals(item.Description);
                result &= Role.Equals(item.Role);
                return result;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashcode = ID.GetHashCode();
            hashcode ^= Description.GetHashCode();
            hashcode ^= Role.GetHashCode();
            return hashcode;
        }
    }
}