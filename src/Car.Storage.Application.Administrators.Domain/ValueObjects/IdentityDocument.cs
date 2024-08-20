using Car.Storage.Application.Administrators.Domain.FluentValidators;
using Car.Storage.Application.SharedKernel.ValueObjects;

namespace Car.Storage.Application.Administrators.Domain.ValueObjects
{
    public class IdentityDocument : ValueObjectsBase
    {

        /// <summary>
        /// Default constructor 
        /// </summary>
        public IdentityDocument()
        {
            Task.Run(() => this.ValidateAsync(this, new IdentityDocumentValidation())).Wait();
        }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public DateTime DocumentExpiryDate { get; set; }
        public string DocumentIssuingAuthority { get; set; }
        public DateTime DocumentDateOfCreation { get; set; }

        public void ValidateIdentityDocument(IdentityDocument identityDocument)
        {
            if (identityDocument == null)
            {
              Task.Run(() => this.ValidateAsync(this, new IdentityDocumentValidation())).Wait();
            }
            else 
            {
                Task.Run(() => this.ValidateAsync(identityDocument, new IdentityDocumentValidation())).Wait();
            }
           
        }
    }
}
