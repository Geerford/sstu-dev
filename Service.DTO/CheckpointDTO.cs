using Domain.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    public class CheckpointDTO
    {
        public int ID { get; set; }
        [Required]
        public string IP { get; set; }
        [Required]
        public int Campus { get; set; }
        [Required]
        public int Row { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public Type Type { get; set; }
        [Required]
        public IEnumerable<Admission> Admissions { get; set; }

        public CheckpointDTO()
        {
            Admissions = new List<Admission>();
        }

        public CheckpointDTO(Checkpoint item)
        {
            ID = item.ID;
            IP = item.IP;
            Campus = item.Campus;
            Row = item.Row;
            Description = item.Description;
            Status = item.Status;
        }

        public static explicit operator CheckpointDTO(Checkpoint item)
        {
            return new CheckpointDTO(item);
        }
    }
}
