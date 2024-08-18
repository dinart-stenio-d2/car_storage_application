﻿using Car.Storage.Application.Administrators.Domain.ValueObjects;
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
        private void CreateIdentityDocument(IdentityDocument identityDocument)
        {
            if (identityDocument == null)
            {
                this.IdentityDocument = new  IdentityDocument();
            }
            else
            {
                this.IdentityDocument = new IdentityDocument();
                this.IdentityDocument = identityDocument;
            }
        }
        #endregion Private Methods
        public void DisplayOwnerInfo()
        {

        }
    }
}
