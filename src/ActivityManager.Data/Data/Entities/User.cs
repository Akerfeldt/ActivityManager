using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ActivityManager.Data.Entities
{
    public class User : IdentityUser
    {
        public ICollection<UserActivity> UserActivities { get; set; }
    }
}
