namespace ActivityManager.Models
{
    public class UserActivity
    {
        public int ActivityId { get; set; }
        public string UserId { get; set; }
        public Activity Activity {get; set;}
    }
}
