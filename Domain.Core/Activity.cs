using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    public class Activity
    {
        public int ID { get; set; }
        [Required]
        public string IdentityGUID { get; set; }
        [Required]
        public string CheckpointIP { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        [StringLength(20)]
        public string Mode { get; set; }
    }
}
