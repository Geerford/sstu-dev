using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    public class Checkpoint
    {
        public int ID { get; set; }
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
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }

        public int TypeID { get; set; }
        public Type Type { get; set; }
    }
}
