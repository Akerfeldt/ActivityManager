using System.ComponentModel.DataAnnotations;

namespace ActivityManager.Data.Entities
{
    public class Activity
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Description { get; set; }

        [StringLength(255)]
        [Required]
        public string CreatedBy { get; set; }
    }
}
