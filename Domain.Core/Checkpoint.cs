using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    public class Checkpoint
    {
        public int ID { get; set; }
        [Required]
        public string IP { get; set; }
        [Required]
        public int Campus { get; set; }
        [Required]
        public int Row { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public int TypeID { get; set; }
        public Type Type { get; set; }

        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is Checkpoint item)
            {
                result = ID == item.ID;
                result &= IP.Equals(item.IP);
                result &= Campus.Equals(item.Campus);
                result &= Row.Equals(item.Row);
                result &= Description.Equals(item.Description);
                result &= Status.Equals(item.Status);
                result &= Type.Equals(item.Type);
                result &= TypeID.Equals(item.TypeID);
                return result;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashcode = ID.GetHashCode();
            hashcode ^= IP.GetHashCode();
            hashcode ^= Campus.GetHashCode();
            hashcode ^= Row.GetHashCode();
            hashcode ^= Description.GetHashCode();
            hashcode ^= Status.GetHashCode();
            hashcode ^= Type.GetHashCode();
            hashcode ^= TypeID.GetHashCode();
            return hashcode;
        }
    }
}