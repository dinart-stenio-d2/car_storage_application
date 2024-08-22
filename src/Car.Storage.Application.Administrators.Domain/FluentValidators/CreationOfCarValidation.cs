using FluentValidation;
using FluentValidation.Results;

namespace Car.Storage.Application.Administrators.Domain.FluentValidators
{
    public class CreationOfCarValidation : AbstractValidator<Entities.Car>
    {
        public CreationOfCarValidation()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("The Id is a required value.");
            RuleFor(x => x.Brand).NotEmpty().WithMessage("The Brand is a required value.").Length(1, 50).WithMessage("The Brand field must be between 1 and 50 characters.");
            RuleFor(c => c.Model).NotEmpty().WithMessage("The Model is a required value.").Length(1, 50).WithMessage("The Model field must be between 1 and 50 characters.");

            RuleFor(x => x.Year)
           .NotNull().WithMessage("The field Year is required.")
           .GreaterThan(0).WithMessage("Year must be greater than zero.")
           .InclusiveBetween(1900, DateTime.Now.Year).WithMessage($"Year must be between 1900 and {DateTime.Now.Year}.");
            
            RuleFor(c => c.Color).NotEmpty().WithMessage("The Color is a required value.").Length(1, 20).WithMessage("The Color field must be between 1 and 20 characters.");

            RuleFor(x => x.IsRunning)
            .Must(value => value == true || value == false)
            .WithMessage("The field IsRunning must be explicitly set to true or false.");

            RuleFor(x => x.IsNew)
           .Must(value => value == true || value == false)
           .WithMessage("The field IsNew must be explicitly set to true or false.");

            RuleFor(x => x.IsForSale)
           .Must(value => value == true || value == false)
           .WithMessage("The Field IsForSale must be explicitly set to true or false.");

            RuleFor(x => x.Price)
           .NotNull().WithMessage("The Field Price is required.")
           .GreaterThan(0).WithMessage("The Field Price must be greater than zero.");

            RuleFor(c => c.VehicleIdentificationNumber).NotEmpty().WithMessage("The Field VehicleIdentificationNumber is a required value.").Length(1, 17).WithMessage("The Feild VehicleIdentificationNumber must be between 1 and 17 characters.");
            
            RuleFor(c => c.CarPlate).NotEmpty().WithMessage("The CarPlate is a required value.").Length(1,7).WithMessage("The Feild CarPlate must be between 1 and 7 characters.");
        }

        protected override bool PreValidate(ValidationContext<Entities.Car> context, ValidationResult result)
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
