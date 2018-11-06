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
    }
}