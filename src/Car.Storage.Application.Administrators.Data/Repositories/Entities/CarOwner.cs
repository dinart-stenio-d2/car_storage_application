namespace Car.Storage.Application.Administrators.Data.Repositories.Entities
{
    public class CarOwner
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public DateTime DocumentExpiryDate { get; set; }
        public string DocumentIssuingAuthority { get; set; }
        public DateTime DocumentDateOfCreation { get; set; }
    }
}
