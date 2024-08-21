using FluentValidation;

namespace Car.Storage.Application.Administrators.Domain.FluentValidators
{
    public class ValidationOfCarToBeUpdated : AbstractValidator<(Entities.Car newCar, Entities.Car oldCar)>
    {
        public ValidationOfCarToBeUpdated()
        {
            RuleFor(x => x.newCar.Id)
           .Equal(x => x.oldCar.Id)
           .WithMessage("It is not possible to change the id of the owner of the existing car");

            RuleFor(x => x.newCar.Id)
           .Equal(x => x.oldCar.Id)
           .WithMessage("The car ID to be updated is not identical to the existing car ID");

            RuleFor(x => x.newCar.Brand)
           .Equal(x => x.oldCar.Brand)
           .WithMessage("It is not possible to change the brand of the existing car");

            RuleFor(x => x.newCar.Model)
           .Equal(x => x.oldCar.Model)
           .WithMessage("It is not possible to change the model of the existing car");

            RuleFor(x => x.newCar.Year)
           .Equal(x => x.oldCar.Year)
           .WithMessage("It is not possible to change the year of the existing car");

            RuleFor(x => x.newCar.VehicleIdentificationNumber)
           .Equal(x => x.oldCar.VehicleIdentificationNumber)
           .WithMessage("It is not possible to change the vehicleIdentificationNumber of the existing car");

            RuleFor(x => x.newCar.Owner.Id)
               .Equal(x => x.oldCar.Owner.Id)
               .WithMessage("It is not possible to change the id of the owner of the existing car")
               .When(x => !string.IsNullOrEmpty(x.oldCar.Owner?.Id.ToString()) && x.oldCar.Owner.Id != Guid.Empty);

            RuleFor(x => x.newCar.Owner.FirstName)
           .Equal(x => x.oldCar.Owner.FirstName)
           .WithMessage("It is not possible to change the firstName of the owner of the existing car")
           .When(x => !string.IsNullOrEmpty(x.oldCar.Owner?.Id.ToString()) && x.oldCar.Owner.Id != Guid.Empty);

            RuleFor(x => x.newCar.Owner.LastName)
            .Equal(x => x.oldCar.Owner.LastName)
            .WithMessage("It is not possible to change the lastName of the owner of the existing car")
            .When(x => !string.IsNullOrEmpty(x.oldCar.Owner?.Id.ToString()) && x.oldCar.Owner.Id != Guid.Empty);

            RuleFor(x => x.newCar.Owner.IdentityDocument.DocumentNumber)
           .Equal(x => x.oldCar.Owner.IdentityDocument.DocumentNumber)
           .WithMessage("It is not possible to change the documentNumber of the owner of the existing car")
           .When(x => x.oldCar.Owner != null && x.oldCar.Owner.Id != Guid.Empty);

            RuleFor(x => x.newCar.Owner.IdentityDocument.DocumentIssuingAuthority)
            .Equal(x => x.oldCar.Owner.IdentityDocument.DocumentIssuingAuthority)
            .WithMessage("It is not possible to change the documentIssuingAuthority of the owner of the existing car")
            .When(x => x.oldCar.Owner != null && x.oldCar.Owner.Id != Guid.Empty);

            RuleFor(x => x.newCar.Owner.IdentityDocument.DocumentDateOfCreation)
           .Equal(x => x.oldCar.Owner.IdentityDocument.DocumentDateOfCreation)
           .WithMessage("It is not possible to change the documentDateOfCreation of the owner of the existing car")
           .When(x => x.oldCar.Owner != null && x.oldCar.Owner.Id != Guid.Empty);

            RuleFor(x => x.newCar.Owner.IdentityDocument.DocumentType)
           .Equal(x => x.oldCar.Owner.IdentityDocument.DocumentType)
           .WithMessage("It is not possible to change the documentType of the owner of the existing car")
           .When(x => x.oldCar.Owner != null && x.oldCar.Owner.Id != Guid.Empty);

            RuleFor(x => x.newCar.Owner.IdentityDocument.DocumentExpiryDate)
           .Equal(x => x.oldCar.Owner.IdentityDocument.DocumentExpiryDate)
           .WithMessage("It is not possible to change the documentType of the owner of the existing car")
           .When(x => x.oldCar.Owner != null && x.oldCar.Owner.Id != Guid.Empty);
        }

    }
}
