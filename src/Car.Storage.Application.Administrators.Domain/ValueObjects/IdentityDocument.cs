namespace Car.Storage.Application.Administrators.Domain.ValueObjects
{
    public class IdentityDocument
    {
        public string DocumentNumber { get; }
        public string DocumentType { get;  }
        public string DocumentExpiryDate { get; }
        public string DocumentIssuingAuthority { get; }
        public string DocumentDateOfCreation { get; }

    }
}
