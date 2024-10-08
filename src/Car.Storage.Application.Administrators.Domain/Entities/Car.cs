﻿using Car.Storage.Application.Administrators.Domain.FluentValidators;
using Car.Storage.Application.SharedKernel.DomainObjects;
using FluentValidation.Results;

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
            Task.Run(() => this.ValidateAsync(this, new CreationOfCarValidation())).Wait();
        }

        /// <summary>
        /// Create a instance of car 
        /// PS: The car's ID is generated by the constructor of the abstract Entity class if the ID is not passed as a parameter.
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
            this.UpdateCarOwner(carOwner);
            Task.Run(() => this.ValidateAsync(this, new CreationOfCarValidation())).Wait();
        }

        /// <summary>
        ///  Update a instance of car 
        ///  PS: The car's ID is generated by the constructor of the abstract Entity class if the ID is not passed as a parameter.
        ///  PS: In this case, to update an existing Car instance, the ID must be passed as a parameter.
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
        : base(idParam)
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
            this.UpdateCarOwner(carOwner);
            Task.Run(() => this.ValidateAsync(this, new CreationOfCarValidation())).Wait();
        }



        #region  Private Methods

        #endregion Private Methods
        /// <summary>
        /// UpdateCarOwner a instance of Owner
        /// </summary>
        /// <param name="carOwner"></param>
        public void UpdateCarOwner(CarOwner carOwner)
        {
            if (carOwner == null)
            {
                this.Owner = new CarOwner();
            }
            else
            {
                if (string.IsNullOrEmpty(carOwner.Id.ToString()))
                {
                    this.Owner = new CarOwner(carOwner.FirstName, carOwner.LastName, carOwner.Address, carOwner.PhoneNumber, carOwner.Email, carOwner.IdentityDocument);
                }
                else 
                {
                  
                    this.Owner = new CarOwner(carOwner.Id.ToString(), carOwner.FirstName, carOwner.LastName, carOwner.Address, carOwner.PhoneNumber, carOwner.Email, carOwner.IdentityDocument);
                }
                
            }
        }

        /// <summary>
        /// Method that updates the Id field if there is already a valid CargOwner instance
        /// </summary>
        /// <param name="ownerId"></param>
        public void UpdateCarOwnerIdIfExistingCarOwnerId(Guid ownerId)
        {
           this.Owner.Id = ownerId; 
        }

        /// <summary>
        /// Validate is an instance of car has a owner
        /// </summary>
        /// <param name="carOwner"></param>
        public bool HasCarOwner(CarOwner carOwner)
        {
            var result = default(bool);
            if (carOwner != null && !string.IsNullOrEmpty(carOwner.Id.ToString()))
            {
                return true;
            }
            return result;
        }

        /// <summary>
        /// Validates which fields of a Car instance can be updated
        /// </summary>
        /// <param name="newCar"></param>
        /// <param name="oldCar"></param>
        /// <returns></returns>
        public Car ValidateCarToBeUpdated(Car newCar,Car oldCar)
        {
            var validatorOfUpdate = new ValidationOfCarToBeUpdated();
            var resultValidation = validatorOfUpdate.Validate((newCar,oldCar));
            var listOfFaillures = new List<ValidationFailure>();

            if (!resultValidation.IsValid)
            {
                foreach (var faillure in resultValidation.Errors)
                {
                   var  faillError  = new ValidationFailure(faillure.PropertyName, faillure.ErrorMessage);                 
                    listOfFaillures.Add(faillure);
                }
                var validatorInvalid = new AlwaysInvalidValidator<Car>(listOfFaillures);

                Task.Run(() => this.ValidateAsync(newCar, validatorInvalid)).Wait();
            }

            return newCar;

        }
    }
}
