using System.Security.Claims;
using ActivityManager.Models;
using MediatR;

namespace ActivityManager.Services
{
    public class GetUserActivity : IRequest<UserActivity>
    {
        public ClaimsPrincipal User { get; set; }
        public int ActivityId { get; set; }
    }
}
