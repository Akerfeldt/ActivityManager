using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ActivityManager.Data;
using ActivityManager.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ActivityManager.Services
{
    public class DeleteUserActivityHandler : IRequestHandler<DeleteUserActivity>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeleteUserActivityHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<Unit> Handle(DeleteUserActivity request, CancellationToken cancellationToken)
        {
            var userId = request.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var userActivity = await _applicationDbContext.UserActivities.FirstOrDefaultAsync(m => m.UserId == userId && m.ActivityId == request.ActivityId) ?? throw new NotFoundException();

            _applicationDbContext.UserActivities.Remove(userActivity);

            await _applicationDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
