using ActivityManager.Models;
using ActivityManager.Services.Caches;
using FluentValidation;

namespace ActivityManager.Services.Validators
{
    public class NewUserActivityInfoValidator : AbstractValidator<NewUserActivityInfo>
    {
        public NewUserActivityInfoValidator(ILookupsCache lookupsCache)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.ActivityId)
                .MustAsync(async (y, _) =>
                {
                    var activities = await lookupsCache.GetActivities();

                    return activities.ContainsKey(y);
                })
                .WithMessage((x, y) => $"Activity {y} does not exist");
        }
    }
}
