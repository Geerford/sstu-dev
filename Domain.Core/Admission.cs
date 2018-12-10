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
    }
}