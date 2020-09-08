using ActivityManager.Services.Caches;
using ActivityManager.Services.Validators;
using FluentValidation;

namespace ActivityManager.Services
{
    public class UpdateActivityValidator : AbstractValidator<UpdateActivity>
    {
        public UpdateActivityValidator(ILookupsCache lookupsCache)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
                .Equal(x => x.Activity.Id)
                .When(x => x.Activity != null);

            RuleFor(x => x.Activity)
                .SetValidator(new ActivityValidator(lookupsCache));
        }
    }
}
