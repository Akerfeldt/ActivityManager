using System.ComponentModel.DataAnnotations;

namespace ActivityManager.Data.Entities
{
    public class UserActivity
    {
        public string UserId { get; set; }

        [Required]
        public int ActivityId { get; set; }

        public User User { get; set; }
        public Activity Activity { get; set; }
    }
}
