using Car.Storage.Application.Administrators.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;

namespace Car.Storage.Application.Administrators.Domain.FluentValidators
{
    public class IdentityDocumentValidation : AbstractValidator<IdentityDocument>
    {
        public IdentityDocumentValidation()
        {
            RuleFor(x => x.DocumentNumber)
           .NotEmpty().WithMessage("DocumentNumber is required.")
           .Length(1, 20).WithMessage("DocumentNumber must be between 1 and 20 characters.");

            RuleFor(x => x.DocumentType)
            .NotEmpty().WithMessage("DocumentType is required.")
            .Length(1, 20).WithMessage("DocumentType must be between 1 and 20 characters.");

            RuleFor(x => x.DocumentExpiryDate)
            .NotNull().WithMessage("DocumentExpiryDate is required.")
            .GreaterThan(DateTime.Now).WithMessage("DocumentExpiryDate must be in the future.");

          
            RuleFor(x => x.DocumentIssuingAuthority)
           .NotEmpty().WithMessage("DocumentIssuingAuthority is required.")
           .Length(1, 20).WithMessage("DocumentIssuingAuthority must be between 1 and 20 characters.");


            RuleFor(x => x.DocumentDateOfCreation)
            .NotNull().WithMessage("DocumentDateOfCreation is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("DocumentDateOfCreation must be in the past or present.");

        }

        protected override bool PreValidate(ValidationContext<IdentityDocument> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("", "No value was found or the queried car instance is empty"));
                return false;
            }
            return true;
        }
    }
}
