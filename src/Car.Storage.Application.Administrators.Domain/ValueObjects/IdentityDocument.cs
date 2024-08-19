namespace Car.Storage.Application.Administrators.Domain.ValueObjects
{
    public class IdentityDocument
    {
        public string DocumentNumber { get; }
        public string DocumentType { get;  }
        public DateTime DocumentExpiryDate { get; }
        public string DocumentIssuingAuthority { get; }
        public DateTime DocumentDateOfCreation { get; }

    }
}
