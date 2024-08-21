using AutoMapper;
using Car.Storage.Application.Administrators.Application.ApiViewModels;
using Car.Storage.Application.Administrators.Application.Services.Interfaces;
using Car.Storage.Application.Administrators.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;

namespace Car.Storage.Application.Administrators.Application.Services
{
    public class AdministratorsApplicationService : IAdministratorsApplicationService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Data.Repositories.Entities.Car> carRepository;
        private readonly IRepository<Data.Repositories.Entities.CarOwner> carOwnerRepository;

        private readonly ILogger<AdministratorsApplicationService> logger;

        public AdministratorsApplicationService(IMapper mapper, 
                                                IRepository<Data.Repositories.Entities.Car> carRepository,
                                                IRepository<Data.Repositories.Entities.CarOwner> carOwnerRepository,
                                                ILogger<AdministratorsApplicationService> logger)
        {
            this.mapper=mapper;
            this.carRepository=carRepository;
            this.carOwnerRepository = carOwnerRepository;
            this.logger=logger;
        }

        public async Task<CarViewModel> CreateResourceAsync(CarViewModel carViewModel)
        {
            this.logger.LogInformation($"AdministratorsApplicationService : Try to create car resource Started at -- {DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}");
            var resultViewModel = new CarViewModel();

            try
            {
                var newCarTobeValidate = this.mapper.Map<Domain.Entities.Car>(carViewModel);

                var newCarTobeValidateOwner = this.mapper.Map<Domain.Entities.CarOwner>(carViewModel.Owner);

                newCarTobeValidate.UpdateCarOwner(newCarTobeValidateOwner);
                newCarTobeValidate.Owner.ValidateCarOwner(null);

                var newCarTobeValidateOwnerDocumentIdentity = this.mapper.Map<Domain.ValueObjects.IdentityDocument>(carViewModel.Owner.IdentityDocument);

                newCarTobeValidate.Owner.CreateIdentityDocument(newCarTobeValidateOwnerDocumentIdentity);
                newCarTobeValidate.Owner.IdentityDocument.ValidateIdentityDocument(null);



                if (!newCarTobeValidate.Owner.CarOwnerIsValid(newCarTobeValidate.Owner))
                {
                    resultViewModel.GenrateInvalidateViewModelResult(newCarTobeValidate.Owner.ValidationResult, string.Empty);
                    this.logger.LogError($"AdministratorsApplicationService : Error in an attempt to create car resource  Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")} ERROR");
                    return resultViewModel;
                }

               

                if (!newCarTobeValidate.Owner.IdentityDocumentIsValid(newCarTobeValidate.Owner.IdentityDocument))
                {
                    resultViewModel.GenrateInvalidateViewModelResult(newCarTobeValidate.Owner.IdentityDocument.ValidationResult, string.Empty);
                    this.logger.LogError($"AdministratorsApplicationService : Error in an attempt to create car resource  Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")} ERROR");
                    return resultViewModel;
                }

                var newCarTobeCreated = new Domain.Entities.Car(newCarTobeValidate.Brand,
                                                                newCarTobeValidate.Model,
                                                                newCarTobeValidate.Year,
                                                                newCarTobeValidate.Color,
                                                                newCarTobeValidate.IsRunning,
                                                                newCarTobeValidate.IsForSale,
                                                                newCarTobeValidate.IsNew,
                                                                newCarTobeValidate.Price,
                                                                newCarTobeValidate.VehicleIdentificationNumber,
                                                                newCarTobeValidate.CarPlate,
                                                                newCarTobeValidate.Owner);

                if (!newCarTobeCreated.ValidationResult.IsValid)
                {
                    resultViewModel.GenrateInvalidateViewModelResult(newCarTobeCreated.ValidationResult, string.Empty);
                    this.logger.LogError($"AdministratorsApplicationService : Error in an attempt to create car resource  Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")} ERROR");
                    return resultViewModel;
                }
                else
                {
                    var newEntity = this.mapper.Map<Data.Repositories.Entities.Car>(newCarTobeCreated);

                    var dbResult = await this.carRepository
                                             .CreateAsync(newEntity)
                                             .ConfigureAwait(false);
                    if (dbResult != 2)
                    {
                        resultViewModel.GenrateInvalidateViewModelResult(null, "Error in an attempt to create car resource on the database");
                        this.logger.LogError($"AdministratorsApplicationService : Error in an attempt to create car resource  Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")} ERROR");
                    }

                    resultViewModel = (CarViewModel)resultViewModel.GenerateValidViewModel(resultViewModel);

                    this.logger.LogInformation($"AdministratorsApplicationService : Car resource successfully created Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")}");
                    
                    return resultViewModel;
                }
            }
            catch (Exception ex )
            {
                throw;
            }
            
        }

        public async Task<CarViewModel> GetResourceAsyncById(Guid Id)
        {
            this.logger.LogInformation($"AdministratorsApplicationService : Try to get car resource Started at -- {DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}");
            var resultViewModel = new CarViewModel();
            try
            {
                var dbResult = await this.carRepository
                                             .FindFirstOrDefaultByIdWithEntitiesRelatedAsync(
                                              c => c.Id == Id,
                                              c => c.Owner
                                              ).ConfigureAwait(false);

                if (dbResult == null)
                {
                    resultViewModel.GenrateInvalidateViewModelResult(null, $"There is no data in the database for the Id:{Id}");
                    this.logger.LogInformation($"AdministratorsApplicationService : There is no data in the database for the Id:{Id}  Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")} ERROR");
                }
                else
                {
                    var carFound = this.mapper.Map<Domain.Entities.Car>(dbResult);
                    var viewModelOftheCarFound = this.mapper.Map<Application.ApiViewModels.CarViewModel>(carFound);

                    resultViewModel = (CarViewModel)resultViewModel.GenerateValidViewModel(viewModelOftheCarFound);
                    this.logger.LogInformation($"AdministratorsApplicationService : Data for Id:{Id} was found successfully  Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")} ERROR");
                    
                    resultViewModel = viewModelOftheCarFound;
                }

                return resultViewModel;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<CarViewModel> UpdateResourceAsync(CarViewModel carViewModel ,Guid Id)
        {
            this.logger.LogInformation($"AdministratorsApplicationService : Try to update car resource Started at -- {DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}");
            var resultViewModel = new CarViewModel();
            //Mapping viewModel entity to a domain entity 
            var newCar = this.mapper.Map<Domain.Entities.Car>(carViewModel);
            



            try
            {
                var newCarToBeUpdated = new Domain.Entities.Car(Id.ToString(),
                                                                newCar.Brand,
                                                                newCar.Model,
                                                                newCar.Year,
                                                                newCar.Color,
                                                                newCar.IsRunning,
                                                                newCar.IsForSale,
                                                                newCar.IsNew,
                                                                newCar.Price,
                                                                newCar.VehicleIdentificationNumber,
                                                                newCar.CarPlate,
                                                                newCar.Owner);

             
                var existingDbEntityCarToBeValidated = await this.carRepository
                                                        .FindFirstOrDefaultByIdWithEntitiesRelatedAsync(
                                                         c => c.Id == Id,
                                                         c => c.Owner
                                                          ).ConfigureAwait(false);
                //Mapping database entity to a domain entity 
                var existingCar = this.mapper.Map<Domain.Entities.Car>(existingDbEntityCarToBeValidated);

                if (!existingCar.HasCarOwner(existingCar.Owner))
                {
                    resultViewModel.GenrateInvalidateViewModelResult(null, "This car does not have a valid owner");
                    this.logger.LogError($"AdministratorsApplicationService : Error in an attempt to update car resource  Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")} ERROR");
                    return resultViewModel;
                }

                newCarToBeUpdated.UpdateCarOwnerIdIfExistingCarOwnerId(existingCar.Owner.Id);    

                newCarToBeUpdated.UpdateCarOwner(newCarToBeUpdated.Owner);

                var newValidCar = newCarToBeUpdated.ValidateCarToBeUpdated(newCarToBeUpdated,existingCar);

                if (!newValidCar.ValidationResult.IsValid)
                {
                    resultViewModel.GenrateInvalidateViewModelResult(newValidCar.ValidationResult, string.Empty);
                    this.logger.LogError($"AdministratorsApplicationService : Error in an attempt to update car resource  Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")} ERROR");
                    return resultViewModel;
                }

                //Mapping a domain entity to a database entity
                var newCarEntityDbTobeUpdated = this.mapper.Map<Data.Repositories.Entities.Car>(newValidCar);

                var dbResult = await this.carRepository
                              .UpdateAsync(c => c.Id == Id, newCarEntityDbTobeUpdated)
                              .ConfigureAwait(false);

                if (dbResult != 2)
                {
                    resultViewModel.GenrateInvalidateViewModelResult(null, "Error in an attempt to update car resource on the database");
                    this.logger.LogError($"AdministratorsApplicationService : Error in an attempt to update car resource  Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")} ERROR");
                    return resultViewModel;
                }

                resultViewModel = (CarViewModel)resultViewModel.GenerateValidViewModel(resultViewModel);

                this.logger.LogInformation($"AdministratorsApplicationService : Car resource successfully updated Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")}");

                return resultViewModel;
            }
            catch (Exception ex )
            {
                throw;
            }
        
        }
    }
}
