using Car.Storage.Application.Administrators.Domain.FluentValidators;
using Car.Storage.Application.SharedKernel.DomainObjects;

namespace Car.Storage.Application.Administrators.Domain.Entities
{
    public class Car : Entity, IAggregateRoot
    {
 
        public string Brand{ get; set; }
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


        public Car(string brand, string model, int year, string color, bool isRunning, bool isForSale, bool isNew, decimal price,
        string vehicleIdentificationNumber,string carPlate ,CarOwner carOwner)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            IsRunning = isRunning;
            IsForSale = isForSale;
            IsNew  = isNew;
            Price = price;
            VehicleIdentificationNumber = vehicleIdentificationNumber;  
            CarPlate = carPlate;
            this.CreateCarOwner(carOwner);
            Task.Run(() => this.ValidateAsync(this, new CarValidation())).Wait();
        }

        #region  Private Methods
        private void CreateCarOwner(CarOwner carOwner)
        {
            if (carOwner == null)
            {
                this.Owner = new CarOwner();
            }
            else 
            {
                this.Owner = new CarOwner(carOwner.FirstName, carOwner.LastName,carOwner.Address,carOwner.PhoneNumber,carOwner.Email, carOwner.IdentityDocument);   
            }        
        }
        #endregion Private Methods


        public void DisplayCarInfo()
        {

        }
    }
}
