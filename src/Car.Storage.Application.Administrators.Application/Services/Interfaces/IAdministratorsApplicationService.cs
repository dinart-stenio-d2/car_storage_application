using Car.Storage.Application.Administrators.Application.ApiViewModels;

namespace Car.Storage.Application.Administrators.Application.Services.Interfaces
{
    public interface IAdministratorsApplicationService
    {
        public Task<CarViewModel> CreateResourceAsync(CarViewModel carViewModel);

        public Task<CarViewModel> UpdateResourceAsync(CarViewModel carViewModel , Guid Id);

        public Task<CarViewModel> GetResourceAsyncById(Guid Id);

        public Task<List<CarViewModel>> GetAllResourceAsync();
        public Task<List<CarViewModel>> GetAllResourceAsync(int pageNumber, int pageSize);
        public Task<bool> DeleteAsync(Guid Id);
    }
}
