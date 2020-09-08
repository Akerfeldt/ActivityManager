using System.Security.Claims;
using ActivityManager.Models;
using MediatR;

namespace ActivityManager.Services
{
    public class UpdateActivity : IRequest<Activity>
    {
        public ClaimsPrincipal User { get; set; }
        public Activity Activity { get; set; }
        public int Id { get; set; }
    }
}
