using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ActivityManager.Data;
using ActivityManager.Data.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ActivityManager.Services
{
    public class CreateUserActivityHandler : IRequestHandler<CreateUserActivity>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<CreateUserActivityHandler> _logger;

        public CreateUserActivityHandler(ApplicationDbContext applicationDbContext, ILogger<CreateUserActivityHandler> logger)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(CreateUserActivity request, CancellationToken cancellationToken)
        {
            var userActivity = new UserActivity
            {
                UserId = request.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value,
                ActivityId = request.NewUserActivityInfo.ActivityId
            };

            _applicationDbContext.UserActivities.Add(userActivity);

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                return Unit.Value;
            }
            catch (Exception e)
            {
                _logger.LogError(System.Diagnostics.Activity.Current?.Id, e, "Failed to save UserActivity");

                throw;
            }
        }
    }
}
