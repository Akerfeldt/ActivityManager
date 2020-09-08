using System.Linq;
using ActivityManager.Models;
using ActivityManager.Services.Caches;
using FluentValidation;

namespace ActivityManager.Services.Validators
{
    public class NewActivityInfoValidator : AbstractValidator<NewActivityInfo>
    {
        public NewActivityInfoValidator(ILookupsCache lookupsCache)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Description)
                .NotNull()
                .MaximumLength(50)
                .MustAsync(async (y, _) =>
                {
                    var activities = await lookupsCache.GetActivities();

                    return activities.Values.Any(z => z.Description.ToUpper() == y.ToUpper()) == false;
                })
                .WithMessage(y => $"{y} already exists");
        }
    }
}
