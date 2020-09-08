using FluentValidation;

namespace ActivityManager.Services
{
    public class GetUserActivityValidator : AbstractValidator<GetUserActivity>
    {
        public GetUserActivityValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.User)
                .NotNull();
        }
    }
}
