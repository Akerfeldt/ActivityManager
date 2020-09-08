using FluentValidation;

namespace ActivityManager.Services
{
    public class GetUserActivitiesValidator : AbstractValidator<GetUserActivities>
    {
        public GetUserActivitiesValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.User)
                .NotNull();
        }
    }
}
