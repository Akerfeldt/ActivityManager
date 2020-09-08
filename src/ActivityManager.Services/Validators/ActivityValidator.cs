using System.Linq;
using ActivityManager.Models;
using ActivityManager.Services.Caches;
using FluentValidation;

namespace ActivityManager.Services.Validators
{
    public class ActivityValidator : AbstractValidator<Activity>
    {
        public ActivityValidator(ILookupsCache lookupsCache)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Description)
                .NotNull()
                .MaximumLength(50)
                .MustAsync(async (x, y, _) =>
                {
                    var activities = await lookupsCache.GetActivities();

                    return activities.Any(z => z.Value.Description.ToUpper() == y.ToUpper() && z.Key != x.Id) == false;
                })
                .WithMessage((x, y) => $"{y} already exists");
        }
    }
}
