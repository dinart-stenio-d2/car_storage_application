namespace Car.Storage.Application.Administrators.Application.ApiViewModels
{
    public class IdentityDocumentViewModel
    {
        public string DocumentNumber { get; }
        public string DocumentType { get; }
        public DateTime DocumentExpiryDate { get; }
        public string DocumentIssuingAuthority { get; }
        public DateTime DocumentDateOfCreation { get; }
    }
}
