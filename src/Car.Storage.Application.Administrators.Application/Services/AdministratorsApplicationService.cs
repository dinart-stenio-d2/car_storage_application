using AutoMapper;
using Car.Storage.Application.Administrators.Application.ApiViewModels;
using Car.Storage.Application.Administrators.Application.Services.Interfaces;
using Car.Storage.Application.Administrators.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Car.Storage.Application.Administrators.Application.Services
{
    public class AdministratorsApplicationService : IAdministratorsApplicationService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Data.Repositories.Entities.Car> carRepository;
        private readonly ILogger<AdministratorsApplicationService> logger;

        public AdministratorsApplicationService(IMapper mapper, 
                                                IRepository<Data.Repositories.Entities.Car> carRepository,
                                                ILogger<AdministratorsApplicationService> logger)
        {
            this.mapper=mapper;
            this.carRepository=carRepository;
            this.logger=logger;
        }

        public async Task<CarViewModel> CreateResourceAsync(CarViewModel carViewModel)
        {
            this.logger.LogInformation($"AdministratorsApplicationService : Try to create car resource Started at -- {DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}");
            var resultViewModel = new CarViewModel();
            
            var newCar = this.mapper.Map<Domain.Entities.Car>(carViewModel);

            if (!newCar.ValidationResult.IsValid)
            {
                resultViewModel.GenrateInvalidateViewModelResult(newCar.ValidationResult, string.Empty);
                this.logger.LogError($"AdministratorsApplicationService : Error in an attempt to create car resource  Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")} ERROR");
                return resultViewModel;
            }
            else
            {
                var newEntity = this.mapper.Map<Data.Repositories.Entities.Car>(newCar);

                var dbResult  = await this.carRepository
                                        .CreateAsync(newEntity)
                                        .ConfigureAwait(false);
                if (dbResult != 1)
                {
                    resultViewModel.GenrateInvalidateViewModelResult(null, "Error in an attempt to create car resource on the database");
                    this.logger.LogError($"AdministratorsApplicationService : Error in an attempt to create car resource  Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")} ERROR");
                }

                resultViewModel.GenerateValidViewModel();

                this.logger.LogInformation($"ReasonsApplicationService : Car resource successfully created Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")}");

                return resultViewModel;
            }
        }

        public async Task<CarViewModel> UpdateResourceAsync(CarViewModel carViewModel)
        {

            this.logger.LogInformation($"AdministratorsApplicationService : Try to update car resource Started at -- {DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}");
            var resultViewModel = new CarViewModel();

            var newCar = this.mapper.Map<Domain.Entities.Car>(carViewModel);

            if (!newCar.ValidationResult.IsValid)
            {
                resultViewModel.GenrateInvalidateViewModelResult(newCar.ValidationResult, string.Empty);
                this.logger.LogError($"AdministratorsApplicationService : Error in an attempt to update car resource  Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")} ERROR");
                return resultViewModel;
            }
            else
            {
                var newEntity = this.mapper.Map<Data.Repositories.Entities.Car>(newCar);

                var dbResult = await this.carRepository
                          .UpdateAsync(c => c.Id == newEntity.Id, newEntity)
                          .ConfigureAwait(false);

                if (dbResult != 1)
                {
                    resultViewModel.GenrateInvalidateViewModelResult(null, "Error in an attempt to update car resource on the database");
                    this.logger.LogError($"AdministratorsApplicationService : Error in an attempt to update car resource  Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")} ERROR");
                }

                resultViewModel.GenerateValidViewModel();

                this.logger.LogInformation($"AdministratorsApplicationService : Car resource successfully updated Ended at -- {DateTime.UtcNow.ToString("MM / dd / yyyy hh: mm:ss.fff tt")}");

                return resultViewModel;
            }
        
        }
    }
}
