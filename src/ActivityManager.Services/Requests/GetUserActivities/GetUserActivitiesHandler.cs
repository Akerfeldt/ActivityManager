using System;
using System.Collections.Generic;
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
    public class GetUserActivitiesHandler : IRequestHandler<GetUserActivities, IList<UserActivity>>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GetUserActivitiesHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<IList<UserActivity>> Handle(GetUserActivities request, CancellationToken cancellationToken)
        {
            var userId = request.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            return await _applicationDbContext
                .UserActivities
                .AsNoTracking()
                .Where(x => x.UserId == userId)
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
                .ToListAsync();
        }
    }
}
