using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ActivityManager.Data;
using ActivityManager.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ActivityManager.Services
{
    public class GetUserActivityHandler : IRequestHandler<GetUserActivity, UserActivity>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GetUserActivityHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public Task<UserActivity> Handle(GetUserActivity request, CancellationToken cancellationToken)
        {
            var userId = request.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            return _applicationDbContext
                .UserActivities
                .AsNoTracking()
                .Include(u => u.Activity)
                .Select(x => new UserActivity
                {
                    ActivityId = x.ActivityId,
                    UserId = userId,
                    Activity = new Activity
                    {
                        Id = x.Activity.Id,
                        Description = x.Activity.Description
                    }
                })
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ActivityId == request.ActivityId);
        }
    }
}
