using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ActivityManager.Data;
using ActivityManager.Exceptions;
using ActivityManager.Services.Caches;
using MediatR;

namespace ActivityManager.Services
{
    public class DeleteActivityHandler : IRequestHandler<DeleteActivity>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILookupsCache _lookupsCache;

        public DeleteActivityHandler(ApplicationDbContext applicationDbContext, ILookupsCache lookupsCache)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _lookupsCache = lookupsCache ?? throw new ArgumentNullException(nameof(lookupsCache));
        }

        public async Task<Unit> Handle(DeleteActivity request, CancellationToken cancellationToken)
        {
            var userId = request.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var activity = await _applicationDbContext.Activities.FindAsync(request.ActivityId) ?? throw new NotFoundException();

            _applicationDbContext.Activities.Remove(activity);

            await _applicationDbContext.SaveChangesAsync();

            await _lookupsCache.GetActivities(true);

            return Unit.Value;
        }
    }
}
