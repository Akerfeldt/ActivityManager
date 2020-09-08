using System;
using System.Threading;
using System.Threading.Tasks;
using ActivityManager.Data;
using ActivityManager.Models;
using ActivityManager.Services.Caches;
using MediatR;

namespace ActivityManager.Services
{
    public class UpdateActivityHandler : IRequestHandler<UpdateActivity, Activity>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILookupsCache _lookupsCache;

        public UpdateActivityHandler(ApplicationDbContext applicationDbContext, ILookupsCache lookupsCache)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _lookupsCache = lookupsCache ?? throw new ArgumentNullException(nameof(lookupsCache));
        }

        public async Task<Activity> Handle(UpdateActivity request, CancellationToken cancellationToken)
        {
            var entity = _applicationDbContext.Activities.Find(request.Activity.Id);

            entity.Description = request.Activity.Description;

            await _applicationDbContext.SaveChangesAsync();

            await _lookupsCache.GetActivities(true);

            return new Activity
            {
                Id = entity.Id,
                Description = entity.Description
            };
        }
    }
}
