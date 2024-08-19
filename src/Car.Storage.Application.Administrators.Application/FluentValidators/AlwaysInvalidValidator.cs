using FluentValidation;
using FluentValidation.Results;

namespace Car.Storage.Application.Administrators.Application.FluentValidators
{
    public class AlwaysInvalidValidator<T> : AbstractValidator<T>
    {
        public AlwaysInvalidValidator()
        {
            RuleFor(x => x).Custom((x, context) =>
            {
                context.AddFailure(new ValidationFailure("", "This validation always fails."));
            });
        }
    }
}
