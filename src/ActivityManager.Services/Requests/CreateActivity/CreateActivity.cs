using System.Security.Claims;
using ActivityManager.Models;
using MediatR;

namespace ActivityManager.Services
{
    public class CreateActivity : IRequest<Activity>
    {
        public ClaimsPrincipal User { get; set; }
        public NewActivityInfo NewActivityInfo { get; set; }
    }
}
