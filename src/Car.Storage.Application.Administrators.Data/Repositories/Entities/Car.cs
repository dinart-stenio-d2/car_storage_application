namespace Car.Storage.Application.Administrators.Data.Repositories.Entities
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public bool IsRunning { get; set; }
        public bool IsNew { get; set; }
        public bool IsForSale { get; set; }
        public decimal Price { get; set; }
        public string VehicleIdentificationNumber { get; set; }
        public string? CarPlate { get; set; }
        public CarOwner? Owner { get; set; }
    }
}
