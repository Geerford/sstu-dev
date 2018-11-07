using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sstu_nevdev.Models
{
    public class CheckpointModel
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

        public class CheckboxItem
        {
            public int ID { get; set; }
            public string Display { get; set; }
            public bool IsChecked { get; set; }
        }
        public List<CheckboxItem> Admissions { get; set; }

        public class StatusForList
        {
            public string Key { get; set; }
            public string Display { get; set; }
        }
        public SelectList Type { get; set; }
    }
}
