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

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Car()
        {
            Task.Run(() => this.ValidateAsync(this, new CarValidation())).Wait();
        }

        /// <summary>
        /// Create a instance of car 
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="year"></param>
        /// <param name="color"></param>
        /// <param name="isRunning"></param>
        /// <param name="isForSale"></param>
        /// <param name="isNew"></param>
        /// <param name="price"></param>
        /// <param name="vehicleIdentificationNumber"></param>
        /// <param name="carPlate"></param>
        /// <param name="carOwner"></param>
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

        /// <summary>
        ///  Update  a instance of car 
        /// </summary>
        /// <param name="idParam"></param>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="year"></param>
        /// <param name="color"></param>
        /// <param name="isRunning"></param>
        /// <param name="isForSale"></param>
        /// <param name="isNew"></param>
        /// <param name="price"></param>
        /// <param name="vehicleIdentificationNumber"></param>
        /// <param name="carPlate"></param>
        /// <param name="carOwner"></param>
        public Car(string idParam ,string brand, string model, int year, string color, bool isRunning, bool isForSale, bool isNew, decimal price,
        string vehicleIdentificationNumber, string carPlate, CarOwner carOwner)
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

        #endregion Private Methods

        public void CreateCarOwner(CarOwner carOwner)
        {
            if (carOwner == null)
            {
                this.Owner = new CarOwner();
            }
            else
            {
                this.Owner = new CarOwner(carOwner.FirstName, carOwner.LastName, carOwner.Address, carOwner.PhoneNumber, carOwner.Email,carOwner.IdentityDocument);
            }
        }
        public void DisplayCarInfo()
        {

        }
    }
}
