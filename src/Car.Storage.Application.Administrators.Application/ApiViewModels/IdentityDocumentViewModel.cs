namespace Car.Storage.Application.Administrators.Application.ApiViewModels
{
    public class IdentityDocumentViewModel
    {
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public DateTime DocumentExpiryDate { get; set; }
        public string DocumentIssuingAuthority { get; set; }
        public DateTime DocumentDateOfCreation { get; set; }
    }
}
