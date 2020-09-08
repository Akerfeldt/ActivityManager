using ActivityManager.Services.Caches;
using ActivityManager.Services.Validators;
using FluentValidation;

namespace ActivityManager.Services
{
    public class CreateUserActivityValidator : AbstractValidator<CreateUserActivity>
    {
        public CreateUserActivityValidator(ILookupsCache lookupsCache)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.NewUserActivityInfo)
                .SetValidator(new NewUserActivityInfoValidator(lookupsCache));
        }
    }
}
