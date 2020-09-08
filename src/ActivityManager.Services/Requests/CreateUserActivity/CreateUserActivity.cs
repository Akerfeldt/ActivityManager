using System.Security.Claims;
using ActivityManager.Models;
using MediatR;

namespace ActivityManager.Services
{
    public class CreateUserActivity : IRequest
    {
        public ClaimsPrincipal User { get; set; }
        public NewUserActivityInfo NewUserActivityInfo { get; set; }
    }
}
