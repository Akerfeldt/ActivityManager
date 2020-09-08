using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ActivityManager.Data;
using ActivityManager.Models;
using ActivityManager.Services.Caches;
using MediatR;

namespace ActivityManager.Services
{
    public class CreateActivityHandler : IRequestHandler<CreateActivity, Activity>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILookupsCache _lookupsCache;

        public CreateActivityHandler(ApplicationDbContext applicationDbContext, ILookupsCache lookupsCache)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _lookupsCache = lookupsCache ?? throw new ArgumentNullException(nameof(lookupsCache));
        }

        public async Task<Activity> Handle(CreateActivity request, CancellationToken cancellationToken)
        {
            var entity = new Data.Entities.Activity
            {
                Description = request.NewActivityInfo.Description,
                CreatedBy = request.User.Claims.FirstOrDefault(x => x.Type == "email")?.Value
            };

            _applicationDbContext.Activities.Add(entity);

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
