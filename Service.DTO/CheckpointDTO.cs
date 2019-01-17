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
        public TypeDTO Type { get; set; }
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

        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is CheckpointDTO item)
            {
                result = ID == item.ID;
                result &= IP.Equals(item.IP);
                result &= Campus.Equals(item.Campus);
                result &= Row.Equals(item.Row);
                result &= Description.Equals(item.Description);
                result &= Status.Equals(item.Status);
                result &= Type.Equals(item.Type);
                result &= Admissions.Equals(item.Admissions);
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
            hashcode ^= Admissions.GetHashCode();
            return hashcode;
        }
    }
}
