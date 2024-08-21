using FluentValidation;
using FluentValidation.Results;

namespace Car.Storage.Application.Administrators.Domain.FluentValidators
{
    public class AlwaysInvalidValidator<T> : AbstractValidator<T>
    {
        private readonly List<ValidationFailure> _preexistingFailures = new List<ValidationFailure>();
        public AlwaysInvalidValidator()
        {
            RuleFor(x => x).Custom((x, context) =>
            {
                context.AddFailure(new ValidationFailure("", "This validation always fails."));
            });
        }


        public AlwaysInvalidValidator(List<ValidationFailure> preexistingFailures)
        {
            _preexistingFailures = preexistingFailures;

            RuleFor(x => x).Custom((x, context) =>
            {
                foreach (var failure in _preexistingFailures)
                {
                    context.AddFailure(failure);
                }
            });
        }
    }
}
