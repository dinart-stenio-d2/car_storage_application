using Car.Storage.Application.Administrators.Application.ApiViewModels;

namespace Car.Storage.Application.Administrators.Application.Services.Interfaces
{
    public interface IAdministratorsApplicationService
    {
        public Task<CarViewModel> CreateResourceAsync(CarViewModel carViewModel);

        public Task<CarViewModel> UpdateResourceAsync(CarViewModel carViewModel);

    }
}
