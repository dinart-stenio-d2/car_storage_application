using FluentValidation;
using FluentValidation.Results;

namespace Car.Storage.Application.Administrators.Application.FluentValidators
{
    public class AlwaysValidValidator<T> : AbstractValidator<T>
    {
        public AlwaysValidValidator()
        {
            // No validation rules are added, so validation always succeeds
        }

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            return new ValidationResult(); // By default, ValidationResult.IsValid is true
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<T> context, CancellationToken cancellation = default)
        {
            return Task.FromResult(new ValidationResult()); // By default, ValidationResult.IsValid is true
        }
    }
}
