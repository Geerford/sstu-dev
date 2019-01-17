using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    public class CheckpointAdmission
    {
        public int ID { get; set; }
        [Required]
        public int CheckpointID { get; set; }
        [Required]
        public int AdmissionID { get; set; }

        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is CheckpointAdmission item)
            {
                result = ID == item.ID;
                result &= CheckpointID.Equals(item.CheckpointID);
                result &= AdmissionID.Equals(item.AdmissionID);
                return result;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashcode = ID.GetHashCode();
            hashcode ^= CheckpointID.GetHashCode();
            hashcode ^= AdmissionID.GetHashCode();
            return hashcode;
        }
    }
}