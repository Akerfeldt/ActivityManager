using System.Collections.Generic;
using System.Security.Claims;
using ActivityManager.Models;
using MediatR;

namespace ActivityManager.Services
{
    public class GetUserActivities : IRequest<IList<UserActivity>>
    {
        public ClaimsPrincipal User { get; set; }
    }
}
