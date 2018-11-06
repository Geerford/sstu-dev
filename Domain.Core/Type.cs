using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    public class Type
    {
        public int ID { get; set; }
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
        public ICollection<Checkpoint> Checkpoints { get; set; }
    }
}
