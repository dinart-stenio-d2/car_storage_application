using Car.Storage.Application.Administrators.Domain.FluentValidators;
using Car.Storage.Application.Administrators.Domain.ValueObjects;
using Car.Storage.Application.SharedKernel.DomainObjects;

namespace Car.Storage.Application.Administrators.Domain.Entities
{
    public class CarOwner : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public IdentityDocument? IdentityDocument { get; set; }

        public CarOwner()
        {
                
        }
        public CarOwner(string firstName, string lastName, string address, string phoneNumber, string email , IdentityDocument? identityDocument)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            this.CreateIdentityDocument(identityDocument);
        }

        #region  Private Methods

        #endregion Private Methods

        #region  Public Methods
        public void CreateIdentityDocument(IdentityDocument identityDocument)
        {
            if (identityDocument == null)
            {
                this.IdentityDocument = new IdentityDocument();
            }
            else
            {
                this.IdentityDocument = new IdentityDocument();
                this.IdentityDocument = identityDocument;
            }
        }

        public void ValidateCarOwner(CarOwner carOwner)
        {
            if (carOwner == null)
            {
                Task.Run(() => this.ValidateAsync(this, new CarOwnerValidation())).Wait();
            }
            else 
            {
                Task.Run(() => this.ValidateAsync(carOwner, new CarOwnerValidation())).Wait();
            }
            
        }

        public bool CarOwnerIsValid(CarOwner carOwner)
        {
          bool isValid = false; 

            if (carOwner.ValidationResult.IsValid) 
            {
                isValid = true;
            }

            return isValid; 
        }
        public bool IdentityDocumentIsValid(IdentityDocument identityDocument)
        {
            bool isValid = false;

            if (identityDocument.ValidationResult.IsValid)
            {
                isValid = true;
            }
            return isValid;
        }


        #endregion Public Methods

    }
}
