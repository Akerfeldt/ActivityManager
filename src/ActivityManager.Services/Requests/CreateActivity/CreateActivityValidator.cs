using ActivityManager.Services.Caches;
using ActivityManager.Services.Validators;
using FluentValidation;

namespace ActivityManager.Services
{
    public class CreateActivityValidator : AbstractValidator<CreateActivity>
    {
        public CreateActivityValidator(ILookupsCache lookupsCache)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.NewActivityInfo)
                .SetValidator(new NewActivityInfoValidator(lookupsCache));
        }
    }
}
