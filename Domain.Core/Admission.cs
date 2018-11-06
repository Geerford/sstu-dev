using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    public class Admission
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
    }
}