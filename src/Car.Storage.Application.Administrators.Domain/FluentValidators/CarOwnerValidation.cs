using FluentValidation;
using FluentValidation.Results;

namespace Car.Storage.Application.Administrators.Domain.FluentValidators
{
    public class CarOwnerValidation : AbstractValidator<Entities.CarOwner>
    {
        public CarOwnerValidation()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("The Id is a required value.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("The FirstName is a required value.").Length(1, 50).WithMessage("The FirstName field must be between 1 and 50 characters.");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("The LastName is a required value.").Length(1, 50).WithMessage("The LastName field must be between 1 and 50 characters.");

            RuleFor(c => c.Address).NotEmpty().WithMessage("The Address is a required value.").Length(1, 100).WithMessage("The Address field must be between 1 and 100 characters.");
            RuleFor(c => c.PhoneNumber).NotEmpty().WithMessage("The PhoneNumber is a required value.").Length(1, 20).WithMessage("The PhoneNumber field must be between 1 and 20 characters.");

             RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not a valid email address.");

            RuleFor(x => x.IdentityDocument)
           .SetValidator(new IdentityDocumentValidation());
        }

        protected override bool PreValidate(ValidationContext<Entities.CarOwner> context, ValidationResult result)
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
